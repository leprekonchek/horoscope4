using System.Collections.Generic;
using _04_Lopukhina.Models;

namespace _04_Lopukhina.Tools.DataStorage
{
    internal interface IDataStorage
    {
        List<Person> PersonsList { get; }
        void AddPerson(Person person);
        void DeletePerson(Person person);
        void SaveChanges();
    }
}
