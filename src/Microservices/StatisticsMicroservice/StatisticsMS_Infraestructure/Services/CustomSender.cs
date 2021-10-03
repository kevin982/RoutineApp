using StatisticsMS_Core.Exceptions;
using StatisticsMS_Core.Models;
using StatisticsMS_Core.Models.Response;
using StatisticsMS_Core.Services;
using System;
using System.Collections.Generic;

namespace StatisticsMS_Infraestructure.Services
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


            if (ex is StatisticsMSException)
            {
                StatisticsMSException e = ex as StatisticsMSException;

                response.StatusCode = e.StatusCode;
                response.Title = (e.StatusCode == 500) ? "Server error" : e.Message;

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
