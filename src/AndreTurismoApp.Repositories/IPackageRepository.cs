using AndreTurismoApp.Models;

namespace AndreTurismoApp.Repositories
{
    public interface IPackageRepository
    {
        int Add(Package package);

        List<Package> GetAll();

        int Update(Package package);

        int Delete(int id);
    }
}
