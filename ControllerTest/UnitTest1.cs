using System;
using System.Collections.Generic;
using System.Net.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelLib.Model;
using SmartCanteenREST.Controllers;

namespace ControllerTest
{
    [TestClass]
    public class UnitTest1
    {
        private ProductsController cntrl = null;

        [TestInitialize]
        public void SetUp()
        {
            cntrl = new ProductsController();
        }

        [TestMethod]
        public void GetProducts()
        {
            List<Products> liste = new List<Products>(cntrl.Get());

            Assert.AreEqual(27, liste.Count);
        }
    }
}
