﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExerciseMS_Core.Models.Requests
{
    public class UploadImageRequest
    {
        public Stream Image { get; set; }

        public string ImageName { get; set; } = string.Empty;

        public string Cloud { get; set; } = string.Empty;

        public string ApiKey { get; set; } = string.Empty;

        public string Secret { get; set; } = string.Empty;
    }
}
