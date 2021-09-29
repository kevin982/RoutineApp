using ExerciseMS_Core.Exceptions;
using ExerciseMS_Core.Models;
using ExerciseMS_Core.Models.Responses;
using ExerciseMS_Core.Services;
using FluentValidation;
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
            HateoasResponse response = new()
            {
                Links = links,
                Succeeded = false,
                Content = null
            };


            if (ex is ExerciseMSException)
            {
                ExerciseMSException e = ex as ExerciseMSException;

                response.StatusCode = e.StatusCode;
                response.Title = (e.StatusCode == 500) ? "Server error" : e.Message;

            }
            else if (ex is ValidationException)
            {
                response.StatusCode = 400;
                response.Title = $"Bad Request due to {ex.Message}";
            }
            else
            {
                response.StatusCode = 500;
                response.Title = "Server error";
            }

            return response;

        }
 
    }
}
