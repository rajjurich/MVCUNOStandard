--select * from tasc,unomvc.dbo.ta_shift where Tasc_flag=0
----truncate table tasc
--update tasc set TAsc_empcode='abc111'

--select * from  unomvc.dbo.tday t
--join ent_user e on t.TDAY_EMPCDE=e.user_id  where user_code='3333'

--select * from unomvc.dbo.ent_user

--select * from unomvc.dbo.ta_shift
--update t
--set t.TDAY_INTIME=max(tasc_time), t.TDAY_OUTIME=case when c.outtime = c.intime then t.TDAY_OUTIME else c.outtime end
--from  unomvc.dbo.tday  t
--join unomvc.dbo.ENT_EMPLOYEE_DTLS e on e.EMPLOYEE_ID=t.TDAY_EMPCDE
--join ( 

--select TAsc_empcode,tascdate,TAsc_Mode,case when TAsc_Mode in ('i','n') then MIN(TAsc_time) when TAsc_Mode in ('o','n') then MAX(TAsc_time) end as tt
--from
--(

set nocount on
declare @tmptbl Table(TAsc_empcode varchar(20),TAsc_Mode varchar(5),TAsc_time datetime,tascdate datetime
--,LATECOMING datetime
--	,EARLYGOING datetime
	,startmin datetime
	,endmax datetime
	,COMPANY_ID int
--	,TDAY_EROT datetime
--,TDAY_LTOT datetime
--,EXTRA_CHECK bit
--,EXHRS_BEFORE_SHIFT_HRS datetime
--,EXHRS_AFTER_SHIFT_HRS datetime
--,DED_FROM_EXHRS_EARLY_GOING datetime
--,DED_FROM_EXHRS_LATE_COMING datetime
)

insert into @tmptbl(TAsc_empcode,TAsc_Mode,TAsc_time,tascdate
--,LATECOMING
--	,EARLYGOING
	,startmin
	,endmax
	,COMPANY_ID
--	,TDAY_EROT
--,TDAY_LTOT
--,EXTRA_CHECK
--,EXHRS_BEFORE_SHIFT_HRS
--,EXHRS_AFTER_SHIFT_HRS
--,DED_FROM_EXHRS_EARLY_GOING
--,DED_FROM_EXHRS_LATE_COMING
	)
select TAsc_empcode,TAsc_Mode,TAsc_time,tascdate
--,LATECOMING
--	,EARLYGOING
	,startmin
	,endmax
--	,TDAY_EROT
--,TDAY_LTOT
--,EXTRA_CHECK
--,EXHRS_BEFORE_SHIFT_HRS
--,EXHRS_AFTER_SHIFT_HRS
--,DED_FROM_EXHRS_EARLY_GOING
--,DED_FROM_EXHRS_LATE_COMING
,COMPANY_ID
	
	
	 from (
select TAsc_empcode
,case 
	when TAsc_time between startminboundary and endmaxboundary then cast(cast(startminboundary as date) as datetime) 
	when TAsc_time between startminboundaryprev and endmaxboundaryprev then cast(cast(startminboundaryprev as date) as datetime) 
	when TAsc_time between startminboundarynext and endmaxboundarynext then cast(cast(startminboundarynext as date) as datetime) 
end as tascdate 
--,case 
--	when TAsc_time between startminboundary and endmaxboundary then cast(cast(startminboundary as date) as datetime) 
--	when TAsc_time between startminboundaryprev and endmaxboundaryprev then cast(cast(startminboundaryprev as date) as datetime) 
--	when TAsc_time between startminboundarynext and endmaxboundarynext then cast(cast(startminboundarynext as date) as datetime) 
--end + cast(cast(TAsc_time as time) as datetime) as tdaydate 
,cast(TAsc_time as time) TAsctime
,TAsc_time
,TAsc_Mode
--,case 
--	when TAsc_time between startminboundary and endmaxboundary then startmin 
--	when TAsc_time between startminboundaryprev and endmaxboundaryprev then startminprev 
--	when TAsc_time between startminboundarynext and endmaxboundarynext then startminnext
--end  + LATE_COMING as LATECOMING
--,case 
--	when TAsc_time between startminboundary and endmaxboundary then endmax
--	when TAsc_time between startminboundaryprev and endmaxboundaryprev then endmaxprev 
--	when TAsc_time between startminboundarynext and endmaxboundarynext then endmaxnext
--end  - EARLY_GOING as EARLYGOING
,case 
	when TAsc_time between startminboundary and endmaxboundary then startmin 
	when TAsc_time between startminboundaryprev and endmaxboundaryprev then startminprev 
	when TAsc_time between startminboundarynext and endmaxboundarynext then startminnext
end as startmin
,case 
	when TAsc_time between startminboundary and endmaxboundary then endmax 
	when TAsc_time between startminboundaryprev and endmaxboundaryprev then endmaxprev 
	when TAsc_time between startminboundarynext and endmaxboundarynext then endmaxnext
end as endmax
,tasc.COMPANY_ID
--,TDAY_EROT
--,TDAY_LTOT
--,EXTRA_CHECK
--,EXHRS_BEFORE_SHIFT_HRS
--,EXHRS_AFTER_SHIFT_HRS
--,DED_FROM_EXHRS_EARLY_GOING
--,DED_FROM_EXHRS_LATE_COMING
--,ttin,ttout
--,shift_id
--,startminboundary,endmaxboundary,startminboundaryprev,endmaxboundaryprev,startminboundarynext,endmaxboundarynext
from 
(
select TAsc_empcode,TAsc_date,TAsc_time,EOD_COMPANY_ID,tday_sftrepo,shift_id,TAsc_Mode
,datepart(dw,TAsc_date) as dd,cast(floor((day(TAsc_date)-1)/7)+1 as varchar(2)) as wd
,wkend.mwk_pat as wepat,wkend.mwk_day as weday
,TAsc_date + case 
	when SHIFT_WEEKEND_DIFF_TIME=1 and  wkend.mwk_pat  not like '%'+cast(floor((day(TAsc_date)-1)/7)+1 as varchar(2))+'%' and wkend.mwk_day=datepart(dw,TAsc_date)	then SHIFT_WEEKEND_START 
	else SHIFT_START end - SHIFT_EARLY_SEARCH_HRS as startminboundary
,TAsc_date + case when SHIFT_START > SHIFT_END then 1 else 0 end + case 
	when SHIFT_WEEKEND_DIFF_TIME=1 and  wkend.mwk_pat  not like '%'+cast(floor((day(TAsc_date)-1)/7)+1 as varchar(2))+'%' and wkend.mwk_day=datepart(dw,TAsc_date)	then SHIFT_WEEKEND_END 
	else SHIFT_END end + SHIFT_LATE_SEARCH_HRS as endmaxboundary
,TAsc_date -1  + case 
	when SHIFT_WEEKEND_DIFF_TIME=1 and  wkend.mwk_pat  not like '%'+cast(floor((day(TAsc_date)-1)/7)+1 as varchar(2))+'%' and wkend.mwk_day=datepart(dw,TAsc_date)	then SHIFT_WEEKEND_START 
	else SHIFT_START end - SHIFT_EARLY_SEARCH_HRS as startminboundaryprev
	,TAsc_date + case when SHIFT_START > SHIFT_END then 1 else 0 end -1 + case 
	when SHIFT_WEEKEND_DIFF_TIME=1 and  wkend.mwk_pat  not like '%'+cast(floor((day(TAsc_date)-1)/7)+1 as varchar(2))+'%' and wkend.mwk_day=datepart(dw,TAsc_date)	then SHIFT_WEEKEND_END 
	else SHIFT_END end + SHIFT_LATE_SEARCH_HRS as endmaxboundaryprev
	,TAsc_date +1  + case 
	when SHIFT_WEEKEND_DIFF_TIME=1 and  wkend.mwk_pat  not like '%'+cast(floor((day(TAsc_date)-1)/7)+1 as varchar(2))+'%' and wkend.mwk_day=datepart(dw,TAsc_date)	then SHIFT_WEEKEND_START 
	else SHIFT_START end - SHIFT_EARLY_SEARCH_HRS as startminboundarynext
	,TAsc_date + case when SHIFT_START > SHIFT_END then 1 else 0 end+1 + case 
	when SHIFT_WEEKEND_DIFF_TIME=1 and  wkend.mwk_pat  not like '%'+cast(floor((day(TAsc_date)-1)/7)+1 as varchar(2))+'%' and wkend.mwk_day=datepart(dw,TAsc_date)	then SHIFT_WEEKEND_END 
	else SHIFT_END end + SHIFT_LATE_SEARCH_HRS as endmaxboundarynext
,TAsc_date + case 
	when SHIFT_WEEKEND_DIFF_TIME=1 and  wkend.mwk_pat  not like '%'+cast(floor((day(TAsc_date)-1)/7)+1 as varchar(2))+'%' and wkend.mwk_day=datepart(dw,TAsc_date)	then SHIFT_WEEKEND_START 
	else SHIFT_START end as startmin
,TAsc_date + case when SHIFT_START > SHIFT_END then 1 else 0 end + case 
	when SHIFT_WEEKEND_DIFF_TIME=1 and  wkend.mwk_pat  not like '%'+cast(floor((day(TAsc_date)-1)/7)+1 as varchar(2))+'%' and wkend.mwk_day=datepart(dw,TAsc_date)	then SHIFT_WEEKEND_END 
	else SHIFT_END end as endmax
,TAsc_date -1  + case 
	when SHIFT_WEEKEND_DIFF_TIME=1 and  wkend.mwk_pat  not like '%'+cast(floor((day(TAsc_date)-1)/7)+1 as varchar(2))+'%' and wkend.mwk_day=datepart(dw,TAsc_date)	then SHIFT_WEEKEND_START 
	else SHIFT_START end as startminprev
,TAsc_date + case when SHIFT_START > SHIFT_END then 1 else 0 end -1 + case 
	when SHIFT_WEEKEND_DIFF_TIME=1 and  wkend.mwk_pat  not like '%'+cast(floor((day(TAsc_date)-1)/7)+1 as varchar(2))+'%' and wkend.mwk_day=datepart(dw,TAsc_date)	then SHIFT_WEEKEND_END 
	else SHIFT_END end as endmaxprev
,TAsc_date +1  + case 
	when SHIFT_WEEKEND_DIFF_TIME=1 and  wkend.mwk_pat  not like '%'+cast(floor((day(TAsc_date)-1)/7)+1 as varchar(2))+'%' and wkend.mwk_day=datepart(dw,TAsc_date)	then SHIFT_WEEKEND_START 
	else SHIFT_START end as startminnext
,TAsc_date + case when SHIFT_START > SHIFT_END then 1 else 0 end+1 + case 
	when SHIFT_WEEKEND_DIFF_TIME=1 and  wkend.mwk_pat  not like '%'+cast(floor((day(TAsc_date)-1)/7)+1 as varchar(2))+'%' and wkend.mwk_day=datepart(dw,TAsc_date)	then SHIFT_WEEKEND_END 
	else SHIFT_END end  as endmaxnext
	--,LATE_COMING
	--,EARLY_GOING
--	,TDAY_EROT
--,TDAY_LTOT
--	,EXTRA_CHECK
--,EXHRS_BEFORE_SHIFT_HRS
--,EXHRS_AFTER_SHIFT_HRS
--,DED_FROM_EXHRS_EARLY_GOING
--,DED_FROM_EXHRS_LATE_COMING
--,case 
--	when TAsc_Mode = 'i' then  case when TAsc_time<TDAY_INTIME or TDAY_INTIME is null then TAsc_time else TDAY_INTIME end
--	--when TAsc_Mode = 'o' then case when TAsc_time>TDAY_OUTIME or TDAY_OUTIME is null then TAsc_time else TDAY_OUTIME end
--	when TAsc_Mode = 'n' then
--	case when TAsc_time<TDAY_INTIME or TDAY_INTIME is null then TAsc_time else TDAY_INTIME end

--	end as ttin
--	,case 
--	--when TAsc_Mode = 'i' then  case when TAsc_time<TDAY_INTIME or TDAY_INTIME is null then TAsc_time else TDAY_INTIME end
--	when TAsc_Mode = 'o' then case when TAsc_time>TDAY_OUTIME or TDAY_OUTIME is null then TAsc_time else TDAY_OUTIME end
--	when TAsc_Mode = 'n' then
--	case when TAsc_time>TDAY_OUTIME or TDAY_OUTIME is null then TAsc_time else TDAY_OUTIME end

--	end as ttout
,tasc.COMPANY_ID
from [UNOMVC_TRANSACTION].dbo.tasc
join unomvc.dbo.ENT_EMPLOYEE_DTLS e on e.EOD_EMPID=tasc.TAsc_empcode and e.EOD_COMPANY_ID=tasc.[COMPANY_ID]
	join unomvc.dbo.tday tday on tday.TDAY_DATE=TASC.TAsc_date and tday.TDAY_EMPCDE=e.EMPLOYEE_ID  
	join unomvc.[dbo].[TNA_EMPLOYEE_TA_CONFIG] tna on tna.etc_emp_id=e.EMPLOYEE_ID
	--join unomvc.[dbo].ENT_CATEGORY cat on cat.ORG_CATEGORY_ID=e.EOD_CATEGORY_ID and ISDELETED=0 
	join (select * from unomvc.dbo.TA_WKLYOFF  where MWK_OFF=0 and IsDeleted=0) wkoff on TNA.ETC_WEEKOFF=wkoff.MWK_CD and TNA.ETC_ISDELETED=0 
    join (select * from unomvc.dbo.TA_WKLYOFF  where MWK_OFF=1 and IsDeleted=0) wkend on TNA.ETC_WEEKEND=wkend.MWK_CD and TNA.ETC_ISDELETED=0
	join unomvc.dbo.ta_shift tashift on tashift.COMPANY_ID=e.EOD_COMPANY_ID
	and tashift.SHIFT_ID=tday.tday_sftrepo
	--join unomvc.[dbo].[SHIFT_SHIFT_PATTERN] sftpat on sftpat.SHIFT_ID=tday.tday_sftrepo
	WHERE	1=1 and Tasc_flag=0--and TAsc_empcode =@pTasc_empcode
	) tasc
	
	 )b where tascdate is not null
	 --group  by TAsc_empcode,tascdate
	 order by tascdate,tasc_mode
	 




	 --select * from @tmptbl
	 --order by tascdate,tasc_mode

	 --,case when EXTRA_CHECK=1 
--	  then case when TAsc_time < startmin then  startmin-TAsc_time else TDAY_EROT end
--	  else TDAY_EROT end as TDAY_EROT
--,case when EXTRA_CHECK=1 
--	  then TDAY_LTOT 
--	  else TDAY_LTOT end as TDAY_LTOT
	  update t
set t.TDAY_INTIME=intime,t.TDAY_InDATE=intime,t.TDAY_LATE=case when intime > (startmin + LATE_COMING) then intime - (startmin + LATE_COMING) else t.TDAY_LATE end

,t.TDAY_EROT=case when EXTRA_CHECK=1 
                 then case when intime < startmin then  startmin-intime else t.TDAY_EROT end
				 else t.TDAY_EROT end
from unomvc.dbo.tday  t
join unomvc.dbo.ENT_EMPLOYEE_DTLS e on e.EMPLOYEE_ID=t.TDAY_EMPCDE 
join(
	 	 select TAsc_empcode,tascdate,min(tasc_time) as intime,startmin,COMPANY_ID from @tmptbl
		 where tasc_mode in ('i','n')
	 group by tascdate,TAsc_empcode,startmin,COMPANY_ID)c 
	 on c.TAsc_empcode=e.EOD_EMPID and e.EOD_COMPANY_ID=c.[COMPANY_ID] and c.tascdate=t.TDAY_DATE and tascdate is not null
	 join unomvc.[dbo].ENT_CATEGORY cat on cat.ORG_CATEGORY_ID=e.EOD_CATEGORY_ID and ISDELETED=0 

	 update t
set t.TDAY_OUTIME=case when outtime=t.TDAY_INTIME then null else outtime end,t.TDAY_OUDATE=case when outtime=t.TDAY_INTIME then null else outtime end,t.TDAY_EARLY=case when outtime=t.TDAY_INTIME then null else case when outtime < (endmax-EARLY_GOING) then (endmax-EARLY_GOING) -outtime  else t.TDAY_EARLY end end
,t.TDAY_LTOT=case when outtime=t.TDAY_INTIME then null else case when EXTRA_CHECK=1 
                 then case when outtime > endmax  then  outtime-endmax else t.TDAY_LTOT end
				 else t.TDAY_LTOT end end
from unomvc.dbo.tday  t
join unomvc.dbo.ENT_EMPLOYEE_DTLS e on e.EMPLOYEE_ID=t.TDAY_EMPCDE 
join(
	 	 select TAsc_empcode,tascdate,max(tasc_time) as outtime,endmax,COMPANY_ID from @tmptbl
		 where tasc_mode in ('o','n')
	 group by tascdate,TAsc_empcode,endmax,COMPANY_ID)c on c.TAsc_empcode=e.EOD_EMPID and e.EOD_COMPANY_ID=c.[COMPANY_ID] and c.tascdate=t.TDAY_DATE and tascdate is not null
	 join unomvc.[dbo].ENT_CATEGORY cat on cat.ORG_CATEGORY_ID=e.EOD_CATEGORY_ID and ISDELETED=0 




	  update t 

	 set TDAY_LTOT = case when EXTRA_CHECK=1 
						  then case when TDAY_LTOT > EXHRS_AFTER_SHIFT_HRS then TDAY_LTOT else null end
						  else
							TDAY_LTOT
						  end
	, TDAY_EROT = case when EXTRA_CHECK=1 
						  then case when TDAY_EROT > EXHRS_BEFORE_SHIFT_HRS then TDAY_EROT else null end  
						  else
							TDAY_EROT
						  end


from unomvc.dbo.tday  t
join unomvc.dbo.ENT_EMPLOYEE_DTLS e on e.EMPLOYEE_ID=t.TDAY_EMPCDE 
join(
	 	 select distinct TAsc_empcode,tascdate,COMPANY_ID from @tmptbl
		 
)c on c.TAsc_empcode=e.EOD_EMPID and e.EOD_COMPANY_ID=c.[COMPANY_ID] and c.tascdate=t.TDAY_DATE and tascdate is not null
	 join unomvc.[dbo].ENT_CATEGORY cat on cat.ORG_CATEGORY_ID=e.EOD_CATEGORY_ID and ISDELETED=0 


	 update t
	 set TDAY_EXHR =isnull(case when DED_FROM_EXHRS_LATE_COMING=1 and TDAY_LATE<TDAY_LTOT then TDAY_LTOT-TDAY_LATE else TDAY_LTOT end,'1900-01-01') + 
					 isnull(case when DED_FROM_EXHRS_EARLY_GOING=1 and TDAY_EARLY<TDAY_EROT then TDAY_EROT-TDAY_EARLY else TDAY_EROT end,'1900-01-01')
	 ,TDAY_WRKHR=TDAY_OUTIME-TDAY_INTIME
from unomvc.dbo.tday  t
join unomvc.dbo.ENT_EMPLOYEE_DTLS e on e.EMPLOYEE_ID=t.TDAY_EMPCDE 
join(
	 	 select distinct TAsc_empcode,tascdate,COMPANY_ID from @tmptbl
		 
)c on c.TAsc_empcode=e.EOD_EMPID and e.EOD_COMPANY_ID=c.[COMPANY_ID] and c.tascdate=t.TDAY_DATE and tascdate is not null
join unomvc.[dbo].ENT_CATEGORY cat on cat.ORG_CATEGORY_ID=e.EOD_CATEGORY_ID and ISDELETED=0 


update t
set t.TDAY_STATUS_ID = case when t.TDAY_INTIME is not null and t.TDAY_OUTIME is not null then
case when st.tday_status_code in ('AB','PRAB','PR','ABWO','ABW2','ABHO','PRWO','PRW2','PRHO','LWP') then 
case 
  when TDAY_WRKHR > (select cast(value as datetime) from unomvc.dbo.ent_params where module='ta' and identifier='DAILYPROCESS' and code='fd') then (select top 1 TDAY_STATUS_ID from unomvc.dbo.[TDAY_STATUS] where TDAY_STATUS_CODE='pr' + isnull((SUBSTRING(st.Tday_status_code,3,2)),''))
  when TDAY_WRKHR < (select cast(value as datetime) from unomvc.dbo.ent_params where module='ta' and identifier='DAILYPROCESS' and code='fd') and TDAY_WRKHR > (select cast(value as datetime) from unomvc.dbo.ent_params where module='ta' and identifier='DAILYPROCESS' and code='hd') then (select top 1 TDAY_STATUS_ID from unomvc.dbo.[TDAY_STATUS] where TDAY_STATUS_CODE='pr'+ isnull((SUBSTRING(st.Tday_status_code,3,2)),'ab'))
  when TDAY_WRKHR < (select cast(value as datetime) from unomvc.dbo.ent_params where module='ta' and identifier='DAILYPROCESS' and code='hd') then (select top 1 TDAY_STATUS_ID from unomvc.dbo.[TDAY_STATUS] where TDAY_STATUS_CODE='ab'+ isnull((SUBSTRING(st.Tday_status_code,3,2)),''))
  else t.TDAY_STATUS_ID end 
  else t.TDAY_STATUS_ID
  end else t.TDAY_STATUS_ID end
--select *

from unomvc.dbo.tday  t
join unomvc.dbo.ENT_EMPLOYEE_DTLS e on e.EMPLOYEE_ID=t.TDAY_EMPCDE 
join(
	 	 select distinct TAsc_empcode,tascdate,COMPANY_ID from @tmptbl
		 
)c on c.TAsc_empcode=e.EOD_EMPID and e.EOD_COMPANY_ID=c.[COMPANY_ID] and c.tascdate=t.TDAY_DATE and tascdate is not null
join unomvc.[dbo].[TDAY_STATUS] st on st.TDAY_STATUS_ID=t.TDAY_STATUS_ID




update [UNOMVC_TRANSACTION].dbo.tasc set tasc_flag=1 where tasc_flag=0

select @@rowcount












--	 update t 

--	 set TDAY_LTOT = case when EXTRA_CHECK=1 
--						  then case when TDAY_LATE is null 
--								    then case when TDAY_LTOT > EXHRS_AFTER_SHIFT_HRS then TDAY_LTOT else null end
--									else case when DED_FROM_EXHRS_LATE_COMING=1 and TDAY_LATE<TDAY_LTOT
--											  then case when (TDAY_LTOT-TDAY_LATE)> EXHRS_AFTER_SHIFT_HRS then TDAY_LTOT-TDAY_LATE else null end
--											  else case when TDAY_LTOT > EXHRS_AFTER_SHIFT_HRS then TDAY_LTOT else null end 
--											  end
--									end
--						else
--							case when TDAY_LTOT > EXHRS_BEFORE_SHIFT_HRS then TDAY_LTOT else null end
--						end
--	, TDAY_EROT = case when EXTRA_CHECK=1 
--						  then case when TDAY_EARLY is null 
--									then case when TDAY_EROT > EXHRS_BEFORE_SHIFT_HRS then TDAY_EROT else null end  
--									else case when DED_FROM_EXHRS_EARLY_GOING=1 and TDAY_EARLY<TDAY_EROT
--											  then case when (TDAY_EROT-TDAY_EARLY) >EXHRS_BEFORE_SHIFT_HRS then TDAY_EROT-TDAY_EARLY else null end
--											  else case when TDAY_EROT > EXHRS_BEFORE_SHIFT_HRS then TDAY_EROT else null end 
--											  end
--							   end
--						  else
--							case when TDAY_EROT > EXHRS_BEFORE_SHIFT_HRS then TDAY_EROT else null end
--						  end

--from unomvc.dbo.tday  t
--join unomvc.dbo.ENT_EMPLOYEE_DTLS e on e.EMPLOYEE_ID=t.TDAY_EMPCDE
--join(
--	 	 select distinct TAsc_empcode,tascdate from @tmptbl
		 
--)c on c.TAsc_empcode=e.EOD_EMPID and c.tascdate=t.TDAY_DATE and tascdate is not null
--	 join unomvc.[dbo].ENT_CATEGORY cat on cat.ORG_CATEGORY_ID=e.EOD_CATEGORY_ID and ISDELETED=0 

--	 update t
--set t.TDAY_INTIME=min(tasc_time),t.TDAY_InDATE=min(tasc_time)
--from  unomvc.dbo.tday  t
--join unomvc.dbo.ENT_EMPLOYEE_DTLS e on e.EMPLOYEE_ID=t.TDAY_EMPCDE
--join @tmptbl c on c.TAsc_empcode=e.EOD_EMPID and c.tascdate=t.TDAY_DATE and tascdate is not null
--and c.tasc_mode in ('i','n')


--update t
--set t.TDAY_OUTIME=max(tasc_time),t.TDAY_OUDATE=max(tasc_time)
--from  unomvc.dbo.tday  t
--join unomvc.dbo.ENT_EMPLOYEE_DTLS e on e.EMPLOYEE_ID=t.TDAY_EMPCDE
--join @tmptbl c on c.TAsc_empcode=e.EOD_EMPID and c.tascdate=t.TDAY_DATE and tascdate is not null
--and c.tasc_mode in ('o','n')
	
	 --)c 
	 --group  by TAsc_empcode,tascdate,TAsc_Mode

	 --on c.TAsc_empcode=e.EOD_EMPID and c.tascdate=t.TDAY_DATE and tascdate is not null
	--order by TAsc_date
	
	--select * from unomvc.[dbo].[SHIFT_SHIFT_PATTERN]

	--select * from unomvc.[dbo].[ta_SHIFT_PATTERN]


	--select * from unomvc.[dbo].[TNA_EMPLOYEE_TA_CONFIG]


	


	