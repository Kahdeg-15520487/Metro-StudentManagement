Create proc GetStudentInfoByID
@StudentID nvarchar(30)

as
begin
select * from Student 
where Student.StudentID=@StudentID

end