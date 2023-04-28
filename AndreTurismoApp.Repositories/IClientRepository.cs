using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AndreTurismoApp.Models;

namespace AndreTurismoApp.Repositories
{
    public interface IClientRepository
    {
        
    
        int Add(Client client);

        List<Client> GetAll();

        bool Update(Client client);

        bool Delete(int id);

    }
}
