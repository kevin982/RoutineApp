using CategoryMS_Core.Exceptions;
using CategoryMS_Core.Models;
using CategoryMS_Core.Models.Responses;
using CategoryMS_Core.Services;
using System;
using System.Collections.Generic;

namespace CategoryMS_Infraestructure.Services
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
            if (ex is not CategoryMSException)

                return new HateoasResponse
                {
                    Links = links,
                    Succeeded = false,
                    StatusCode = 500,
                    Title = "Server error",
                    Content = null,
                };

            CategoryMSException e = ex as CategoryMSException;

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
