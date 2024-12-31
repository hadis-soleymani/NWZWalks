using Microsoft.AspNetCore.Mvc;

namespace NWZWalks.UI.Controllers
{
    public class RegionsController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;

        public RegionsController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            try
            {

                var client = httpClientFactory.CreateClient();

                var httpResponseMessage = await client.GetAsync(client.BaseAddress);
                httpResponseMessage.EnsureSuccessStatusCode();

                var stringResponseBody = await httpResponseMessage.Content.ReadAsStringAsync();

                ViewBag.Response = stringResponseBody;

            }
            catch (Exception ex)
            {
            }
            return View();
        }
    }
}
