using AutoMapper;
using CoreWebApiTraining.Data.Entities;
using CoreWebApiTraining.Data.Repositories;
using CoreWebApiTraining.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreWebApiTraining.services
{
    public class UserService : IUserService
    {
        private readonly IGenericRepository<Users> repository;
        private readonly IMapper mapper;

        public UserService(IGenericRepository<Users> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<bool> addUser(UserModel item)
        {
            var user = mapper.Map<Users>(item);
            return await this.repository.Add(user);
        }

        public async Task<bool> deleteUser(UserModel item)
        {
            var user = mapper.Map<Users>(item);
            return await this.repository.Delete(user);
        }

        public async Task<UserModel> getById(int id)
        {
            var user = (await this.repository.findBy(x => x.Id == id, -1, -1)).FirstOrDefault();
            return mapper.Map<UserModel>(user);
        }

        public async Task<IEnumerable<UserModel>> getUsers(int pageNumber, int pageSize)
        {
            var users = await this.repository.findBy(x => true, (pageNumber - 1) * pageSize, pageSize);
            return mapper.Map<IEnumerable<UserModel>>(users);
        }

        public async Task<IEnumerable<UserModel>> getUsers(int pageNumber, int pageSize, string vFilter)
        {
            var users = await this.repository.findBy(x => x.Name.Contains(vFilter) || x.Surname.Contains(vFilter), (pageNumber - 1) * pageSize, pageSize);
            return mapper.Map<IEnumerable<UserModel>>(users);
        }
    }
}
