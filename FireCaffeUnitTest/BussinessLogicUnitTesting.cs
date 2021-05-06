using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FireCaffeDAL.Services;
using FireCaffeDAL.Models;
using FireCaffeDAL;
using System.Collections.Generic;
using Moq;

namespace FireCaffeUnitTest
{
    [TestClass]
    public class BussinessLogicUnitTesting
    {

        [TestMethod]
        public void TestBuyNotOverflowMocking()
        {
            var mockContext = new Mock<FireCaffeDAL.Models.Client>();
            var client = new Mock<FireCaffeDAL.Models.IClient>();
            client.SetupGet(p => p.Id).Returns(1);
            client.SetupGet(p => p.SilverCups).Returns(2);
            client.SetupGet(p => p.GoldenCups).Returns(2);

            Assert.AreEqual(2, client.Object.SilverCups);
            Assert.AreEqual(2, client.Object.GoldenCups);
        }

        [TestMethod]
        public void TestBuyOverflowMocking()
        {
            var mockContext = new Mock<FireCaffeDAL.Models.Client>();
            var client = new Mock<FireCaffeDAL.Models.IClient>();
            var number = 12;
            client.SetupGet(p => p.Id).Returns(1);
            client.SetupGet(p => p.SilverCups).Returns(number);
            if (client.Object.SilverCups >= 10) { 
                client.SetupGet(p => p.SilverCups).Returns(number % 10);
                client.SetupGet(p => p.GoldenCups).Returns(number / 10);
            }
            Assert.AreEqual(2, client.Object.SilverCups);
            Assert.AreEqual(1, client.Object.GoldenCups);
        }
        [TestMethod]
        public void TestUseSilverCupsMocking()
        {
            var mockContext = new Mock<FireCaffeDAL.Models.Client>();
            var client = new Mock<FireCaffeDAL.Models.IClient>();
            client.SetupGet(p => p.Id).Returns(1);
            client.SetupGet(p => p.SilverCups).Returns(9);
            var number = client.Object.SilverCups - 1;
            Assert.AreEqual(8, number);

        }
        [TestMethod]
        public void TestUseGoldenCupsMocking()
        {
            var mockContext = new Mock<FireCaffeDAL.Models.Client>();
            var client = new Mock<FireCaffeDAL.Models.IClient>();
            client.SetupGet(p => p.Id).Returns(1);
            client.SetupGet(p => p.GoldenCups).Returns(9);
            var number = client.Object.GoldenCups - 1;
            client.SetupGet(p => p.GoldenCups).Returns(number);
            Assert.AreEqual(8, client.Object.GoldenCups);

        }

        [TestMethod]
        public void TestProductNameMocking()
        {
            var mockContext = new Mock<FireCaffeDAL.Models.Product>();
            var client = new Mock<FireCaffeDAL.Models.IProduct>();
            client.SetupGet(p => p.Name).Returns("Latte");
            Assert.AreEqual("Latte", client.Object.Name);

        }
        [TestMethod]
        public void TestProductPriceMocking()
        {
            var mockContext = new Mock<FireCaffeDAL.Models.Product>();
            var client = new Mock<FireCaffeDAL.Models.IProduct>();
            client.SetupGet(p => p.Price).Returns(15);
            Assert.AreEqual(15, client.Object.Price);

        }
    }

}
