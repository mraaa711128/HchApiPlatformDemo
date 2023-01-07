using HchApiPlatform.DbContexts;
using HchApiPlatform.Models;
using HchApiPlatform.RequestModels;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Xml.Linq;
using Utility.Extensions;

namespace HchApiPlatform.Biz
{
    public class AdmitBedStatBiz
    {
        private IDbContextFactory<HchPlatformContext> _PlatformFactory;
        private AdmitBedBiz _admitBedBiz;
        private AdmitPatientBiz _admitPatientBiz;

        public AdmitBedStatBiz(IDbContextFactory<HchPlatformContext> platformFactory, AdmitBedBiz admitBedBiz, AdmitPatientBiz admitPatientBiz)
        {
            _PlatformFactory = platformFactory;
            _admitBedBiz = admitBedBiz;
            _admitPatientBiz = admitPatientBiz;
        }

        public async Task<IEnumerable<AdmitBed>> GetAdmitBedsAsync(string[] nsCodes)
        {
            var result = new List<AdmitBed>();
            await Task.Factory.StartNew(() => {
                using (var platformCtx = _PlatformFactory.CreateDbContext())
                {
                    var query = platformCtx.AdmitBedStats.AsNoTracking();
                    if (!nsCodes.IsNullOrEmpty())
                    {
                        query = query.Where(b => nsCodes.Contains(b.NsCode));
                    }
                    var bedStats = query.ToList();

                    bedStats.AsParallel().ForAll(bedStat =>
                    {
                        var bed = new AdmitBed
                        {
                            BedNo = bedStat.BedNo.Trim(),
                            NsCode = bedStat.NsCode?.Trim(),
                            NsName = bedStat.NsName,
                            WardNo = bedStat.WardNo?.Trim(),
                            Status = bedStat.Status,
                            StatusDesc = bedStat.StatusDesc,
                            AdmitNo = bedStat.AdmitNo?.Trim(),
                            ChartNo = bedStat.ChartNo?.Trim()
                        };
                        result.Add(bed);
                    });
                }
            });
            return result;
        }

        public async Task<IEnumerable<AdmitBedDetail>> GetAdmitBedsWithDetailAsync(string[] nsCodes)
        {
            var result = new List<AdmitBedDetail>();
            await Task.Factory.StartNew(() => { 
                using (var platformCtx = _PlatformFactory.CreateDbContext())
                {
                    var query = platformCtx.AdmitBedStats.AsNoTracking();
                    if (!nsCodes.IsNullOrEmpty())
                    {
                        query = query.Where(b => nsCodes.Contains(b.NsCode));
                    }
                    var bedStats = query.ToList();

                    bedStats.AsParallel().ForAll(bedStat => {
                        var bedDetail = new AdmitBedDetail
                        {
                            BedNo = bedStat.BedNo.Trim(),
                            NsCode = bedStat.NsCode?.Trim(),
                            NsName = bedStat.NsName,
                            WardNo = bedStat.WardNo?.Trim(),
                            Status = bedStat.Status,
                            StatusDesc = bedStat.StatusDesc,
                            AdmitNo = bedStat.AdmitNo?.Trim(),
                            ChartNo = bedStat.ChartNo?.Trim(),
                            AdmitStatus = bedStat.AdmitStatus,
                            AdmitStatusDesc = bedStat.AdmitStatusDesc,
                            Name = bedStat.Name,
                            IdNo = bedStat.IdNo,
                            BirthDate = bedStat.BirthDate,
                            Gender = bedStat.Gender,
                            CheckinDatetime = bedStat.CheckinDateTime,
                            DoctorNo = bedStat.DoctorNo?.Trim(),
                            DoctorName = bedStat.DoctorName,
                            DivNo = bedStat.DivNo?.Trim(),
                            DivName = bedStat.DivName,
                            PrivacyFlag = bedStat.PrivacyFlag,
                            ExclusiveRoomFlag = bedStat.ExclusiveRoomFlag,
                            ExclusiveRoomFlagDesc = bedStat.ExclusiveRoomFlagDesc,
                            IsolateType = bedStat.IsolateType,
                            IsolateTypeDesc = bedStat.IsolateTypeDesc
                        };
                        result.Add(bedDetail);
                    });
                }
            });
            return result;
        }

