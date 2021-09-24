using RoutineMS_Core.Models;
using RoutineMS_Core.Models.Responses;
using System;
using System.Collections.Generic;

namespace RoutineMS_Core.Services
{
    public interface ICustomSender
    {
        HateoasResponse SendResult(object data, IEnumerable<Link> links, string message);
        HateoasResponse SendError(Exception ex, IEnumerable<Link> links);
    }
}
