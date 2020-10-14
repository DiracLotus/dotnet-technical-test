using dotnet_technical_test.Helper;
using dotnet_technical_test.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace dotnet_technical_test.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IConfiguration config;

        #region Bindings
        public Currency Money { get; set; }
        public SelectList Currencies { get; set; }

        [BindProperty]
        [Required]
        public string ConvertFromCurrency { get; set; }
        [BindProperty]
        [Required]
        public string ConvertToCurrency { get; set; }

        // Would have liked to actually implement this validation but ran out of time :(
        [RegularExpression(@"^\d+\.\d{6}$")]
        [Range(0, 9999999999.99)]
        [BindProperty]
        public decimal MoneyToConvert { get; set; }
        public decimal ConvertedMoney { get; set; }

        #endregion

        public IndexModel(ILogger<IndexModel> logger, IConfiguration config)
        {
            _logger = logger;
            this.config = config;
        }

        public async Task OnGetAsync()
        {
            await BuildSelectList("USD");
        }

        public async Task OnPostAsync()
        { 
            decimal rate;

            await BuildSelectList(ConvertFromCurrency);

            if (Money.Rates.TryGetValue(ConvertToCurrency, out rate))
            {
                ConvertedMoney = MoneyToConvert * rate;
            }
        }

        /// <summary>
        /// Call the currency converter for a list of all currencies and build a select list.
        /// </summary>
        private async Task BuildSelectList(string endpoint)
        {
            var currencyConverter = new CurrencyHelper();
            Money = await currencyConverter.GetCurrencyAsync(this.config["ApiUrl"].ToString(), endpoint);
            Currencies = new SelectList(Money.Rates, "Key", "Key");
        }
    }
}
