using ClientRoutineApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientRoutineApp.Services.Interfaces
{
    public interface IImageService
    {
        Task<string> PostImageAsync(PostImageRequestModel model);
    }
}
