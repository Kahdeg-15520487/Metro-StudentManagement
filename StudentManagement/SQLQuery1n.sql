ALTER proc GetScheduleForDetail1
@StudentID nvarchar(10),
@Semester nvarchar(100),
@StudyDate nvarchar(100)
as
begin
select Teacher.LastName+' '+MiddleName+' '+Name as TeacherName ,Semester.StartDay,Semester.FinishDay, Department.DepartmentName,Discipline.DepartmentID,Discipline.Period ,Department.Credits*43 as Credits ,Room
from Discipline join RegisterStudyUnit on Discipline.DisciplineID=RegisterStudyUnit.DisciplineID join Teacher on Teacher.TeacherID=Discipline.TeacherID join Department on Department.DepartmentID=Discipline.DepartmentID join semester on semester.semesterid=RegisterStudyUnit.semesterid
where StudentID=@StudentID and  Discipline.StudyDate=@StudyDate and Semester.SemesterName=@Semester
order by Discipline.Period
end

 go
alter TRIGGER OnInsertStudentAddNumberID
On Student
For Insert
as
begin
declare  @IdNumber nvarchar(100), @yearAcademic int,@studentID nvarchar(10)
select @IdNumber=inserted.IDNumber, @studentID=inserted.StudentID from inserted
set @yearAcademic=right(@IdNumber,2)+2006
update Student
set AcademicYear=@yearAcademic
where StudentID=@studentID
end



 go
create proc UpdateStarDay 
@StudentID nvarchar(10)
as
 begin
 declare @YearAcademic int
 select @YearAcademic=Student.AcademicYear from Student  where StudentID=@StudentID
 
 update  Semester
 set StartDay= CONVERT(nvarchar ,@YearAcademic)+'/'+CONVERT(nvarchar, MONTH(StartDay))+'/'+CONVERT(nvarchar,DAY(StartDay))
 where YearSemester= 'year 1'
 set @YearAcademic+=1
 update  Semester
 set StartDay= CONVERT(nvarchar ,@YearAcademic)+'/'+CONVERT(nvarchar, MONTH(StartDay))+'/'+CONVERT(nvarchar,DAY(StartDay))
 where YearSemester= 'year 2'
  set @YearAcademic+=1
 update  Semester
 set StartDay= CONVERT(nvarchar ,@YearAcademic)+'/'+CONVERT(nvarchar, MONTH(StartDay))+'/'+CONVERT(nvarchar,DAY(StartDay))
 where YearSemester= 'year 3'
 set @YearAcademic+=1
 update  Semester
 set StartDay= CONVERT(nvarchar ,@YearAcademic)+'/'+CONVERT(nvarchar, MONTH(StartDay))+'/'+CONVERT(nvarchar,DAY(StartDay))
 where YearSemester= 'year 4'
 set @YearAcademic+=1
 update  Semester
 set StartDay= CONVERT(nvarchar ,@YearAcademic)+'/'+CONVERT(nvarchar, MONTH(StartDay))+'/'+CONVERT(nvarchar,DAY(StartDay))
 where YearSemester= 'Graduate'
  update  Semester
  set FinishDay=StartDay+7*15
end
GO
alter table Discipline
add Room Nvarchar(10)

GO












