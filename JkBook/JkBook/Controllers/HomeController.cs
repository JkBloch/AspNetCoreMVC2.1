using JkBook.Models;
using JkBook.Repository;
using JkBook.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace JkBook.Controllers
{
    public class HomeController : Controller
    {

        private readonly IConfiguration configuration;
        private readonly NewBookAlertConfig _newBookAlertConfig;
        private readonly NewBookAlertConfig _thirePartyBookAlertConfig;
        private readonly IMessageRepository _messageRepository;
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;

        public HomeController(IConfiguration _configuration,            
            IOptionsSnapshot<NewBookAlertConfig> newBookAlertConfigIoptionSnapShot,
            IMessageRepository messageRepository,
            IUserService userService,
            IEmailService emailService)
        {
            configuration = _configuration;
            _newBookAlertConfig = newBookAlertConfigIoptionSnapShot.Get("InternalBook");
            _thirePartyBookAlertConfig = newBookAlertConfigIoptionSnapShot.Get("ThirdPartyBook");
            _messageRepository = messageRepository;
            _userService = userService;
            _emailService = emailService;
        }
        [ViewData]
        public string CustomProp { get; set; }

        [ViewData]
        public string MyTitle { get; set; }

        [ViewData]
        public BookModel book{ get; set; }
        public IConfiguration _Configuration { get; }

        public ViewResult Index()
        {

            var userId = _userService.GetUserId();
            var IsLogged = _userService.IsAuthenticated();

            //var bAlert = _newBookAlertConfig .DisplayAlert;
            //var sBookMessage = _newBookAlertConfig.BookMessage;
            //var value = _messageRepository.GetName();



            var bAlert = _newBookAlertConfig.DisplayAlert;
            var sBookMessage = _newBookAlertConfig.BookMessage;


            var bAlert1 = _thirePartyBookAlertConfig.DisplayAlert;
            var sBookMessage1 = _thirePartyBookAlertConfig.BookMessage;



            //var newBookAlert = new NewBookAlertConfig();
            //configuration.Bind("NewBookAlert", newBookAlert);
            //var bAlert = _newBookAlertConfig.DisplayAlert;
            //var sBookMessage = _newBookAlertConfig.BookMessage;

            //var bAlert = _newBookAlertConfigIoptionSnapShot.DisplayAlert;
            //var sBookMessage = _newBookAlertConfigIoptionSnapShot.BookMessage;
            //var value = _messageRepository.GetName();




            //var newbookmsg = configuration.GetSection("NewBookAlert");
            //var bAlert = newbookmsg.GetValue<bool>("DisplayAlert");
            //var sBookMessage = newbookmsg.GetValue<string>("BookMessage");

            var myAppName = configuration["AppName"];
            var Key1 = configuration["InfoObj:key1"];
            var Key2 = configuration["InfoObj:key2"];
            var Key31 = configuration["InfoObj:key3:Key3obj1"];
            var Key32 = configuration["InfoObj:key3:Key3obj2"];

            var myAppJsonKeyData = myAppName + " " + Key1 + " " + Key2 + " " + Key31 + " " + Key32
                + " New book Alert :" + bAlert.ToString() + " " + sBookMessage
                + " Third Party boook :" + bAlert1.ToString() + " " + sBookMessage1
            +" User Id :" + IsLogged.ToString() + userId?.ToString();
            //+  " From message repo :" + value;
            //View bag code
            ViewBag.AppSettingData = myAppJsonKeyData;
            ViewBag.Name = "Jk Blcoh" + "App name is :" + myAppName;
            ViewBag.jdata = new { Id = 1, Name = "JKB app name" + myAppName };

            dynamic data = new ExpandoObject();
            data.Id = 5;
                data.Title = "Title JKBook";
            ViewBag.data = data;

            ViewBag.Type = new BookModel { Id = 7, Author = "JK" };

            //ViewData code
            ViewData["Name"] = "JK Bloch";

            ViewData["bookdetail"] = new BookModel { Id = 8, Title = "MVC Core 2.1", Author = "JK" };

            //ViewData Attribute
            CustomProp = "This is Custom value";
            MyTitle = "MyHome";
            book = new BookModel { Id = 9, Title = "MS SQL", Author = "Javed" };
            return View();
        }

        [Route("about-us/{id?}/{name:alpha:minlength(5)}")]
        public ViewResult AboutUs(int id,string name)
        {
            ViewData["Id"] = id;
            ViewData["name"] = name;

            return View();
        }

        [Authorize(Roles ="Admin"), Route("contact-us")]
        public ViewResult ContactUs(int id, string name)
        {
            ViewBag.Id = id;
            ViewBag.Name  = name;
            return View();
        }

        [Route("test/a{a}")]
        public string test(string a)
        {
            return a;
        }

        [Route("test/b{a}")]
        public string test1(string a)
        {
            return a;
        }
        [Route("test/prm{a}")]
        public string test2(string a)
        {
            return a;
        }

       [HttpPost]
        public async Task<IActionResult> SendEmailToUser(string toEmailId)
        {
             ViewBag.AppSettingData = toEmailId;
            UserEmailOptions options = new UserEmailOptions
            {
                ToEmails = new List<string>() { toEmailId },
                PlaceHolders = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string,string>("{UserName}","Jk" )
                    
                }
            };
            await _emailService.SendTestEmail(options);
            return RedirectToAction("Index", "Home");
           
        }

    }
}
