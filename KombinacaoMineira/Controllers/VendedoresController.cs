using KombinacaoMineira.Models;
using KombinacaoMineira.Models.ViewModel;
using KombinacaoMineira.Services;
using KombinacaoMineira.Services.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace KombinacaoMineira.Controllers
{
    public class VendedoresController : Controller
    {
        private readonly VendedorService _vendedorService;
        private readonly DepartamentoService _departamentoService;
        public VendedoresController(VendedorService vendedorService , DepartamentoService departamentoService)
        {
            _vendedorService = vendedorService;
            _departamentoService = departamentoService; 
        }

        public async Task<IActionResult> Index()
        {
            var lista = await _vendedorService.EncontrarTodosAsync();
            return View(lista);
        }

        public async Task<IActionResult> Create()
        {
            var departamentos = await _departamentoService.EncontrarTodosAsync();
            var viewModel = new VendedorFormViewModel { Departamentos = departamentos };
            return View(viewModel);
        }

        public async Task<IActionResult> Delete(int? id)
        {
           if(id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "ID não fornecido" });
            }
            var vendedor = await _vendedorService.EncontrePorIdAsync(id.Value);
            if(vendedor==null)
            {
                return RedirectToAction(nameof(Error), new { message = "ID não encontrado" });

            }

            return View(vendedor);
        }



        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "ID não fornecido" });

            }
            var vendedor = await _vendedorService.EncontrePorIdAsync(id.Value);
            if (vendedor == null)
            {
                return RedirectToAction(nameof(Error), new { message = "ID não encontrado" });

            }

            return View(vendedor);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if(id ==null)
            {
                return RedirectToAction(nameof(Error), new { message = "ID não fornecido" });

            }
            var obj = await _vendedorService.EncontrePorIdAsync(id.Value);
            if(obj ==null)
            {
                return RedirectToAction(nameof(Error), new { message = "ID não encontrado" });

            }

            List<Departamento> departamentos = await _departamentoService.EncontrarTodosAsync();
            VendedorFormViewModel viewModel = new VendedorFormViewModel { Vendedor = obj, Departamentos = departamentos };
            
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Vendedor vendedor)
        {
            if (!ModelState.IsValid)
            {
                var departamentos = await _departamentoService.EncontrarTodosAsync();
                var viewModel = new VendedorFormViewModel { Vendedor = vendedor, Departamentos = departamentos };
                  return View(viewModel);
            }
            await _vendedorService.InserirAsync(vendedor);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _vendedorService.RemoverAsync(id);
                return RedirectToAction(nameof(Index));
            }
           catch(ExcecaoDeIntegridade e)
            {
                return RedirectToAction(nameof(Error), new { message = "Vendedor possui vendas cadastradas. Não é possível deletar fornecedor" });
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(int id, Vendedor vendedor)
        {
            if (!ModelState.IsValid)
            {
                var departamentos = await _departamentoService.EncontrarTodosAsync();
                var viewModel = new VendedorFormViewModel { Vendedor = vendedor, Departamentos = departamentos };
                return View(viewModel);
            }
            if (id != vendedor.Id)
            {
                return BadRequest();
            }

            try
            {
                await _vendedorService.AtualizarAsync(vendedor);
                return RedirectToAction(nameof(Index));
            }
            catch(ApplicationException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });

            }
          
        }

        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };
            return View(viewModel);
        }

    }
}
