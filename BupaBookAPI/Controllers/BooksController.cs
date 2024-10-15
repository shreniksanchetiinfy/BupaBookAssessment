using BupaBookAPI.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BupaBookAPI.Controllers
{
    public class BooksController : ApiController
    {
        [HttpGet]
        [ActionName("GetChildrenBookOwners")]
        /// <summary>
        /// This API will return the book owners who are children that is having age less than 18 years.
        /// To call this API please use "http://localhost:59677/api/Books/GetChildrenBookOwners" link
        /// </summary>

        public HttpResponseMessage GetChildrenBookOwners()
        {

            List<BookOwner> bookOwners = new List<BookOwner>();
            BookOwner bookOwner = new BookOwner();
            
            try
            {
                bookOwners = bookOwner.getDataAsync("Children").Result;
                if (bookOwners == null || bookOwners.Count == 0)
                {
                    //if empty object is received from source then return no content response.
                    var noContentResponse = new HttpResponseMessage(HttpStatusCode.NoContent)
                    {
                        Content = new StringContent("No data reterieved from source API"),
                        ReasonPhrase = "Data not received from source",
                        StatusCode = HttpStatusCode.NoContent
                    };
                    return noContentResponse;
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, bookOwners);
                }
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

        /// <summary>
        /// This API will return the book owners who are adult that is having age more than or equal to 18 years.
        /// To call this API please use "http://localhost:59677/api/Books/GetAdultBookOwners" link
        /// </summary>
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
                    //if empty object is received from source then return no content response.
                    var noContentResponse = new HttpResponseMessage(HttpStatusCode.NoContent)
                    {
                        Content = new StringContent("No data reterieved from source API"),
                        ReasonPhrase = "Data not received from source",
                        StatusCode = HttpStatusCode.NoContent
                    };
                    return noContentResponse;
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, bookOwners);
                }
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
