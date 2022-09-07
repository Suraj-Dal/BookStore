use BookStore;

CREATE TABLE AdminTable
(
   AdminID int identity primary key,
   AdminName varchar(Max) Not null,
   EmailID varchar(Max) Not null,
   Password varchar(Max) Not null,
   AdminPhone varchar(20) Not null
);

SELECT * FROM AdminTable;

go
CREATE PROCEDURE AdminLogin
		@EmailID varchar(255),
		@Password varchar(255)
AS
BEGIN
		SELECT EmailID, Password FROM AdminTable WHERE EmailID=@EmailID AND Password=@Password
END

ALTER TABLE AdminTable ADD Address varchar(300);

INSERT INTO AdminTable(AdminName, EmailID, Password, AdminPhone, Address) 
values('Admin', 'admin@gmail.com', 'admin123', '1234567890', 'Pune, Maharashtra');