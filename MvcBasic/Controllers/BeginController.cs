using MvcBasic.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcBasic.Controllers
{
    public class BeginController : Controller
    {
        private MvcBasicContext db = new MvcBasicContext();

        // SQL命令を出力ウィンドウから見れるようにできる 
        public BeginController()
        {
            db.Database.Log = sql =>
            {
                Debug.Write(sql);
            };
        }

        // GET: Begin
        public ActionResult Index()
        {
            return Content("こんにちわ、世界！");
        }

        public ActionResult Show()
        {
            ViewBag.Message = "こんにちわ、世界！";
            return View();
        }

        public ActionResult List()
        {
            return View(db.Members);
        }
    }
}