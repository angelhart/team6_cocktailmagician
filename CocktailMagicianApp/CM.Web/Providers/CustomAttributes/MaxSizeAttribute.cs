using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CM.Web.Providers.CustomAttributes
{
    public class MaxSizeAttribute : ValidationAttribute, IClientModelValidator
    {

        /// <summary>
        /// Initializes new instance of <see cref="MaxSizeAttribute"/> class.
        /// </summary>
        /// <param name="maxFileSize">Size in bytes</param>
        public MaxSizeAttribute(int maxFileSize)
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

        // Set up attribute inclusion by tag helpers

        public void AddValidation(ClientModelValidationContext context)
        {
            MergeAttribute(context.Attributes, "data-val", "true");
           
            MergeAttribute(context.Attributes, "data-val-maxsize", GetErrorMessage());

            MergeAttribute(context.Attributes, "data-val-maxsize-maxfilesize", MaxFileSize.ToString());
        }

        private bool MergeAttribute(IDictionary<string, string> attributes, string key, string value)
        {
            if (attributes.ContainsKey(key))
            {
                return false;
            }

            attributes.Add(key, value);
            return true;
        }
    }
}
