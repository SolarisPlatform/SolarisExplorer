using Newtonsoft.Json;

namespace Solaris.BlockExplorer.UI.Extensions
{
    public static class Extensions
    {
        public static string PrettyPrintJson(this string source)
        {
            dynamic json = JsonConvert.DeserializeObject(source);
            return JsonConvert.SerializeObject(json, Formatting.Indented);
        }
    }
}
