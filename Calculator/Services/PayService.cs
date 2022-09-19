using Calculator.Data;
using Calculator.Models.AppModels;
using Calculator.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Calculator.Services
{
    public interface IPayService
    {
        Task<GetStructureresponse> GetMonthlyPayDetails(PayView payView, Extras ExtraPayOptions = null);
        Task<List<PayCategory>> GetPayCategory();
        Task<bool> SavePromoPayload(PromotionArr PromoPayload);
        Task<PromotionArr> LoadPromoPayload(string StaffNumber);

    }

    public class PayService : IPayService
    {
        private readonly ApplicationDbContext _paycontext;
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

            var OpertionalPay = await GetMonthlyPay(OperationalPStructure, payView.Grade, payView.Step, ExtraPayOptions);
            var ApprovedPay = await GetMonthlyPay(ApprovedPStructure, payView.Grade, payView.Step, ExtraPayOptions);

            return new GetStructureresponse()
            {
                ApprovedStructure = ApprovedPStructure.Name,
                ApprovedStructureAmount = ApprovedPay,
                OpStructure = OperationalPStructure.Name,
                OpStructureAmount = OpertionalPay
            };

        }

        public async Task<decimal> GetMonthlyPay(PaymentStructure Pstructure, int Grade, int Step, Extras ExtraPayOptions = null)
        {
            //get applicable payment stuctures
            PaymentStructure OperationalPStructure = (await GetPaymentStructure())
                                                            .Where(w => w.Id == Pstructure.Id)
                                                            .FirstOrDefault();

            var grd = OperationalPStructure.Grades.Where(g => g.Number == Grade).FirstOrDefault();
            var stp = grd.Steps.Where(s => s.Number == Step).FirstOrDefault();

            decimal amnt = 0;

            if (Pstructure.CategoryId == 1)
            {
                amnt = stp.NonTeachingAmount;
            }
            else
            {
                amnt = stp.TeachingAmount;
            }
            

            if (ExtraPayOptions != null)
            {
                amnt += ExtraPayOptions.IsProffesional ? stp.ProffesionalAllowance : 0;
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

            return prom==null ? new PromotionArr() : prom;

        }
    }
}

