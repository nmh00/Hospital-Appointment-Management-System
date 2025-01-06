using AppointmentBookingWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace AppointmentBookingWeb.Controllers
{
    public class DoctorController : Controller
    {
        Uri baseAddress = new Uri("https://p3vsg5cb-7077.asse.devtunnels.ms/api");
        private readonly HttpClient _client;

        public DoctorController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }

        // GET LIST //
        [HttpGet]
        public IActionResult Index()
        {
            List<DoctorViewModel> doctorList = new List<DoctorViewModel>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Doctors/GetDoctors").Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                doctorList = JsonConvert.DeserializeObject<List<DoctorViewModel>>(data);
            }
            return View(doctorList);
        }

        // CREATE / ADD //
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(DoctorViewModel model)
        {
            try
            {
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "/Doctors/PostDoctor", content).Result;

                if (response.IsSuccessStatusCode)
                {
                }
                TempData["successMessage"] = "Doctor successfully added.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }

        // EDIT //

        [HttpGet]
        public IActionResult Edit(int id)
        {
            try
            {
                DoctorViewModel doctor = new DoctorViewModel();
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Doctors/GetDoctor/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    doctor = JsonConvert.DeserializeObject<DoctorViewModel>(data);
                }
                return View(doctor);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }

        [HttpPost]
        public IActionResult Edit(DoctorViewModel model)
        {
            try
            {
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PutAsync(_client.BaseAddress + "/Doctors/PutDoctor/", content).Result;

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                TempData["successMessage"] = "Doctor details updated.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View(model);
            }
        }


        // DELETE //

        [HttpGet]
        public IActionResult Delete(int id)
        {
            try
            {
                DoctorViewModel doctor = new DoctorViewModel();
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Doctors/GetDoctor/" + id).Result;

                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    doctor = JsonConvert.DeserializeObject<DoctorViewModel>(data);
                }
                return View(doctor);
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
                HttpResponseMessage response = _client.DeleteAsync(_client.BaseAddress + "/Doctors/DeleteDoctor/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Doctor deleted.";
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
