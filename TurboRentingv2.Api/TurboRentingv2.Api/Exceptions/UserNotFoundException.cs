using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboRentingv2.Api.Models.Entities;

namespace TurboRentingv2.Api.Exceptions
{
    public class UserNotFoundException : EntityNotFoundException<User>
    {
        public UserNotFoundException(object? entityId) : base(entityId)
        {
        }
    }
}
