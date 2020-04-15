----UNOMVC procedure


USE [unomvc]
GO
/****** Object:  StoredProcedure [dbo].[usp_getAccessControlDetails]    Script Date: 22/01/2018 2:04:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create PROCEDURE [dbo].[usp_getAccessControlDetails]
(
	 @strPersonnelType VARCHAR(10)        
 ,@strTA VARCHAR(10)        
 ,@strUID VARCHAR(10)        
 ,@strEventStatus VARCHAR(MAX) = '0'        
 ,@strEmployeeHdn VARCHAR(MAX) = ''        
 ,@strComapnyHdn VARCHAR(MAX) = ''        
 ,@strLocationHdn VARCHAR(MAX) = ''        
 ,@strDivisionHdn VARCHAR(MAX) = ''        
 ,@strDepartmentHdn VARCHAR(MAX) = ''        
 ,@strGradeHdn VARCHAR(MAX) = ''        
 ,@strGroupHdn VARCHAR(MAX) = ''        
 ,@strReaderHdn VARCHAR(MAX) = ''        
 ,@strZoneHdn VARCHAR(MAX) = ''        
 ,@strCalendarFrom VARCHAR(MAX)         
 ,@strToDate VARCHAR(MAX)  
)
as
set nocount on
declare @startyear int
declare @endyear int
declare @thisyear int
declare @tblName varchar(100)
set @startyear=year(convert(datetime,@strCalendarFrom,103))
set @endyear=year(convert(datetime,@strToDate,103))
set @thisyear=year(getdate())

--print @startyear
--print @endyear
--print @thisyear
if(@startyear>@endyear)
begin
	print 'invalid'
	return
end
DECLARE @strQuery VARCHAR(MAX)        
        
SET @strQuery=' SELECT DISTINCT convert(VARCHAR(30), Event_Datetime, 103) + '' '' + convert(VARCHAR(30), Event_Datetime, 108) AS date_time
       
 ,EPD.emp_name        
 ,  CASE 			
 WHEN SUBSTRING( Event_Employee_Code ,1,3) = ''000''	
 THEN  SUBSTRING(Event_Employee_Code, 4, 7) 			
 ELSE Event_Employee_Code 	END AS Event_Employee_Code  
 ,READER_DESCRIPTION AS Reader        
 ,CASE ACEV.Event_Status        
  WHEN ''1''        
   THEN ''ACCESS GRANTED''        
  ELSE ''ACCESS DENIED''        
  END AS Message        
 ,ENT_COMPANY.COMPANY_NAME Company        
 ,GRD.OCE_DESCRIPTION AS Grade        
 ,LOC.OCE_DESCRIPTION AS Location        
 ,DIV.OCE_DESCRIPTION AS Division        
 ,DEP.OCE_DESCRIPTION AS Department
  ,GRP.OCE_DESCRIPTION AS ''Group'',
  Event_Datetime            
  FROM ACS_Events ACEV WITH (NOLOCK)        
  LEFT JOIN ACS_READER WITH (NOLOCK) ON ACEV.Event_Reader_ID = ACS_READER.READER_ID        
   AND ACEV.Event_Controller_ID = ACS_READER.CTLR_ID        
  LEFT JOIN (        
   SELECT EPD_EMPID        
    ,EPD_FIRST_NAME + '' '' + EPD_LAST_NAME AS emp_name        
   FROM ENT_EMPLOYEE_PERSONAL_DTLS        
   ) EPD ON EPD.EPD_EMPID =  (CASE 			
 WHEN SUBSTRING( Event_Employee_Code ,1,3) = ''000''	
 THEN  SUBSTRING(Event_Employee_Code, 4, 7) 			
 ELSE Event_Employee_Code 	END)   
  LEFT JOIN (        
   SELECT EOD_EMPID        
    ,EOD_DIVISION_ID        
    ,EOD_DEPARTMENT_ID        
    ,EOD_GRADE_ID        
    ,EOD_GROUP_ID        
    ,EOD_LOCATION_ID        
    ,EOD_TYPE        
    ,EOD_COMPANY_ID        
   FROM ENT_EMPLOYEE_OFFICIAL_DTLS        
   ) EOD ON EOD.EOD_EMPID = (CASE 			
 WHEN SUBSTRING( Event_Employee_Code ,1,3) = ''000''	
 THEN  SUBSTRING(Event_Employee_Code, 4, 7) 			
 ELSE Event_Employee_Code 	END)
     
  LEFT JOIN ZONE_READER_REL ZR WITH (NOLOCK) ON ZR.CONTROLLER_ID = ACEV.Event_Controller_ID        
   AND ZR.READER_ID = ACEV.Event_Reader_ID        
  LEFT JOIN ACS_CONTROLLER AC WITH (NOLOCK) ON AC.CTLR_ID = ACEV.Event_Controller_ID        
  INNER JOIN ENT_COMPANY ON EOD.EOD_COMPANY_ID = ENT_COMPANY.COMPANY_ID        
  INNER JOIN ENT_ORG_COMMON_ENTITIES AS DIV ON EOD.EOD_DIVISION_ID = DIV.OCE_ID        
   AND DIV.CEM_ENTITY_ID = ''DIV''        
  INNER JOIN ENT_ORG_COMMON_ENTITIES AS DEP ON EOD.EOD_DEPARTMENT_ID = DEP.OCE_ID        
   AND DEP.CEM_ENTITY_ID = ''DEP''        
  INNER JOIN ENT_ORG_COMMON_ENTITIES AS GRD ON EOD.EOD_GRADE_ID = GRD.OCE_ID        
   AND GRD.CEM_ENTITY_ID = ''GRD''        
  INNER JOIN ENT_ORG_COMMON_ENTITIES AS LOC ON EOD.EOD_LOCATION_ID = LOC.OCE_ID        
   AND LOC.CEM_ENTITY_ID = ''LOC''        
  INNER JOIN ENT_ORG_COMMON_ENTITIES AS GRP ON EOD.EOD_GROUP_ID = GRP.OCE_ID        
   AND GRP.CEM_ENTITY_ID = ''GRP''        
  WHERE CTLR_ISDELETED = ''0'''    

  IF (@strPersonnelType = 'E')        
   BEGIN        
    SET @strQuery = @strQuery + ' AND EOD.EOD_TYPE = ''E'''        
   END        
   ELSE IF (@strPersonnelType = 'NE')        
   BEGIN        
    SET @strQuery = @strQuery + ' AND EOD.EOD_TYPE = ''NE'''        
   END        
        
   IF (@strTA <> '0')        
   BEGIN        
    SET @strQuery = @strQuery + ' AND CLTR_FOR_TA = ''' +  CASE WHEN @strTA = '1' THEN 'TA' ELSE '' END + ''''        
   END         
           
       IF (@strEventStatus <> '3')        
       BEGIN        
    SET @strQuery = @strQuery + ' AND Event_Status = ''' + CASE WHEN @strEventStatus = '0' THEN '01' ELSE '02' END + ''''        
    END        
           
   --IF (@strUID <> 'admin')        
   --BEGIN        
    IF (@strEmployeeHdn <> '')        
    BEGIN        
     SET @strQuery = @strQuery + ' AND EOD_EMPID in ' + @strEmployeeHdn         
    END        
        
    IF (@strComapnyHdn <> '')        
    BEGIN        
     SET @strQuery = @strQuery + '  AND COMPANY_ID in ' + @strComapnyHdn       
    END        
        
    IF (@strLocationHdn <> '')        
    BEGIN        
     SET @strQuery = @strQuery + ' AND  LOC.OCE_ID in ' + @strLocationHdn       
    END        
        
    IF (@strDivisionHdn <> '')        
    BEGIN        
     SET @strQuery = @strQuery + ' AND  DIV.OCE_ID in ' + @strDivisionHdn        
    END        
        
    IF (@strDepartmentHdn <> '')        
    BEGIN        
     SET @strQuery = @strQuery + ' AND  DEP.OCE_ID in ' + @strDepartmentHdn        
    END        
        
    IF (@strGradeHdn <> '')        
    BEGIN        
     SET @strQuery = @strQuery + ' AND  GRD.OCE_ID in ' + @strGradeHdn         
    END        
        
    IF (@strGroupHdn <> '')        
    BEGIN        
     SET @strQuery = @strQuery + ' AND  GRP.OCE_ID in ' + @strGroupHdn        
    END        
        
    IF (@strReaderHdn <> '')        
    BEGIN        
     SET @strQuery = @strQuery + ' AND (SELECT Convert(varchar(30), acs_reader.READER_ID)+''-''+convert(varchar(30),acs_reader.CTLR_ID)) IN ' + @strReaderHdn         
    END        
        
    IF (@strZoneHdn <> '')        
    BEGIN        
     SET @strQuery = @strQuery + ' AND (SELECT Convert(varchar(30), acs_reader.READER_ID)+''-''+convert(varchar(30),acs_reader.CTLR_ID))   
     IN (SELECT Convert(varchar(30), READER_ID)+ ''-''+ convert(varchar(30),CONTROLLER_ID) FROM ZONE_READER_REL WHERE  ZONE_ID IN ' + @strZoneHdn        
    END        
   --END        
         
         		   IF (@strCalendarFrom <> '')      
   BEGIN      
    SET @strQuery = @strQuery + ' And convert(datetime,CONVERT(varchar(10),Event_Datetime,103),103) between 
    convert(datetime,CONVERT(varchar(10),''' + @strCalendarFrom + ''',103),103) and 
    convert(datetime,CONVERT(varchar(10),''' + @strToDate + ''',103),103 )'      
   END      

if(@startyear=@thisyear)
	print '--do nothing'
else
begin
	while(@startyear<=@endyear)
	begin
		if(@startyear=@thisyear)
			print '--do nothing'
		else
		begin
			set @tblName='acs_events'+convert(varchar(10),@startyear)
			IF object_id(@tblName, 'U') is not null
			begin
				PRINT '--'+@tblName +' exists! '
				DECLARE @str VARCHAR(MAX)        
				SET @str = ' SELECT DISTINCT convert(VARCHAR(30), Event_Datetime, 103) + '' '' + convert(VARCHAR(30), Event_Datetime, 108) AS date_time
       
 ,EPD.emp_name        
 ,  CASE 			
 WHEN SUBSTRING( Event_Employee_Code ,1,3) = ''000''	
 THEN  SUBSTRING(Event_Employee_Code, 4, 7) 			
 ELSE Event_Employee_Code 	END AS Event_Employee_Code  
 ,READER_DESCRIPTION AS Reader        
 ,CASE ACEV.Event_Status        
  WHEN ''1''        
   THEN ''ACCESS GRANTED''        
  ELSE ''ACCESS DENIED''        
  END AS Message        
 ,ENT_COMPANY.COMPANY_NAME Company        
 ,GRD.OCE_DESCRIPTION AS Grade        
 ,LOC.OCE_DESCRIPTION AS Location        
 ,DIV.OCE_DESCRIPTION AS Division        
 ,DEP.OCE_DESCRIPTION AS Department
  ,GRP.OCE_DESCRIPTION AS ''Group'',
  Event_Datetime            
  FROM '+@tblName+' ACEV WITH (NOLOCK)        
  LEFT JOIN ACS_READER WITH (NOLOCK) ON ACEV.Event_Reader_ID = ACS_READER.READER_ID        
   AND ACEV.Event_Controller_ID = ACS_READER.CTLR_ID        
  LEFT JOIN (        
   SELECT EPD_EMPID        
    ,EPD_FIRST_NAME + '' '' + EPD_LAST_NAME AS emp_name        
   FROM ENT_EMPLOYEE_PERSONAL_DTLS        
   ) EPD ON EPD.EPD_EMPID =  (CASE 			
 WHEN SUBSTRING( Event_Employee_Code ,1,3) = ''000''	
 THEN  SUBSTRING(Event_Employee_Code, 4, 7) 			
 ELSE Event_Employee_Code 	END)   
  LEFT JOIN (        
   SELECT EOD_EMPID        
    ,EOD_DIVISION_ID        
    ,EOD_DEPARTMENT_ID        
    ,EOD_GRADE_ID        
    ,EOD_GROUP_ID        
    ,EOD_LOCATION_ID        
    ,EOD_TYPE        
    ,EOD_COMPANY_ID        
   FROM ENT_EMPLOYEE_OFFICIAL_DTLS        
   ) EOD ON EOD.EOD_EMPID = (CASE 			
 WHEN SUBSTRING( Event_Employee_Code ,1,3) = ''000''	
 THEN  SUBSTRING(Event_Employee_Code, 4, 7) 			
 ELSE Event_Employee_Code 	END)
     
  LEFT JOIN ZONE_READER_REL ZR WITH (NOLOCK) ON ZR.CONTROLLER_ID = ACEV.Event_Controller_ID        
   AND ZR.READER_ID = ACEV.Event_Reader_ID        
  LEFT JOIN ACS_CONTROLLER AC WITH (NOLOCK) ON AC.CTLR_ID = ACEV.Event_Controller_ID        
  INNER JOIN ENT_COMPANY ON EOD.EOD_COMPANY_ID = ENT_COMPANY.COMPANY_ID        
  INNER JOIN ENT_ORG_COMMON_ENTITIES AS DIV ON EOD.EOD_DIVISION_ID = DIV.OCE_ID        
   AND DIV.CEM_ENTITY_ID = ''DIV''        
  INNER JOIN ENT_ORG_COMMON_ENTITIES AS DEP ON EOD.EOD_DEPARTMENT_ID = DEP.OCE_ID        
   AND DEP.CEM_ENTITY_ID = ''DEP''        
  INNER JOIN ENT_ORG_COMMON_ENTITIES AS GRD ON EOD.EOD_GRADE_ID = GRD.OCE_ID        
   AND GRD.CEM_ENTITY_ID = ''GRD''        
  INNER JOIN ENT_ORG_COMMON_ENTITIES AS LOC ON EOD.EOD_LOCATION_ID = LOC.OCE_ID        
   AND LOC.CEM_ENTITY_ID = ''LOC''        
  INNER JOIN ENT_ORG_COMMON_ENTITIES AS GRP ON EOD.EOD_GROUP_ID = GRP.OCE_ID        
   AND GRP.CEM_ENTITY_ID = ''GRP''        
  WHERE CTLR_ISDELETED = ''0'''    

  IF (@strPersonnelType = 'E')        
   BEGIN        
    SET @str = @str + ' AND EOD.EOD_TYPE = ''E'''        
   END        
   ELSE IF (@strPersonnelType = 'NE')        
   BEGIN        
    SET @str = @str + ' AND EOD.EOD_TYPE = ''NE'''        
   END        
        
   IF (@strTA <> '0')        
   BEGIN        
    SET @str = @str + ' AND CLTR_FOR_TA = ''' +  CASE WHEN @strTA = '1' THEN 'TA' ELSE '' END + ''''        
   END         
           
       IF (@strEventStatus <> '3')        
       BEGIN        
    SET @str = @str + ' AND Event_Status = ''' + CASE WHEN @strEventStatus = '0' THEN '01' ELSE '02' END + ''''        
    END        
           
   --IF (@strUID <> 'admin')        
   --BEGIN        
    IF (@strEmployeeHdn <> '')        
    BEGIN        
     SET @str = @str + ' AND EOD_EMPID in ' + @strEmployeeHdn         
    END        
        
    IF (@strComapnyHdn <> '')        
    BEGIN        
     SET @str = @str + '  AND COMPANY_ID in ' + @strComapnyHdn       
    END        
        
    IF (@strLocationHdn <> '')        
    BEGIN        
     SET @str = @str + ' AND  LOC.OCE_ID in ' + @strLocationHdn       
    END        
        
    IF (@strDivisionHdn <> '')        
    BEGIN        
     SET @str = @str + ' AND  DIV.OCE_ID in ' + @strDivisionHdn        
    END        
        
    IF (@strDepartmentHdn <> '')        
    BEGIN        
     SET @str = @str + ' AND  DEP.OCE_ID in ' + @strDepartmentHdn        
    END        
        
    IF (@strGradeHdn <> '')        
    BEGIN        
     SET @str = @str + ' AND  GRD.OCE_ID in ' + @strGradeHdn         
    END        
        
    IF (@strGroupHdn <> '')        
    BEGIN        
     SET @str = @str + ' AND  GRP.OCE_ID in ' + @strGroupHdn        
    END        
        
    IF (@strReaderHdn <> '')        
    BEGIN        
     SET @str = @str + ' AND (SELECT Convert(varchar(30), acs_reader.READER_ID)+''-''+convert(varchar(30),acs_reader.CTLR_ID)) IN ' + @strReaderHdn         
    END        
        
    IF (@strZoneHdn <> '')        
    BEGIN        
     SET @str = @str + ' AND (SELECT Convert(varchar(30), acs_reader.READER_ID)+''-''+convert(varchar(30),acs_reader.CTLR_ID))   
     IN (SELECT Convert(varchar(30), READER_ID)+ ''-''+ convert(varchar(30),CONTROLLER_ID) FROM ZONE_READER_REL WHERE  ZONE_ID IN ' + @strZoneHdn        
    END        
   --END        
         
         		   IF (@strCalendarFrom <> '')      
   BEGIN      
    SET @str = @str + ' And convert(datetime,CONVERT(varchar(10),Event_Datetime,103),103) between 
    convert(datetime,CONVERT(varchar(10),''' + @strCalendarFrom + ''',103),103) and 
    convert(datetime,CONVERT(varchar(10),''' + @strToDate + ''',103),103 )'      
   END      
				set @strQuery = @strQuery + ' 


				UNION 
				

				' +@str
			end
			ELSE
			PRINT '--does not exists'
			--select 1			
		end
			set @startyear=@startyear+1
	end
end



  
          
   SET @strQuery = @strQuery + ' order by Event_Datetime  DESC ' 
print (@strQuery)
exec(@strQuery)

--   exec usp_getAccessControlDetails_test 'E','0','','3','','','','','','','','','','25/12/2017','26/12/2015'