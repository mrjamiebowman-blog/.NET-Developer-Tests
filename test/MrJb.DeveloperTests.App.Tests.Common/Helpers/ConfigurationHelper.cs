using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace MrJB.DeveloperTests.App.Tests.Common.Helpers
{
    public static class ConfigurationHelper
    {
        public static IConfiguration Build(this Dictionary<string, string> dictionary)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(dictionary)
                .Build();

            return configuration;
        }

        public static Dictionary<string, string> Create()
        {
            return new Dictionary<string, string>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dictionary"></param>
        /// <param name="obj"></param>
        /// <param name="prePath">Example: "ConsumerSettings". This would be prepended to the configuration path, I.E., "ConsumerSettings:AzureServiceBus".</param>
        /// <returns></returns>
        public static Dictionary<string, string> AddConfiguration<T>(this Dictionary<string, string> dictionary, T obj, string prePath = "") where T : class
        {
            // property
            var property = obj.GetType().GetField("Position", BindingFlags.Public | BindingFlags.Static);

            // position
            var position = String.Empty;

            if (property != null) {
                var val = property.GetValue(obj);
                position = $"{val}:";
            }
            if (!String.IsNullOrWhiteSpace(prePath))
            {
                position = $"{prePath}:{position}";
            }

            // create dictionary
            var dict = obj.GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .ToDictionary(prop => $"{position}{prop.Name}", prop => Convert.ToString(prop.GetValue(obj, null)));

            // merge dictionary
            dict.ToList().ForEach(x => dictionary.Add(x.Key, x.Value));

            return dictionary;
        }
    }
}
