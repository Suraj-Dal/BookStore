use BookStore;

CREATE TABLE FeedbackTable
(
	FeedbackID int identity primary key,
	rating int Not Null,
	Comment varchar(Max) Not Null,
	UserID int Not Null,
	BookID int Not Null
)

SELECT * FROM FeedbackTable

GO
CREATE PROCEDURE InsertIntoFeedbackTable
		@rating int,
		@Comment varchar(max),
		@UserID int,
		@BookID int
AS
	BEGIN
			INSERT into FeedbackTable(
			rating,
			Comment,
			UserID,
			BookID
			)
			values
			(
			@rating,
			@Comment,
			@UserID,
			@BookID
			)
	END