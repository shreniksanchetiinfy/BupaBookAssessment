using Microsoft.VisualStudio.TestTools.UnitTesting;
using BupaBookAPI.Models;
using System.Net.Http;
using System.Net;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace BooksApiUnitTest
{
    [TestClass]
    public class BookAPITest
    {
        private List<BookOwner> bookOwners = new List<BookOwner>();
        HttpResponseMessage getResponse(string sourceURI)//, out string jsonString)
        {
            var client = new HttpClient();

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
               new MediaTypeWithQualityHeaderValue("application/json"));

            var response = client.GetAsync(sourceURI).Result;
            //jsonString = response.Content.read
            return response;

        }
        /// <summary>
        /// This test method will check if the children book owners api is sending 200 ok status
        /// </summary>
        [TestMethod]
        public void okResponseForChildrenBookOwnersTest()
        {
            // Get responce from common method
            var response = getResponse("http://localhost:59677/api/Books/GetChildrenBookOwners");
            // Assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        /// <summary>
        /// This test method will check if the adult book owners api is sending 200 ok status
        /// </summary>
        [TestMethod]
        public void okResponseForAdultBookOwnersTest()
        {
            // Get responce from common method
            var response = getResponse("http://localhost:59677/api/Books/GetAdultBookOwners");
            // Assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        /// <summary>
        /// This test method will check when adult book owner API is called then only adult book owner related data is returned
        /// </summary>
        [TestMethod]
        public  void OnlyAdultOwnerDataIsReturnedTest()
        {
            // Get responce from common method
            var response = getResponse("http://localhost:59677/api/Books/GetAdultBookOwners");
            // Assert
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var bookOwnerJSONStr =  response.Content.ReadAsStringAsync().Result;
                bookOwners = JsonConvert.DeserializeObject<List<BookOwner>>(bookOwnerJSONStr.ToString());

            }
            Assert.IsTrue(bookOwners.Where(x => x.ageCategory != "Adult").ToList().Count == 0);
        }

        /// <summary>
        /// This test method will check when Children book owner API is called then only adult book owner related data is returned
        /// </summary>
        [TestMethod]
        public void OnlyChildrenOwnerDataIsReturnedTest()
        {
            // Get responce from common method
            var response = getResponse("http://localhost:59677/api/Books/GetChildrenBookOwners");
            // Assert
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var bookOwnerJSONStr =  response.Content.ReadAsStringAsync().Result;
                bookOwners = JsonConvert.DeserializeObject<List<BookOwner>>(bookOwnerJSONStr.ToString());

            }
            Assert.IsTrue(bookOwners.Where(x => x.ageCategory != "Children").ToList().Count == 0);
        }

        /// <summary>
        /// This test method will check all the books owned by a children is sorted by name of the book
        /// </summary>
        [TestMethod]
        public void AllChildrenBooksAreSortedByNameTest()
        {
            // Get responce from common method

            var response = getResponse("http://localhost:59677/api/Books/GetChildrenBookOwners");
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var bookOwnerJSONStr =  response.Content.ReadAsStringAsync().Result;
                bookOwners = JsonConvert.DeserializeObject<List<BookOwner>>(bookOwnerJSONStr.ToString());
            }
            List<BookOwner> sortedBooksOfBookOwners = new List<BookOwner>();
            foreach(BookOwner bookOwner in bookOwners)
            {
                bookOwner.books = bookOwner.books.OrderBy(x => x.name).ToList();
                sortedBooksOfBookOwners.Add(bookOwner);
            }
            CollectionAssert.AreEqual(bookOwners, sortedBooksOfBookOwners);

        }

        /// <summary>
        /// This test method will check all the books owned by a adults is sorted by name of the book
        /// </summary>
        [TestMethod]
        public void AllAdultBooksAreSortedByNameTest()
        {
            // Get responce from common method

            var response = getResponse("http://localhost:59677/api/Books/GetAdultBookOwners");
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var bookOwnerJSONStr =  response.Content.ReadAsStringAsync().Result;
                bookOwners = JsonConvert.DeserializeObject<List<BookOwner>>(bookOwnerJSONStr.ToString());
            }
            List<BookOwner> sortedBooksOfBookOwners = new List<BookOwner>();
            foreach (BookOwner bookOwner in bookOwners)
            {
                bookOwner.books = bookOwner.books.OrderBy(x => x.name).ToList();
                sortedBooksOfBookOwners.Add(bookOwner);
            }
            CollectionAssert.AreEqual(bookOwners, sortedBooksOfBookOwners);

        }

        /// <summary>
        /// This test method will check all the book owners having age greater than or equal to 18 is marked as Adult
        /// </summary>
        [TestMethod]
        public void AgeCategoryForAdultBookOwnerTest()
        {
            // Get responce from common method
            var response = getResponse("http://localhost:59677/api/Books/GetAdultBookOwners");
            // Assert
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var bookOwnerJSONStr = response.Content.ReadAsStringAsync().Result;
                bookOwners = JsonConvert.DeserializeObject<List<BookOwner>>(bookOwnerJSONStr.ToString());

            }
            Assert.IsTrue(bookOwners.Where(x => x.ageCategory == "Adult" && x.age >= 18).ToList().Count == bookOwners.Count);
        }


        /// <summary>
        /// This test method will check all the book owners having age less than 18 is marked as children
        /// </summary>
        [TestMethod]
        public void AgeCategoryForChildrenBookOwnerTest()
        {
            // Get responce from common method
            var response = getResponse("http://localhost:59677/api/Books/GetChildrenBookOwners");
            // Assert
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var bookOwnerJSONStr = response.Content.ReadAsStringAsync().Result;
                bookOwners = JsonConvert.DeserializeObject<List<BookOwner>>(bookOwnerJSONStr.ToString());

            }
            Assert.IsTrue(bookOwners.Where(x => x.ageCategory == "Children" && x.age < 18).ToList().Count == bookOwners.Count);
        }

    }
}
