using HchApiPlatform.DbContexts;
using HchApiPlatform.Models;
using Microsoft.EntityFrameworkCore;
using Utility.Comparer;
using Utility.Extensions;

namespace HchApiPlatform.Biz
{
    public class AdmitBedBiz
    {
        public const string BedStatus_Empty = "E";
        public const string BedStatus_Occupied = "O";
        public const string BedStatus_Isolated = "I";
        public const string BedStatus_Reserved = "A";
        public const string BedStatus_Cleaning = "C";

        IDbContextFactory<UnimaxHoContext> _hoFactory;
        IDbContextFactory<UnimaxHiContext> _hiFactory;

        public AdmitBedBiz(IDbContextFactory<UnimaxHoContext> hoFactory, IDbContextFactory<UnimaxHiContext> hiFactory)
        {
            _hoFactory = hoFactory;
            _hiFactory = hiFactory;
        }

        public async Task<IEnumerable<AdmitBed>> GetAdmitBedsAsync(string[] nsCodes)
        {
            using (var hiContext1 = _hiFactory.CreateDbContext())
            using (var hiContext2 = _hiFactory.CreateDbContext())
            using (var hiContext3 = _hiFactory.CreateDbContext())
            using (var hiContext4 = _hiFactory.CreateDbContext())
            {
                var taskBedList = hiContext1.Beds
                                .AsNoTracking()
                                .GroupBy(b => new
                                {
                                    b.BedNo,
                                    //b.EffectiveDateC
                                }).Select(r => new
                                {
                                    r.Key.BedNo,
                                    EffectiveDateC = r.Min(b => b.EffectiveDateC)
                                }).ToListAsync();

                var taskNsList = hiContext2.Ns
                                .AsNoTracking()
                                .ToListAsync();


                //var bedList = await taskBedList;
                //var nsList = await taskNsList;
                await Task.WhenAll(taskBedList, taskNsList);
                var bedList = taskBedList.Result;
                var nsList = taskNsList.Result;

                //var keyBedList = bedList.Select(l => $"{l.BedNo}_{l.EffectiveDateC}").ToList();
                var query = hiContext3.Beds
                                .AsNoTracking();
                                //.Where(b => bedList.Contains(new { BedNo = b.BedNo, EffectiveDateC = b.EffectiveDateC }));
                if (!nsCodes.IsNullOrEmpty())
                {
                    query = query.Where(b => nsCodes.Contains(b.NsCode));
                }
                var taskNsBedList = query.ToListAsync();
                var taskBedStatus = hiContext4.Bedstatus
                                              .AsNoTracking()
                                              .ToListAsync();
                await Task.WhenAll(taskNsBedList, taskBedStatus);
                var nsBedList = taskNsBedList.Result;
                var bedStatus = taskBedStatus.Result;

                var finalBedList = nsBedList.Where(n => bedList.Any(b => b.BedNo == n.BedNo && b.EffectiveDateC == n.EffectiveDateC))
                                            .ToList();
                var result = finalBedList.GroupJoin(nsList, o => o.NsCode, i => i.NsCode, (bed, ns) => new { Bed = bed, Ns = ns })
                                         .SelectMany(bed => bed.Ns.DefaultIfEmpty(), (bed, n) => new
                                         {
                                             BedNo = bed.Bed.BedNo,
                                             NsCode = bed.Bed.NsCode,
                                             NsName = n?.NsName?.Trim(),
                                             WardNo = bed.Bed.WardNo
                                         })
                                         .GroupJoin(bedStatus, o => o.BedNo, i => i.BedNo, (bed, statuses) => new { Bed = bed, Statuses = statuses })
                                         .SelectMany(bed => bed.Statuses.DefaultIfEmpty(), (bed, status) => new AdmitBed
                                         {
                                             BedNo = bed.Bed.BedNo.Trim(),
                                             NsCode = bed.Bed.NsCode?.Trim(),
                                             NsName = bed.Bed.NsName?.Trim(),
                                             WardNo = bed.Bed.WardNo?.Trim(),
                                             Status = status?.Status?.Trim(),
                                             StatusDesc = GetBedStatusDescription(status?.Status?.Trim() ?? ""),
                                             AdmitNo = status?.AdmitNo?.Trim(),
                                             //ChartNo = status.AssignedChartNo?.Trim() ?? ""
                                             ChartNo = GetActualAssignChartNo(status?.Status?.Trim(), status?.AssignedChartNo?.Trim())
                                         })
                                         .OrderBy(x => x.NsCode)
                                         .ThenBy(x => x.BedNo)
                                         .ToList();

                return result;
            }

        }

