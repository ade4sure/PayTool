using Calculator.Models;
using Calculator.Models.ViewModels;
using Calculator.Services;
using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Calculator.Controllers
{
    public partial class HomeController : Controller
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

            var structure = await _payService.GetMonthlyPayDetails(payView);

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
            IList<Stafcsv> realUploads = await GetNorminalRollfromCSV(viewModel);

            IList<SalaryArrearsResponse> Response = await ProcessNorminalRoll(viewModel, realUploads);

            ViewData["response"] = Response;

            ViewData["CategoryId"] = new SelectList(await _payService.GetPayCategory(), "Id", "Name");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PromoArrears([FromBody] PromotionArr vm)
        {
            vm.Ranges.OrderBy(o => o.StartMonth);
            
            foreach (var range in vm.Ranges)
            {
                //range.Paid.Amount = (await GetMonthlyPay(vm.CategoryId, range.StartMonth, range.Paid.Grade, range.Paid.Step, vm.Extras)).OpStructureAmount;
                //range.Expected.Amount = (await GetMonthlyPay(vm.CategoryId, range.StartMonth, range.Expected.Grade, range.Expected.Step, vm.Extras)).OpStructureAmount;
                // range.Paid.Amount = (await _payService.GetMonthlyPayDetails(new PayView { CategoryId = vm.CategoryId, Dato = range.StartMonth, Grade = range.Paid.Grade, Step = range.Paid.Step }, vm.Extras)).OpStructureAmount;
                range.Paid.Amount = (await _payService.GetMonthlyPayDetails(new PayView { CategoryId = vm.CategoryId, Dato = range.StartMonth, Grade = range.Paid.Grade, Step = range.Paid.Step }, vm.Extras)).OpStructureAmount;
                //range.Expected.Amount = (await _payService.GetMonthlyPayDetails(new PayView { CategoryId = vm.CategoryId, Dato = range.StartMonth, Grade = range.Expected.Grade, Step = range.Expected.Step }, vm.Extras)).ApprovedStructureAmount;
                range.Expected.Amount = (await _payService.GetMonthlyPayDetails(new PayView { CategoryId = vm.CategoryId, Dato = range.StartMonth, Grade = range.Expected.Grade, Step = range.Expected.Step }, vm.Extras)).ApprovedStructureAmount;

               
            }

            vm.ExtraJson = JsonSerializer.Serialize(vm.Extras);
            vm.RangeJson = JsonSerializer.Serialize(vm.Ranges);

            await _payService.SavePromoPayload(vm);

            return Ok(vm);
        }

        [Route("/Home/GetStaffPromo")]
        [HttpGet("{staffNumber}")]
        public async Task<IActionResult> GetStaffPromo(string staffNumber)
        {
            if (string.IsNullOrEmpty(staffNumber))
            {
                return BadRequest("Invalid number");
            }

            var promo = await _payService.LoadPromoPayload(staffNumber.Trim());
            promo.Extras = JsonSerializer.Deserialize<Extras>(promo.ExtraJson);
            promo.Ranges = JsonSerializer.Deserialize<List<MonthRange>>(promo.RangeJson);

            return Ok(promo);

        }

        private async Task<IList<SalaryArrearsResponse>> ProcessNorminalRoll(SalaryArrearsView viewModel, IList<Stafcsv> realUploads)
        {
            IList<SalaryArrearsResponse> Response = new List<SalaryArrearsResponse>();


            foreach (var stf in realUploads)
            {
                var dat = viewModel.Month;
                decimal dif = 0;
                var structure = new GetStructureresponse();

                //structure = await GetMonthlyPay(viewModel.CategoryId, dat, stf.Grade, stf.Step);
                structure = (await _payService.GetMonthlyPayDetails(new PayView { CategoryId = viewModel.CategoryId, Dato = dat, Grade = stf.Grade, Step = stf.Step }, null));
                dif += structure.PayDiffrence;

                Response.Add(new SalaryArrearsResponse { StaffNumber = stf.StaffNumber, DifferenceSum = structure.PayDiffrence, Month = viewModel.Month, AnalysisResponse = structure, Grade = stf.Grade, Step = stf.Step });

            }

            return Response;
        }

        private static async Task<IList<Stafcsv>> GetNorminalRollfromCSV(SalaryArrearsView viewModel)
        {
            var MemStream = new MemoryStream();

            await viewModel.File.CopyToAsync(MemStream);

            MemStream.Position = 0;
            IList<Stafcsv> realUploads = new List<Stafcsv>();


            using (var reader = new StreamReader(MemStream))
            {
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    // csv.Configuration.RegisterClassMap<LoansImportMap>();
                    var csvUpload = csv.GetRecords<Stafcsv>();
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
        private class Stafcsv
        {
            public string StaffNumber { get; set; }
            public int Step { get; set; }
            public int Grade { get; set; }
        }

        public static int smallerNum(string n1, string n2)
        {
            var str = "ade";
            var vowels = "aeiou".ToCharArray();

            var inter = str.ToCharArray().Where(x => str.Contains(x));
            //return str.Count(c => "aeiou".Contains(c));
            //return str.Count(c => "aeiou".Contains(c));


           return inter.Count();

        //    Queue<String> customers = new Queue<String>();

        //    customers.Enqueue("Mark");
        //    customers.Enqueue("Pooja");
        //    customers.Enqueue("Warren");
        //    customers.
        //
        }

    }
}
