using System;
using Speckle.Core.Credentials;
using Speckle.Core.Api;
using System.Threading;
using System.Collections.Generic;
using Speckle.Core.Transports;
using Speckle.Core.Models.GraphTraversal;
using System.Linq;
using Speckle.Core.Models;

namespace CSharpStarter
{
    class Program
    {
        // Running this program will pull the latest commit from the main branch
        // of the specified stream and duplicate it inside a different branch.
        // (branch should exist already or the program will fail)
        static void Main(string[] args)
        {
            // The id of the stream to work with (we're assuming it already exists in your default account's server)
            var streamId = "51d8c73c9d";
            // The name of the branch we'll send data to.
            var branchName = "branch1";

            // Get default account on this machine
            // If you don't have Speckle Manager installed download it from https://speckle-releases.netlify.app
            var defaultAccount = AccountManager.GetDefaultAccount();
            // Or get all the accounts and manually choose the one you want
            // var accounts = AccountManager.GetAccounts();
            // var defaultAccount = accounts.ToList().FirstOrDefault();

            // Authenticate using the account
            var client = new Client(defaultAccount);


            // Now we can start using the client


            // Get the main branch with it's latest commit reference
            var branch = client.BranchGet(streamId, "main", 1).Result;
            // Get the id of the object referenced in the commit
            var hash = branch.commits.items[0].referencedObject;


            // Create the server transport for the specified stream.
            var transport = new ServerTransport(defaultAccount, streamId);
            // Receive the object
            var receivedBase = Operations.Receive(hash, transport).Result;

            // Process the object however you'd like
            Console.WriteLine("Received object:" + receivedBase);
            // Check the sample Revit traversal and conversion method if you'd like to convert your received base to a native object
            TraverseAndConvertToNativeRevit(receivedBase);

            // Sending the object will return it's unique identifier.
            var newHash = Operations.Send(receivedBase, new List<ITransport> { transport }).Result;

            // Create a commit in `processed` branch (it must previously exist)
            var commitId = client.CommitCreate(new CommitCreateInput()
            {
                branchName = branchName,
                message = "Automatic commit created by C# Starter example console app.",
                objectId = newHash,
                streamId = streamId,
                sourceApplication = "C# Starter Script"

            }).Result;

            Console.WriteLine($"Successfully created commit with id: {commitId}");

            // Remember to dispose of the client once you've finished with it.
            client.Dispose();
        }

        static void TraverseAndConvertToNativeRevit(Speckle.Core.Models.Base receivedBase)
        {
          // load your speckle kit and converter
          var kit = Speckle.Core.Kits.KitManager.GetDefaultKit();
          var converter = kit.LoadConverter(Objects.Converter.Revit.ConverterRevit.RevitAppName);

          // set your converter context document and properties
          converter.SetContextDocument(doc); // doc is the Revit ui doc 
          converter.ReceiveMode = Speckle.Core.Kits.ReceiveMode.Create; // this receive mode will always create new objects

          // traverse the received base to get all convertible objects with the revit converter
          // note: for all other applications, use the default traversal function DefaultTraversal.CreateTraverseFunc(converter)
          var func = DefaultTraversal.CreateRevitTraversalFunc(converter);
          var convertibleObjects = func.Traverse(receivedBase)
            .Select(c => c.current)
            .ToList();


          // start your revit transaction here


          // convert each speckle object to native
          foreach (var convertibleObject in convertibleObjects)
          {
            var converted = converter.ConvertToNative(convertibleObject);

            // sometimes the return type may be a speckle ApplicationObject instead of the native object
            if (converted is ApplicationObject appObj)
            {
              if (appObj.Status == ApplicationObject.State.Failed)
              {
                // handle a failed conversion here
                continue;
              }
              var revitObjs = appObj.Converted; // successfully converted revit elements
              var revitObjIds = appObj.CreatedIds; // ElementIds of the revit elements that have been added to the revit doc
            }
          }


          // commit your revit transaction
        }

        static void ReactToCommit(string[] args)
        {
            // Get default account on this machine
            var defaultAccount = AccountManager.GetDefaultAccount();
            var client = new Client(defaultAccount);
            var streamId = "42c06de34f";
            var branch = client.BranchGet(streamId, "main", 1).Result;
            var hash = branch.commits.items[0].referencedObject;


            var exit = false;
            // client.OnCommitCreated += OnCommitCreated;

            // Subscribe to commits created on the stream.
            client.SubscribeCommitCreated(streamId);

            // HACK: This is a super hacky way to get a C# console app to wait for an event to happen.
            // YOU SHOULD NOT DO THIS IN A PRODUCTION APPLICATION 🤣
            Console.WriteLine("Waiting for commit created event...");
            while (!exit)
            {
                // Exit if ESC is pressed
                if (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Escape)
                {
                    exit = true;
                }
                else
                {
                    Thread.Sleep(500);
                    Console.WriteLine("Still waiting...");
                }
            }

            // Remember to dispose of the client once you've finished with it.
            client.Dispose();
        }


    }
}
