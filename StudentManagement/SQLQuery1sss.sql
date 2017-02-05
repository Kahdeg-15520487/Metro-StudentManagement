alter proc GetInfoDiscipline
as
begin
declare @check bit
select @check as [check], Discipline.DisciplineID, Department.DepartmentName,Department.Credits,Discipline.StudyDate,Discipline.Period,Discipline.FinishDay,Discipline.StartDay, Teacher.LastName+' '+MiddleName+' '+Name as Name
from Discipline  join Department on  Department.DepartmentID=Discipline.DepartmentID join Teacher on  Teacher.TeacherID=Discipline.TeacherID

end

alter proc GetInfoRegistered
@StudentID nvarchar(10)
as
begin
declare @check bit
select @check as [check], Discipline.DisciplineID, Department.DepartmentName,Department.Credits,Discipline.StudyDate,Discipline.Period,Discipline.FinishDay,Discipline.StartDay, Teacher.LastName+' '+MiddleName+' '+Name as Name
from RegisterStudyUnit join Discipline on RegisterStudyUnit.DisciplineID=Discipline.DisciplineID join Department on  Department.DepartmentID=Discipline.DepartmentID join Teacher on  Teacher.TeacherID=Discipline.TeacherID
where RegisterStudyUnit.StudentID=@StudentID
end


go
create proc GetTeacherRegister
as
begin
Select Teacher.LastName +' '+ Teacher.MiddleName+' '+Teacher.Name as TeacherName   from Teacher join Discipline on Teacher.TeacherID=Discipline.TeacherID
end
 

 go
  create  Proc GetDepartmentRegister
  as
  begin
  Select Department.DepartmentName from Department join Discipline on Department.DepartmentID=Discipline.DepartmentID
  end


  go
  create proc SortDisciplinebyTeacherAndDepartment
  @TeacherName nvarchar(100),
  @DepartmentName Nvarchar(100)
  as
  begin
  declare @check bit
select @check as [check], Discipline.DisciplineID, Department.DepartmentName,Department.Credits,Discipline.StudyDate,Discipline.Period,Discipline.FinishDay,Discipline.StartDay, Teacher.LastName+' '+MiddleName+' '+Name as Name
from Discipline  join Department on  Department.DepartmentID=Discipline.DepartmentID join Teacher on  Teacher.TeacherID=Discipline.TeacherID
where Teacher.LastName+' '+MiddleName+' '+Name=@TeacherName and Department.DepartmentName=@DepartmentName
  end



   go
  alter  proc SortDisciplinebyTeacherAndDepartment
  @TeacherName nvarchar(100),
  @DepartmentName Nvarchar(100)
  as
  begin
  declare @check bit
   if (@DepartmentName='All' and @TeacherName='All')
begin
select @check as [check], Discipline.DisciplineID, Department.DepartmentName,Department.Credits,Discipline.StudyDate,Discipline.Period,Discipline.FinishDay,Discipline.StartDay, Teacher.LastName+' '+MiddleName+' '+Name as Name
from Discipline  join Department on  Department.DepartmentID=Discipline.DepartmentID join Teacher on  Teacher.TeacherID=Discipline.TeacherID
end
else if(@DepartmentName='All')
  begin
select @check as [check], Discipline.DisciplineID, Department.DepartmentName,Department.Credits,Discipline.StudyDate,Discipline.Period,Discipline.FinishDay,Discipline.StartDay, Teacher.LastName+' '+MiddleName+' '+Name as Name
from Discipline  join Department on  Department.DepartmentID=Discipline.DepartmentID join Teacher on  Teacher.TeacherID=Discipline.TeacherID
where Teacher.LastName+' '+MiddleName+' '+Name=@TeacherName 
end
else if(@TeacherName='All')
begin
select @check as [check], Discipline.DisciplineID, Department.DepartmentName,Department.Credits,Discipline.StudyDate,Discipline.Period,Discipline.FinishDay,Discipline.StartDay, Teacher.LastName+' '+MiddleName+' '+Name as Name
from Discipline  join Department on  Department.DepartmentID=Discipline.DepartmentID join Teacher on  Teacher.TeacherID=Discipline.TeacherID
where  Department.DepartmentName=@DepartmentName
end 

else 
begin
select @check as [check], Discipline.DisciplineID, Department.DepartmentName,Department.Credits,Discipline.StudyDate,Discipline.Period,Discipline.FinishDay,Discipline.StartDay, Teacher.LastName+' '+MiddleName+' '+Name as Name
from Discipline  join Department on  Department.DepartmentID=Discipline.DepartmentID join Teacher on  Teacher.TeacherID=Discipline.TeacherID
where Teacher.LastName+' '+MiddleName+' '+Name=@TeacherName and Department.DepartmentName=@DepartmentName
end 
  end
