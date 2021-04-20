using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FireCaffeDAL.Services;
using FireCaffeDAL.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireCaffeUnitTest
{
    [TestClass]
    public class DBUnitTesting
    {
        [TestMethod]
        public void DBCountAll()
        {
            ClientServices cs = new ClientServices();
            List<Client> clients = cs.GetClients();
            Assert.AreEqual(4, clients.Count);
        }
        [TestMethod]
        public void DBGetByPassword()
        {
            ClientServices cs = new ClientServices();
            List<Client> clients = cs.GetClientByPassword("c");
            Assert.AreEqual(1, clients.Count);
        }
        [TestMethod]
        public void DBGetByCreds()
        {
            ClientServices cs = new ClientServices();
            List<Client> clients = cs.GetClientByCred("bb","c");
            Assert.AreEqual(1, clients.Count);
        }
    }
}
