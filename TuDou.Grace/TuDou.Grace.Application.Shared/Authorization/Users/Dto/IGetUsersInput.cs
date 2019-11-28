﻿using Abp.Application.Services.Dto;
using Abp.Runtime.Validation;
using System.Collections.Generic;

namespace TuDou.Grace.Authorization.Users.Dto
{
    public interface IGetUsersInput : ISortedResultRequest
    {
        string Filter { get; set; }

        List<string> Permissions { get; set; }

        int? Role { get; set; }

        bool OnlyLockedUsers { get; set; }
    }
}