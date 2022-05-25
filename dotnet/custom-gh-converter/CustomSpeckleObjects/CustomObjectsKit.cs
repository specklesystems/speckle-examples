using System.Collections.Generic;
using Speckle.Core.Models;
using Speckle.Core.Kits;
using System;
using System.Reflection;
using System.Linq;
using System.IO;
using Speckle.Core.Logging;

namespace CustomGrasshopperConverter
{
    public class CustomObjectsKit : ISpeckleKit
    {
        public string Description => "My custom Speckle Kit.";
        public string Name => "CustomObjects";
        public string Author => "Alan Rynne";
        public string WebsiteOrEmail => "https://speckle.systems";

        public IEnumerable<Type> Types
        {
            get
            {
                //the types in this assembly
                var types = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.IsSubclassOf(typeof(Base)) && !t.IsAbstract);
                return types;
            }
        }

        public List<string>? _Converters;
        public IEnumerable<string> Converters
        {
            get
            {
                if (_Converters == null)
                {
                    _Converters = GetAvailableConverters();
                }

                return _Converters;
            }
        }

        private Dictionary<string, Type> _LoadedConverters = new Dictionary<string, Type>();

        public ISpeckleConverter? LoadConverter(string app)
        {
            _Converters = GetAvailableConverters();
            if (_LoadedConverters.ContainsKey(app) && _LoadedConverters[app] != null)
            {
                return Activator.CreateInstance(_LoadedConverters[app]) as ISpeckleConverter;
            }

            try
            {
                var basePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

                var path = Path.Combine(basePath, $"{Name}.Converter.{app}.dll");
                if (File.Exists(path))
                {
                    var assembly = Assembly.LoadFrom(path);

                    var converterClass = assembly.GetTypes().FirstOrDefault(type =>
                      (type.GetInterfaces().FirstOrDefault(i => i.Name == typeof(ISpeckleConverter).Name) != null) &&
                       (Activator.CreateInstance(type) as ISpeckleConverter).GetServicedApplications().Contains(app)
                    );

                    _LoadedConverters[app] = converterClass;
                    return Activator.CreateInstance(converterClass) as ISpeckleConverter;
                }
                else
                {
                    throw new SpeckleException($"Converter for {app} was not found in kit {basePath}", level: Sentry.SentryLevel.Warning);
                }

            }
            catch (Exception e)
            {
                Log.CaptureException(e, Sentry.SentryLevel.Error);
                return null;
            }
        }

        public List<string> GetAvailableConverters()
        {
            var basePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var list = Directory.EnumerateFiles(basePath, $"{Name}.Converter.*");

            return list.ToList().Select(dllPath => dllPath.Split('.').Reverse().ToList()[1]).ToList();
        }
    }
}
