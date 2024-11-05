DELIMITER // 
CREATE PROCEDURE usp_CustomerSelect (in custId int)
BEGIN
	Select * from customers where CustomerID=custId;
END //
DELIMITER ;

DELIMITER // 
CREATE PROCEDURE usp_CustomerSelectAll ()
BEGIN
	Select * from customers order by CustomerID;
END //
DELIMITER ;

DELIMITER // 
CREATE PROCEDURE usp_CustomerUpdate (in custId int, in name_p varchar(100), in address_p varchar(50), in city_p varchar(20), in state_p varchar(2), in zipcode_p varchar(15), in conCurrId int)
BEGIN
	Update customers
    Set Name = name_p, Address = address_p, City = city_p, State = state_p, ZipCode = zipcode_p, concurrencyid = (concurrencyid + 1)
    Where CustomerID = custId and concurrencyid = conCurrId;
END //
DELIMITER ;

DELIMITER // 
CREATE PROCEDURE usp_CustomerDelete (in custId int, in conCurrId int)
BEGIN
	Delete from customers where CustomerID = custId and ConcurrencyID = conCurrId;
END //
DELIMITER ; 

DELIMITER // 
CREATE PROCEDURE usp_CustomerCreate (out custId int, in name_p varchar(100), in address_p varchar(50), in city_p varchar(20), in state_p varchar(2), in zipcode_p varchar(15))
BEGIN
	Insert into customers (name, address, city, customers.State, zipcode, concurrencyid)
    Values (name_p, address_p, city_p, state_p, zipcode_p, 1);
    Select LAST_INSERT_ID() into custId;
END //
DELIMITER ;