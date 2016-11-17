using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Web;

namespace ActorsAPI.Actors
{
    public class WorkerActor : ReceiveActor
    {
        private HttpClient httpClient;
        public class DoWork
        {
            public DoWork(int clientId)
            {
                ClientId = clientId;
            }

            public int ClientId { get; private set; }
        }
        public WorkerActor()
        {                        
            Receive<DoWork>(doWork =>
           {
               //want reference to this sender in the C# closure
               var sender = Sender;
               var clientId = doWork.ClientId;
               var httpResult = httpClient.GetAsync("http://www.bbc.co.uk").ContinueWith<object>(response =>
               {
                   var httpResponse = response.Result;
                   Thread.Sleep(2000);
                   return new TopLevelActor.HttpResponseMessageLD(clientId, httpResponse, this.GetHashCode());
               }).PipeTo(sender);
           });
        }


        protected override void PreStart()
        {
            httpClient = new HttpClient();
            base.PreStart();
        }
    }
}