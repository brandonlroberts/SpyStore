using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using SpyStore.Hol.Dal.Repos.Interfaces;

namespace SpyStore.Hol.Mvc.ViewComponents
{
    public class Menu : ViewComponent
    {
        private readonly ICategoryRepo _categoryRepo;
        public Menu(ICategoryRepo categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.Run<IViewComponentResult>(() =>
            {
                var cats = _categoryRepo.GetAll();
                if (cats == null)
                {
                    return new ContentViewComponentResult("There was an error getting the categories");
                }
                return View("MenuView", cats);
            });
        }
    }
}