        public async Task<bool> LeaveAdmitPatientAsync(AdmitPatientLeave admitPatient)
        {
            using (var platformCtx = _PlatformFactory.CreateDbContext())
            {
                try
                {
                    var bed = await platformCtx.AdmitBedStats.FirstOrDefaultAsync(b => b.BedNo == admitPatient.BedNo);
                    if (bed == null) { return false; }

                    if (bed.Status != "A")
                    {
                        if (bed.AdmitNo == admitPatient.AdmitNo && bed.ChartNo == admitPatient.ChartNo)
                        {
                            var newBed = new AdmitBedStat
                            {
                                BedNo = bed.BedNo,
                                NsCode = bed.NsCode,
                                NsName = bed.NsName,
                                WardNo = bed.WardNo,
                                Status = AdmitBedBiz.BedStatus_Empty,
                                StatusDesc = AdmitBedBiz.GetBedStatusDescription(AdmitBedBiz.BedStatus_Empty)
                            };
                            platformCtx.AdmitBedStats.Remove(bed);
                            await platformCtx.SaveChangesAsync(); 
                            platformCtx.AdmitBedStats.Add(newBed);
                            await platformCtx.SaveChangesAsync();
                        }
                    }
                    return true;
                }
                catch (Exception ex)
                {

                    return false;
                }
            }
        }

        public async Task<bool> UpdateAdmitBedStatsAsync()
        {
            var admitBeds = await _admitBedBiz.GetAdmitBedsAsync(new string[] { });
            return await Task.Factory.StartNew<bool>(() => {
                admitBeds.AsParallel().ForAll(async (admitBed) => { 
                    using(var platformCtx = _PlatformFactory.CreateDbContext())
                    {
                        var admitBedStat = await platformCtx.AdmitBedStats.FirstOrDefaultAsync(b => b.BedNo == admitBed.BedNo);
                        if (admitBedStat == null)
                        {
                            admitBedStat = new AdmitBedStat {
                                BedNo = admitBed.BedNo,
                                NsCode = admitBed.NsCode,
                                NsName = admitBed.NsName,
                                WardNo = admitBed.WardNo,
                                Status = admitBed.Status,
                                StatusDesc = admitBed.StatusDesc,
                            };
                            var admitBedDetail = await _admitBedBiz.GetAdmitBedWithDetailAsync(admitBed);
                            UpdateBedStatFromBedDetail(admitBedDetail, admitBedStat);
                            await platformCtx.AdmitBedStats.AddAsync(admitBedStat);
                            await platformCtx.SaveChangesAsync();
                        } else
                        {
                            await UpdateAdmitBedStatAsync(admitBed, admitBedStat);
                            var result = platformCtx.AdmitBedStats.Update(admitBedStat);
                            if (result.State == EntityState.Modified)
                            {
                                await platformCtx.SaveChangesAsync();
                            }
                        }
                    }
                });
                return true;
            });
        }
         
