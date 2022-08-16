using Dashboard.Resources;

namespace Dashboard.Services
{
    public class LocalizationService
    {
        private readonly IStringLocalizer localizer;
        public LocalizationService(IStringLocalizerFactory factory)
        {
            AssemblyName assemblyName = new(typeof(LocalizerResources).GetTypeInfo().Assembly.FullName);
            localizer = factory.Create(nameof(LocalizerResources), assemblyName.Name);
        }

        public string Get(string key)
        {
            return localizer[key];
        }
    }
}
