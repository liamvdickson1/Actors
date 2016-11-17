using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ActorsAPI.Statics
{
    public static class StaticActorRefs
    {
        public static IActorRef TopLevelActor { get; set; }
    }
}