        private async Task UpdateAdmitBedStatAsync(AdmitBed admitBed, AdmitBedStat admitBedStat)
        {
            AdmitBedDetail admitBedDetail;
            switch (admitBed.Status)
            {
                case AdmitBedBiz.BedStatus_Reserved:
                    admitBedDetail = await _admitBedBiz.GetAdmitBedWithDetailAsync(admitBed);
                    UpdateBedStatFromBedDetail(admitBedDetail, admitBedStat);
                    admitBedStat.Status = admitBed.Status;
                    admitBedStat.StatusDesc = admitBed.StatusDesc;
                    break;
                case AdmitBedBiz.BedStatus_Occupied:
                    admitBedDetail = await _admitBedBiz.GetAdmitBedWithDetailAsync(admitBed);
                    UpdateBedStatFromBedDetail(admitBedDetail, admitBedStat);
                    admitBedStat.Status = admitBed.Status;
                    admitBedStat.StatusDesc = admitBed.StatusDesc;
                    break;
                case AdmitBedBiz.BedStatus_Empty:
                case AdmitBedBiz.BedStatus_Isolated:
                case AdmitBedBiz.BedStatus_Cleaning:
                    if (admitBedStat.Status == AdmitBedBiz.BedStatus_Occupied)
                    {
                        var admitPatient = await _admitPatientBiz.GetAdmitPatientAsync(admitBedStat.AdmitNo);
                        if (admitPatient?.BedNo?.Trim() != admitBedStat.BedNo?.Trim()) {    // Bed Transfer 
                            admitBedDetail = await _admitBedBiz.GetAdmitBedWithDetailAsync(admitBed);
                            UpdateBedStatFromBedDetail(admitBedDetail, admitBedStat);
                            admitBedStat.Status = admitBed.Status;
                            admitBedStat.StatusDesc = admitBed.StatusDesc;
                        }
                        else    // Close or Checkout need to keep old patient data
                        {       
                            var newAdmitBed = new AdmitBed
                            {
                                BedNo = admitBed.BedNo,
                                NsCode = admitBed.NsCode,
                                NsName = admitBed.NsName,
                                WardNo = admitBed.WardNo,
                                Status = admitBedStat.Status,
                                StatusDesc = admitBedStat.StatusDesc,
                                AdmitNo = admitBedStat.AdmitNo,
                                ChartNo = admitBedStat.ChartNo
                            };
                            //admitBed.Status = admitBedStat.Status;
                            //admitBed.StatusDesc = admitBedStat.StatusDesc;
                            //admitBed.AdmitNo = admitBedStat.AdmitNo;
                            //admitBed.ChartNo = admitBedStat.ChartNo;
                            admitBedDetail = await _admitBedBiz.GetAdmitBedWithDetailAsync(newAdmitBed);
                            if (admitBedDetail.AdmitStatus == AdmitPatientBiz.AdmitStatus_AdmitCanceled ||
                                    admitBedDetail.AdmitStatus == AdmitPatientBiz.AdmitStatus_NoAdmit ||
                                    admitBedDetail.AdmitStatus == AdmitPatientBiz.AdmitStatus_Apply)
                            {
                                admitBedDetail = await _admitBedBiz.GetAdmitBedWithDetailAsync(admitBed);
                            }
                            UpdateBedStatFromBedDetail(admitBedDetail, admitBedStat);
                        }
                    }
                    else
                    {
                        admitBedDetail = await _admitBedBiz.GetAdmitBedWithDetailAsync(admitBed);
                        UpdateBedStatFromBedDetail(admitBedDetail, admitBedStat);
                        admitBedStat.Status = admitBed.Status;
                        admitBedStat.StatusDesc = admitBed.StatusDesc;
                    }
                    break;
            }
            return;
        }

        private void UpdateBedStatFromBedDetail(AdmitBedDetail admitBedDetail, AdmitBedStat admitBedStat)
        {
            if (admitBedDetail == null || admitBedStat == null) { return; }
            admitBedStat.AdmitNo = admitBedDetail.AdmitNo;
            admitBedStat.ChartNo = admitBedDetail.ChartNo;
            admitBedStat.AdmitStatus = admitBedDetail.AdmitStatus;
            admitBedStat.AdmitStatusDesc = admitBedDetail.AdmitStatusDesc;
            admitBedStat.Name = admitBedDetail.Name;
            admitBedStat.IdNo = admitBedDetail.IdNo;
            admitBedStat.BirthDate = admitBedDetail.BirthDate;
            admitBedStat.Gender = admitBedDetail.Gender;
            admitBedStat.CheckinDateTime = admitBedDetail.CheckinDatetime;
            admitBedStat.DoctorNo = admitBedDetail.DoctorNo;
            admitBedStat.DoctorName = admitBedDetail.DoctorName;
            admitBedStat.DivNo = admitBedDetail.DivNo;
            admitBedStat.DivName = admitBedDetail.DivName;
            admitBedStat.PrivacyFlag = admitBedDetail.PrivacyFlag;
            admitBedStat.ExclusiveRoomFlag = admitBedDetail.ExclusiveRoomFlag;
            admitBedStat.ExclusiveRoomFlagDesc = admitBedDetail.ExclusiveRoomFlagDesc;
            admitBedStat.IsolateType = admitBedDetail.IsolateType;
            admitBedStat.IsolateTypeDesc = admitBedDetail.IsolateTypeDesc;
            return;
        }
    }
}
