using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using TurboRenting.Front.Enums;
using TurboRenting.Front.HttpClientHelpper.HCUsers;

namespace TurboRenting.Front.ViewModels
{
    public class UserRepository
    {
        public UserRepository()
        {
        }

        internal async void PerformDelete(int id)
        {
            await UserRestService.DeleteUserAsync(id);

        }

        internal async void PerformCreate(User user)
        {
            await UserRestService.CreateUserAsync(user);
        }

        internal async void PerformUpdate(int id, User user)
        {
            await UserRestService.UpdateUserAsync(id, user);
        }

        internal async IAsyncEnumerable<User> GetUserList()
        {
            await foreach (var user in UserRestService.GetUsersAsync())
            {
                yield return user;
            }
        } 
    }
}
