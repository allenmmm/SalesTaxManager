
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IO;

namespace SalesTaxManager.Configuration
{
    public abstract class File<T>

    {
        protected readonly IConfigurationSection _ConfigurationSection;
        protected readonly IEnumerable<T> _Data;
        public File(
            string fileName,
            string section)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(fileName);

            _Data = builder.Build().GetSection(section).Get<List<T>>();
        }
    }
}
