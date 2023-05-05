using AndreTurismoApp.ExternalService;
using AndreTurismoApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace AndreTurismoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PackageController : ControllerBase
    {
        private readonly ExternalPackageService _packageService;

        public PackageController(ExternalPackageService service)
        {
            _packageService = service;
        }


        [HttpPost(Name = "InsertPackage")]
        public async Task<ActionResult> Add(Package package)
        {
            int statusCode = (int)await _packageService.PostPackage(package);

            return StatusCode(statusCode);
        }

        [HttpGet(Name = "GetAllPackages")]
        public async Task<List<Package>> GetAll()
        {
            List<Package> response = await _packageService.GetPackage();
            return response;
        }

        [HttpPut(Name = "UpdatePackage")]
        public async Task<ActionResult> Update(Package package)
        {
            int statusCode = (int)await _packageService.PutPackage(package);

            return StatusCode(statusCode);
        }

        [HttpDelete(Name = "DeletePackage")]
        public async Task<ActionResult> Delete(int id)
        {
            int statusCode = (int)await _packageService.DeletePackage(id);

            return StatusCode(statusCode);
        }

    }
}
