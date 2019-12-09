using Abp.Dependency;
using System.Collections.Generic;
using TuDou.Grace.Authorization.Users.Importing.Dto;

namespace TuDou.Grace.Authorization.Users.Importing
{
    public interface IUserListExcelDataReader : ITransientDependency
    {
        List<ImportUserDto> GetUsersFromExcel(byte[] fileBytes);
    }
}
