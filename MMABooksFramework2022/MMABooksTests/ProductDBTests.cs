using NUnit.Framework;

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
    public class ProductDBTests
    {
        ProductDB db;

        [SetUp]
        public void ResetData()
        {
            db = new ProductDB();
            DBCommand command = new DBCommand();
            command.CommandText = "usp_testingResetProductData";
            command.CommandType = CommandType.StoredProcedure;
            db.RunNonQueryProcedure(command);
        }

        [Test]
        public void TestRetrieve()
        {
            ProductProps p = (ProductProps)db.Retrieve(4);
            Assert.AreEqual(4, p.ProductID);
            Assert.AreEqual("ADV4", p.ProductCode);
        }

        [Test]
        public void TestRetrieveAll()
        {
            List<ProductProps> list = (List<ProductProps>)db.RetrieveAll();
            Assert.AreEqual(16, list.Count);
        }

        [Test]
        public void TestDelete()
        {
            ProductProps p = (ProductProps)db.Retrieve(1);
            Assert.True(db.Delete(p));
            Assert.Throws<Exception>(() => db.Retrieve(1));
        }




        [Test]
        public void TestUpdate()
        {
            ProductProps p = (ProductProps)db.Retrieve(5);
            p.Description = "Big Book of All Things";
            Assert.True(db.Update(p));
            p = (ProductProps)db.Retrieve(5);
            Assert.AreEqual("Big Book of All Things", p.Description);
        }

        [Test]
        public void TestUpdateFieldTooLong()
        {
            ProductProps p = (ProductProps)db.Retrieve(5);
            p.ProductCode = "the code for a product";
            Assert.Throws<MySqlException>(() => db.Update(p));
        }

        [Test]
        public void TestCreate()
        {
            ProductProps p = new ProductProps();
            p.ProductCode = "AZ58";
            p.Description = "Big Book of All Things";
            p.UnitPrice = (decimal)56.0001;
            p.OnHandQuantity = 24;
            ProductProps tempId = (ProductProps)db.Create(p);
            ProductProps p2 = (ProductProps)db.Retrieve(tempId.ProductID);
            Assert.AreEqual(p.GetState(), p2.GetState());
        }

    }
}
