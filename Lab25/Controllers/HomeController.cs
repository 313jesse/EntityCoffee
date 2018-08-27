using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lab25.Models;

namespace Lab25.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            GCcoffeeShopEntities ORM = new GCcoffeeShopEntities();
            ViewBag.Items = ORM.Items.ToList();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult NewItem()
        {
            return View();
        }
        public ActionResult SaveNewItem(Item newItem)
        {
            GCcoffeeShopEntities ORM = new GCcoffeeShopEntities();

            //to do : validation !!!!

            ORM.Items.Add(newItem);
            ORM.SaveChanges(); //sync with the database   // if you forget this it will only exist on the MVC side. 

            return View("Index");
        }
        public ActionResult DeleteItem(int itemID)
        {
            GCcoffeeShopEntities ORM = new GCcoffeeShopEntities();

            //for loop with remove
            //find is a built in method. used to find objects using primary key

            Item ItemToDelete = ORM.Items.Find(itemID);

            //make a popup, for the "are you sure" before deleting:::::(below) bootstrap modal popup






            ORM.Items.Remove(ItemToDelete);
            ORM.SaveChanges();

            return RedirectToAction("Index");

           //then remove 


        }

        public ActionResult ItemDetails(int itemID)
        {   // this action will show the old data
            //step 1 
            GCcoffeeShopEntities ORM = new GCcoffeeShopEntities();

            //find item, send it back to view

            Item ItemToEdit = ORM.Items.Find(itemID);

            //send it to the view

            ViewBag.ItemToEdit = ItemToEdit;




            return View();


        }

        public ActionResult SaveChanges(Item UpdatedItem)
        {
            GCcoffeeShopEntities ORM = new GCcoffeeShopEntities();

            Item OldRecord = ORM.Items.Find(UpdatedItem.itemID);
            // todo: check for null for better validation. 

            OldRecord.name = UpdatedItem.name;
            OldRecord.decription = UpdatedItem.decription;
            OldRecord.quantity = UpdatedItem.quantity;
            OldRecord.price = UpdatedItem.price;

            ORM.Entry(OldRecord).State = System.Data.Entity.EntityState.Modified;

            ORM.SaveChanges();

            return RedirectToAction("Index");


        }
    }

}