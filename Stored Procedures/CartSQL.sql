use BookStore

CREATE TABLE CartTable
(
	CartID int identity primary key,
	CartQty int Not Null
)

SELECT * FROM CartTable;

ALTER TABLE CartTable ADD UserID int Not Null, BookID int Not Null;

alter table CartTable add constraint fk_UserID foreign key(UserID) references UserTable(UserID);

alter table CartTable add constraint fk_BookID foreign key(BookID) references BookTable(BookID);

GO
CREATE PROCEDURE InsertIntoCartTable
		@CartQty int,
		@BookID int
AS
	BEGIN
			INSERT into CartTable(
			CartQty,
			BookID
			)
			values
			(
			@CartQty,
			@BookID
			)
	END

GO
CREATE PROCEDURE UpdateCartTable
		@CartQty int,
		@UserID int,
		@CartID int
AS
	BEGIN
		UPDATE CartTable
		SET
			CartQty = @CartQty
		WHERE
			UserID = @UserID AND CartID = @CartID
	END


GO
CREATE PROCEDURE DeleteFromCartTable
		@CartID int,
		@UserID int
AS
	BEGIN
		DELETE FROM CartTable
		WHERE CartID = @CartID AND UserID = @UserID
	END

