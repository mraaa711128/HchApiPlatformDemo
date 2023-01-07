using HchApiPlatform.DbContexts;
using HchApiPlatform.Models;
using Microsoft.EntityFrameworkCore;
using Utility.Extensions;

namespace HchApiPlatform.Biz
{
    public class AdmitPatientBiz
    {
        public const string AdmitStatus_Apply = "0";
        public const string AdmitStatus_Admitted = "1";
        public const string AdmitStatus_Closed = "2";
        public const string AdmitStatus_ReOpen = "3";
        public const string AdmitStatus_Checkout = "4";
        public const string AdmitStatus_CheckoutCanceled = "5";
        public const string AdmitStatus_NonClosedDismiss = "6";
        public const string AdmitStatus_AdmitCanceled = "7";
        public const string AdmitStatus_NoAdmit = "N";

        private IDbContextFactory<UnimaxHoContext> _HoFactory;
        private IDbContextFactory<UnimaxHiContext> _HiFactory;
        
        public AdmitPatientBiz(IDbContextFactory<UnimaxHoContext> hoFactory, IDbContextFactory<UnimaxHiContext> hiFactory) {
            _HoFactory = hoFactory;
            _HiFactory = hiFactory;
        }

        public async Task<IEnumerable<AdmitPatient>> GetAdmitPatientsAsync()
        {
            IEnumerable<AdmitPatient> PtIpds;
            using (var _hiContext1 = _HiFactory.CreateDbContext())
            //using (var _hiContext2 = _HiFactory.CreateDbContext())
            {
                //var taskPtIpds = _hiContext1.Ptipds
                //                .AsNoTracking()
                //                .Where(r => r.Status == "1")
                //                .Select(r => new AdmitPatient
                //                {
                //                    AdmitNo = r.AdmitNo,
                //                    ChartNo = r.ChartNo,
                //                    CheckinDatetime = new string[] { r.AdmitDate.ToString().PadLeft(7, '0'), r.AdmitTime.ToString().PadLeft(6, '0') }.ToAcDateTime(),
                //                    DoctorNo = r.VsNo,
                //                    BedNo = r.BedNo,
                //                    DivNo = r.DivNo,
                //                    PrivacyFlag = r.PrivacyFlag,
                //                    ExclusiveRoomFlag = r.ExclusiveWardFlag,
                //                    ExclusiveRoomFlagDesc = GetExclusiveWradFlagDesc(r.ExclusiveWardFlag.Trim()),
                //                    IsolateType = r.IsolateType,
                //                }).ToListAsync();
                //var taskIsolation = _hiContext2.Bedisolates
                //                    .AsNoTracking()
                //                    .ToListAsync();

                //PtIpds = await taskPtIpds;
                //Isolations = await taskIsolation;
                PtIpds = await _hiContext1.Ptipds
                                .AsNoTracking()
                                .Where(p => p.Status == "1")
                                .GroupJoin(_hiContext1.Bedisolates, o => o.IsolateType, i => i.IsolateType, (ptipd, bedisolates) => new { Ptipd = ptipd, Bedisolates = bedisolates })
                                .SelectMany(ptipd => ptipd.Bedisolates.DefaultIfEmpty(), (ptipd, bedisolate) => new AdmitPatient
                                {
                                    AdmitNo = ptipd.Ptipd.AdmitNo,
                                    ChartNo = ptipd.Ptipd.ChartNo,
                                    CheckinDatetime = new string[] { ptipd.Ptipd.AdmitDate.ToString().PadLeft(7, '0'), ptipd.Ptipd.AdmitTime.ToString().PadLeft(6, '0') }.ToAcDateTime(),
                                    DoctorNo = ptipd.Ptipd.VsNo,
                                    BedNo = ptipd.Ptipd.BedNo,
                                    DivNo = ptipd.Ptipd.DivNo,
                                    PrivacyFlag = ptipd.Ptipd.PrivacyFlag,
                                    ExclusiveRoomFlag = ptipd.Ptipd.ExclusiveWardFlag,
                                    ExclusiveRoomFlagDesc = GetExclusiveWradFlagDesc(ptipd.Ptipd.ExclusiveWardFlag ?? null),
                                    IsolateType = ptipd.Ptipd.IsolateType,
                                    IsolateTypeDesc = bedisolate.IsolateDescription ?? null
                                })
                                .GroupJoin(_hiContext1.Beds, o => o.BedNo, i => i.BedNo, (pt, beds) => new { Pt = pt, Beds = beds })
                                .SelectMany(r => r.Beds.DefaultIfEmpty(), (pt, bed) => new AdmitPatient
                                {
                                    AdmitNo = pt.Pt.AdmitNo,
                                    ChartNo = pt.Pt.ChartNo,
                                    CheckinDatetime = pt.Pt.CheckinDatetime,
                                    DoctorNo = pt.Pt.DoctorNo,
                                    BedNo = pt.Pt.BedNo,
                                    NsCode = bed.NsCode ?? null,
                                    DivNo = pt.Pt.DivNo,
                                    PrivacyFlag= pt.Pt.PrivacyFlag,
                                    ExclusiveRoomFlag= pt.Pt.ExclusiveRoomFlag,
                                    ExclusiveRoomFlagDesc = pt.Pt.ExclusiveRoomFlagDesc,
                                    IsolateType = pt.Pt.IsolateType,
                                    IsolateTypeDesc = pt.Pt.IsolateTypeDesc
                                })
                                .GroupJoin(_hiContext1.Ns, o => o.NsCode, i => i.NsCode, (pt, ns) => new { Pt = pt, Ns = ns})
                                .SelectMany(r => r.Ns.DefaultIfEmpty(), (pt, n) => new AdmitPatient { 
                                    AdmitNo= pt.Pt.AdmitNo,
                                    ChartNo = pt.Pt.ChartNo,
                                    CheckinDatetime = pt.Pt.CheckinDatetime,
                                    DoctorNo = pt.Pt.DoctorNo,
                                    BedNo = pt.Pt.BedNo,
                                    NsCode = pt.Pt.NsCode,
                                    NsName = n.NsName ?? null,
                                    DivNo = pt.Pt.DivNo,
                                    PrivacyFlag = pt.Pt.PrivacyFlag,
                                    ExclusiveRoomFlag = pt.Pt.ExclusiveRoomFlag,
                                    ExclusiveRoomFlagDesc = pt.Pt.ExclusiveRoomFlagDesc,
                                    IsolateType = pt.Pt.IsolateType,
                                    IsolateTypeDesc = pt.Pt.IsolateTypeDesc
                                })
                                .ToListAsync();

            }

            IEnumerable<Chart> Patients;
            IEnumerable<Div> Divs;
            IEnumerable<Doctor> Doctors;

            using (var _hoContext1 = _HoFactory.CreateDbContext())
            using (var _hoContext2 = _HoFactory.CreateDbContext())
            using (var _hoContext3 = _HoFactory.CreateDbContext())
            {
                var taskPts = _hoContext1.Charts
                                .AsNoTracking()
                                .Where(c => PtIpds.Select(p => p.ChartNo).ToList().Contains(c.ChartNo))
                                .ToListAsync();
                var taskDivs = _hoContext2.Divs
                                .AsNoTracking()
                                .Where(d => PtIpds.Select(p => p.DivNo).ToList().Contains(d.DivNo))
                                .ToListAsync();
                var taskDoctors = _hoContext3.Doctors
                                    .AsNoTracking()
                                    .Where(o => PtIpds.Select(p => p.DoctorNo).ToList().Contains(o.DoctorNo))
                                    .ToListAsync();

                Patients = await taskPts;
                Divs = await taskDivs;
                Doctors = await taskDoctors;
            }

            PtIpds.AsParallel().ForAll(p => {
                var pt = Patients.FirstOrDefault(pts => pts.ChartNo == p.ChartNo);
                if (pt != null)
                {
                    p.IdNo = pt.IdNo ?? null;
                    p.Name = pt.PtName ?? null;
                    p.Gender = pt.Sex ?? null;
                    p.BirthDate = pt.BirthDate.FromBirthDateToAcDate();
                }
                p.BedNo = p.BedNo ?? null;
                p.NsCode = p.NsCode ?? null;
                p.NsName = p.NsName ?? null;

                var div = Divs.FirstOrDefault(divs => divs.DivNo == p.DivNo);
                if (div != null)
                {
                    p.DivName = div.DivShortName ?? null;
                } else
                {
                    p.DivName = null;
                }
                p.DivNo = p.DivNo ?? null;

                var doctor = Doctors.FirstOrDefault(doc => doc.DoctorNo == p.DoctorNo);
                if (doctor != null)
                {
                    p.DoctorName = doctor.DoctorName ?? null;
                } else
                {
                    p.DoctorName = null;
                }
                p.DoctorNo = p.DoctorNo ?? null;

                //var isolate = Isolations.FirstOrDefault(iso => iso.IsolateType == p.IsolateType);
                //if (isolate != null)
                //{
                //    p.IsolateTypeDesc = isolate.IsolateDescription.Trim();
                //} else
                //{
                //    p.IsolateTypeDesc = "";
                //}
                p.IsolateType = p.IsolateType ?? null;
                p.IsolateTypeDesc = p.IsolateTypeDesc ?? null;
                //p.IsolateType = p.IsolateType.Trim();


            });

            return PtIpds.OrderBy(p => p.CheckinDatetime).ToList();
        }

