using ExerciseMS_Core.Exceptions;
using ExerciseMS_Core.Models;
using ExerciseMS_Core.Models.Responses;
using ExerciseMS_Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExerciseMS_Infraestructure.Services
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
            if (ex is not ExerciseMSException)
            
                return new HateoasResponse
                {
                    Links = links,
                    Succeeded = false,
                    StatusCode = 500,
                    Title = "Server error",
                    Content = null,
                };
            
            ExerciseMSException e = ex as ExerciseMSException;

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
