using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApplication1.Controllers
{
    public class CustomersController : ApiController
    {

        private bancoPadraoEntities3 db = new bancoPadraoEntities3();

        public HttpResponseMessage Get()
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, db.Customer.ToList());
            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        public HttpResponseMessage Get(int id)
        {
            try
            {
                var customer = db.Customer.Find(id.ToString());

                if (customer == null)
                    return Request.CreateResponse(HttpStatusCode.NotFound);

                return Request.CreateResponse(HttpStatusCode.OK, customer);
            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        public HttpResponseMessage Post(Customer customer)
        {
            try
            {
                var existingCustomer = db.Customer.Find(customer.ID);

                if (existingCustomer != null)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Customer alredy exists");

                db.Customer.Add(customer);
                db.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        public HttpResponseMessage Put(int id, Customer customer)
        {
            try
            {
                var customerDB = db.Customer.FirstOrDefault(q => q.ID == id.ToString());

                if (customerDB == null)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Customer doesn't exists");

                customerDB.FullName = customer.FullName;
                customerDB.Email = customer.Email;
                customerDB.Mobile = customer.Mobile;
                customerDB.Location = customer.Location;
                db.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        public HttpResponseMessage Delete(string id)
        {
            try
            {
                var customer = db.Customer.Find(id);

                if (customer == null)
                    return Request.CreateResponse(HttpStatusCode.NotFound);

                db.Customer.Remove(customer);
                db.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }
    }
}
