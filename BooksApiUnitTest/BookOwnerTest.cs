using Microsoft.VisualStudio.TestTools.UnitTesting;
using BupaBookAPI.Models;
using System.Collections.Generic;

namespace BooksApiUnitTest
{
    [TestClass]
    public class BookOwnerTest
    {

        /// <summary>
        /// This test method will check if data for children book owners is available at source system
        /// </summary>
        [TestMethod]
        public void SourceDataChildrenTest()
        {
            BookOwner bookOwner = new BookOwner();
            List<BookOwner> bookOwners = new List<BookOwner>();
            bookOwners = bookOwner.getDataAsync("Children").Result;
            Assert.IsTrue(bookOwners.Count > 0);
        }

        /// <summary>
        /// This test method will check if data for adult book owners is available at source system
        /// </summary>
        [TestMethod]
        public void SourceDataAdultTest()
        {
            BookOwner bookOwner = new BookOwner();
            List<BookOwner> bookOwners = new List<BookOwner>();
            bookOwners = bookOwner.getDataAsync("Adult").Result;
            Assert.IsTrue(bookOwners.Count > 0);
        }
    }
}
