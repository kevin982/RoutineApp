using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCRoutineAppClient.Services
{
	public class UserService : IUserService
	{
		public bool IsAdmin(string accessToken)
		{
			try
			{
				var handler = new JwtSecurityTokenHandler();

				var roles = handler.ReadJwtToken(accessToken).Claims.Where(c => c.Type == "role").Select(c => c.Value).ToList();

				if (!roles.Contains("admin"))
				{
					return false;
				}

				return (roles.Contains("admin")) ? true : false;

			}
			catch (Exception)
			{
				return false;
			}
		}
	}
}
