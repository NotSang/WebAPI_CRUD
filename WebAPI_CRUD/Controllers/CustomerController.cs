using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI_CRUD.Models;

namespace WebAPI_CRUD.Controllers
{
    public class CustomerController : ApiController
    {
        //GET - retrieve data
        public IHttpActionResult GetAllCustomer()
        {
            IList<CustomerViewModel> customer = null;
            using(var x=new WebAPIEntities1())
            {
                customer = x.Customers
                    .Select(c => new CustomerViewModel()
                    {
                        id = c.id,
                        name = c.name,
                        email = c.email,
                        address = c.address,
                        phone = c.phone
                    }).ToList<CustomerViewModel>();
            }
            if (customer.Count == 0)
                return NotFound();
            return Ok(customer);
        }
        //POST - insert new record
        public IHttpActionResult PostNewCustomer(CustomerViewModel customer)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data. Kiem tra lai");
            using (var x=new WebAPIEntities1())
            {
                x.Customers.Add(new Customer()
                {
                    name = customer.name,
                    email = customer.email,
                    address = customer.address,
                    phone = customer.phone
                });
                x.SaveChanges();
            }
            return Ok();
          
        }
        //PUT update data base on id
        public IHttpActionResult PutCustomer (CustomerViewModel customer)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data. Kiem tra lai");
            using (var x = new WebAPIEntities1())
            {
                var checkExsitingCustomer = x.Customers.Where(c => c.id == customer.id).FirstOrDefault<Customer>();
                if (checkExsitingCustomer != null)
                {
                    checkExsitingCustomer.name = customer.name;
                    checkExsitingCustomer.address = customer.address;
                    checkExsitingCustomer.phone = customer.phone;
                    x.SaveChanges();
                }
                else
                    return NotFound();
            }
            return Ok();
        }
        //DELETE
        public IHttpActionResult Delete(int id)
        {
            if(id<=0)
                return BadRequest("Invalid data. Kiem tra lai");
            using (var x = new WebAPIEntities1())
            {
                var customer = x.Customers
                    .Where(c => c.id == id).FirstOrDefault();
                x.Entry(customer).State = System.Data.Entity.EntityState.Deleted;
                x.SaveChanges();
              
            }
            return Ok();
        }

    }
}
