
CREATE TABLE dbo.Student
(
Pk_Student_Id INT  PRIMARY KEY identity(1,1),
StudentName VARCHAR(255)
);

CREATE TABLE dbo.Course
(
Pk_Course_Id INT PRIMARY KEY identity(1,1),
CourseName     VARCHAR(255)
);

Create Table dbo.StudentCourse
(
Pk_Student_Id INT,
Pk_Course_Id INT,
PRIMARY KEY(Pk_Student_Id,Pk_Course_Id),
Constraint FK_Student Foreign Key (Pk_Student_Id) References Student( Pk_Student_Id ),
Constraint FK_Course Foreign Key (Pk_Course_Id) References Course( Pk_Course_Id )
);

Insert into Student Values ('ahmed khalifa'),('nada lotfy'),('mostafa megria');
Insert into Course Values ('asp.net'),('xamarin'),('sql server'),('html'),('css'),('js'),('math');

insert into StudentCourse values (1,1);
insert into StudentCourse values (1,2);
insert into StudentCourse values (1,3);
insert into StudentCourse values (1,4);
insert into StudentCourse values (1,5);
insert into StudentCourse values (1,6);
insert into StudentCourse values (2,4);
insert into StudentCourse values (2,5);
insert into StudentCourse values (2,7);
insert into StudentCourse values (3,4);
insert into StudentCourse values (3,5);
insert into StudentCourse values (3,6);


