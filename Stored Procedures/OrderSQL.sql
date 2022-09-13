use BookStore;

CREATE TABLE OrderTable
(
	OrderID int identity primary key,
	OrderQty int Not Null,
	AddressID int Not Null,
	BookID int Not Null,
	UserID int Not Null
)

SELECT * FROM OrderTable;

ALTER TABLE OrderTable DROP COLUMN CartID;

ALTER TABLE OrderTable ADD TotalPrice int Not Null, DateTime varchar(Max) Not Null;
ALTER TABLE OrderTable ADD CartID int Not Null;


alter table OrderTable add constraint fk_UserID_OrderTable foreign key(UserID) references UserTable(UserID);
alter table OrderTable add constraint fk_AddressID_OrderTable foreign key(AddressID) references AddressTable(AddressID) ON DELETE CASCADE ON UPDATE CASCADE;
alter table OrderTable add constraint fk_BookID_OrderTable foreign key(BookID) references BookTable(BookID) ON DELETE CASCADE ON UPDATE CASCADE;
alter table OrderTable add constraint fk_CartID_OrderTable foreign key(CartID) references CartTable(CartID) ON DELETE NO ACTION  ON UPDATE NO ACTION ;


GO
CREATE PROCEDURE [dbo].[InsertIntoOrderTable]
		@CartID int,
		@AddressID int,
		@DateTime varchar(Max),
		@UserID int
AS
	BEGIN try
		
		DECLARE @OrderQty int = (SELECT CartQty FROM CartTable WHERE  CartID = @CartID)
		Declare @Quantity int = (SELECT bt.Quantity FROM BookTable bt Inner Join CartTable ct ON bt.BookID = ct.BookID WHERE ct.CartID = @CartID)
		DECLARE @BookID int = (SELECT bt.BookID FROM BookTable bt Inner Join CartTable ct ON bt.BookID = ct.BookID WHERE ct.CartID = @CartID)
		DECLARE @TotalPrice int = (SELECT DiscountPrice FROM BookTable WHERE BookID = @BookID)
		IF (@OrderQty <= @Quantity)
		BEGIN
			INSERT INTO OrderTable
			(
				OrderQty,
				AddressID,
				BookID,
				TotalPrice,
				DateTime,
				UserID
			)
			VALUES
			(
				@OrderQty,
				@AddressID,
				@BookID,
				@TotalPrice * @OrderQty,
				@DateTime,
				@UserID
			)
	BEGIN
		UPDATE BookTable
		SET Quantity = Quantity - @OrderQty
		WHERE BookID = @BookID
	END
	BEGIN
		DELETE FROM CartTable WHERE BookID = @BookID 
	END
	END
	END try
	BEGIN catch
	END catch

	GO
CREATE PROCEDURE DeleteFromOrderTable
		@OrderID int
AS
	BEGIN
	DECLARE @OrderQty int = (SELECT OrderQty FROM OrderTable WHERE OrderID = @OrderID)
	DECLARE @BookID int = (SELECT BookID FROM OrderTable WHERE OrderID = @OrderID)
		BEGIN
			UPDATE BookTable
			SET Quantity = Quantity + @OrderQty
			WHERE BookID = @BookID
		END
		BEGIN
			DELETE FROM OrderTable
			WHERE OrderID = @OrderID
		END
	END