        public async Task<AdmitPatient?> GetAdmitPatientAsync(string? admitNo)
        {
            AdmitPatient? admitPatient = null;
            Ptipd? ptipd;

            if (admitNo.IsNullOrEmpty()) { return null; }

            using (var hiCtx = _HiFactory.CreateDbContext())
            using (var hoCtx = _HoFactory.CreateDbContext())
            {
                ptipd = await hiCtx.Ptipds
                                   .AsNoTracking()
                                   .FirstOrDefaultAsync(p => p.AdmitNo.Trim() == admitNo);
                if (ptipd != null)
                {
                    admitPatient = new AdmitPatient { AdmitNo = admitNo };
                    admitPatient.ChartNo = ptipd.ChartNo?.Trim();
                    admitPatient.AdmitStatus = ptipd.Status;
                    admitPatient.AdmitStatusDesc = AdmitPatientBiz.GetAdmitStatusDesc(admitPatient.AdmitStatus);
                    admitPatient.CheckinDatetime = new string[] { (ptipd.AdmitDate?.ToString() ?? "").PadLeft(7, '0'), (ptipd.AdmitTime?.ToString() ?? "").PadLeft(6, '0') }.ToAcDateTime();
                    admitPatient.DoctorNo = ptipd.VsNo?.Trim();
                    admitPatient.DivNo = ptipd.DivNo?.Trim();
                    admitPatient.BedNo = ptipd.BedNo?.Trim();
                    admitPatient.PrivacyFlag = ptipd.PrivacyFlag?.Trim();
                    admitPatient.ExclusiveRoomFlag = ptipd.ExclusiveWardFlag?.Trim();
                    admitPatient.ExclusiveRoomFlagDesc = AdmitPatientBiz.GetExclusiveWradFlagDesc(admitPatient.ExclusiveRoomFlag);
                    admitPatient.IsolateType = ptipd.IsolateType?.Trim();

                    var patient = await hoCtx.Charts
                                             .AsNoTracking()
                                             .FirstOrDefaultAsync(p => p.ChartNo.Trim() == admitPatient.ChartNo);
                    if (patient != null)
                    {
                        admitPatient.Name = patient.PtName?.Trim();
                        admitPatient.IdNo = patient.IdNo?.Trim();
                        admitPatient.BirthDate = patient.BirthDate.FromBirthDateToAcDate();
                        admitPatient.Gender = patient.Sex?.Trim();
                    }

                    var doctor = await hoCtx.Doctors
                                            .AsNoTracking()
                                            .FirstOrDefaultAsync(d => d.DoctorNo.Trim() == admitPatient.DoctorNo);
                    admitPatient.DoctorName = doctor?.DoctorName?.Trim();

                    var bed = await hiCtx.Beds
                            .AsNoTracking()
                            .FirstOrDefaultAsync(b => b.BedNo.Trim() == admitPatient.BedNo);
                    if (bed?.NsCode != null)
                    {
                        var ns = await hiCtx.Ns
                            .AsNoTracking()
                            .FirstOrDefaultAsync(n => n.NsCode.Trim() == bed.NsCode.Trim());
                        admitPatient.NsCode = ns?.NsCode?.Trim();
                        admitPatient.NsName = ns?.NsName?.Trim();
                    }

                    var div = await hoCtx.Divs
                                         .AsNoTracking()
                                         .FirstOrDefaultAsync(d => d.DivNo.Trim() == admitPatient.DivNo);
                    admitPatient.DivName = div?.DivShortName?.Trim();

                    var isolate = await hiCtx.Bedisolates
                                             .AsNoTracking()
                                             .FirstOrDefaultAsync(i => i.IsolateType.Trim() == admitPatient.IsolateType);
                    admitPatient.IsolateTypeDesc = isolate?.IsolateDescription?.Trim();
                }
            }

            return admitPatient;
        }
        public static string? GetExclusiveWradFlagDesc(string? flag)
        {
            if (flag == null) { return null; }
            switch (flag)
            {
                case "0":
                    return "一般";
                case "1":
                    return "包房";
                case "2":
                    return "隔離";
                default:
                    return null;
            }
        }

        public static string? GetAdmitStatusDesc(string? admitStatus)
        {
            if (admitStatus == null) { return null; }
            switch (admitStatus)
            {
                case "0":
                    return "申報";
                case "1":
                    return "住院";
                case "2":
                    return "關帳";
                case "3":
                    return "重開帳";
                case "4":
                    return "結帳";
                case "5":
                    return "結帳取消";
                case "6":
                    return "退床未關帳";
                case "7":
                    return "取消住院";
                case "N":
                    return "假住院";
                default:
                    return null;
            }
        }
    }
}
