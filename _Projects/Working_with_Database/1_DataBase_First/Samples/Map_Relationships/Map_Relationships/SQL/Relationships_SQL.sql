CREATE TABLE dbo.Person
(
Pk_Person_Id INT IDENTITY PRIMARY KEY,
Name VARCHAR(255),
EmailId VARCHAR(255),
);
CREATE TABLE dbo.PassportDetails
(
Pk_Passport_Id INT PRIMARY KEY,
Passport_Number VARCHAR(255),
Fk_Person_Id INT UNIQUE FOREIGN KEY REFERENCES dbo.Person(Pk_Person_Id)
);
INSERT INTO dbo.Person VALUES ('ahmed khalifa','ahmedkhalifa@emails.com');
INSERT INTO dbo.Person VALUES ('mohamed medina','mohamedmedina@emails.com');
INSERT INTO dbo.Person VALUES ('amera abd allah','amera_abdallah@emails.com');
GO

INSERT INTO dbo.PassportDetails VALUES (101, 'ak_C3031R33', 1);
INSERT INTO dbo.PassportDetails VALUES (102, 'mm_VRDK5695', 2);
INSERT INTO dbo.PassportDetails VALUES (103, 'aa_A4DEK33D', 3);
GO

SELECT * FROM dbo.Person
SELECT * FROM dbo.PassportDetails;
