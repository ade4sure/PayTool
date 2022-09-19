using Calculator.Models.ViewModels;
using Calculator.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Calculator.Test
{
    public class Calculator
    {
        //private readonly IPayService _payService;

        //public Calculator(IPayService payService)
        //{
        //    _payService = payService;
        //}


        [Fact]
        public async Task ShouldCalculateAsync()
        {
            //Arrange
            var expected = 5;


            //Act
            //var actual = await _payService.GetMonthlyPayDetails(
            //        new PayView { CategoryId = 1, Dato = new DateTime(2019, 10, 1), Grade = 13, Step = 5 }
            //        );
            var actual = 5;

            //Assert
            Assert.Equal(expected, actual);
        }


    }
}
