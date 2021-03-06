using System.Collections.Generic;
using AngleSharp.Dom.Html;
using Microsoft.Extensions.Configuration;
using SunEngine.Core.Configuration.Options;
using SunEngine.Core.Errors;

namespace SunEngine.Core.Services
{
    public class SanitizerService
    {
        private const string DefaultCategory = "Default";

        private readonly IConfiguration configuration;
        private readonly Dictionary<string, Sanitizer> optionCategories
            = new Dictionary<string, Sanitizer>();

        public SanitizerService(IConfiguration configuration)
        {
            this.configuration = configuration;
            LoadOptions();
        }

        public Sanitizer GetSanitizer(string sanitizerName = DefaultCategory)
        {
            return FindSanitizer(sanitizerName);
        }
        
        public string Sanitize(IHtmlDocument doc, string sanitizerName = DefaultCategory)
        {
            var sanitizer = FindSanitizer(sanitizerName);
            return sanitizer.Sanitize(doc);
        }
        
        public string Sanitize(string text, string sanitizerName = DefaultCategory)
        {
            var sanitizer = FindSanitizer(sanitizerName);
            return sanitizer.Sanitize(text);
        }

        private Sanitizer FindSanitizer(string sanitizerName)
        {
            if (optionCategories.ContainsKey(sanitizerName))
                return optionCategories[sanitizerName];
            
            throw new SunException($"Not found sanitizer with name \"{sanitizerName}\"");
        }

        private void LoadOptions()
        {
            var sections = configuration.GetSection("Sanitizer").GetChildren();
            foreach (var section in sections)
            {
                var key = section.Key;
                var sanitizerOptions = section.Get<SanitizerOptions>();
                var sanitizer = new Sanitizer(sanitizerOptions);
                optionCategories.Add(key, sanitizer);
            }
        }
    }
}