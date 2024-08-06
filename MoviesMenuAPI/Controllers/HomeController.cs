using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MoviesMenuAPI.Controllers;

[Route("/")]
[ApiController]
public class HomeController : ControllerBase
{
    [HttpGet]
    public ActionResult Get()
    {
        string htmlContent = "<h1>Hello and welcome to the Movies Menu API </h1>";
        return Content(htmlContent, "text/html");
    }
}



