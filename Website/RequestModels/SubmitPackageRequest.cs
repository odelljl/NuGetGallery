﻿using System.ComponentModel.DataAnnotations;
using System.Web;

namespace NuGetGallery
{
    public class SubmitPackageRequest
    {
        [Required]
        [Hint("Your packageRegistration file will be uploaded and hosted on the gallery server.")]
        public HttpPostedFile PackageFile { get; set; }
    }
}