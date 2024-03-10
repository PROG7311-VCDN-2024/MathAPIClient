using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq.Expressions;
using Firebase.Auth;
using MathAPIClient.Models;
using Newtonsoft.Json;
using System.Text;

namespace MathAPIClient.Controllers
{
    public class MathController : Controller
    {

        private static HttpClient httpClient = new()
        {
            BaseAddress = new Uri("https://localhost:7167"),
        };

        public IActionResult Calculate()
        {
            var token = HttpContext.Session.GetString("currentUser");

            if (token == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            List<SelectListItem> operations = new List<SelectListItem> {
            new SelectListItem { Value = "1", Text = "+" },
            new SelectListItem { Value = "2", Text = "-" },
            new SelectListItem { Value = "3", Text = "*" },
            new SelectListItem { Value = "4", Text = "/" },

            };

            ViewBag.Operations = operations;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Calculate(decimal? FirstNumber, decimal? SecondNumber,int Operation)
        {
            var token = HttpContext.Session.GetString("currentUser");

            if (token == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            decimal? Result = 0;
            MathCalculation mathCalculation;

            try
            {
                mathCalculation = MathCalculation.Create(FirstNumber, SecondNumber, Operation, Result, token);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
                throw;
            }
            
            StringContent jsonContent = new(JsonConvert.SerializeObject(mathCalculation), Encoding.UTF8,"application/json"); 
            
            HttpResponseMessage response = await httpClient.PostAsync("api/Math/PostCalculate", jsonContent);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                MathCalculation? deserialisedResponse = JsonConvert.DeserializeObject<MathCalculation>(jsonResponse);
                ViewBag.Result = deserialisedResponse.Result;
                return View();
            } else
            {
                ViewBag.Result = "An error has occurred";
                return View();
            }
        }

        public async Task<IActionResult> History()
        {
            var token = HttpContext.Session.GetString("currentUser");

            if (token == null)
            {
                return RedirectToAction("Login", "Auth");
            }
            
            HttpResponseMessage response = await httpClient.GetAsync("/api/Math/GetHistory?token=" + token);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                List<MathCalculation>? deserialisedResponse = JsonConvert.DeserializeObject<List<MathCalculation>>(jsonResponse);
                if (deserialisedResponse.Count == 0)
                {
                    ViewBag.HistoryMessage = "No history exists";
                }
                return View(deserialisedResponse);
            }  else
            {
                ViewBag.HistoryMessage = "No history to show";
                return View();
            }            
        }

        public async Task<IActionResult> Clear()
        {
            var token = HttpContext.Session.GetString("currentUser");

            if (token == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            HttpResponseMessage response = await httpClient.DeleteAsync("/api/Math/DeleteHistory?token=" + token);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
            }
            return RedirectToAction("History");
        }
    }
}
