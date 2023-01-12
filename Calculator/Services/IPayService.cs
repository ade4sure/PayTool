using Calculator.Models.AppModels;
using Calculator.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Calculator.Services
{
    public partial interface IPayService
    {
        Task<GetStructureresponse> GetMonthlyPayDetails(PayView payView, Extras ExtraPayOptions = null);
        Task<List<PayCategory>> GetPayCategory();       
        Task<bool> SavePromoPayload(PromotionArr PromoPayload);
        Task<PromotionArr> LoadPromoPayload(string StaffNumber);
        Task<List<Union>> GetUnion();
        Task<List<string>> LoadUnionMembers(int UnionId);
        void ToggleSavePayload();
        bool GetSavePayload();

    }
}

