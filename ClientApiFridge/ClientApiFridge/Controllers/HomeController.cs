//using AspNetCore;
using ClientApiFridge.Models;
//using Entities.DataTransferObjects;
using Entities.Models;
using fridge.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
//using Unipluss.Sign.ExternalContract.Entities;

namespace ClientApiFridge.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetFridges()
        {
            var fridges = new List<FridgesDto>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7081/api/fridges"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    fridges = JsonConvert.DeserializeObject<List<FridgesDto>>(apiResponse);
                }
            }
            return View(fridges);
        }

        public ViewResult GetFridgeById() => View();

        [HttpPost]
        public async Task<IActionResult> GetFridgeById(Guid? Id)
        {
            var fridge = new Models.Fridges();
            if (Id == null)
            {
                return View();
            }

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7081/api/fridges/" + Id))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        fridge = JsonConvert.DeserializeObject<Models.Fridges>(apiResponse);
                    }
                    else
                    {
                        ViewBag.StatusCode = response.StatusCode;
                    }
                }
            }
            if (ModelState.IsValid)
                return View(fridge);

            return View();
        }

        public ViewResult CreateFridge() => View();

        [HttpPost]
        public async Task<IActionResult> CreateFridge(FridgesForCreationDto fridgeForCreation)
        {
            var fridge = new FridgesDto();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(fridgeForCreation), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("https://localhost:7081/api/fridges", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    fridge = JsonConvert.DeserializeObject<FridgesDto>(apiResponse);
                }
            }
            if (ModelState.IsValid)
                return View(fridge);

            return View();
        }

        public async Task<IActionResult> UpdateFridge(Guid id)
        {
            var fridgeForUpdate = new FridgesDto();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7081/api/fridges/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    fridgeForUpdate = JsonConvert.DeserializeObject<FridgesDto>(apiResponse);
                }
            }
            if (ModelState.IsValid)
                return View(fridgeForUpdate);

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateFridge(FridgesDto fridgeForUpdate)
        {
            var updatedFridge = new FridgesDto();
            if (!ModelState.IsValid)
                return View();

            using (var httpClient = new HttpClient())
            {
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(fridgeForUpdate.Name.ToString()), "Name");
                content.Add(new StringContent(fridgeForUpdate.Owner_Name.ToString()), "Owner_Name");
                content.Add(new StringContent(fridgeForUpdate.ModelId.ToString()), "ModelId");

                using (var response = await httpClient.PutAsync(("https://localhost:7081/api/fridges/" + fridgeForUpdate.Id), content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    ViewBag.Result = "Success";
                    updatedFridge = JsonConvert.DeserializeObject<FridgesDto>(apiResponse);
                }
            }
            if (ModelState.IsValid)
                return View(updatedFridge);

            return View();
        }

        public async Task<IActionResult> GetProducts()
        {
            var products = new List<ProductsDto>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7081/api/products"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    products = JsonConvert.DeserializeObject<List<ProductsDto>>(apiResponse);
                }
            }

            return View(products);
        }

        public async Task<IActionResult> UpdateProduct(Guid id)
        {
            var productForUpdate = new ProductsDto();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7081/api/products/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    productForUpdate = JsonConvert.DeserializeObject<ProductsDto>(apiResponse);
                }
            }
            if (ModelState.IsValid)
                return View(productForUpdate);

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProduct(ProductsDto productForUpdate)
        {
            var updatedProduct = new ProductsDto();
            using (var httpClient = new HttpClient())
            {
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(productForUpdate.Name.ToString()), "Name");
                content.Add(new StringContent(productForUpdate.Default_Quantity.ToString()), "Default_Quantity");

                using (var response = await httpClient.PutAsync(("https://localhost:7081/api/products/" + productForUpdate.Id), content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    ViewBag.Result = "Success";
                    updatedProduct = JsonConvert.DeserializeObject<ProductsDto>(apiResponse);
                }
            }

            if (ModelState.IsValid)
                return View(updatedProduct);

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> DeleteFridge(FridgesDto fridge)
        {
            var fridgeId = fridge.Id;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync("https://localhost:7081/api/fridges/" + fridgeId))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
            }

            return View();
        }

        public async Task<IActionResult> GetFridgeProducts(Guid Id)
        {
            var fridgeProducts = new List<FridgeProductsDto>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7081/api/fridges/" + Id + "/fridgeProducts"))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        fridgeProducts = JsonConvert.DeserializeObject<List<FridgeProductsDto>>(apiResponse);
                    }
                    else
                    {
                        ViewBag.StatusCode = response.StatusCode;
                    }
                }
            }

            var fridgeProductsForSp = new FridgeProductSpDto { FridgeProducts = fridgeProducts, FridgeId = Id };

            return View(fridgeProductsForSp);
        }

        public async Task<IActionResult> CreateNewFridge()
        {

            var fridgeModelAndProducts = new FridgeModelsAndProductsObject();
            var products = new List<ProductsDto>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7081/api/products"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    products = JsonConvert.DeserializeObject<List<ProductsDto>>(apiResponse);
                }
            }

            var models = new List<FridgeModelsDto>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7081/api/models"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    models = JsonConvert.DeserializeObject<List<FridgeModelsDto>>(apiResponse);
                }
            }

            fridgeModelAndProducts = new FridgeModelsAndProductsObject { Products = products, FridgeModels = models };
            if (ModelState.IsValid)
                return View(fridgeModelAndProducts);

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewFridge(FridgeForCreationWithProductsId fridgeForCreationWithProductsId)
        {
            var fridge = new FridgesDto();

            if (string.IsNullOrEmpty(fridgeForCreationWithProductsId.Name) || string.IsNullOrEmpty(fridgeForCreationWithProductsId.Owner_Name))
            {
                return View();
            }
            var fridgeForCreation = new FridgesForCreationDto
            {
                Name = fridgeForCreationWithProductsId.Name,
                Owner_Name = fridgeForCreationWithProductsId.Owner_Name,
                ModelId = fridgeForCreationWithProductsId.ModelId
            };

            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(fridgeForCreation), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("https://localhost:7081/api/fridges", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    fridge = JsonConvert.DeserializeObject<FridgesDto>(apiResponse);
                }
            }

            var fridgeProducts = new List<FridgeProductsDto>();
            var productsId = fridgeForCreationWithProductsId.ProductId;
            if (productsId == null)
            {
                return View();
            }
            foreach (var productId in productsId)
            {
                var fridgeProduct = new FridgeProductsDto();
                var fridgeProductForCreation = new FridgeProductsForCreationDto
                {
                    ProductId = productId,
                    Quantity = 0
                };

                using (var httpClient = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(fridgeProductForCreation), Encoding.UTF8, "application/json");

                    using (var response = await httpClient.PostAsync($"https://localhost:7081/api/fridges/{fridge.Id}/fridgeProducts", content))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        fridgeProduct = JsonConvert.DeserializeObject<FridgeProductsDto>(apiResponse);
                        fridgeProducts.Add(fridgeProduct);
                    }
                }
            }

            return RedirectToAction("GetFridges");
        }

        public async Task<IActionResult> StoredProcedure(FridgeProductSpDto fridges)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.
                    GetAsync($"https://localhost:7081/api/fridges/{fridges.FridgeId}/fridgeProducts/storedproc"))
                {
                    var result = await response.Content.ReadAsStringAsync();
                }
            }

            return RedirectToAction("GetFridges");
        }

        [HttpGet]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}