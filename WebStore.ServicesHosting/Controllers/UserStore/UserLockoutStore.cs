using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.DomainNew.Dto.User;
using WebStore.DomainNew.Entities;

namespace WebStore.ServicesHosting.Controllers
{
    public partial class UsersApiController
    {
        #region IUserLockoutStore

        [HttpPost("getLockoutEndDate")]
        public async Task<DateTimeOffset?> GetLockoutEndDateAsync(User user)
        {
            return await _userStore.GetLockoutEndDateAsync(user);
        }

        [HttpPost("setLockoutEndDate")]
        public Task SetLockoutEndDateAsync(SetLockoutDto setLockoutDto)
        {
            return _userStore.SetLockoutEndDateAsync(setLockoutDto.User, setLockoutDto.LockoutEnd);
        }

        [HttpPost("IncrementAccessFailedCount")]
        public async Task<int> IncrementAccessFailedCountAsync(User user)
        {
            return await _userStore.IncrementAccessFailedCountAsync(user);
        }

        [HttpPost("ResetAccessFailedCount")]
        public Task ResetAccessFailedCountAsync(User user)
        {
            return _userStore.ResetAccessFailedCountAsync(user);
        }

        [HttpPost("GetAccessFailedCount")]
        public async Task<int> GetAccessFailedCountAsync(User user)
        {
            return await _userStore.GetAccessFailedCountAsync(user);
        }

        [HttpPost("GetLockoutEnabled")]
        public async Task<bool> GetLockoutEnabledAsync(User user)
        {
            return await _userStore.GetLockoutEnabledAsync(user);
        }

        [HttpPost("SetLockoutEnabled/{enabled}")]
        public async Task SetLockoutEnabledAsync(User user, bool enabled)
        {
            await _userStore.SetLockoutEnabledAsync(user, enabled);
            return;
        }

        #endregion
    }
}
