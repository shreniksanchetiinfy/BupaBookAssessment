using Microsoft.VisualStudio.TestTools.UnitTesting;
using BupaBookAPI.Models;
using System.Collections.Generic;
using System;

namespace BooksApiUnitTest
{
    [TestClass]
    public class BookOwnerTest
    {

        /// <summary>
        /// This test method will check if data is available at source system
        /// </summary>
        [TestMethod]
        public void SourceDataTest()
        {
            BookOwner bookOwner = new BookOwner();
            List<BookOwner> bookOwners = new List<BookOwner>();
            bookOwners = bookOwner.getDataAsync("Children").Result;
            Assert.IsTrue(bookOwners.Count > 0);
        }
    }
}
