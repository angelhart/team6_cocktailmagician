using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;

namespace CM.Web.Providers.CustomAttributes
{
    public class AllowedExtensionsAttribute : ValidationAttribute
    {
        /// <summary>
        /// Initializes new instance of <see cref="AllowedExtensionsAttribute"/> class.
        /// </summary>
        /// <param name="AllowedExtensions">Allowed extensions represented as string array.</param>
        public AllowedExtensionsAttribute(string[] extensions)
        {
            AllowedExtensions = extensions;
        }

        public string[] AllowedExtensions { get; }

        public string GetErrorMessage() =>
            $"Incorrect file type! Allowed extensions are {String.Join(", ", AllowedExtensions)}!";

        public override bool IsValid(object value)
        {
            var file = value as IFormFile;
            if (file != null)
            {
                var extension = Path.GetExtension(file.FileName);
                return AllowedExtensions.Contains(extension, StringComparer.InvariantCultureIgnoreCase);                
            }

            return true;
        }
    }
}
