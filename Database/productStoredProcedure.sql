DELIMITER // 
CREATE PROCEDURE usp_ProductSelect (in prodId int)
BEGIN
	Select * from products where ProductID=prodId;
END //
DELIMITER ;

DELIMITER // 
CREATE PROCEDURE usp_ProductSelectAll ()
BEGIN
	Select * from products order by ProductID;
END //
DELIMITER ;

DELIMITER // 
CREATE PROCEDURE usp_ProductUpdate (in prodId int, in productcode_p varchar(10), in description_p varchar(50), in unitprice_p decimal(10,4), in onhandquantity_p int, in conCurrId int)
BEGIN
	Update products
    Set ProductCode = productcode_p, Description = description_p, UnitPrice = unitprice_p, OnHandQuantity = onhandquantity_p, concurrencyid = (concurrencyid + 1)
    Where ProductID = prodId and concurrencyid = conCurrId;
END //
DELIMITER ;

DELIMITER // 
CREATE PROCEDURE usp_ProductDelete (in prodId int, in conCurrId int)
BEGIN
	Delete from products where ProductID = prodId and ConcurrencyID = conCurrId;
END //
DELIMITER ; 

DELIMITER // 
CREATE PROCEDURE usp_ProductCreate (out prodId int, in productcode_p varchar(10), in description_p varchar(50), in unitprice_p decimal(10,4), in onhandquantity_p int)
BEGIN
	Insert into products (productcode, description, unitprice, onhandquantity, concurrencyid)
    Values (productcode_p, description_p, unitprice_p, onhandquantity_p, 1);
    Select LAST_INSERT_ID() into prodId;
END //
DELIMITER ;