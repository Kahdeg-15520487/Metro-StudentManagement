
alter TABLE SEMESTER
ADD DateRegister smalldatetime


update SEMESTER
set DateRegister='2016-12-20'
where SemesterID='01619'


alter proc IsDateRegisters
as
begin

declare ListDateRegister cursor
for select DateRegister  from Semester 
declare @IsDate int,@DateRegister smalldatetime ,@NowDay smalldatetime
set @NowDay=GETDATE()
open ListDateRegister
FETCH NEXT FROM ListDateRegister into @DateRegister
WHILE (@@FETCH_STATUS=0)
begin
if(@NowDay>=@DateRegister and @NowDay<=@DateRegister+3)
begin
set @IsDate=1
FETCH NEXT FROM ListDateRegister into @DateRegister
break
end
else begin set @IsDate=0
FETCH NEXT FROM ListDateRegister into @DateRegister end
end
DEALLOCATE ListDateRegister
return @IsDate
end




declare @a int
exec @a= IsDateRegisters
print @a