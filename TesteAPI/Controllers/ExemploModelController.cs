using System.Web.Http;
using TesteAPI.Models;

namespace TesteAPI.Controllers
{
    public class ExemploModelController : ApiController
    {
        public ExemploModel Post(string url)
        {
            return new ExemploModel
            {
                PropiedadeTeste1 = "Teste"
            };
        }
    }
}
