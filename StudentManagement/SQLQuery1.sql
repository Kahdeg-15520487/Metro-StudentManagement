alter trigger AcademicMarkAverage
on AcademicMark
for insert, update
as
begin
declare @FinalterMark float
declare @AverageMark float
select @AverageMark=@AverageMark from inserted
select @FinalterMark=FinaltermMark from inserted
if(@FinalterMark <> null and @AverageMark is null)
begin
update AcademicMark
set AvarageMark=round(( AcademicMark.ProcessMark+AcademicMark.MidtermMark+AcademicMark.FinaltermMark)/3,2)
from inserted
where AcademicMark.StudentID=inserted.StudentID
end
if(@AverageMark<>null)
begin
if(@AverageMark>=5)
begin
update AcademicMark
set AcademicMark.AvarageMark=1
from inserted
where AcademicMark.StudentID=inserted.StudentID
end
else
begin
update AcademicMark
set AcademicMark.AvarageMark=0
from inserted
where AcademicMark.StudentID=inserted.StudentID
end
end
end

go

alter proc GetListDisciplineForThisUser
@ThisUser nvarchar(10)
as
begin
select Semester.SemesterName,RegisterStudyUnit.DisciplineID,DisciplineStatus
from RegisterStudyUnit join Student on RegisterStudyUnit.StudentID=Student.StudentID join AcademicMark on AcademicMark.StudentID=Student.StudentID join Semester on Semester.SemesterID=RegisterStudyUnit.SemesterID
where RegisterStudyUnit.StudentID=@ThisUser
end

exec GetListDisciplineForThisUser '00351'
go

