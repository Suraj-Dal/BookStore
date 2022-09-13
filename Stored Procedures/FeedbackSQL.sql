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

alter table FeedbackTable add constraint fk_UserID_FeedbackTable foreign key(UserID) references UserTable(UserID);
alter table FeedbackTable add constraint fk_BookID_FeedbackTable foreign key(BookID) references BookTable(BookID) ON DELETE CASCADE ON UPDATE CASCADE;


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