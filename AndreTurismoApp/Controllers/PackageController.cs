using AndreTurismoApp.Models;
using AndreTurismoApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AndreTurismoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PackageController : ControllerBase
    {

        public int Add(Package package)
        {


            return new PackageService().Add(package);
        }

        public List<Package> GetAll()
        {
            return new PackageService().GetAll();
        }

        public int Update(Package package)
        {
            return new PackageService().Update(package);
        }

        public int Delete(int id)
        {
            return new PackageService().Delete(id);
        }

    }
}
