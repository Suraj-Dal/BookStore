use BookStore;

CREATE TABLE BookTable 
(
   BookID int identity primary key,
   BookName varchar(Max) Not null,
   AuthorName varchar(Max) Not null,
   rating int,
   PeopleRated int Not Null,
   Price int Not Null,
   DiscountPrice int Not Null,
   Description varchar(Max) Not Null,
   Quantity int Not Null,
   BookImage varchar(255) Not Null
);

SELECT * FROM BookTable;

GO
CREATE PROCEDURE InsertIntoBookTable
		@BookName varchar(Max),
		@AuthorName varchar(Max),
		@rating int,
		@PeopleRated int,
		@Price int,
		@DiscountPrice int,
		@Description varchar(Max),
		@Quantity int,
		@BookImage varchar(255)
AS
	BEGIN
			INSERT into BookTable(
			BookName,
			AuthorName,
			rating,
			PeopleRated,
			Price,
			DiscountPrice,
			Description,
			Quantity,
			BookImage
			)
			values
			(
			@BookName,
			@AuthorName,
			@rating,
			@PeopleRated,
			@Price,
			@DiscountPrice,
			@Description,
			@Quantity,
			@BookImage
			)
	END

GO
CREATE PROCEDURE UpdateBookTable
		@BookID int,
		@BookName varchar(Max),
		@AuthorName varchar(Max),
		@rating int,
		@PeopleRated int,
		@Price int,
		@DiscountPrice int,
		@Description varchar(Max),
		@Quantity int,
		@BookImage varchar(255)
AS
	BEGIN
		UPDATE BookTable
		SET
			BookName = @BookName,
			AuthorName = @AuthorName,
			rating = @rating,
			PeopleRated = @PeopleRated,
			Price = @Price,
			DiscountPrice = @DiscountPrice,
			Description = @Description,
			Quantity = @Quantity,
			BookImage = @BookImage
		WHERE
			BookID = @BookID
	END

GO
CREATE PROCEDURE DeleteFromBookTable
		@BookID int
AS
	BEGIN
		DELETE FROM BookTable
		WHERE BookID = @BookID
	END

GO
CREATE PROCEDURE GetBook
		@BookID int
AS
	BEGIN
		SELECT 
			BookName,
			AuthorName,
			rating,
			PeopleRated,
			Price,
			DiscountPrice,
			Description,
			Quantity,
			BookImage
		FROM BookTable
		WHERE BookID = @BookID
	END