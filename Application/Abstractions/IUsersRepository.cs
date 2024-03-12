using Application.DTOs;

namespace Application.Abstractions
{
    public interface IUsersRepository
    {
        Task<UserProfileDTO> SignIn(string email, string password);
        Task<int> CreateUser(NewUserDTO profile);
    } 
}