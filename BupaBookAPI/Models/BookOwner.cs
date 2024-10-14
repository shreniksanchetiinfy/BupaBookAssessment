using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace BupaBookAPI.Models
{
    public class BookOwner
    {
        private string _ageCategory;
        private string _name;
        private int _age;
        private List<Book> _books;


        public string ageCategory
        {
            get { return _ageCategory; }
            set { _ageCategory = value; }
        }
        public string name
        {
            get { return _name; }
            set { _name = value; }
        }

        public int age
        {
            get { return _age; }
            set { _age = value; }
        }


        public List<Book> books
        {
            get { return _books; }
            set { _books = value; }
        }


        public async Task<List<BookOwner>> getDataAsync(string ageCategory)
        {
            List<BookOwner> bookOwners = new List<BookOwner>();
            List<BookOwner> bookownersWithCategory = new List<BookOwner>();
            string sourceURI = "https://digitalcodingtest.bupa.com.au/api/v1/bookowners";


            var client = new HttpClient();

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
               new MediaTypeWithQualityHeaderValue("application/json"));
            // Get data response
            var response = client.GetAsync(sourceURI).Result;
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var bookOwnerJSONStr = await response.Content.ReadAsStringAsync();
                bookOwners = JsonConvert.DeserializeObject<List<BookOwner>>(bookOwnerJSONStr);

            }
            if (bookOwners != null)
            {
                foreach (BookOwner bkOwner in bookOwners)
                {
                    bkOwner.ageCategory = bkOwner.age >= 18 ? "Adult" : "Children";
                    bkOwner.books = bkOwner.books.OrderBy(x => x.name).ToList();
                    bookownersWithCategory.Add(bkOwner);
                }
                bookownersWithCategory = bookownersWithCategory.Where(x => x.ageCategory == ageCategory).ToList();
                return bookownersWithCategory;
            }
            else
            {
                return new List<BookOwner>();
            }
        }
    }
}