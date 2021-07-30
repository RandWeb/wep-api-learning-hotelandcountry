using HotelListing.Models;
using System.Threading.Tasks;

namespace HotelListing.Services.Contracts
{
    public interface IAuthManager
    {
        Task<bool> ValidateUser(LoginUserDTO userDTO);
        Task<string> CreateToken();
    }
}
