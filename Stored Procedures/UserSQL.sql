CREATE DATABASE BookStore;
use BookStore;

CREATE TABLE UserTable 
(
   UserID int identity primary key,
   FullName varchar(Max) Not null,
   EmailID varchar(Max) Not null,
   Password varchar(Max) Not null,
   Phone varchar(20) Not null
);

SELECT * FROM UserTable;

go
CREATE PROCEDURE InsertIntoUserTable
		@FullName varchar(255),
		@EmailID varchar(255),
		@Password varchar(255),
		@Phone varchar(20)
AS
	BEGIN
			INSERT into UserTable(
			FullName,
			EmailID,
			Password,
			Phone
			)
			values
			(
			@FullName,
			@EmailID,
			@Password,
			@Phone
			)
	END


go
CREATE PROCEDURE UserLogin
		@EmailID varchar(255),
		@Password varchar(255)
AS
BEGIN
		SELECT EmailID, Password FROM UserTable WHERE EmailID=@EmailID AND Password=@Password
END

go 
CREATE PROCEDURE ResetPassword
		@EmailID varchar(255),
		@Password varchar(255)
AS
BEGIN
	UPDATE UserTable SET Password = @Password WHERE EmailID = @EmailID
END

go 
CREATE PROCEDURE ForgetPassword
		@EmailID varchar(255)
AS
BEGIN
	SELECT * FROM UserTable WHERE EmailID = @EmailID
END

