using StatisticsMS_Core.Models;
using StatisticsMS_Core.Models.Response;
using System;
using System.Collections.Generic;

namespace StatisticsMS_Core.Services
{
    public interface ICustomSender
    {
        HateoasResponse SendResult(object data, IEnumerable<Link> links, string message);
        HateoasResponse SendError(Exception ex, IEnumerable<Link> links);
    }
}
