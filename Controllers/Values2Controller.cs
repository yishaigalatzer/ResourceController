using Microsoft.AspNet.Mvc;

namespace WebApiResourceCentricService.Controllers
{
    public class Values2Controller : ResourceController
    {
        public string Get(int? id)
        {
            return "GET: Values from route defined in startup " + id;
        }

        public string Post([FromBody]string value)
        {
            return "POST: Values from route defined in startup " + value;
        }

        public string Put(int id, [FromBody]string value)
        {
            return "PUT: Values from route defined in startup " + id + ", " + value;
        }

        public string Delete(int id)
        {
            return "DELETE: Values from route defined in startup " + id;
        }
    }
}