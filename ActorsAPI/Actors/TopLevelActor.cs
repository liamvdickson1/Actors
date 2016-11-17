using Akka.Actor;
using Akka.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace ActorsAPI.Actors
{
    public class TopLevelActor: ReceiveActor
    {

        private IActorRef _worker { get; set; } 

        public class GetClient
        {
            public GetClient(int clientId)
            {
                ClientId = clientId;
            }

            public int ClientId { get; private set; }
        }

        public class HttpResponseMessageLD
        {
            public HttpResponseMessageLD(int clientId, HttpResponseMessage response, int hashCodeOfWorker)
            {
                ClientId = clientId;
                Response = response;
                HashCodeOfWorker = hashCodeOfWorker;
            }

            public int ClientId { get; private set; }
            public int HashCodeOfWorker { get; private set; }
            public HttpResponseMessage Response { get; private set; }
        }

        private Dictionary<int, IActorRef> controllers = new Dictionary<int, IActorRef>();
            



        public TopLevelActor()
        {
            Receive<GetClient>(client =>
            {
                var sender = Sender;
                controllers.Add(client.ClientId, sender);
                _worker.Tell(new WorkerActor.DoWork(client.ClientId));
            });

            Receive<HttpResponseMessageLD>(client =>
            {
                controllers[client.ClientId].Tell(client);
                controllers.Remove(client.ClientId);
            });
        }

        protected override void PreStart()
        {
            _worker = Context.ActorOf(Props.Create(() => new WorkerActor()).WithRouter(new RoundRobinPool(10)));
            base.PreStart();
        }



    }
}