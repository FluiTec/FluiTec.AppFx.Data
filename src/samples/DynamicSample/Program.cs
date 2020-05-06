using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace DynamicSample
{
    class Program
    {
        static void Main(string[] args)
        {
            var configValues = new List<KeyValuePair<string, string>>(new[]
            {
                new KeyValuePair<string, string>(), 
            });

            var config = new ConfigurationBuilder()
                .AddInMemoryCollection()
                .Build();
        }
    }
}
