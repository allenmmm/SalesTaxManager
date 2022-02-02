
using LanguageExt;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;


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
