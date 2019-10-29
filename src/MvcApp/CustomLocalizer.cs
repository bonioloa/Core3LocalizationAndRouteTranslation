using System.Diagnostics;
using System.Globalization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Localization;

namespace MvcApp
{
    /// <summary>
    /// wrap localizer to hide resources class type
    /// </summary>
    public class CustomLocalizer : StringLocalizer<Common>
    {
        private readonly IStringLocalizer _internalLocalizer;

        public CustomLocalizer(
            IStringLocalizerFactory factory
            ) : base(factory)
        {
            _internalLocalizer = new StringLocalizer<Common>(factory);
        }

        public override LocalizedString this[string name, params object[] arguments]
        {
            get
            {
                return _internalLocalizer[name, arguments];
            }
        }

        public override LocalizedString this[string name]
        {
            get
            {
                return _internalLocalizer[name];
            }
        }

        //public string CurrentLanguage { get; set; }
    }
}
