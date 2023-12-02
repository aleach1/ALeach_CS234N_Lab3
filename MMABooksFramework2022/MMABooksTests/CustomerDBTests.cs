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
    public class CustomerDBTests
    {
        CustomerDB db;

        [SetUp]
        public void ResetData()
        {
            db = new CustomerDB();
            DBCommand command = new DBCommand();
            command.CommandText = "usp_testingResetData";
            command.CommandType = CommandType.StoredProcedure;
            db.RunNonQueryProcedure(command);
        }

        [Test]
        public void TestRetrieve()
        {
            CustomerProps p = (CustomerProps)db.Retrieve(1);
            Assert.AreEqual(1, p.CustomerID);
            Assert.AreEqual("Molunguri, A", p.Name);
        }

        [Test]
        public void TestRetrieveAll()
        {
            List<CustomerProps> list = (List<CustomerProps>)db.RetrieveAll();
            Assert.AreEqual(696, list.Count);
        }

        [Test]
        public void TestDelete()
        {
            CustomerProps p = (CustomerProps)db.Retrieve(1);
            Assert.True(db.Delete(p));
            Assert.Throws<Exception>(() => db.Retrieve(1));
        }


        

        [Test]
        public void TestUpdate()
        {
            CustomerProps p = (CustomerProps)db.Retrieve(5);
            p.Name = "Mouse, Mickey";
            Assert.True(db.Update(p));
            p = (CustomerProps)db.Retrieve(5);
            Assert.AreEqual("Mouse, Mickey", p.Name);
        }

        [Test]
        public void TestUpdateFieldTooLong()
        {
            CustomerProps p = (CustomerProps)db.Retrieve(5);
            p.State = "Oregon is the state where Crater Lake National Park is.";
            Assert.Throws<MySqlException>(() => db.Update(p));
        }

        [Test]
        public void TestCreate()
        {
            CustomerProps p = new CustomerProps();
            p.Name = "Where am I";
            p.Address = "Where am I";
            p.City = "Where am I";
            p.State = "OR";
            p.ZipCode = "98653";
            CustomerProps tempId = (CustomerProps)db.Create(p);
            CustomerProps p2 = (CustomerProps)db.Retrieve(tempId.CustomerID);
            Assert.AreEqual(p.GetState(), p2.GetState());
        }

    }
}
