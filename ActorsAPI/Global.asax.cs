using ActorsAPI.Actors;
using ActorsAPI.Statics;
using Akka.Actor;
using Akka.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ActorsAPI
{
    public class WebApiApplication : System.Web.HttpApplication
    {
  
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var actSystem  = ActorSystem.Create("LDActorSystem");
            
            StaticActorRefs.TopLevelActor = actSystem.ActorOf(Props.Create(() => new TopLevelActor()));

            //StaticActorRefs.TopLevelActor = actSystem.ActorOf(Props.Create(() => new TopLevelActor()).WithRouter(new RoundRobinPool(10)));

        }
    }
}
