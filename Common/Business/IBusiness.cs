//
//---------------------------------------------- //
//                       @ Project : lhk.POS	   //
//                       @ File Name : IBusiness.cs                  //
//                       @ Date : 6/6/2014		  //
//                       @ Author : khoatd		  //
//--------------------------------------------- //
//

using Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Business
{
    interface IBusiness<T> where T : BaseEntity, new()
    {
        string Insert(T entity, bool isCommit = true);
        string Update(T entity, bool isCommit = true);
        string Delete(int id, bool isCommit = true);
        T GetById(int id);
        IEnumerable<T> GetAll();
    }
}
