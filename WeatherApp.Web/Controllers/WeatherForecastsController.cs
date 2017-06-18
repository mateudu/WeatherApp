using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WeatherApp.Core.Exceptions;
using WeatherApp.Core.Models;
using WeatherApp.Core.Repositories;
using WeatherApp.Core.Services;
using WeatherApp.Infrastructure;

namespace WeatherApp.Web.Controllers
{
    [RoutePrefix("WeatherForecasts")]
    public class WeatherForecastsController : Controller
    {
        private readonly IWeatherForecastRepository _repo;
        private readonly IWeatherService _weatherSvc;

        public WeatherForecastsController(IWeatherForecastRepository repo,
            IWeatherService weatherSvc)
        {
            _repo = repo;
            _weatherSvc = weatherSvc;
        }

        // GET: WeatherForecasts
        [HttpGet, Route("")]
        public async Task<ActionResult> Index()
        {
            var forecasts = await _repo.GetAllAsync();
            return View(forecasts.ToList());
        }

        // GET: WeatherForecasts/Create
        [HttpGet, Route("Create")]
        [Authorize(Roles = Config.AdminRole)]
        public ActionResult Create()
        {
            return View();
        }

        // POST: WeatherForecasts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, Route("Create")]
        [Authorize(Roles = Config.AdminRole)]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,City,TemperatureCelsius,Description")] WeatherForecast weatherForecast)
        {
            if (ModelState.IsValid)
            {
                await _repo.AddAsync(weatherForecast);
                return RedirectToAction("Index");
            }

            return View(weatherForecast);
        }

        // GET: WeatherForecasts/Update/5
        [HttpGet, Route("Update")]
        [Authorize(Roles = Config.AdminRole)]
        public async Task<ActionResult> Update(int id)
        {
            try
            {
                var currentForecast = await _repo.GetAsync(id);
                var newForecast = await _weatherSvc.GetWeatherForecastAsync(currentForecast.City);

                currentForecast.Description = newForecast.Description;
                currentForecast.TemperatureCelsius = newForecast.TemperatureCelsius;

                await _repo.UpdateAsync(currentForecast);
                return RedirectToAction("Index");
            }
            catch (WeatherServiceException exc)
            {
                return new HttpNotFoundResult(exc.Message);
            }
        }

        // GET: WeatherForecasts/Delete/5
        [HttpGet, Route("Delete/{id}"), ActionName("Delete")]
        [Authorize(Roles = Config.AdminRole)]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WeatherForecast weatherForecast = await _repo.GetAsync(id.Value);
            if (weatherForecast == null)
            {
                return HttpNotFound();
            }
            return View(weatherForecast);
        }

        // POST: WeatherForecasts/Delete/5
        [HttpPost, Route("Delete/{id}"), ActionName("Delete")]
        [Authorize(Roles = Config.AdminRole)]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await _repo.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}
