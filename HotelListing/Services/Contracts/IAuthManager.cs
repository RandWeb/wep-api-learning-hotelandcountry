using HotelListing.Models;
using System.Threading.Tasks;

namespace HotelListing.Services.Contracts
{
    public interface IAuthManager
    {
        Task<bool> ValidateUser(UserDTO userDTO);
        Task<string> CreateToken();
    }
}
