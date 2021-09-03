using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExerciseMS_Core.Services
{
    public interface IUserService
    {
        string GetUserId();
        bool UserIsAuthenticated();
    }
}
