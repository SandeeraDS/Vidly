using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class CustomersController : ApiController
    {
        private VidlyContext _context;

        public CustomersController()
        {
            _context = new VidlyContext();
        }

        //Get /api/customers
        [HttpGet]
        public IEnumerable<CustomerDto> GetCustomers()
        {
            return _context.Customers.ToList().Select(Mapper.Map<Customer,CustomerDto>); 
        }   

        //Get /api/customers/1
        [HttpGet]
        public IHttpActionResult GetCustomers(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);
            if(customer == null)
            {
                return NotFound();
            }
            else
            {
                return  Ok(Mapper.Map<Customer,CustomerDto>(customer));

            }
        }

        //Post api/customer
        [HttpPost]
        public IHttpActionResult CreateCustomer(CustomerDto customerDto) {

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                var customer = Mapper.Map<CustomerDto, Customer>(customerDto);
                _context.Customers.Add(customer);
                _context.SaveChanges();
                customerDto.Id = customer.Id;//customer containn the new id of database
                return Created(new Uri(Request.RequestUri+"/"+customer.Id),customerDto);
            }
        }

        //Put /api/customer/id
        [HttpPut]
        public void UpdateCustomer(int id,CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            else
            {
                var customerInDb = _context.Customers.SingleOrDefault(c=>c.Id==id);

                if (customerInDb == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }
                else
                {
                    Mapper.Map(customerDto, customerInDb); //directly map with database item 
                    //customerInDb.Name = customer.Name;
                    //customerInDb.Birthdate = customer.Birthdate;
                    //customerInDb.IsSubcribeToNewsletter = customer.IsSubcribeToNewsletter;
                    //customerInDb.MembershipTypeId = customer.MembershipTypeId;

                    _context.SaveChanges();

                }

            }
        }

        //Delete /api/customer/id
        [HttpDelete]
        public void DeleteCustomer(int id)
        {
            var customerInDb = _context.Customers.SingleOrDefault(c=>c.Id==id);
            if (customerInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);

            }
            else
            {
                _context.Customers.Remove(customerInDb);
                _context.SaveChanges();
            }
        }


    }
}
