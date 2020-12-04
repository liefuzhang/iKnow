﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;

namespace iKnow.Controllers
{
    public class ErrorController : Controller {
        public ActionResult Index() {
            return View("Error");
        }

        public ActionResult NotFound() {
            Response.StatusCode = 200;
            return View("404");
        }
    }
}