        public async Task<IEnumerable<AdmitBedDetail>> GetAdmitBedsWithDetailAsync(IList<AdmitBed> admitBeds)
        {
            List<AdmitBedDetail> result = new List<AdmitBedDetail>();

            await Task.Factory.StartNew(() => {
                admitBeds.AsParallel().ForAll(admitBed => {
                    var detail = new AdmitBedDetail
                    {
                        BedNo = admitBed.BedNo,
                        NsCode = admitBed.NsCode,
                        NsName = admitBed.NsName,
                        WardNo = admitBed.WardNo,
                        Status = admitBed.Status,
                        StatusDesc = admitBed.StatusDesc,
                        AdmitNo = admitBed.AdmitNo,
                        //ChartNo = admitBed.ChartNo.IsNullOrEmpty() ? "" : admitBed.ChartNo.Replace("@@", "").PadLeft(10, '0'),
                        ChartNo = admitBed.ChartNo,
                        Name = string.Empty,
                        IdNo = string.Empty,
                        Gender = string.Empty,
                        DivNo = string.Empty,
                        DivName = string.Empty,
                        DoctorNo = string.Empty,
                        DoctorName = string.Empty,
                        PrivacyFlag = string.Empty,
                        ExclusiveRoomFlag = string.Empty,
                        ExclusiveRoomFlagDesc = string.Empty,
                        IsolateType = string.Empty,
                        IsolateTypeDesc = string.Empty
                    };
                    if (!detail.AdmitNo.IsNullOrEmpty())
                    {
                        Ptipd? ptipd;
                        using (var hiCtx = _hiFactory.CreateDbContext())
                        using (var hoCtx = _hoFactory.CreateDbContext())
                        {
                            ptipd = hiCtx.Ptipds
                                         .AsNoTracking()
                                         .FirstOrDefault(p => p.AdmitNo.Trim() == detail.AdmitNo);
                            if (ptipd != null)
                            {
                                detail.ChartNo = ptipd.ChartNo?.Trim() ?? "";
                                detail.CheckinDatetime = new string[] { (ptipd.AdmitDate?.ToString() ?? "").PadLeft(7, '0'), (ptipd.AdmitTime?.ToString() ?? "").PadLeft(6, '0') }.ToAcDateTime();
                                detail.DoctorNo = ptipd.VsNo?.Trim() ?? "";
                                detail.DivNo = ptipd.DivNo?.Trim() ?? "";
                                detail.PrivacyFlag = ptipd.PrivacyFlag?.Trim() ?? "";
                                detail.ExclusiveRoomFlag = ptipd.ExclusiveWardFlag?.Trim() ?? "";
                                detail.ExclusiveRoomFlagDesc = AdmitPatientBiz.GetExclusiveWradFlagDesc(detail.ExclusiveRoomFlag);
                                detail.IsolateType = ptipd.IsolateType?.Trim() ?? "";

                                var doctor = hoCtx.Doctors
                                                  .AsNoTracking()
                                                  .FirstOrDefault(d => d.DoctorNo.Trim() == detail.DoctorNo);
                                detail.DoctorName = doctor?.DoctorName?.Trim() ?? "";

                                var div = hoCtx.Divs
                                               .AsNoTracking()
                                               .FirstOrDefault(d => d.DivNo.Trim() == detail.DivNo);
                                detail.DivName = div?.DivShortName?.Trim() ?? "";

                                var isolate = hiCtx.Bedisolates
                                                   .AsNoTracking()
                                                   .FirstOrDefault(i => i.IsolateType.Trim() == detail.IsolateType);
                                detail.IsolateTypeDesc = isolate?.IsolateDescription?.Trim() ?? "";
                            }

                        }
                    }
                    if (detail.Status == "A" || detail.Status == "O")
                    {
                        Chart? patient;
                        using (var hoCtx = _hoFactory.CreateDbContext())
                        {
                            patient = hoCtx.Charts
                                           .AsNoTracking()
                                           .FirstOrDefault(p => p.ChartNo.Trim() == detail.ChartNo);
                            if (patient != null)
                            {
                                detail.Name = patient.PtName?.Trim() ?? "";
                                detail.IdNo = patient.IdNo?.Trim() ?? "";
                                detail.BirthDate = patient.BirthDate.FromBirthDateToAcDate();
                                detail.Gender = patient.Sex?.Trim() ?? "";
                            }
                        }
                    } else
                    {
                        detail.ChartNo = string.Empty;
                    }

                    result.Add(detail);
                });
            });

            result.OrderBy(r => r.NsCode)
                  .ThenBy(r => r.BedNo)
                  .ToList();
            
            return result;
        }
        
