select * from Student,Course,StudentCourse
where Student.Pk_Student_Id =  StudentCourse.Pk_Student_Id 
and Course.Pk_Course_Id =  StudentCourse.Pk_Course_Id 
and Student.StudentName = 'ahmed khalifa';
