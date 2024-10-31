using DCSS.CW1.FRONTEND._14610.Models; // Assuming you have a Food model here
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DCSS.CW1.FRONTEND._14610.Controllers
{
    public class FoodController : Controller
    {
        private readonly string baseUrl = "https://localhost:44397/api/Food";

        // GET: Food
        public async Task<ActionResult> Index()
        {
            List<Food> foodList = new List<Food>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("");

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    foodList = JsonConvert.DeserializeObject<List<Food>>(responseContent);
                }
                else
                {
                    ViewBag.ErrorMessage = "Failed to retrieve food items.";
                }
            }

            return View(foodList);
        }

        // GET: Food/Details/5
        public async Task<ActionResult> Details(int id)
        {
            Food food = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync($"/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    food = JsonConvert.DeserializeObject<Food>(responseContent);
                }
                else
                {
                    return HttpNotFound("Food item not found.");
                }
            }

            return View(food);
        }

        // GET: Food/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Food/Create
        [HttpPost]
        public async Task<ActionResult> Create(Food food)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var content = new StringContent(JsonConvert.SerializeObject(food), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.ErrorMessage = "Failed to create the food item.";
                    return View(food);
                }
            }
        }

        // GET: Food/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            Food food = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync($"/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    food = JsonConvert.DeserializeObject<Food>(responseContent);
                }
                else
                {
                    return HttpNotFound("Food item not found.");
                }
            }

            return View(food);
        }

        // POST: Food/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(int id, Food food)
        {
            if (id != food.Id)
            {
                return new HttpStatusCodeResult(400, "ID mismatch between route and body.");
            }

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var content = new StringContent(JsonConvert.SerializeObject(food), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync($"/{id}", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.ErrorMessage = "Failed to update the food item.";
                    return View(food);
                }
            }
        }

        // GET: Food/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            Food food = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync($"/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    food = JsonConvert.DeserializeObject<Food>(responseContent);
                }
                else
                {
                    return HttpNotFound("Food item not found.");
                }
            }

            return View(food);
        }

        // POST: Food/Delete/5
        [HttpPost]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.DeleteAsync($"/{id}");

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.ErrorMessage = "Failed to delete the food item.";
                    return RedirectToAction("Delete", new { id = id });
                }
            }
        }
    }
}
