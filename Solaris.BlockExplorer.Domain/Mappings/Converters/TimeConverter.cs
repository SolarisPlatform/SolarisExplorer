using System;
using AutoMapper;

namespace Solaris.BlockExplorer.Domain.Mappings.Converters
{
    public class TimeConverter : IValueConverter<long, DateTime>
    {
        public DateTime Convert(long sourceMember, ResolutionContext context)
        {
            return DateTimeOffset.FromUnixTimeSeconds(sourceMember).DateTime;
        }
    }
}
