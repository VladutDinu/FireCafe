using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FireCaffeDAL.Services;
using FireCaffeDAL.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;

namespace FireCaffeUnitTest
{
    [TestClass]
    public class DBUnitTesting
    {
        [TestMethod]
        public void DBMockClientNameTest()
        {
            var mockContext = new Mock<FireCaffeDAL.Models.Client>();
            var client = new Mock<FireCaffeDAL.Models.IClient>();
            client.SetupGet(p => p.Id).Returns(1);
            client.SetupGet(p => p.FirstName).Returns("Joydip");
            client.SetupGet(p => p.LastName).Returns("Kanjilal");
            Assert.AreEqual("Joydip", client.Object.FirstName);
            Assert.AreEqual("Kanjilal", client.Object.LastName);
        }
        [TestMethod]
        public void DBMockClientPasswordTest()
        {
            var mockContext = new Mock<FireCaffeDAL.Models.Client>();
            var client = new Mock<FireCaffeDAL.Models.IClient>();
            client.SetupGet(p => p.Id).Returns(1);
            client.SetupGet(p => p.Password).Returns("Joydip");
            Assert.AreEqual("Joydip", client.Object.Password);

        }
        [TestMethod]
        public void DBMockProductDescriptionTest()
        {
            var mockContext = new Mock<FireCaffeDAL.Models.Product>();
            var client = new Mock<FireCaffeDAL.Models.IProduct>();
            client.SetupGet(p => p.Id).Returns(1);
            client.SetupGet(p => p.Description).Returns("Joydip");
            Assert.AreEqual("Joydip", client.Object.Description);
        }
        [TestMethod]
        public void DBMockProductTypeTest()
        {
            var mockContext = new Mock<FireCaffeDAL.Models.Product>();
            var client = new Mock<FireCaffeDAL.Models.IProduct>();
            client.SetupGet(p => p.Id).Returns(1);
            client.SetupGet(p => p.Type).Returns("Joydip");
            Assert.AreEqual("Joydip", client.Object.Type);

        }
    }
}
