using NUnit.Framework;

using MMABooksBusiness;
using MMABooksProps;
using MMABooksDB;

using DBCommand = MySql.Data.MySqlClient.MySqlCommand;
using System.Data;

using System.Collections.Generic;
using System;
using MySql.Data.MySqlClient;

namespace MMABooksTests
{
    [TestFixture]
    public class CustomerTests
    {

        [SetUp]
        public void TestResetDatabase()
        {
            CustomerDB db = new CustomerDB();
            DBCommand command = new DBCommand();
            command.CommandText = "usp_testingResetData";
            command.CommandType = CommandType.StoredProcedure;
            db.RunNonQueryProcedure(command);
        }

        [Test]
        public void TestNewCustomerConstructor()
        {
            // not in Data Store - no code
            Customer c = new Customer();
            //Assert.AreEqual(, c.CustomerID);
            Assert.AreEqual(string.Empty, c.Name);
            Assert.AreEqual(string.Empty, c.Address);
            Assert.AreEqual(string.Empty, c.City);
            Assert.AreEqual(string.Empty, c.State);
            Assert.AreEqual(string.Empty, c.ZipCode);
            Assert.IsTrue(c.IsNew);
            Assert.IsFalse(c.IsValid);
        }


        [Test]
        public void TestRetrieveFromDataStoreContructor()
        {
            // retrieves from Data Store
            Customer c = new Customer(5);
            Assert.AreEqual(5, c.CustomerID);
            Assert.IsTrue(c.Name.Length > 0);
            Assert.IsFalse(c.IsNew);
            Assert.IsTrue(c.IsValid);
        }
        

        [Test]
        public void TestUpdate()
        {
            Customer c = new Customer(5);
            c.Name = "Edited Name";
            c.Save();

            Customer c2 = new Customer(5);
            Assert.AreEqual(c2.CustomerID, c.CustomerID);
            Assert.AreEqual(c2.Name, c.Name);
        }

        [Test]
        public void TestDelete()
        {
            Customer c = new Customer(1);
            c.Delete();
            c.Save();
            Assert.Throws<Exception>(() => new Customer(1));
        }

        [Test]
        public void TestGetList()
        {
            Customer c = new Customer();
            List<Customer> customers = (List<Customer>)c.GetList();
            Assert.AreEqual(696, customers.Count);
            Assert.AreEqual(2, customers[1].CustomerID);
            Assert.AreEqual("Muhinyi, Mauda", customers[1].Name);
            Assert.AreEqual("1420 North Charles Street", customers[1].Address);
            Assert.AreEqual("New York", customers[1].City);
            Assert.AreEqual("NY", customers[1].State);
            Assert.AreEqual("10044", customers[1].ZipCode);
        }

        [Test]
        public void TestNoRequiredPropertiesNotSet()
        {
            // not in Data Store - abbreviation and name must be provided
            Customer c = new Customer();
            Assert.Throws<Exception>(() => c.Save());
        }

        [Test]
        public void TestSomeRequiredPropertiesNotSet()
        {
            // not in Data Store - abbreviation and name must be provided
            Customer c = new Customer();
            Assert.Throws<Exception>(() => c.Save());
            c.Name = "??";
            Assert.Throws<Exception>(() => c.Save());
        }

        [Test]
        public void TestInvalidPropertySet()
        {
            Customer c = new Customer();
            Assert.Throws<ArgumentOutOfRangeException>(() => c.State = "???");
        }

        [Test]
        public void TestConcurrencyIssue()
        {
            Customer c1 = new Customer(5);
            Customer c2 = new Customer(5);

            c1.Name = "Updated first";
            c1.Save();

            c2.Name = "Updated second";
            Assert.Throws<Exception>(() => c2.Save());
        }


        [Test]
        public void TestSaveToDataStore()
        {
            Customer c = new Customer();
            c.CustomerID = 700;
            c.Name = "Where am I";
            c.Address = "address";
            c.City = "Moscow";
            c.State = "FL";
            c.ZipCode = "97779";
            c.Save();
            Customer c2 = new Customer(c.CustomerID);
            Assert.AreEqual(c2.CustomerID, c.CustomerID);
            Assert.AreEqual(c2.Name, c.Name);
        }
    }
}