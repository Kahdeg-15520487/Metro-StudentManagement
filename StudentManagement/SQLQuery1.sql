CREATE proc IsDateRegister
as
begin
--create cursor
declare ListDateRegister cursor
for select SemesterID, DateRegister  from Semester 
declare @IsDate bit,@DateRegister smalldatetime ,@NowDay smalldatetime,@SemesterID nvarchar(10),@temp nvarchar(10)
set @NowDay=GETDATE()
set @temp='0'
open ListDateRegister
FETCH NEXT FROM ListDateRegister into @SemesterID, @DateRegister
WHILE (@@FETCH_STATUS=0)
begin
if(@NowDay>=@DateRegister and @NowDay<=@DateRegister+3)
begin
set @IsDate=1
set @temp=@SemesterID
end
else begin set @IsDate=0
 end
update Semester
set IsDateRegister=@IsDate
where DateRegister=@DateRegister
FETCH NEXT FROM ListDateRegister into @SemesterID,@DateRegister
end
DEALLOCATE ListDateRegister
if(@temp='0') set @temp=@SemesterID
select IsDateRegister,SemesterID from Semester where SemesterID=@temp
end


alter TABLE SEMESTER
ADD DateRegister smalldatetime

alter table SEMESTER
add IsDateRegister bit

update SEMESTER
set IsDateRegister=0

go
CREATE proc IsDateRegister
as
begin
--create cursor
declare ListDateRegister cursor
for select SemesterID, DateRegister  from Semester 
declare @IsDate bit,@DateRegister smalldatetime ,@NowDay smalldatetime,@SemesterID nvarchar(10),@temp nvarchar(10)
set @NowDay=GETDATE()
set @temp='0'
open ListDateRegister
FETCH NEXT FROM ListDateRegister into @SemesterID, @DateRegister
WHILE (@@FETCH_STATUS=0)
begin
if(@NowDay>=@DateRegister and @NowDay<=@DateRegister+3)
begin
set @IsDate=1
set @temp=@SemesterID
end
else begin set @IsDate=0
 end
update Semester
set IsDateRegister=@IsDate
where DateRegister=@DateRegister
FETCH NEXT FROM ListDateRegister into @SemesterID,@DateRegister
end
DEALLOCATE ListDateRegister
if(@temp='0') set @temp=@SemesterID
select IsDateRegister,SemesterID from Semester where SemesterID=@temp
end

exec IsDateRegister


update Semester
set DateRegister='2016-12-21'
where SemesterID='01619'