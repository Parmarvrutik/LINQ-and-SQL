using LINQ_and_SQL.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LINQ_and_SQL.Controllers
{
    public class ProductController : Controller
    {
         productsDataContext db = new productsDataContext(ConfigurationManager.ConnectionStrings["myshopConnectionString"].ToString());
        // GET: Product
        public ActionResult Index()
        {
            IList<productModel> productList = new List<productModel>();
            var query = from s in db.ProductStores select s;
            var listdata = query.ToList();
            foreach (var pdata in listdata)
            {
                productList.Add(new productModel()
                {
                    Id = pdata.Id,
                    ProductName = pdata.productName,
                    ProductPrice = (int)pdata.productPrice,
                });
            }
            return View(productList);
        }

        // GET: Product/Details/5
        public ActionResult Details(int id)
        {
            productModel pmodel = db.ProductStores.Where(val => val.Id == id).Select(val => new productModel
            {
                Id = val.Id,
                ProductName = val.productName,
                ProductPrice = (int)val.productPrice,
            }).SingleOrDefault();
            return View(pmodel);
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            productModel pModel = new productModel(); 
            return View(pModel);
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(productModel pmd)
        {
            try
            {
                ProductStore pModel = new ProductStore();
                // TODO: Add insert logic here
                pModel.Id = pmd.Id;
                pModel.productName = pmd.ProductName;
                pModel.productPrice = pmd.ProductPrice;

                db.ProductStores.InsertOnSubmit(pModel);
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View("Index");
            }
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            productModel pmodel = db.ProductStores.Where(val => val.Id == id).Select(val => new productModel
            {
                Id = val.Id,
                ProductName = val.productName,
                ProductPrice = (int)val.productPrice,
            }).SingleOrDefault();
            return View(pmodel);
        }

        // POST: Product/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int id)
        {
            productModel pmodel = db.ProductStores.Where(val => val.Id == id).Select(val => new productModel
            {
                Id = val.Id,
                ProductName = val.productName,
                ProductPrice = (int)val.productPrice
            }).SingleOrDefault();
            return View(pmodel);
        }

        // POST: Product/Delete/5
        [HttpPost]
        public ActionResult Delete(productModel pmd)
        {
            try
            {
                ProductStore pstore = db.ProductStores.Where(val => val.Id == pmd.Id).Single<ProductStore>();
                db.ProductStores.DeleteOnSubmit(pstore);
                db.SubmitChanges();
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
