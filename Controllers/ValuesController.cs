using Microsoft.AspNet.Mvc;

namespace WebApiResourceCentricService.Controllers
{
    [Route("/api/[controller]/{id?}")]
    public class ValuesController : ResourceController
    {
        public string Get(int id)
        {
            return "value";
        }

        public void Post([FromBody]string value)
        {
        }

        public void Put(int id, [FromBody]string value)
        {
        }

        public void Delete(int id)
        {
        }
    }
}
