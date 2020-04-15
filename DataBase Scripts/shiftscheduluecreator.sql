USE [UNOMVC]
GO
/****** Object:  StoredProcedure [dbo].[shiftchedulecretor]    Script Date: Apr/26/2019 9:10:33 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER proc [dbo].[shiftchedulecretor]

as

declare @year varchar(4)
declare @month varchar(2)
declare @numberOfdaysinMonth int
declare @createShiftStartDay int

set @year = '2019'
set @month='4'
set @numberOfdaysinMonth=30
set @createShiftStartDay=3


select *,(select top 1 shift_id from shift_shift_pattern where SHIFT_PATTERN_ID= sftpatid and shiftorder = rn) as dnew 
,(select top 1 shift_id from shift_shift_pattern where SHIFT_PATTERN_ID= sftpatid and shiftorder = wrn) as wnew 
from (

		select EMPLOYEE_ID,td
		,SHIFT_PATTERN_ID as sftpatid
		--,case 
		--	when SHIFT_PATTERN_TYPE='MN' then 30 
		--	when SHIFT_PATTERN_TYPE='DL' then 1 
		--	when SHIFT_PATTERN_TYPE='WK' then 7
		--	when SHIFT_PATTERN_TYPE='BW' then 14
		--end as tc
		--,(select top 1 SHIFT_ID from SHIFT_SHIFT_PATTERN where SHIFT_PATTERN_ID=tna.ETC_SHIFTCODE
		--and SHIFT_ID not in (select top 1 TDAY_SFTREPO from tday where tday_empcde=tna.ETC_EMP_ID and tday_date<tday_days.td order by tday_date desc)
		--) as sft
		,(select top 1 TDAY_SFTREPO from tday where tday_empcde=tna.ETC_EMP_ID and tday_date<tday_days.td order by tday_date desc) as presft
		,(select top 1 SHIFT_ID from shift_shift_pattern where  SHIFT_PATTERN_ID=tna.ETC_SHIFTCODE and shiftorder > case when 
																				(select shiftorder from shift_shift_pattern where SHIFT_ID = (select top 1 tday_sftrepo from tday where tday_empcde=tna.ETC_EMP_ID and tday_date<tday_days.td order by tday_date desc) ) = (select count(1) from shift_shift_pattern where SHIFT_PATTERN_ID=tna.ETC_SHIFTCODE)  or 
																				(select shiftorder from shift_shift_pattern where SHIFT_ID = (select top 1 tday_sftrepo from tday where tday_empcde=tna.ETC_EMP_ID and tday_date<tday_days.td order by tday_date desc) )  is null
																				then 0
																				else 
																				(select shiftorder from shift_shift_pattern where SHIFT_ID = (select top 1 tday_sftrepo from tday where tday_empcde=tna.ETC_EMP_ID and tday_date<tday_days.td order by tday_date desc) )
																				end
																				order by shiftorder asc
																				 ) as newsft
,7 - 
		
		(select ct from (select  count(1) as ct from tday where tday_empcde=tna.ETC_EMP_ID and tday_date<tday_days.td 
and tday_sftrepo = (select top 1 tday_sftrepo from tday where tday_empcde=tna.ETC_EMP_ID and tday_date<tday_days.td  order by tday_date desc)
)t) as tttt
		,
		case when row_number() over(partition by EMPLOYEE_ID order by EMPLOYEE_ID )
		<=
		(
		7 - 
		
		(select ct from (select  count(1) as ct from tday where tday_empcde=tna.ETC_EMP_ID and tday_date<tday_days.td 
and tday_sftrepo = (select top 1 tday_sftrepo from tday where tday_empcde=tna.ETC_EMP_ID and tday_date<tday_days.td  order by tday_date desc)
)t))
then 
	 case when 
																				(select shiftorder from shift_shift_pattern where SHIFT_ID = (select top 1 tday_sftrepo from tday where tday_empcde=tna.ETC_EMP_ID and tday_date<tday_days.td order by tday_date desc) ) = (select count(1) from shift_shift_pattern where SHIFT_PATTERN_ID=tna.ETC_SHIFTCODE)  or 
																				(select shiftorder from shift_shift_pattern where SHIFT_ID = (select top 1 tday_sftrepo from tday where tday_empcde=tna.ETC_EMP_ID and tday_date<tday_days.td order by tday_date desc) )  is null
																				then 1
																				else 
																				(select shiftorder from shift_shift_pattern where SHIFT_ID = (select top 1 tday_sftrepo from tday where tday_empcde=tna.ETC_EMP_ID and tday_date<tday_days.td order by tday_date desc) )
																				end
else

			case when (row_number() over(partition by EMPLOYEE_ID order by EMPLOYEE_ID ) 

			- (7 -
			(select ct from (select  count(1) as ct from tday where tday_empcde=tna.ETC_EMP_ID and tday_date<tday_days.td 
			and tday_sftrepo = (select top 1 tday_sftrepo from tday where tday_empcde=tna.ETC_EMP_ID and tday_date<tday_days.td  order by tday_date desc)
			)t))-1)/7

			+ 
			
			case when 
																				(select shiftorder from shift_shift_pattern where SHIFT_ID = (select top 1 tday_sftrepo from tday where tday_empcde=tna.ETC_EMP_ID and tday_date<tday_days.td order by tday_date desc) ) = (select count(1) from shift_shift_pattern where SHIFT_PATTERN_ID=tna.ETC_SHIFTCODE)  or 
																				(select shiftorder from shift_shift_pattern where SHIFT_ID = (select top 1 tday_sftrepo from tday where tday_empcde=tna.ETC_EMP_ID and tday_date<tday_days.td order by tday_date desc) )  is null
																				then 2
																				else 
																				(select shiftorder from shift_shift_pattern where SHIFT_ID = (select top 1 tday_sftrepo from tday where tday_empcde=tna.ETC_EMP_ID and tday_date<tday_days.td order by tday_date desc) )+1
																				end

			 > (select count(1) from shift_shift_pattern where SHIFT_PATTERN_ID=tna.ETC_SHIFTCODE )

			then 
			(
			row_number() over(partition by EMPLOYEE_ID order by EMPLOYEE_ID ) 

			- (7 -
			(select ct from (select  count(1) as ct from tday where tday_empcde=tna.ETC_EMP_ID and tday_date<tday_days.td 
			and tday_sftrepo = (select top 1 tday_sftrepo from tday where tday_empcde=tna.ETC_EMP_ID and tday_date<tday_days.td  order by tday_date desc)
			)t))-1)/7
			
			
			+ 

				 case when 
																				(select shiftorder from shift_shift_pattern where SHIFT_ID = (select top 1 tday_sftrepo from tday where tday_empcde=tna.ETC_EMP_ID and tday_date<tday_days.td order by tday_date desc) ) = (select count(1) from shift_shift_pattern where SHIFT_PATTERN_ID=tna.ETC_SHIFTCODE)  or 
																				(select shiftorder from shift_shift_pattern where SHIFT_ID = (select top 1 tday_sftrepo from tday where tday_empcde=tna.ETC_EMP_ID and tday_date<tday_days.td order by tday_date desc) )  is null
																				then 2
																				else 
																				(select shiftorder from shift_shift_pattern where SHIFT_ID = (select top 1 tday_sftrepo from tday where tday_empcde=tna.ETC_EMP_ID and tday_date<tday_days.td order by tday_date desc) )+1
																				end
			- (select count(1) from shift_shift_pattern where SHIFT_PATTERN_ID=tna.ETC_SHIFTCODE )
			else

			(row_number() over(partition by EMPLOYEE_ID order by EMPLOYEE_ID ) 

			- (7 -
			(select ct from (select  count(1) as ct from tday where tday_empcde=tna.ETC_EMP_ID and tday_date<tday_days.td 
			and tday_sftrepo = (select top 1 tday_sftrepo from tday where tday_empcde=tna.ETC_EMP_ID and tday_date<tday_days.td  order by tday_date desc)
			)t))-1)/7
			
			+ 

				 case when 
																				(select shiftorder from shift_shift_pattern where SHIFT_ID = (select top 1 tday_sftrepo from tday where tday_empcde=tna.ETC_EMP_ID and tday_date<tday_days.td order by tday_date desc) ) = (select count(1) from shift_shift_pattern where SHIFT_PATTERN_ID=tna.ETC_SHIFTCODE)  or 
																				(select shiftorder from shift_shift_pattern where SHIFT_ID = (select top 1 tday_sftrepo from tday where tday_empcde=tna.ETC_EMP_ID and tday_date<tday_days.td order by tday_date desc) )  is null
																				then 2
																				else 
																				(select shiftorder from shift_shift_pattern where SHIFT_ID = (select top 1 tday_sftrepo from tday where tday_empcde=tna.ETC_EMP_ID and tday_date<tday_days.td order by tday_date desc) )+1
																				end

			end
end
		
		
		   as  wrn
		, case when (row_number() over(partition by EMPLOYEE_ID order by EMPLOYEE_ID )
				%(select count(1) from shift_shift_pattern where SHIFT_PATTERN_ID=tna.ETC_SHIFTCODE )	
				+ 
				case when (select shiftorder from shift_shift_pattern where SHIFT_PATTERN_ID=tna.ETC_SHIFTCODE and shift_id = (select top 1 TDAY_SFTREPO from tday where tday_empcde=tna.ETC_EMP_ID and tday_date<tday_days.td order by tday_date desc)) is null
						then 0
						else (select shiftorder from shift_shift_pattern where SHIFT_PATTERN_ID=tna.ETC_SHIFTCODE and shift_id = (select top 1 TDAY_SFTREPO from tday where tday_empcde=tna.ETC_EMP_ID and tday_date<tday_days.td order by tday_date desc))
						end
				) > (select count(1) from shift_shift_pattern where SHIFT_PATTERN_ID=tna.ETC_SHIFTCODE ) 
				
				then 
				(row_number() over(partition by EMPLOYEE_ID order by EMPLOYEE_ID )
				%(select count(1) from shift_shift_pattern where SHIFT_PATTERN_ID=tna.ETC_SHIFTCODE )	
				+
				case when (select shiftorder from shift_shift_pattern where SHIFT_PATTERN_ID=tna.ETC_SHIFTCODE and shift_id = (select top 1 TDAY_SFTREPO from tday where tday_empcde=tna.ETC_EMP_ID and tday_date<tday_days.td order by tday_date desc)) is null
						then 0
						else (select shiftorder from shift_shift_pattern where SHIFT_PATTERN_ID=tna.ETC_SHIFTCODE and shift_id = (select top 1 TDAY_SFTREPO from tday where tday_empcde=tna.ETC_EMP_ID and tday_date<tday_days.td order by tday_date desc))
						end
				) - (select count(1) from shift_shift_pattern where SHIFT_PATTERN_ID=tna.ETC_SHIFTCODE )
				when (row_number() over(partition by EMPLOYEE_ID order by EMPLOYEE_ID )
				%(select count(1) from shift_shift_pattern where SHIFT_PATTERN_ID=tna.ETC_SHIFTCODE )	
				+ 
				case when (select shiftorder from shift_shift_pattern where SHIFT_PATTERN_ID=tna.ETC_SHIFTCODE and shift_id = (select top 1 TDAY_SFTREPO from tday where tday_empcde=tna.ETC_EMP_ID and tday_date<tday_days.td order by tday_date desc)) is null
						then 0
						else (select shiftorder from shift_shift_pattern where SHIFT_PATTERN_ID=tna.ETC_SHIFTCODE and shift_id = (select top 1 TDAY_SFTREPO from tday where tday_empcde=tna.ETC_EMP_ID and tday_date<tday_days.td order by tday_date desc))
						end
				) < 1
				
				then
				(select count(1) from shift_shift_pattern where SHIFT_PATTERN_ID=tna.ETC_SHIFTCODE )  
				else
				(row_number() over(partition by EMPLOYEE_ID order by EMPLOYEE_ID )
				%(select count(1) from shift_shift_pattern where SHIFT_PATTERN_ID=tna.ETC_SHIFTCODE )	
				+
				case when (select shiftorder from shift_shift_pattern where SHIFT_PATTERN_ID=tna.ETC_SHIFTCODE and shift_id = (select top 1 TDAY_SFTREPO from tday where tday_empcde=tna.ETC_EMP_ID and tday_date<tday_days.td order by tday_date desc)) is null
						then 0
						else (select shiftorder from shift_shift_pattern where SHIFT_PATTERN_ID=tna.ETC_SHIFTCODE and shift_id = (select top 1 TDAY_SFTREPO from tday where tday_empcde=tna.ETC_EMP_ID and tday_date<tday_days.td order by tday_date desc))
						end
				)
				end
					as rn									 
		--,case when SHIFT_PATTERN_TYPE ='MN' then SHIFT_ID else SHIFT_ID end as shiftId
		,case 
			when wkend.MWK_PAT like '%'+cast(floor((day(td)-1)/7)+1 as varchar(2))+'%' and wkend.MWK_DAY=datepart(dw,td)
			then (select top 1 TDAY_STATUS_ID from tday_status where TDAY_STATUS_CODE='ABW2')
			when wkoff.MWK_PAT like '%'+cast(floor((day(td)-1)/7)+1 as varchar(2))+'%' and wkoff.MWK_DAY=datepart(dw,td)
			then (select top 1 TDAY_STATUS_ID from tday_status where TDAY_STATUS_CODE='ABWO')
			else (select top 1 TDAY_STATUS_ID from tday_status where TDAY_STATUS_CODE='AB') 
			end as tdayast
		
		from ENT_EMPLOYEE_DTLS eod
		cross join (select @year+'/'+@month+'/'+  tdays as td,tday  from tday_days where tday<@numberOfdaysinMonth+1) as tday_days
		join TNA_EMPLOYEE_TA_CONFIG tna on tna.ETC_EMP_ID=eod.EMPLOYEE_ID
		join ta_shift_pattern pat on pat.SHIFT_PATTERN_ID=tna.ETC_SHIFTCODE 
		--join SHIFT_SHIFT_PATTERN sftpat on sftpat.SHIFT_PATTERN_ID=tna.ETC_SHIFTCODE
		join TA_WKLYOFF wkend on wkend.MWK_CD=tna.ETC_WEEKEND
		join TA_WKLYOFF wkoff on wkoff.MWK_CD=tna.ETC_WEEKOFF
 where eod.EMPLOYEE_ID not in 
(select TDAY_EMPCDE from tday where month(tday_date)=@month and year(tday_date)=@year and day(tday_date)>@createShiftStartDay-1)
and td>=EOD_JOINING_DATE and day(td)>@createShiftStartDay-1

)a

--order by shiftorder


return


SELECT ETC_EMP_ID,ShiftType,ETC_SHIFTCODE,ScheduleType,EOD_JOINING_DATE
		/*ALTERED ON 07-JAN-2015*/
		,ETC_WEEKEND,ETC_WEEKOFF,SHIFT_ID,wkend.MWK_DAY as wkend,wkend.MWK_PAT,wkoff.MWK_DAY as wkoff,wkoff.MWK_PAT,datepart(dw,getdate())
		,SHIFT_PATTERN_TYPE
		/*ALTERED ON 07-JAN-2015*/
		FROM dbo.TNA_EMPLOYEE_TA_CONFIG
		INNER JOIN ENT_EMPLOYEE_PERSONAL_DTLS ON TNA_EMPLOYEE_TA_CONFIG.ETC_EMP_ID=ENT_EMPLOYEE_PERSONAL_DTLS.EMPLOYEE_ID
		INNER JOIN ENT_EMPLOYEE_DTLS ON ENT_EMPLOYEE_DTLS.EMPLOYEE_ID=ENT_EMPLOYEE_PERSONAL_DTLS.EMPLOYEE_ID
		inner join ta_shift_pattern sftpat on sftpat.SHIFT_PATTERN_ID=TNA_EMPLOYEE_TA_CONFIG.ETC_SHIFTCODE
		inner join SHIFT_SHIFT_PATTERN pat on pat.SHIFT_PATTERN_ID=TNA_EMPLOYEE_TA_CONFIG.ETC_SHIFTCODE
		inner join TA_WKLYOFF wkend on wkend.MWK_CD=TNA_EMPLOYEE_TA_CONFIG.ETC_WEEKEND
		inner join TA_WKLYOFF wkoff on wkoff.MWK_CD=TNA_EMPLOYEE_TA_CONFIG.ETC_WEEKOFF
		WHERE EPD_ISDELETED=0 and EOD_STATUS=1
		
		
		select * from tday_status where TDAY_STATUS_CODE='ABW2'


--select floor((day('2018/12/1')-1)/7)+1
select * from tday_days  where tday<29

--select * from tday

select * from SHIFT_SHIFT_PATTERN

--update tday set TDAY_SFTASSG=6,TDAY_SFTREPO=6

select * from ent_params