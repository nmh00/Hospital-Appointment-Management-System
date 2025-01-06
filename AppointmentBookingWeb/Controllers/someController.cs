using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

public class SomeController : Controller
{
    private readonly HttpClient _httpClient;

    public SomeController()
    {
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri("https://p3vsg5cb-7077.asse.devtunnels.ms/api"); // Adjust the URL as necessary
    }

    public async Task<IActionResult> SomeAction()
    {
        var token = HttpContext.Session.GetString("JWToken");
        if (!string.IsNullOrEmpty(token))
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        // Make your authorized API request
        var response = await _httpClient.GetAsync("some/endpoint");
        if (response.IsSuccessStatusCode)
        {
            var data = await response.Content.ReadAsStringAsync();
            // Process the data as needed
        }
        else
        {
            // Handle errors
        }

        return View();
    }
}
