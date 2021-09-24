using RoutineMS_Core.Exceptions;
using RoutineMS_Core.Models;
using RoutineMS_Core.Models.Responses;
using RoutineMS_Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoutineMS_Infraestructure.Services
{
    public class CustomSender : ICustomSender
    {
        public HateoasResponse SendResult(object data, IEnumerable<Link> links, string message)
        {
            return new HateoasResponse()
            {
                Links = links,
                Succeeded = true,
                StatusCode = 200,
                Title = message,
                Content = data
            };
        }

        public HateoasResponse SendError(Exception ex, IEnumerable<Link> links)
        {
            if (ex is not RoutineMSException)

                return new HateoasResponse
                {
                    Links = links,
                    Succeeded = false,
                    StatusCode = 500,
                    Title = "Server error",
                    Content = null,
                };

            RoutineMSException e = ex as RoutineMSException;

            return new HateoasResponse
            {
                Links = links,
                Succeeded = false,
                StatusCode = e.StatusCode,
                Title = (e.StatusCode == 500) ? "Server error" : e.Message,
                Content = null,
            };

        }
    }
}
