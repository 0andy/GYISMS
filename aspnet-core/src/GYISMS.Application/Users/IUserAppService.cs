using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using GYISMS.Roles.Dto;
using GYISMS.Users.Dto;

namespace GYISMS.Users
{
    public interface IUserAppService : IAsyncCrudAppService<UserDto, long, PagedResultRequestDto, CreateUserDto, UserDto>
    {
        Task<ListResultDto<RoleDto>> GetRoles();

        Task ChangeLanguage(ChangeUserLanguageDto input);

        /// <summary>
        /// �޸�����
        /// </summary>
        /// <param name="password">������</param>
        /// <returns></returns>
        Task GYUpdatePassword(string password);

        /// <summary>
        /// ��������ԭ���������ݿ��������Ƿ����
        /// </summary>
        /// <returns></returns>
        Task<bool> CheckOldPassword(string oldPassword);
    }
}
