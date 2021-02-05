using CoreWebApiTraining.Data.Entities;
using CoreWebApiTraining.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreWebApiTraining.services
{
    public interface IUserService
    {
        Task<bool> addUser(UserModel item);
        Task<bool> deleteUser(UserModel item);
        Task<IEnumerable<UserModel>> getUsers(int pageNumber, int pageSize);
        Task<UserModel> getById(int id);
        Task<IEnumerable<UserModel>> getUsers(int pageNumber, int pageSize, string vFilter);
    }
}
