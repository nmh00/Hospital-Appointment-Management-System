using AppointmentBookingWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace AppointmentBookingWeb.Controllers
{
    public class DepartmentController : Controller
    {
        Uri baseAddress = new Uri("https://p3vsg5cb-7077.asse.devtunnels.ms/api");
        private readonly HttpClient _client;

        public DepartmentController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }

        // GET LIST //
        [HttpGet]
        public IActionResult Index()
        {
            List<DepartmentViewModel> departmentList = new List<DepartmentViewModel>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Departments/GetDepartments").Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                departmentList = JsonConvert.DeserializeObject<List<DepartmentViewModel>>(data);
            }
            return View(departmentList);
        }

        // CREATE / ADD //
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(DepartmentViewModel model)
        {
            try
            {
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "/Departments/PostDepartment", content).Result;

                if (response.IsSuccessStatusCode)
                {
                }
                TempData["successMessage"] = "Department successfully added..";
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
                DepartmentViewModel department = new DepartmentViewModel();
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Departments/GetDepartment/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    department = JsonConvert.DeserializeObject<DepartmentViewModel>(data);
                }
                return View(department);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }

        [HttpPost]
        public IActionResult Edit(DepartmentViewModel model)
        {
            try
            {
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PutAsync(_client.BaseAddress + "/Departments/PutDepartment/", content).Result;

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                TempData["successMessage"] = "Department details updated.";
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
                DepartmentViewModel department = new DepartmentViewModel();
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Departments/GetDepartment/" + id).Result;

                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    department = JsonConvert.DeserializeObject<DepartmentViewModel>(data);
                }
                return View(department);
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
                HttpResponseMessage response = _client.DeleteAsync(_client.BaseAddress + "/Departments/DeleteDepartment/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Department deleted.";
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
