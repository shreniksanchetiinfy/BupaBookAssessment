using BupaBookAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;

namespace BupaBookAPI.Controllers
{
    public class BooksController : ApiController
    {
        [HttpGet]
        [ActionName("GetChildrenBookOwners")]
        public HttpResponseMessage GetChildrenBookOwners()
        {

            List<BookOwner> bookOwners = new List<BookOwner>();
            BookOwner bookOwner = new BookOwner();
            
            try
            {
                bookOwners = bookOwner.getDataAsync("Children").Result;
                if (bookOwners == null || bookOwners.Count == 0)
                {
                    var noContentResponse = new HttpResponseMessage(HttpStatusCode.NoContent)
                    {
                        Content = new StringContent("No data reterieved from source API"),
                        ReasonPhrase = "Data not received from source",
                        StatusCode = HttpStatusCode.NoContent
                    };
                    return noContentResponse;
                }
                return Request.CreateResponse(HttpStatusCode.OK, bookOwners);
            }
            catch (Exception ex)
            {
                var excpResp = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    ReasonPhrase = "Server error",
                    StatusCode = HttpStatusCode.InternalServerError
                };
                throw new HttpResponseException(excpResp);
            }
            
        }

        [HttpGet]
        [ActionName("GetAdultBookOwners")]
        public HttpResponseMessage GetAdultBookOwners()
        {

            List<BookOwner> bookOwners = new List<BookOwner>();
            BookOwner bookOwner = new BookOwner();
            try
            {
                bookOwners = bookOwner.getDataAsync("Adult").Result;

                if (bookOwners == null || bookOwners.Count == 0)
                {
                    var noContentResponse = new HttpResponseMessage(HttpStatusCode.NoContent)
                    {
                        Content = new StringContent("No data reterieved from source API"),
                        ReasonPhrase = "Data not received from source",
                        StatusCode = HttpStatusCode.NoContent
                    };
                    return noContentResponse;
                }
                return Request.CreateResponse(HttpStatusCode.OK,bookOwners);
            }
            catch (Exception ex)
            {
                var excpResp = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    ReasonPhrase = "Server error",
                    StatusCode = HttpStatusCode.InternalServerError
                };
                throw new HttpResponseException(excpResp);
            }

        }


    }
}
