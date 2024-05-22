using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biograf.Repo.Interface.GenericInterface
{
    public interface IGeneric<T> where T : class
    {
        List<T> GetAll();
        T Create(T item); // This method is responsible for creating a new
                          // record in a data store. It takes an item of type T
                          // (representing the object to be created) as a parameter
                          // and returns an object of type T, which typically
                          // represents the created object.

        T CreateMoviePhoto(T item);
        T GetById(int id);//It takes an integer id as a parameter and
                      //returns an object of type T,
                      //representing the record retrieved from the data store.
        T Update(int id, T item); // item of type T containing the updated data.
     // It returns an object of type T, typically representing the updated record.
        void Delete(int id);
    }
}
