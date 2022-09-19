using Calculator.Data;
using Calculator.Models.ViewModels;
using Calculator.Services;

namespace MSTest
{
    [TestClass]
    public class UnitTest1
    {
        

        [TestMethod]
        public  async void TrialCase()
        {
            //IPayService _payService = new PayService();
            var act = 5;
            var expected = 5;
            //var res = await PayService.GetMonthlyPayDetails(new PayView());

           //var mid =   _payService.LoadPromoPayload("1359").Result;
            Assert.AreEqual(act, expected);

        }
    }
}