using KombinacaoMineira.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KombinacaoMineira.Controllers
{
    public class RegistroVendasController : Controller
    {
        private readonly RegistroVendasService _registroVendasService;

        public RegistroVendasController(RegistroVendasService registroVendasService)
        {
            _registroVendasService = registroVendasService;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> BuscaSimples(DateTime? minDate, DateTime? maxDate)
        {
            if(!minDate.HasValue)
            {
                minDate = new DateTime(DateTime.Now.Year, 1, 1);
            }

            if(!maxDate.HasValue)
            {
                maxDate = DateTime.Now;
            }

            ViewData["MinDate"] = minDate.Value.ToString("yyyy-MM-dd");
            ViewData["MaxDate"] = maxDate.Value.ToString("yyyy-MM-dd");
            var result = await _registroVendasService.EncontrePorDataAsync(minDate, maxDate);
            return View(result);
        }

        public async Task<IActionResult> BuscaGrupo(DateTime? minDate, DateTime? maxDate)
        {
            if (!minDate.HasValue)
            {
                minDate = new DateTime(DateTime.Now.Year, 1, 1);
            }

            if (!maxDate.HasValue)
            {
                maxDate = DateTime.Now;
            }

            ViewData["MinDate"] = minDate.Value.ToString("yyyy-MM-dd");
            ViewData["MaxDate"] = maxDate.Value.ToString("yyyy-MM-dd");
            var result = await _registroVendasService.EncontrePorDataGroupAsync(minDate, maxDate);
            return View(result);
        }

    }
}
