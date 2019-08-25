using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;
using System.Data.Entity;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        private MyDbContext _context;

        public CustomersController()
        {
            this._context = new MyDbContext();
        }
        public ActionResult Index()
        {
            //var customers = this._context.Customers.Include(c => c.MembershipType).ToList(); // Eager loading

            //return View(customers);
            return View();
        }

        public ActionResult Details(int id)
        {
            var customer = this._context.Customers.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == id);

            if(customer == null)
            {
                return HttpNotFound();
            }

            return View(customer);
        }

        public ActionResult Edit(int id)
        {
            var customer = this._context.Customers.SingleOrDefault(c => c.Id == id);

            var viewModel = new CustomerFormViewModel()
            {
                Customer = customer,
                MembershipTypes = this._context.MembershipType.ToList()
            };

            return View("CustomerForm", viewModel);
        }

        public ActionResult New()
        {
            var memberShipTypes = this._context.MembershipType.ToList();

            var viewModel = new CustomerFormViewModel()
            {
                Customer = new Customer(),
                MembershipTypes = memberShipTypes
            };

            return View("CustomerForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(CustomerFormViewModel model)
        {
            if(!ModelState.IsValid)
            {
                var viewModel = new CustomerFormViewModel
                {
                    Customer = model.Customer,
                    MembershipTypes = this._context.MembershipType.ToList()
                };

                return View("CustomerForm", viewModel);
            }

            if(model.Customer.Id == 0)
            {
                this._context.Customers.Add(model.Customer);
            }
            else
            {
                var customerInDb = this._context.Customers.Single(c => c.Id == model.Customer.Id);

                customerInDb.IsSubscribedToNewsletter = model.Customer.IsSubscribedToNewsletter;
                customerInDb.MembershipTypeId = model.Customer.MembershipTypeId;
                customerInDb.Name = model.Customer.Name;
                customerInDb.Birthdate = model.Customer.Birthdate;
            }

            this._context.SaveChanges();

            return RedirectToAction("Index", "Customers");
        }

        //private IEnumerable<Customer> GetCustomers()
        //{
        //    return new List<Customer>
        //    {
        //        new Customer() {Id=1, Name="John"},
        //        new Customer() {Id=2, Name="Mary"}
        //    };
        //}
    }
}