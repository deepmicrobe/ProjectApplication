using Microsoft.AspNetCore.Mvc;
using ProjectApplication.Models;
using ProjectApplication.Services;

namespace ProjectApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController
    {
        private IHomeService _homeService;

        public HomeController(IHomeService homeService)
        {
            _homeService = homeService;
        }

        [HttpPost]
        public string Login(LoginRequest model)
        {
            var response = _homeService.Login(model);
            return response.Token;
        }
    }
}
