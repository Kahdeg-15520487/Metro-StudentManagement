Alter  proc GetDiplomaProject
@StudenID nvarchar(10)
as
begin
select DiplomaProject.*,Student.LastName,MiddleName,Name from DiplomaProject join Student on DiplomaProject.StudentID=Student.StudentID
where DiplomaProject.StudentID=@StudenID
end

select * from Student