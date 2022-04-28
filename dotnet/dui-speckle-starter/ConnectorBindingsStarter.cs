
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using DesktopUI2.Models;
using DesktopUI2.Models.Filters;
using DesktopUI2.Models.Settings;
using DesktopUI2.ViewModels;

namespace Speckle.DesktopUIStarter
{
    public class ConnectorBindingsStarter : DesktopUI2.ConnectorBindings
    {
        public override string GetActiveViewName()
        {
            Console.WriteLine("GetActiveViewName was called");
            return "Default";
        }

        public override List<MenuItem> GetCustomStreamMenuItems()
        {
            return new List<MenuItem>{
                new MenuItem{Header = "First"},
                new MenuItem{Header = "Second"},
                new MenuItem{Header = "Third"},
            };
        }

        public override string GetDocumentId()
        {
            return new Guid().ToString();
        }

        public override string GetDocumentLocation()
        {
            return "./";
        }

        public override string GetFileName()
        {
            return "Fake File Name";
        }

        public override string GetHostAppName()
        {
            return "Speckle DUI Starter";
        }

        public override string GetHostAppNameVersion()
        {
            return "1.0";
        }

        public override List<string> GetObjectsInView()
        {
            // TODO
            return new List<string>();
        }

        public override List<string> GetSelectedObjects()
        {
            // TODO
            return new List<string>();
        }

        public override List<ISelectionFilter> GetSelectionFilters()
        {
            // TODO
            return new List<ISelectionFilter>();
        }

        public override List<ISetting> GetSettings()
        {
            // TODO
            return new List<ISetting>();
        }

        public override List<StreamState> GetStreamsInFile()
        {
            return new List<StreamState>();
        }

        public override Task<StreamState> ReceiveStream(StreamState state, ProgressViewModel progress)
        {
            throw new System.NotImplementedException();
        }

        public override void SelectClientObjects(string args)
        {
            // TODO
        }

        public override Task<string> SendStream(StreamState state, ProgressViewModel progress)
        {
            throw new System.NotImplementedException();
        }

        public override void WriteStreamsToFile(List<StreamState> streams)
        {
            throw new System.NotImplementedException();
        }
    }

}