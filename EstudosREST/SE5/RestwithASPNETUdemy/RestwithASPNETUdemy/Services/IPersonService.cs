using RestwithASPNETUdemy.Model;
using System.Collections.Generic;

namespace RestwithASPNETUdemy.Services
{
    public interface IPersonService
    {
        Person Create(Person person);
        Person FindById(long Id);
        Person Update(Person person);
        List<Person> FindAll();
        void Delete(long Id);
    }
}
