using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreezerOrganizer.Model
{
    public interface IRepository<T> where T : class
    {
        IList<T> Load(string path);
        void Save(string path);
        void Delete(T element);
        IList<T> Search(string substring);
    }
}
