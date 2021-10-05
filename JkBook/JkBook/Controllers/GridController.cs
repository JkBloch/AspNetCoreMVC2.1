using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JkBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace JkBook.Controllers
{
    public class GridController : Controller
    {
        public IActionResult Index()
        {
            GridModelList glm = new GridModelList();
            List<GridModel> gl = new List<GridModel>();
            GridModel gm = new GridModel();
            gm.Id = 1;
            gm.Name = "Test1";
            gm.Desc = "Test 1 Description";
            gm.Date = "29-03-2019";
            gl.Add(gm);

            GridModel gm1 = new GridModel();
            gm1.Id = 2;
            gm1.Name = "Test2";
            gm1.Desc = "Test 2 Description";
            gm1.Date = "30-03-2019";
            gl.Add(gm1);

            GridModel gm2 = new GridModel();
            gm2.Id = 3;
            gm2.Name = "Test3";
            gm2.Desc = "Test 3 Description";
            gm2.Date = "01-04-2019";
            gl.Add(gm2);
            gl.Add(gm2);
            gl.Add(gm2);
            gl.Add(gm2);
            gl.Add(gm2);
            gl.Add(gm2);
            gl.Add(gm2);

            gl.Add(gm2);
            gl.Add(gm2);
            gl.Add(gm2);
            gl.Add(gm2);
            gl.Add(gm2);
            gl.Add(gm2);
            gl.Add(gm2);
            gl.Add(gm2);
            gl.Add(gm2);
            gl.Add(gm2); gl.Add(gm2);
            gl.Add(gm2);

            glm.GridData = gl;

            return View("~/Views/Grid/index.cshtml", glm);
        }
    }
}