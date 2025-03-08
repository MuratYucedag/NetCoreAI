using Microsoft.AspNetCore.Mvc;
using NetCoreAI.Project20_RecipeSuggestionWithOpenAI.Models;

namespace NetCoreAI.Project20_RecipeSuggestionWithOpenAI.Controllers
{
    public class DefaultController : Controller
    {
        private readonly OpenAiService _openAiService;
        public DefaultController(OpenAiService openAiService)
        {
            _openAiService = openAiService;
        }

        [HttpGet]
        public IActionResult CreateRecipe()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRecipe(string ingredients)
        {
            var result = await _openAiService.GetRecipeAsync(ingredients);
            ViewBag.recipe = result;
            return View();
        }
    }
}
