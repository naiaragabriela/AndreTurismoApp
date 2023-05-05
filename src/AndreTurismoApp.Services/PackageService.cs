using AndreTurismoApp.Models;
using AndreTurismoApp.Repositories;

namespace AndreTurismoApp.Services
{
    public class PackageService
    {
        private readonly IPackageRepository _packageRepository;

        public PackageService()
        {
            _packageRepository = new PackageRepository();
        }

        public int Add(Package package)
        {
            return _packageRepository.Add(package);
        }

        public List<Package> GetAll()
        {
            return _packageRepository.GetAll();
        }

        public int Update(Package package)
        {
            return _packageRepository.Update(package);
        }

        public int Delete(int id)
        {
            return _packageRepository.Delete(id);
        }
    }
}
