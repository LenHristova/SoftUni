namespace SIS.Demo.Controllers
{
    using System.Runtime.Serialization.Json;
    using System.Text;
    using Framework.ActionResults.Contracts;
    using Framework.Controllers;

    public class HomeController : Controller
    {
        public IActionResult Index() => this.View();

        public string In()
        {
        //    Newtonsoft.Json.JsonConvert.SerializeObject(new { foo = "bar" })
            return "h";
        }

        public class Person
        {
            public string Name { get; set; }

            public int Age { get; set; }

            public override string ToString()
            {
                return $"{Name} - {Age}";
            }
        }
    }
}
