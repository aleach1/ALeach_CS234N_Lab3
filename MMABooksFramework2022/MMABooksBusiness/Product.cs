using System;

using MMABooksTools;
using MMABooksProps;
using MMABooksDB;

using System.Collections.Generic;

namespace MMABooksBusiness
{
    public class Product : BaseBusiness
    {

        public int ID
        {
            get
            {
                return ((ProductProps)mProps).ProductID;
            }
            set
            {
                if (!(value == ((ProductProps)mProps).ProductID))
                {
                    if (value.GetType() == typeof(int))
                    {
                        mRules.RuleBroken("ID", false);
                        ((ProductProps)mProps).ProductID = value;
                        mIsDirty = true;
                    }

                    else
                    {
                        throw new ArgumentOutOfRangeException("ID must be an integer.");
                    }
                }
            }
        }

        public string Code
        {
            get
            {
                return ((ProductProps)mProps).ProductCode;
            }
            set
            {
                if (!(value == ((ProductProps)mProps).ProductCode))
                {
                    if (value.Trim().Length >= 1 && value.Trim().Length <= 10)
                    {
                        mRules.RuleBroken("Code", false);
                        ((ProductProps)mProps).ProductCode = value;
                        mIsDirty = true;
                    }

                    else
                    {
                        throw new ArgumentOutOfRangeException("Code must be no more than 10 characters long.");
                    }
                }
            }
        }

        public string Description
        {
            get
            {
                return ((ProductProps)mProps).Description;
            }
            set
            {
                if (!(value == ((ProductProps)mProps).Description))
                {
                    if (value.Trim().Length >= 1 && value.Trim().Length <= 100)
                    {
                        mRules.RuleBroken("Description", false);
                        ((ProductProps)mProps).Description = value;
                        mIsDirty = true;
                    }

                    else
                    {
                        throw new ArgumentOutOfRangeException("Description must be no more than 100 characters long.");
                    }
                }
            }
        }

        public decimal UnitPrice
        {
            get
            {
                return ((ProductProps)mProps).UnitPrice;
            }
            set
            {
                if (!(value == ((ProductProps)mProps).UnitPrice))
                {
                    if (value.GetType() == typeof(decimal))
                    {
                        mRules.RuleBroken("UnitPrice", false);
                        ((ProductProps)mProps).UnitPrice = value;
                        mIsDirty = true;
                    }

                    else
                    {
                        throw new ArgumentOutOfRangeException("Unit Price must be a decimal.");
                    }
                }
            }
        }

        public int Quantity
        {
            get
            {
                return ((ProductProps)mProps).OnHandQuantity;
            }
            set
            {
                if (!(value == ((ProductProps)mProps).OnHandQuantity))
                {
                    if (value.GetType() == typeof(int))
                    {
                        mRules.RuleBroken("Quantity", false);
                        ((ProductProps)mProps).OnHandQuantity = value;
                        mIsDirty = true;
                    }

                    else
                    {
                        throw new ArgumentOutOfRangeException("Quantity must be an integer.");
                    }
                }
            }
        }
        public override object GetList()
        {
            List<Product> products = new List<Product>();
            List<ProductProps> props = new List<ProductProps>();


            props = (List<ProductProps>)mdbReadable.RetrieveAll();
            foreach (ProductProps prop in props)
            {
                Product p = new Product(prop);
                products.Add(p);
            }

            return products;
        }

        protected override void SetDefaultProperties()
        {
        }

        protected override void SetRequiredRules()
        {
            mRules.RuleBroken("ID", true);
            mRules.RuleBroken("Code", true);
            mRules.RuleBroken("Description", true);
            mRules.RuleBroken("UnitPrice", true);
            mRules.RuleBroken("Quantity", true);
        }

        protected override void SetUp()
        {
            mProps = new ProductProps();
            mOldProps = new ProductProps();

            mdbReadable = new ProductDB();
            mdbWriteable = new ProductDB();
        }

        #region constructors
        /// <summary>
        /// Default constructor - gets the connection string - assumes a new record that is not in the database.
        /// </summary>
        public Product() : base()
        {
        }

        /// <summary>
        /// Calls methods SetUp() and Load().
        /// Use this constructor when the object is in the database AND the connection string is in a config file
        /// </summary>
        /// <param name="key">ID number of a record in the database.
        /// Sent as an arg to Load() to set values of record to properties of an 
        /// object.</param>
        public Product(int key)
            : base(key)
        {
        }

        private Product(ProductProps props)
            : base(props)
        {
        }

        #endregion
    }
}
