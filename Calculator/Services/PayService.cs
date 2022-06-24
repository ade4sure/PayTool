using Calculator.Data;
using Calculator.Models.AppModels;
using Calculator.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Calculator.Services
{
    public interface IPayService
    {

        Task<GetStructureresponse> GetMonthlyPayDetails(PayView payView);
        Task<List<PayCategory>> GetPayCategory();
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

        public async Task<GetStructureresponse> GetMonthlyPayDetails(PayView payView)
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

            var OpertionalPay = await GetMonthlyPayAsync(OperationalPStructure, payView.Grade, payView.Step);
            var ApprovedPay = await GetMonthlyPayAsync(ApprovedPStructure, payView.Grade, payView.Step);

            return new GetStructureresponse()
            {
                ApprovedStructure = ApprovedPStructure.Name,
                ApprovedStructureAmount = ApprovedPay,
                OpStructure = OperationalPStructure.Name,
                OpStructureAmount = OpertionalPay
            };

        }

        public async Task<decimal> GetMonthlyPayAsync(PaymentStructure Pstructure, int Grade, int Step)
        {
            //get applicable payment stuctures
            PaymentStructure OperationalPStructure = (await GetPaymentStructure())
                                                            .Where(w => w.Id == Pstructure.Id)
                                                            .FirstOrDefault();

            var grd = OperationalPStructure.Grades.Where(g => g.Number == Grade).FirstOrDefault();
            var stp = grd.Steps.Where(s => s.Number == Step).FirstOrDefault();

            var amnt = stp.Amount;

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
    }
}

