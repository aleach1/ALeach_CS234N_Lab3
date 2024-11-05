using NUnit.Framework;

using MMABooksBusiness;
using MMABooksProps;
using MMABooksDB;

using DBCommand = MySql.Data.MySqlClient.MySqlCommand;
using System.Data;

using System.Collections.Generic;
using System;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.Pkcs;

namespace MMABooksTests
{
    [TestFixture]
    public class ProductTests
    {

        [SetUp]
        public void TestResetDatabase()
        {
            ProductDB db = new ProductDB();
            DBCommand command = new DBCommand();
            command.CommandText = "usp_testingResetProductData";
            command.CommandType = CommandType.StoredProcedure;
            db.RunNonQueryProcedure(command);
        }

        [Test]
        public void TestNewStateConstructor()
        {
            // not in Data Store - no code
            Product p = new Product();
            Assert.AreEqual(string.Empty, p.Code);
            Assert.AreEqual(string.Empty, p.Description);
            Assert.AreEqual(0, p.ID);
            Assert.AreEqual(0, p.UnitPrice);
            Assert.AreEqual(0, p.Quantity);
            Assert.IsTrue(p.IsNew);
            Assert.IsFalse(p.IsValid);
        }


        [Test]
        public void TestRetrieveFromDataStoreContructor()
        {
            // retrieves from Data Store
            Product p = new Product(4);
            Assert.AreEqual(4, p.ID);
            Assert.IsTrue(p.Description.Length > 0);
            Assert.IsFalse(p.IsNew);
            Assert.IsTrue(p.IsValid);
        }

        [Test]
        public void TestSaveToDataStore()
        {
            Product p = new Product();
            p.ID = 17;
            p.Code = "AZU9";
            p.Description = "Description of object";
            p.UnitPrice = 44;
            p.Quantity = 10;
            p.Save();
            Product p2 = new Product(17);
            Assert.AreEqual(p2.ID, p.ID);
            Assert.AreEqual(p2.Code, p.Code);
        }

        [Test]
        public void TestUpdate()
        {
            Product p = new Product(4);
            p.Description = "Edited Description";
            p.Save();

            Product p2 = new Product(4);
            Assert.AreEqual(p2.ID, p.ID);
            Assert.AreEqual(p2.Code, p.Code);
        }

        [Test]
        public void TestDelete()
        {
            Product p = new Product(6);
            p.Delete();
            p.Save();
            Assert.Throws<Exception>(() => new Product(6));
        }

        [Test]
        public void TestGetList()
        {
            Product p = new Product();
            List<Product> products = (List<Product>)p.GetList();
            Assert.AreEqual(16, products.Count);
            Assert.AreEqual(1, products[0].ID);
            Assert.AreEqual("A4CS", products[0].Code);
        }

        [Test]
        public void TestNoRequiredPropertiesNotSet()
        {
            // not in Data Store - abbreviation and name must be provided
            Product p = new Product();
            Assert.Throws<Exception>(() => p.Save());
        }

        [Test]
        public void TestSomeRequiredPropertiesNotSet()
        {
            // not in Data Store - abbreviation and name must be provided
            Product p = new Product();
            Assert.Throws<Exception>(() => p.Save());
            p.Description = "Happy fun time";
            Assert.Throws<Exception>(() => p.Save());
        }

        [Test]
        public void TestInvalidPropertySet()
        {
            Product p = new Product();
            Assert.Throws<ArgumentOutOfRangeException>(() => p.Code = "????????????");
        }

        [Test]
        public void TestConcurrencyIssue()
        {
            Product p1 = new Product(4);
            Product p2 = new Product(4);

            p1.Description = "Updated first";
            p1.Save();

            p2.Description = "Updated second";
            Assert.Throws<Exception>(() => p2.Save());
        }
    }
}