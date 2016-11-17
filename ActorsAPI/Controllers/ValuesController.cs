using ActorsAPI.Actors;
using ActorsAPI.Dto;
using ActorsAPI.Statics;
using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace ActorsAPI.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public async Task<WebSiteResponseLD> Get(int id)
        {
            //Call top level actor and await response. Timeout 3 seconds.
            var  actorResponse = await StaticActorRefs.TopLevelActor.Ask(new TopLevelActor.GetClient(id), TimeSpan.FromSeconds(3));
            WebSiteResponseLD response = null;
            var ldResposne = actorResponse as TopLevelActor.HttpResponseMessageLD;
            if(ldResposne != null)
            {
                response = new WebSiteResponseLD  { ClientId = ldResposne.ClientId, StatsCode = ldResposne.Response.StatusCode.ToString(), HashCodeOfWorker = ldResposne.HashCodeOfWorker };
            }                        
            return response;
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
