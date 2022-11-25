using HchApiPlatform.DbContexts;
using HchApiPlatform.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System.Reflection.Metadata.Ecma335;
using Utility.Extensions;

namespace HchApiPlatform.Biz
{
    public class AdmitPatientBiz
    {
        private IDbContextFactory<UnimaxHoContext> _HoFactory;
        private IDbContextFactory<UnimaxHiContext> _HiFactory;
        
        public AdmitPatientBiz(IDbContextFactory<UnimaxHoContext> hoFactory, IDbContextFactory<UnimaxHiContext> hiFactory) {
            _HoFactory = hoFactory;
            _HiFactory = hiFactory;
        }

        public async Task<IEnumerable<AdmitPatient>> GetAdmitPatientsAsync()
        {
            IEnumerable<AdmitPatient> PtIpds;
            IEnumerable<Bedisolate> Isolations;
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
                                    ExclusiveRoomFlagDesc = GetExclusiveWradFlagDesc(ptipd.Ptipd.ExclusiveWardFlag.Trim()),
                                    IsolateType = ptipd.Ptipd.IsolateType,
                                    IsolateTypeDesc = bedisolate.IsolateDescription ?? string.Empty
                                })
                                .GroupJoin(_hiContext1.Beds, o => o.BedNo, i => i.BedNo, (pt, beds) => new { Pt = pt, Beds = beds })
                                .SelectMany(r => r.Beds.DefaultIfEmpty(), (pt, bed) => new AdmitPatient
                                {
                                    AdmitNo = pt.Pt.AdmitNo,
                                    ChartNo = pt.Pt.ChartNo,
                                    CheckinDatetime = pt.Pt.CheckinDatetime,
                                    DoctorNo = pt.Pt.DoctorNo,
                                    BedNo = pt.Pt.BedNo,
                                    NsCode = bed.NsCode ?? string.Empty,
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
                                    NsName = n.NsName ?? string.Empty,
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
                    p.IdNo = pt.IdNo.Trim();
                    p.Name = pt.PtName.Trim();
                    p.Gender = pt.Sex.Trim();
                    p.BirthDate = pt.BirthDate.FromBirthDateToAcDate();
                }
                p.BedNo = p.BedNo.Trim();
                p.NsCode = p.NsCode.Trim();
                p.NsName = p.NsName.Trim();

                var div = Divs.FirstOrDefault(divs => divs.DivNo == p.DivNo);
                if (div != null)
                {
                    p.DivName = div.DivShortName.Trim();
                } else
                {
                    p.DivName = "";
                }
                p.DivNo = p.DivNo.Trim();

                var doctor = Doctors.FirstOrDefault(doc => doc.DoctorNo == p.DoctorNo);
                if (doctor != null)
                {
                    p.DoctorName = doctor.DoctorName.Trim();
                } else
                {
                    p.DoctorName = "";
                }
                p.DoctorNo = p.DoctorNo.Trim();

                //var isolate = Isolations.FirstOrDefault(iso => iso.IsolateType == p.IsolateType);
                //if (isolate != null)
                //{
                //    p.IsolateTypeDesc = isolate.IsolateDescription.Trim();
                //} else
                //{
                //    p.IsolateTypeDesc = "";
                //}
                p.IsolateType = p.IsolateType.Trim();
                p.IsolateTypeDesc = p.IsolateTypeDesc ?? string.Empty;
                //p.IsolateType = p.IsolateType.Trim();


            });

            return PtIpds.OrderBy(p => p.CheckinDatetime).ToList();
        }

        private static string GetExclusiveWradFlagDesc(string flag)
        {
            switch (flag)
            {
                case "0":
                    return "一般";
                case "1":
                    return "包房";
                case "2":
                    return "隔離";
                default:
                    return "";
            }
        }
    }
}
