using AndreTurismoApp.Models;

namespace AndreTurismoApp.Repositories
{
    public interface IHotelRepository
    {
        int Add(Hotel hotel);

        List<Hotel> GetAll();

        int Update(Hotel hotel);

        int Delete(int id);
    }
}
