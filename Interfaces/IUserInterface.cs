using System.Collections.Generic;
using System.Threading.Tasks;
using TenisHolly.DTO;

namespace TenisHolly.Interface
{
    public interface IUserInterface
    {
        Task Register(UserDto userDto);
        Task<UserDto> Add(UserDto userDto);
        Task<IEnumerable<UserDto>> GetAll();
        Task<UserDto> GetById(int id);
        Task Update(UserDto userDto);
        Task Delete(int id);
    }
}
