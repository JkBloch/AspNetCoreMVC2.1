using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JkBook.Controllers
{
    public class HomeController : Controller
    {
        public string Index()
        {
            return "Jk Bloch";
        }
    }
}
