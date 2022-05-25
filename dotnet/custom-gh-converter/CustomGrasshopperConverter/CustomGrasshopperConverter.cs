using System.Collections.Generic;
using Speckle.Core;
using Speckle.Core.Models;
using Objects.Converter.RhinoGh;
using Speckle.Core.Kits;
using CustomSpeckleObjects;

namespace CustomGrasshopperConverter
{

    public class CustomGrasshopperConverter : ISpeckleConverter
    {
        private ConverterRhinoGh BaseConverter = new();

        public string Description => "My custom converter";
        public string Name => "Custom GH converter";
        public string Author => "Alan Rynne";
        public string WebsiteOrEmail => "alan@speckle.systems";

        public ProgressReport Report => BaseConverter.Report;

        public ReceiveMode ReceiveMode { get; set; }

        public bool CanConvertToNative(Base @object)
        {
            if (@object is FancyObject)
            {
                return true;
            }
            return BaseConverter.CanConvertToNative(@object);
        }

        public bool CanConvertToSpeckle(object @object)
        {
            return BaseConverter.CanConvertToSpeckle(@object);
        }

        public object ConvertToNative(Base @object)
        {
            if (@object is FancyObject fancy)
            {
                return BaseConverter.ConvertToNative(fancy.Origin);
            }
            return BaseConverter.ConvertToNative(@object);
        }

        public List<object> ConvertToNative(List<Base> objects)
        {
            return BaseConverter.ConvertToNative(objects);
        }

        public Base ConvertToSpeckle(object @object)
        {
            return BaseConverter.ConvertToSpeckle(@object);
        }

        public List<Base> ConvertToSpeckle(List<object> objects)
        {
            return BaseConverter.ConvertToSpeckle(objects);
        }

        public IEnumerable<string> GetServicedApplications()
        {
            return BaseConverter.GetServicedApplications();
        }

        public void SetContextDocument(object doc)
        {
            BaseConverter.SetContextDocument(doc);
        }

        public void SetContextObjects(List<ApplicationPlaceholderObject> objects)
        {
            BaseConverter.SetContextObjects(objects);
        }

        public void SetConverterSettings(object settings)
        {
            BaseConverter.SetConverterSettings(settings);
        }

        public void SetPreviousContextObjects(List<ApplicationPlaceholderObject> objects)
        {
            BaseConverter.SetPreviousContextObjects(objects);
        }
    }
}
