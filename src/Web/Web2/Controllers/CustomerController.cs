using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.ViewModels;

namespace Web.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }
        
        // GET: Customer
        public ActionResult Index()
        {
            List<CustomerViewModel> model = new List<CustomerViewModel>();
            _customerService.GetCustomer().ToList().ForEach(u =>
                {
                    CustomerViewModel _custmerViewModel = new CustomerViewModel
                    {
                        guid = u.guid,
                        name = u.name,
                        email = u.email,
                        phone = u.phone,
                        address = u.address
                    };
                    model.Add(_custmerViewModel);
                });
                       
            return View(model);
        }

        // GET: Customer/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Customer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customer/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CustomerViewModel model)
        {
            try
            {
                Customer _customer = new Customer
                {
                    name = model.name,
                    email = model.email,
                    phone = model.phone,
                    address = model.address,
                };

                _customerService.InsertCustomerAsync(_customer);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(model);
            }
        }

        // GET: Customer/Edit/5
        public ActionResult Edit(Guid guid)
        {
            CustomerViewModel Model = new CustomerViewModel();

            if (guid != Guid.Empty)
            {
                Customer _customer =  _customerService.GetCustomer(guid);

                Model.name = _customer.name;
                Model.email = _customer.email;
                Model.phone = _customer.phone;
                Model.address = _customer.address;

                return View(Model);
            }

            return RedirectToAction(nameof(Index));
        }

        // POST: Customer/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid guid, CustomerViewModel model)
        {
            try
            {
                // TODO: Add update logic here
                //CustomerViewModel Model = new CustomerViewModel();

                if (guid != Guid.Empty)
                {
                    Customer _customer =  _customerService.GetCustomer(guid);

                    _customer.name = model.name;
                    _customer.email = model.email;
                    _customer.phone = model.phone;
                    _customer.address = model.address;
                     _customerService.UpdateCustomer(_customer);
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(model);
            }
        }

        // GET: Customer/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Customer/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}