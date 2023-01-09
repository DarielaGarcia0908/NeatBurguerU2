using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NeatBurguerU2.Models;
using NeatBurguerU2.Models.ViewModels;

namespace NeatBurguerU2.Controllers
{
    public class HomeController : Controller
    {
        NeatBurguerU2.Models.neatContext context = new();
        [Route("/")]
        [Route("/Principal")]
        public IActionResult Index()
        {
            return View();
        }
        [Route("Menu1/{burger}")]
        public IActionResult Menu1(int burger)

        {
            var count = context.Menus.Count();
            int anterior = 0;
            int siguiente = 0;

            if (burger < 1 || burger > count)
            {
                return RedirectToAction("Index");
            }

            var actual = context.Menus.Skip(burger - 1).First();

            if (burger < 2)
            {
                anterior = count;
                siguiente = burger + 1;
            }
            else if (burger == count)
            {
                anterior = burger - 1;
                siguiente = 1;
            }
            else
            {
                anterior = burger - 1;
                siguiente = burger + 1;
            }
            Menu1VM vm = new();

            vm.Nombre = actual.Nombre;
            vm.Id = actual.Id;
            vm.Precio = actual.Precio;
            vm.Descripción = actual.Descripción;
            vm.Anterior = anterior;
            vm.Siguiente = siguiente;


            return View(vm);

        }
        [Route("/menu2")]
        public IActionResult Menu2()
        {
            var h = context.Clasificacions
               .Include(x => x.Menus)
               .OrderBy(x => x.Nombre);
            return View(h);



        }
        [Route("menu3/{id}")]
        public IActionResult Menu3(string id)
        {
            id = id.Replace("-", " ");

            if (id == null)
            {
                id = context.Menus.OrderBy(x => x.Nombre).FirstOrDefault().ToString();
            }

            Menu3VM vm = new Menu3VM();
            vm.Menus = context.Menus.OrderBy(x => x.Nombre);
           
            vm.IdBurger = context.Menus.Where(x => x.Nombre == id).Select(x => x.Id).FirstOrDefault();
            vm.Descripcion = context.Menus.Find(vm.IdBurger).Descripción;
            return View(vm);

           
        }

    }
}

