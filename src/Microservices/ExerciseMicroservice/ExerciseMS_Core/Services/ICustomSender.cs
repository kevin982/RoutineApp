using ExerciseMS_Core.Models;
using ExerciseMS_Core.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExerciseMS_Core.Services
{
    public interface ICustomSender
    {
        HateoasResponse SendResult(object data, IEnumerable<Link> links, string message);
        HateoasResponse SendError(Exception ex, IEnumerable<Link> links);
    }
}