        public async Task<AdmitBedDetail> GetAdmitBedWithDetailAsync(AdmitBed admitBed)
        {
            var detail = new AdmitBedDetail
            {
                BedNo = admitBed.BedNo,
                NsCode = admitBed.NsCode,
                NsName = admitBed.NsName,
                WardNo = admitBed.WardNo,
                Status = admitBed.Status,
                StatusDesc = admitBed.StatusDesc,
                AdmitNo = admitBed.AdmitNo,
                ChartNo = admitBed.ChartNo,
                Name = null,
                IdNo = null,
                Gender = null,
                DivNo = null,
                DivName = null,
                DoctorNo = null,
                DoctorName = null,
                PrivacyFlag = null,
                ExclusiveRoomFlag = null,
                ExclusiveRoomFlagDesc = null,
                IsolateType = null,
                IsolateTypeDesc = null
            };
            if (!detail.AdmitNo.IsNullOrEmpty())
            {
                Ptipd? ptipd;
                using (var hiCtx = _hiFactory.CreateDbContext())
                using (var hoCtx = _hoFactory.CreateDbContext())
                {
                    ptipd = await hiCtx.Ptipds
                                       .AsNoTracking()
                                       .FirstOrDefaultAsync(p => p.AdmitNo.Trim() == detail.AdmitNo);
                    if (ptipd != null)
                    {
                        detail.ChartNo = ptipd.ChartNo?.Trim();
                        detail.AdmitStatus = ptipd.Status;
                        detail.AdmitStatusDesc = AdmitPatientBiz.GetAdmitStatusDesc(detail.AdmitStatus);
                        detail.CheckinDatetime = new string[] { (ptipd.AdmitDate?.ToString() ?? "").PadLeft(7, '0'), (ptipd.AdmitTime?.ToString() ?? "").PadLeft(6, '0') }.ToAcDateTime();
                        detail.DoctorNo = ptipd.VsNo?.Trim();
                        detail.DivNo = ptipd.DivNo?.Trim();
                        detail.PrivacyFlag = ptipd.PrivacyFlag?.Trim();
                        detail.ExclusiveRoomFlag = ptipd.ExclusiveWardFlag?.Trim();
                        detail.ExclusiveRoomFlagDesc = AdmitPatientBiz.GetExclusiveWradFlagDesc(detail.ExclusiveRoomFlag);
                        detail.IsolateType = ptipd.IsolateType?.Trim();

                        if (detail.Status == "A" || detail.Status == "O")
                        {

                            var patient = await hoCtx.Charts
                                                     .AsNoTracking()
                                                     .FirstOrDefaultAsync(p => p.ChartNo.Trim() == detail.ChartNo);
                            if (patient != null)
                            {
                                detail.Name = patient.PtName?.Trim();
                                detail.IdNo = patient.IdNo?.Trim();
                                detail.BirthDate = patient.BirthDate.FromBirthDateToAcDate();
                                detail.Gender = patient.Sex?.Trim();
                            }

                            var doctor = await hoCtx.Doctors
                                                    .AsNoTracking()
                                                    .FirstOrDefaultAsync(d => d.DoctorNo.Trim() == detail.DoctorNo);
                            detail.DoctorName = doctor?.DoctorName?.Trim();

                            var div = await hoCtx.Divs
                                                 .AsNoTracking()
                                                 .FirstOrDefaultAsync(d => d.DivNo.Trim() == detail.DivNo);
                            detail.DivName = div?.DivShortName?.Trim();

                            var isolate = await hiCtx.Bedisolates
                                                     .AsNoTracking()
                                                     .FirstOrDefaultAsync(i => i.IsolateType.Trim() == detail.IsolateType);
                            detail.IsolateTypeDesc = isolate?.IsolateDescription?.Trim();
                        }
                    }
                }
            }
            return detail;
        }

        public static string? GetBedStatusDescription(string? bedStatus)
        {
            switch (bedStatus?.Trim())
            {
                case "E":
                    return "空床";
                case "O":
                    return "佔床";
                case "I":
                    return "隔離床";
                case "A":
                    return "預約";
                case "C":
                    return "清床中";
                default:
                    return null;
            }
        }

        public static string? GetActualAssignChartNo(string? bedStatus, string? assignedChartNo)
        {
            if (assignedChartNo.IsNullOrEmpty()) { return null; }
            switch (bedStatus?.Trim())
            {
                case "O":
                case "A":
                    return assignedChartNo?.Replace("@@", "").PadLeft(10, '0');
                default:
                    return null;
            }
        }
    }

}
