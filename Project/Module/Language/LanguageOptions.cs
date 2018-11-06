using System.Collections.Generic;

namespace Project.Module.Language
{ 
    public class Language
    {
        public string ISOCode { get; set; }
        public string FriendlyISOCode { get; set; }
    }
    public class LanguageOptions
    {
        public Language DefaultLanguage { get; set; } = new Language {
            ISOCode = "en",
            FriendlyISOCode = "english"
        };
        public List<Language> SupportedLanguages { get; set; } = new List<Language> {
            new Language {
                ISOCode = "en",
                FriendlyISOCode = "english"
            }
        };
    }
}
