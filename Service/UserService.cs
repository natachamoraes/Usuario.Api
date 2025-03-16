using Usuario.Api.Data.Repository;
using Usuario.Api.Entity;
using Usuario.Api.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;

namespace Usuario.Api.Service
{
    public class UserService
    {
        private readonly UserRepository _repository;
        private readonly IMapper _mapper;

        public UserService(UserRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<User> CreateUser(CreateUserRequestDto user)
        {
            if (user == null)
                throw new ArgumentException("Usuário inválido");
            
            var userEntity = _mapper.Map<User>(user);

            var entity = await _repository.InsertAsync(userEntity);
            return entity;
        }

        public async Task<User?> GetUserById(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<User>> GetAllUsers(UserRequestDto requestDto)
        {
            var users = await _repository.GetAllAsync(requestDto.PageNumber, requestDto.PageSize); 
            return users;
        }

        public async Task<User> UpdateUser(UpdateUserRequestDto user)
        {
            var map = _mapper.Map<User>(user);
            var updatedUser = await _repository.UpdateAsync(map);

            return updatedUser;
        }

        public async Task DeleteUser(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}