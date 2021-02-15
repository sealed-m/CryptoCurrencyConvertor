using Application.Contract.Requests;
using Application.CQRS.Commands;
using Application.CQRS.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace WebUI.Controllers
{
    public class CryptoCurrencyController : Controller
    {
        private readonly IMediator _mediator;

        public CryptoCurrencyController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var result = await _mediator.Send(new GetAllCryptoCurrencies());
                return View(result);
            }
            catch
            {
                ViewBag.Error = "Internal Server Error";
                return View();
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CryptoCurrencyAddRequest request)
        {
            try
            {
                var command = new AddCryptoCurrencyCommand(request);
                var res = await _mediator.Send(command);

                if (res.HasError)
                {
                    ViewBag.Errors = res.Errors;
                    return View();
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new GetCryptoCurrencyRawDataQuery(id));
            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                var command = new RemoveCryptoCurrencyCommand(id);
                var res = await _mediator.Send(command);

                if (res.HasError)
                {
                    ViewBag.Errors = res.Errors;
                    return View();
                }

                return RedirectToAction(nameof(Index));
            }
            catch 
            {
                return View();
            }
        }
    }
}
