using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace CM.Web.Providers.CustomAttributes
{
    public class MaxFileSizeAttribute : ValidationAttribute
    {

        /// <summary>
        /// Initializes new instance of <see cref="MaxFileSizeAttribute"/> class.
        /// </summary>
        /// <param name="maxFileSize">Size in bytes</param>
        public MaxFileSizeAttribute(int maxFileSize)
        {
            MaxFileSize = maxFileSize;
        }

        public int MaxFileSize { get; }

        public string GetErrorMessage() =>
             $"Maximum allowed file size is { Math.Round(MaxFileSize / (1024 * 1024d))} Mbs.";

        public override bool IsValid(object value)
        {
            var file = value as IFormFile;

            if (file != null)
            {
                return file.Length < MaxFileSize;
            }

            return false;
        }
    }
}
