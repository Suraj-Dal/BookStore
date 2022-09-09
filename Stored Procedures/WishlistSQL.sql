use BookStore

CREATE TABLE Wishlist
(
	WishlistID int identity primary key
)

SELECT * FROM Wishlist;

ALTER TABLE Wishlist ADD UserID int Not Null, BookID int Not Null;

alter table Wishlist add constraint fk_UserID_Wishlist foreign key(UserID) references UserTable(UserID);

alter table Wishlist add constraint fk_BookID_Wishlist foreign key(BookID) references BookTable(BookID);

GO
CREATE PROCEDURE InsertIntoWishlist
		@UserID int,
		@BookID int
AS
	BEGIN
			INSERT into Wishlist(
			UserID,
			BookID
			)
			values
			(
			@UserID,
			@BookID
			)
	END


GO
CREATE PROCEDURE DeleteFromWishlist
		@WishlistID int,
		@UserID int
AS
	BEGIN
		DELETE FROM Wishlist
		WHERE WishlistID = @WishlistID AND UserID = @UserID
	END