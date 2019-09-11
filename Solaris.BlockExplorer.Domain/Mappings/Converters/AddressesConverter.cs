using AutoMapper;

namespace Solaris.BlockExplorer.Domain.Mappings.Converters
{
    internal class AddressesConverter : IValueConverter<string, string[]>
    {
        public string[] Convert(string sourceMember, ResolutionContext context)
        {
            return sourceMember?.Split(",") ?? new string[0];
        }
    }
}
