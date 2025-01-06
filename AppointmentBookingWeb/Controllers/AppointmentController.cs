using AppointmentBookingWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace AppointmentBookingWeb.Controllers
{
    public class AppointmentController : Controller
    {
        Uri baseAddress = new Uri("https://p3vsg5cb-7077.asse.devtunnels.ms/api");
        private readonly HttpClient _client;

        public AppointmentController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }

        // GET LIST //
        [HttpGet]
        public IActionResult Index()
        {
            List<AppointmentViewModel> appointmentList = new List<AppointmentViewModel>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/appointments/GetAppointments").Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                appointmentList = JsonConvert.DeserializeObject<List<AppointmentViewModel>>(data);
            }
            return View(appointmentList);
        }


        // CREATE / ADD //
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(AppointmentViewModel model)
        {
            try
            {
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "/Appointments/PostAppointment", content).Result;

                if (response.IsSuccessStatusCode)
                {

                }
                TempData["successMessage"] = "Appointemnet successfully submitted.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
            return View();
        }


        // EDIT //

        [HttpGet]
        public IActionResult Edit(int id)
        {
            try
            {
                AppointmentViewModel appointment = new AppointmentViewModel();
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Appointments/GetAppointment/" + id).Result;

                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    appointment = JsonConvert.DeserializeObject<AppointmentViewModel>(data);
                }
                return View(appointment);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }

        [HttpPost]
        public IActionResult Edit(AppointmentViewModel model)
        {
            try
            {
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PutAsync(_client.BaseAddress + "Appointments/PutAppointment/", content).Result;
                if (response.IsSuccessStatusCode)
                {
                }
                TempData["successMessage"] = "Appointment details updated.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }

        }

        // DELETE //

        [HttpGet]
        public IActionResult Delete(int id)
        {
            try
            {
                AppointmentViewModel appointment = new AppointmentViewModel();
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Appointments/GetAppointment/" + id).Result;

                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    appointment = JsonConvert.DeserializeObject<AppointmentViewModel>(data);
                }
                return View(appointment);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                HttpResponseMessage response = _client.DeleteAsync(_client.BaseAddress + "/Appointments/DeleteAppointment/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Appointment details deleted.";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
            return View();
        }

    }
}
