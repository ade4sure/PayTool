using Calculator.Data;
using Calculator.Models.AppModels;
using Calculator.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Calculator.Services
{

    public partial class PayService : IPayService
    {
        private readonly ApplicationDbContext _paycontext;
        private bool _SavePayload = true;
        private List<PayCategory> PayCategories { get; set; } = new List<PayCategory>();
        private List<PaymentStructure> PaymentStructures { get; set; } = new List<PaymentStructure>();

        public PayService(ApplicationDbContext Paycontext)
        {
            _paycontext = Paycontext;
        }

        public async Task<GetStructureresponse> GetMonthlyPayDetails(PayView payView, Extras ExtraPayOptions = null)
        {

            //get applicable payment stuctures
            var OperationalPStructure = (await GetPaymentStructure())
                                                .Where(w => w.EffectiveDate.OperationStartDate.CompareTo(payView.Dato) <= 0 &&
                                                            w.EffectiveDate.OperationEndDate.CompareTo(payView.Dato) >= 0 &&
                                                            w.CategoryId == payView.CategoryId)
                                                .FirstOrDefault();

            // the Pstructure to be used (also used)
            var ApprovedPStructure = (await GetPaymentStructure())
                                                .Where(w => w.EffectiveDate.ApprovedStartDate.CompareTo(payView.Dato) <= 0 &&
                                                            w.EffectiveDate.ApprovedEndDate.CompareTo(payView.Dato) >= 0 &&
                                                            w.CategoryId == payView.CategoryId)
                                                .FirstOrDefault();

            if (ApprovedPStructure.Id == OperationalPStructure.Id)
            {
                var OnlyPay = await GetMonthlyPay(OperationalPStructure,payView.UnionId, payView.Grade, payView.Step, ExtraPayOptions);

                return new GetStructureresponse()
                {
                    ApprovedStructure = ApprovedPStructure.Name,
                    ApprovedStructureAmount = OnlyPay,
                    OpStructure = OperationalPStructure.Name,
                    OpStructureAmount = OnlyPay
                };
            }

            var OpertionalPay = await GetMonthlyPay(OperationalPStructure, payView.UnionId, payView.Grade, payView.Step, ExtraPayOptions);
            var ApprovedPay = await GetMonthlyPay(ApprovedPStructure, payView.UnionId, payView.Grade, payView.Step, ExtraPayOptions);

            return new GetStructureresponse()
            {
                ApprovedStructure = ApprovedPStructure.Name,
                ApprovedStructureAmount = ApprovedPay,
                OpStructure = OperationalPStructure.Name,
                OpStructureAmount = OpertionalPay
            };

        }

        public async Task<decimal> GetMonthlyPay(PaymentStructure Pstructure, int UnionId, int Grade, int Step, Extras ExtraPayOptions = null)
        {
            //get applicable payment stuctures
            PaymentStructure OperationalPStructure = (await GetPaymentStructure())
                                                            .Where(w => w.Id == Pstructure.Id)
                                                            .FirstOrDefault();

            var grd = OperationalPStructure.Grades.Where(g => g.Number == Grade).FirstOrDefault();
            var stp = grd.Steps.Where(s => s.Number == Step).FirstOrDefault();

            decimal amnt = 0;

            //Debug.WriteLine("Grade = " + Grade + "Step = " + Step + "Stucture =" + OperationalPStructure.Name  );

            if (Pstructure.CategoryId == 1)
            {
                amnt = stp.NonTeachingAmount;
            }
            else
            {
                amnt = stp.TeachingAmount;
                Debug.WriteLine("Grade = " + Grade + " Step = " + Step + " Amount = " + amnt + " Stucture = " + OperationalPStructure.Name);
            }


            if (ExtraPayOptions != null)
            {
                if (ExtraPayOptions.IsProffesional)
                {
                    amnt += UnionId == 3 ? stp.ProffSsanu : 0;  //Add proffAllowance for SSANU
                    amnt += UnionId == 2 ? stp.ProffNaat : 0; //Add proffAllowance for NAAT
                }
                amnt += ExtraPayOptions.IsShiftDutyNurse ? stp.ShiftDutyNurses : 0;
                amnt += ExtraPayOptions.IsShiftDutyOthers ? stp.ShiftDutyOthersComputed : 0;
                amnt += ExtraPayOptions.IsCallDutyNurse ? stp.CallDutyNurses : 0;
                amnt += ExtraPayOptions.IsCallDutyOthers ? stp.CallDutyOthers : 0;
                amnt += ExtraPayOptions.IsCallDutyASUU ? stp.CallDutyASUU : 0;
            }

            return amnt;

        }

        private async Task<List<PaymentStructure>> GetPaymentStructure()
        {
            if (PaymentStructures.Count < 1)
            {
                PaymentStructures = await _paycontext.PaymentStructures
                                    .Include("Grades.Steps")
                                    .Include("EffectiveDate.CStructure")
                                    .ToListAsync();
            }
            return PaymentStructures;

        }

        public async Task<List<PayCategory>> GetPayCategory()
        {
            if (PayCategories.Count() == 0)
            {
                PayCategories = await _paycontext.PayCategorys.ToListAsync();
            }
            return PayCategories;

        }

        public async Task<bool> SavePromoPayload(PromotionArr PromoPayload)
        {
            var DBpayload = await _paycontext.PromotionPayloads
                .Where(w => w.staffNumber.ToLower() == PromoPayload.staffNumber.ToLower())
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (DBpayload == null)
            {
                _paycontext.Add(PromoPayload);
            }
            else
            {
                PromoPayload.Id = DBpayload.Id;
                PromoPayload.staffNumber = PromoPayload.staffNumber.Trim();
                PromoPayload.staffName = PromoPayload.staffName.Trim();
                PromoPayload.staffStatus = PromoPayload.staffStatus.Trim();
                PromoPayload.staffUnit = PromoPayload.staffUnit.Trim();
                Debug.WriteLine("PayLoad id = " + DBpayload.Id + ":" + PromoPayload.Id + ", StaffName = " + DBpayload.staffName+ ", StaffNumber = " + DBpayload.staffNumber);
                _paycontext.Update(PromoPayload);
            }

            await _paycontext.SaveChangesAsync();

            return true;
        }

        public async Task<PromotionArr> LoadPromoPayload(string StaffNumber)
        {
            var prom = await _paycontext.PromotionPayloads
                               .Where(w => w.staffNumber.ToLower() == StaffNumber.ToLower())
                               .AsNoTracking()
                               .FirstOrDefaultAsync();

            return prom == null ? new PromotionArr() : prom;

        }

        public async Task<List<Union>> GetUnion()
        {

            return await _paycontext.Unions.ToListAsync();

        }

        public async Task<List<string>> LoadUnionMembers(int UnionId)
        {
            var payload = await _paycontext.PromotionPayloads
                               .Where(w => w.UnionId == UnionId)
                               .AsNoTracking()
                               .OrderBy(o => o.staffNumber)
                               //.Skip(206)
                               //.Take(207)
                               .Select(s => s.staffNumber)
                               .ToListAsync();
            return payload != null ? payload : new List<string>();

        }

        public void ToggleSavePayload()
        {
            _SavePayload = !_SavePayload;
        }

        public bool GetSavePayload()
        {
            return _SavePayload;
        }
    }
}

