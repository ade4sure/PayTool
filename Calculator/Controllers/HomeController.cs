using Calculator.Models;
using Calculator.Models.ViewModels;
using Calculator.Services;
using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Calculator.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPayService _payService;

        public HomeController(ILogger<HomeController> logger, IPayService payService)
        {
            _logger = logger;
            _payService = payService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Pay()
        {
            var res = await _payService.GetPayCategory();

            ViewData["CategoryId"] = new SelectList(res, "Id", "Name");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Pay(PayView payView)
        {
            // var dat = DateTime.Now.AddMonths(Pad);
            //var dat = DateTime.Now;
            payView.Dato.AddHours(5);
            var dat = payView.Dato;

            var structure = await _payService.GetMonthlyPayDetails( payView);

            var res = await _payService.GetPayCategory();

            ViewData["CategoryId"] = new SelectList(res, "Id", "Name");

            ViewData["response"] = structure;

            return View();
        }

        public async Task<IActionResult> SalArrears()
        {
            var res = await _payService.GetPayCategory();

            ViewData["CategoryId"] = new SelectList(res, "Id", "Name");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SalArrears(SalaryArrearsView viewModel)
        {
            IList<Staf> realUploads = await GetNorminalRollfromCSV(viewModel);

            IList<SalaryArrearsResponse> Response = await ProcessNorminalRoll(viewModel, realUploads);         
           
            ViewData["response"] = Response;

            ViewData["CategoryId"] = new SelectList(await _payService.GetPayCategory(), "Id", "Name");

            return View();
        }

         [HttpPost]
        public async Task<IActionResult> PromoArrearsAsync([FromBody] PromotionArrManyView viewModel)
        {
            foreach (var range in viewModel.ranges)
            {
                range.Paid.Amount = (await GetMonthlyPayAsync(viewModel.CategoryId, range.StartMonth, range.Paid.Grade, range.Paid.Step)).OpStructureAmount;
                range.Expected.Amount = (await GetMonthlyPayAsync(viewModel.CategoryId, range.StartMonth, range.Expected.Grade, range.Expected.Step)).OpStructureAmount;
            }
            
            return Ok(viewModel);
        }

        private async Task<IList<SalaryArrearsResponse>> ProcessNorminalRoll(SalaryArrearsView viewModel, IList<Staf> realUploads)
        {
            IList<SalaryArrearsResponse> Response = new List<SalaryArrearsResponse>();


            foreach (var stf in realUploads)
            {
                var dat = viewModel.Month;
                decimal dif = 0;
                var structure = new GetStructureresponse();

                structure = await GetMonthlyPayAsync(viewModel.CategoryId, dat, stf.Grade, stf.Step);
                dif += structure.PayDiffrence;

                Response.Add(new SalaryArrearsResponse { StaffNumber = stf.StaffNumber, DifferenceSum = structure.PayDiffrence, Month = viewModel.Month, AnalysisResponse = structure, Grade = stf.Grade, Step = stf.Step });

            }

            return Response;
        }

        private static async Task<IList<Staf>> GetNorminalRollfromCSV(SalaryArrearsView viewModel)
        {
            var MemStream = new MemoryStream();

            await viewModel.File.CopyToAsync(MemStream);

            MemStream.Position = 0;
            IList<Staf> realUploads = new List<Staf>();


            using (var reader = new StreamReader(MemStream))
            {
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    // csv.Configuration.RegisterClassMap<LoansImportMap>();
                    var csvUpload = csv.GetRecords<Staf>();
                    foreach (var upl in csvUpload)
                    {
                        //staf.AmountAppliedFor = lon.AmountApproved;
                        realUploads.Add(upl);
                    }
                }
            }

            return realUploads;
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
         
        [ResponseCache(Duration =  30, Location = ResponseCacheLocation.Any, NoStore = true)]
        public async Task<GetStructureresponse> GetMonthlyPayAsync(int CategoryId, DateTime Dato, int Grade ,int Step )
        {
            Debug.WriteLine("Grade = " + Grade + " Step = " + Step + " Dat = " + Dato + " CategoryId = " + CategoryId);

            return await _payService.GetMonthlyPayDetails(new PayView {  CategoryId = CategoryId, Dato= Dato, Grade = Grade, Step= Step});

        }

        private class Staf
        {
            public string StaffNumber { get; set; }
            public int Step { get; set; }           
            public int Grade { get; set; }
        }
    }
}
