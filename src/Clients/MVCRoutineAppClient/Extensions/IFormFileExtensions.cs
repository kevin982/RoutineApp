using Microsoft.AspNetCore.Http;
using System;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;

namespace MVCRoutineAppClient.Extensions
{
    public static class IFormFileExtensions
    {
 
        public static bool IsImage(this IFormFile file)
        {
            return (file.ContentType.Contains("image"));
        }
    }
}
