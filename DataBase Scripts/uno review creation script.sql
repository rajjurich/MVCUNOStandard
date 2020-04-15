USE MASTER;

IF DB_ID('UNOMVC') IS NOT NULL
BEGIN
ALTER DATABASE UNOMVC SET  SINGLE_USER WITH ROLLBACK IMMEDIATE
    DROP DATABASE UNOMVC
END
create database UNOMVC
GO
use UNOMVC;
----Visitor 
IF OBJECT_ID (N'DBO.Visitor_Appointment_Request', N'U') IS NOT NULL  
DROP TABLE DBO.Visitor_Appointment_Request;

IF OBJECT_ID (N'DBO.Visitor_Master', N'U') IS NOT NULL  
DROP TABLE DBO.Visitor_Master;

--LEAVE
IF OBJECT_ID (N'DBO.ESS_TA_OD', N'U') IS NOT NULL  
DROP TABLE DBO.ESS_TA_OD;

IF OBJECT_ID (N'DBO.ESS_TA_CO', N'U') IS NOT NULL  
DROP TABLE DBO.ESS_TA_CO;

IF OBJECT_ID (N'DBO.ESS_TA_MA', N'U') IS NOT NULL  
DROP TABLE DBO.ESS_TA_MA;
IF OBJECT_ID (N'DBO.ESS_TA_GP', N'U') IS NOT NULL  
DROP TABLE DBO.ESS_TA_GP;

IF OBJECT_ID (N'DBO.ESS_TA_LA', N'U') IS NOT NULL  
DROP TABLE DBO.ESS_TA_LA;

IF OBJECT_ID (N'DBO.ESS_TA_OPT_HO', N'U') IS NOT NULL  
DROP TABLE DBO.ESS_TA_OPT_HO;

---SENTINAL
IF OBJECT_ID (N'DBO.ACS_CARD_CONFIG', N'U') IS NOT NULL  
DROP TABLE DBO.ACS_CARD_CONFIG;

IF OBJECT_ID (N'DBO.ENT_Reader_MessageTemplate', N'U') IS NOT NULL  
DROP TABLE DBO.ENT_Reader_MessageTemplate;

IF OBJECT_ID (N'DBO.EAL_CONFIG', N'U') IS NOT NULL  
DROP TABLE DBO.EAL_CONFIG;

IF OBJECT_ID (N'DBO.ACS_ACCESSLEVEL_RELATION', N'U') IS NOT NULL  
DROP TABLE DBO.ACS_ACCESSLEVEL_RELATION;

IF OBJECT_ID (N'DBO.ACS_ACCESSLEVEL', N'U') IS NOT NULL  
DROP TABLE DBO.ACS_ACCESSLEVEL;

IF OBJECT_ID (N'DBO.ACS_TIMEZONE_RELATION', N'U') IS NOT NULL  
DROP TABLE DBO.ACS_TIMEZONE_RELATION;

IF OBJECT_ID (N'DBO.ACS_TIMEZONE', N'U') IS NOT NULL  
DROP TABLE DBO.ACS_TIMEZONE;

IF OBJECT_ID (N'DBO.ZONE_READER_REL', N'U') IS NOT NULL  
DROP TABLE DBO.ZONE_READER_REL;

IF OBJECT_ID (N'DBO.ZONE', N'U') IS NOT NULL  
DROP TABLE DBO.ZONE;




IF OBJECT_ID (N'DBO.ACS_DOOR', N'U') IS NOT NULL  
DROP TABLE DBO.ACS_DOOR;

IF OBJECT_ID (N'DBO.ACS_ACCESSPOINT_RELATION', N'U') IS NOT NULL  
DROP TABLE DBO.ACS_ACCESSPOINT_RELATION;

IF OBJECT_ID (N'DBO.ACS_READER', N'U') IS NOT NULL  
DROP TABLE DBO.ACS_READER;

IF OBJECT_ID (N'DBO.ACS_CONTROLLER', N'U') IS NOT NULL  
DROP TABLE DBO.ACS_CONTROLLER;






----TEMPUS

IF OBJECT_ID (N'DBO.TDAY', N'U') IS NOT NULL  
DROP TABLE DBO.TDAY;

IF OBJECT_ID (N'DBO.TDAY_STATUS', N'U') IS NOT NULL  
DROP TABLE DBO.TDAY_STATUS;

---LEAVE

IF OBJECT_ID (N'DBO.TA_LEAVE_SUMMARY', N'U') IS NOT NULL  
DROP TABLE DBO.TA_LEAVE_SUMMARY; 


IF OBJECT_ID (N'DBO.TA_LEAVE_YEAR', N'U') IS NOT NULL  
DROP TABLE DBO.TA_LEAVE_YEAR; 


IF OBJECT_ID (N'DBO.TA_LEAVE_RULE_NEW', N'U') IS NOT NULL  
DROP TABLE DBO.TA_LEAVE_RULE_NEW; 
IF OBJECT_ID (N'DBO.TA_LEAVE_RULE', N'U') IS NOT NULL  
DROP TABLE DBO.TA_LEAVE_RULE; 

IF OBJECT_ID (N'DBO.TA_LEAVE_ENTITIES', N'U') IS NOT NULL  
DROP TABLE DBO.TA_LEAVE_ENTITIES; 

IF OBJECT_ID (N'DBO.TA_Leave_File', N'U') IS NOT NULL  
DROP TABLE DBO.TA_Leave_File; 


IF OBJECT_ID (N'DBO.TA_WKLYOFF', N'U') IS NOT NULL  
DROP TABLE DBO.TA_WKLYOFF; 

----SHIFT


IF OBJECT_ID (N'DBO.TNA_EMPLOYEE_TA_CONFIG', N'U') IS NOT NULL  
DROP TABLE DBO.TNA_EMPLOYEE_TA_CONFIG; 

IF OBJECT_ID (N'DBO.SHIFT_SHIFT_PATTERN', N'U') IS NOT NULL  
DROP TABLE DBO.SHIFT_SHIFT_PATTERN; 

IF OBJECT_ID (N'DBO.TA_SHIFT_PATTERN', N'U') IS NOT NULL  
DROP TABLE DBO.TA_SHIFT_PATTERN; 

IF OBJECT_ID (N'DBO.TA_SHIFT', N'U') IS NOT NULL  
DROP TABLE DBO.TA_SHIFT; 


---CATEGORY




IF OBJECT_ID (N'DBO.ENT_CATEGORY', N'U') IS NOT NULL  
DROP TABLE DBO.ENT_CATEGORY; 


--RoLE


IF OBJECT_ID (N'DBO.ENT_ROLE_DATA_ACCESS', N'U') IS NOT NULL  
DROP TABLE DBO.ENT_ROLE_DATA_ACCESS; 


IF OBJECT_ID (N'DBO.ENT_User', N'U') IS NOT NULL  
DROP TABLE DBO.ENT_User;  
IF OBJECT_ID (N'DBO.ENT_ROLE_MENU_ACCESS', N'U') IS NOT NULL  
DROP TABLE DBO.ENT_ROLE_MENU_ACCESS;  


IF OBJECT_ID (N'DBO.ENT_ROLE_MASTER', N'U') IS NOT NULL  
DROP TABLE DBO.ENT_ROLE_MASTER;  

--MENU

IF OBJECT_ID (N'DBO.ENT_MENU_MASTER', N'U') IS NOT NULL  
DROP TABLE DBO.ENT_MENU_MASTER;  

IF OBJECT_ID (N'DBO.ENT_SUB_MODULE_MASTER', N'U') IS NOT NULL  
DROP TABLE DBO.ENT_SUB_MODULE_MASTER;

IF OBJECT_ID (N'DBO.ENT_MODULE_MASTER', N'U') IS NOT NULL  
DROP TABLE DBO.ENT_MODULE_MASTER; 
IF OBJECT_ID (N'DBO.MenuCompanyRelation', N'U') IS NOT NULL  
DROP TABLE DBO.MenuCompanyRelation;  



 

--EMPLOYEE


--IF OBJECT_ID (N'DBO.checkhierarchy', N'FN') IS NOT NULL  
--DROP FUNCTION DBO.checkhierarchy; 


IF OBJECT_ID (N'DBO.ENT_EMPLOYEE_LEFT', N'U') IS NOT NULL  
DROP TABLE DBO.ENT_EMPLOYEE_LEFT;  

IF OBJECT_ID (N'DBO.ENT_HierarchyDef', N'U') IS NOT NULL  
DROP TABLE DBO.ENT_HierarchyDef;  


IF OBJECT_ID (N'DBO.[ENT_EMPLOYEE_FAMILY_DETAILS]', N'U') IS NOT NULL  
DROP TABLE DBO.[ENT_EMPLOYEE_FAMILY_DETAILS]; 

IF OBJECT_ID (N'DBO.ENT_EMPLOYEE_Nomineedetails', N'U') IS NOT NULL  
DROP TABLE DBO.ENT_EMPLOYEE_Nomineedetails; 

IF OBJECT_ID (N'DBO.[ENT_NOMINEE_TYPES]', N'U') IS NOT NULL  
DROP TABLE DBO.[ENT_NOMINEE_TYPES]; 

IF OBJECT_ID (N'DBO.ENT_EMPLOYEE_ADDRESS', N'U') IS NOT NULL  
DROP TABLE DBO.ENT_EMPLOYEE_ADDRESS; 
IF OBJECT_ID (N'DBO.ENT_EMPLOYEE_PERSONAL_DTLS', N'U') IS NOT NULL  
DROP TABLE DBO.ENT_EMPLOYEE_PERSONAL_DTLS; 
IF OBJECT_ID (N'DBO.ENT_EMPLOYEE_DTLS_HISTORY', N'U') IS NOT NULL  
DROP TABLE DBO.ENT_EMPLOYEE_DTLS_HISTORY; 
IF OBJECT_ID (N'DBO.ENT_EMPLOYEE_DTLS', N'U') IS NOT NULL  
DROP TABLE DBO.ENT_EMPLOYEE_DTLS; 
--HOLIDAY
IF OBJECT_ID (N'DBO.ENT_HOLIDAYLOC', N'U') IS NOT NULL  
DROP TABLE DBO.ENT_HOLIDAYLOC; 
IF OBJECT_ID (N'DBO.ENT_HOLIDAY', N'U') IS NOT NULL  
DROP TABLE DBO.ENT_HOLIDAY; 

--REASON
IF OBJECT_ID (N'DBO.ENT_REASON', N'U') IS NOT NULL  
DROP TABLE DBO.ENT_REASON; 
IF OBJECT_ID (N'DBO.ENT_REASON_TYPE', N'U') IS NOT NULL  
DROP TABLE DBO.ENT_REASON_TYPE;  

--COMMON
IF OBJECT_ID (N'DBO.ENT_ORG_COMMON_ENTITIES', N'U') IS NOT NULL  
DROP TABLE DBO.ENT_ORG_COMMON_ENTITIES;  
IF OBJECT_ID (N'DBO.ENT_ORG_COMMON_TYPES', N'U') IS NOT NULL  
DROP TABLE DBO.ENT_ORG_COMMON_TYPES;  


----COMPANY
IF OBJECT_ID (N'DBO.ENT_COMPANY_DETAILS', N'U') IS NOT NULL  
DROP TABLE DBO.ENT_COMPANY_DETAILS;
IF OBJECT_ID (N'DBO.ENT_COMPANY', N'U') IS NOT NULL  
DROP TABLE DBO.ENT_COMPANY;
IF OBJECT_ID (N'DBO.ENT_COMPANY_ADDRESS_TYPE', N'U') IS NOT NULL  
DROP TABLE DBO.ENT_COMPANY_ADDRESS_TYPE;


----CONTROLLER
IF OBJECT_ID (N'DBO.ENT_PARAMS', N'U') IS NOT NULL  
DROP TABLE DBO.ENT_PARAMS;

CREATE SEQUENCE autogeneratepin
START WITH 1111
INCREMENT BY 1
MINVALUE 1111
MAXVALUE 9999 ;
	

CREATE TABLE ENT_COMPANY
(
	[COMPANY_ID] int primary key Identity, 
	[COMPANY_CODE] nvarchar(15) not null,
	[COMPANY_NAME] [varchar](50) NULL,	
	[COMPANY_EMP_CODE_LENGTH] INT not null,
	[COMPANY_CREATEDDATE] [datetime] NULL,
	[COMPANY_CREATEDBY] varchar(10),
	[COMPANY_MODIFIEDDATE] [datetime] NULL,	
	[COMPANY_MODIFIEDBY] varchar(10),
	[COMPANY_ISDELETED] [bit] NULL,
	[COMPANY_DELETEDDATE] [datetime] NULL,
	[COMPANY_DELETEDBY] varchar(10)	,
	[IS_SYNC] [bit] NOT NULL DEFAULT 0,
	CONSTRAINT ENT_COMPANY_COMPANY_CODE_COMPANY_ISDELETED_Composite UNIQUE(COMPANY_CODE,COMPANY_ISDELETED)
)


CREATE TABLE ENT_COMPANY_ADDRESS_TYPE
(
	ADDRESS_TYPE_ID int primary key identity,
	ADDRESS_TYPE nvarchar(20) unique not null,
    [IS_SYNC] [bit] NOT NULL DEFAULT 0,
)

insert into ENT_COMPANY_ADDRESS_TYPE(ADDRESS_TYPE) values ('HEAD OFFICE') ,('REGIONAL OFFICE')


CREATE TABLE ENT_COMPANY_DETAILS
(
	COMPANY_ADDRESS_ID int primary key Identity, 
	[COMPANY_ADDRESS] [varchar](150) NULL,
	[COMPANY_CITY] [varchar](20) NULL,
	[COMPANY_PIN] [varchar](10) NULL,
	[COMPANY_PHONE1] [varchar](15) NULL,
	[COMPANY_PHONE2] [varchar](15) NULL,	
	[COMPANY_STATE] [varchar](50) NULL,
	[COMPANY_ISDELETED] [bit] NULL,
	[ADDRESS_TYPE_ID] int foreign key references ENT_COMPANY_ADDRESS_TYPE(ADDRESS_TYPE_ID),
	[COMPANY_ID] [int] foreign key references ENT_COMPANY(COMPANY_ID),
	[IS_SYNC] [bit] NOT NULL DEFAULT 0
)

	
CREATE TABLE [dbo].[ENT_ORG_COMMON_TYPES](
	[COMMON_TYPES_ID] [int] primary key IDENTITY(1,1) NOT NULL,
	[COMMON_TYPES] [nvarchar](15) unique NOT NULL,
	[IS_SYNC] [bit] NOT NULL DEFAULT 0,
	[COMPANY_ID] [int] foreign key references ENT_COMPANY(COMPANY_ID),	
	[ORG_TYPES_ORDER] [int]
	CONSTRAINT COMMON_TYPES_COMPANY_ID_Composite UNIQUE(COMMON_TYPES,COMPANY_ID)
)

insert into ENT_ORG_COMMON_TYPES(COMMON_TYPES,ORG_TYPES_ORDER) values('LOCATION',3),('GRADE',7),('GROUP',6),('CATEGORY',2),('COMPANY',1),('DIVISION',4),('DEPARTMENT',5),('DESIGNATION',8),('EMPLOYEE',9)




CREATE TABLE [dbo].[ENT_ORG_COMMON_ENTITIES]
(
	[ID] [int] IDENTITY(1,1) primary key,
	[COMMON_TYPES_ID] int foreign key references ENT_ORG_COMMON_TYPES(COMMON_TYPES_ID),	
	[OCE_ID] [nvarchar](100) NOT NULL,
	[OCE_DESCRIPTION] [nvarchar](50) NULL,
	[OCE_CREATEDDATE] [datetime] NULL,
	[OCE_CREATEDBY] varchar(10),
	[OCE_MODIFIEDDATE] [datetime] NULL,	
	[OCE_MODIFIEDBY] varchar(10),
	[OCE_ISDELETED] [bit] NULL,
	[OCE_DELETEDDATE] [datetime] NULL,
	[OCE_DELETEDBY] varchar(10)	,
	[IS_SYNC] [bit] NOT NULL DEFAULT 0,
	[COMPANY_ID] [int] foreign key references ENT_COMPANY(COMPANY_ID)
	CONSTRAINT OCE_ID_CommonType_COMPANY_ID_Composite UNIQUE(OCE_ID,COMMON_TYPES_ID,COMPANY_ID,OCE_ISDELETED)
)
	
CREATE TABLE ENT_REASON_TYPE
(
	REASON_TYPE_ID int primary key identity,
	REASON_TYPE nvarchar(5) unique not null,
	REASON_DESC nvarchar(50),
	[IS_SYNC] [bit] NOT NULL DEFAULT 0,
	[COMPANY_ID] [int] foreign key references ENT_COMPANY(COMPANY_ID),	
)
INSERT INTO [dbo].[ENT_REASON_TYPE]
           ([REASON_TYPE]
           ,[REASON_DESC]
           )
     VALUES
           ('MA','MANUAL ATTENDANCE APPLICATION'),
('LA','LEAVE APPLICATION'),
('OT','OVERTIME'),
('GP','OUTPASS'),
('LE','LEAVE ENCASHMENT'),
('LAV','LEAVE APPLICATION ACTION'),
('MR','MANUAL ATTENDANCE ACTION'),
('CO','COMPENSATORY OFF'),
('OD','OUTDOOR DUTY'),
('EL','EMPLOYEE LEFT')
GO
CREATE TABLE ENT_REASON
(
	REASON_ID int primary key identity,
	REASON_CODE nvarchar(5) not null,
	REASON_DESC nvarchar(50),
	[REASON_ISDELETED] [bit] NULL,
	[IS_SYNC] [bit] NOT NULL DEFAULT 0,
	REASON_TYPE_ID [int] foreign key references ENT_REASON_TYPE(REASON_TYPE_ID),
	[COMPANY_ID] [int] foreign key references ENT_COMPANY(COMPANY_ID),	
	CONSTRAINT REASON_CODE_COMPANY_ID_Composite UNIQUE(REASON_CODE,COMPANY_ID)
)

	
CREATE TABLE [dbo].[ENT_HOLIDAY]
(
	[HOLIDAY_ID] int identity(1,1) primary key,
	[HOLIDAY_CODE] [nvarchar](15) NOT NULL,
	[HOLIDAY_DESCRIPTION] [nvarchar](50) NULL,
	[HOLIDAY_TYPE] [nvarchar](2) NULL,
	[HOLIDAY_DATE] [datetime] NULL,
	[HOLIDAY_SWAP] [datetime] NULL,
	[HOLIDAY_CREATEDDATE] [datetime] NULL,
	[HOLIDAY_CREATEDBY] varchar(10),
	[HOLIDAY_MODIFIEDDATE] [datetime] NULL,	
	[HOLIDAY_MODIFIEDBY] varchar(10),
	[HOLIDAY_ISDELETED] [bit] NULL,
	[HOLIDAY_DELETEDDATE] [datetime] NULL,
	[HOLIDAY_DELETEDBY] varchar(10),
    [IS_SYNC] [bit] NOT NULL DEFAULT 0,
	[COMPANY_ID] [int] foreign key references ENT_COMPANY(COMPANY_ID),	
	CONSTRAINT HOLIDAY_CODE_COMPANY_ID_Composite UNIQUE(HOLIDAY_CODE,COMPANY_ID)
)


CREATE TABLE [dbo].[ENT_HOLIDAYLOC]
(
	[HOLIDAYLOC_ID] int identity(1,1) primary key,
	[HOLIDAY_ID] int foreign key references ENT_HOLIDAY(HOLIDAY_ID),
	[IS_OPTIONAL] [bit] NULL,
	[IS_SYNC] [bit] NOT NULL DEFAULT 0,
	[HOLIDAY_LOC_ID] int foreign key references ENT_ORG_COMMON_ENTITIES(ID),
	[COMPANY_ID] [int] foreign key references ENT_COMPANY(COMPANY_ID),	
) 


CREATE TABLE [dbo].[ENT_EMPLOYEE_DTLS]
(
	[EMPLOYEE_ID] int identity(1,1) primary key,
	[EOD_EMPID] [nvarchar](15) NOT NULL,
	[EPD_SALUTATION] [nvarchar](50) NULL,
	[EPD_FIRST_NAME] [nvarchar](50) NULL,
	[EPD_MIDDLE_NAME] [nvarchar](50) NULL,
	[EPD_LAST_NAME] [nvarchar](50) NULL,
	[EPD_PERSO_FLAG] [varchar](5) NULL,
	[EPD_CARD_ID] [nvarchar](15) NULL,
	[EOD_JOINING_DATE] [datetime] NULL,
	[EOD_CONFIRM_DATE] [datetime] NULL,
	[EOD_LEFT_DATE] [datetime] NULL,
	[EOD_LEFT_REASON_ID] [int] foreign key references ENT_REASON(REASON_ID),
	[EOD_COMPANY_ID] int foreign key references ENT_COMPANY(COMPANY_ID),
	[EOD_LOCATION_ID] [int] foreign key references ENT_ORG_COMMON_ENTITIES(ID),
	[EOD_DIVISION_ID] [int] foreign key references ENT_ORG_COMMON_ENTITIES(ID),
	[EOD_DEPARTMENT_ID] [int] foreign key references ENT_ORG_COMMON_ENTITIES(ID),
	[EOD_DESIGNATION_ID] [int] foreign key references ENT_ORG_COMMON_ENTITIES(ID),
	[EOD_CATEGORY_ID] [int] foreign key references ENT_ORG_COMMON_ENTITIES(ID),
	[EOD_GROUP_ID] [int] foreign key references ENT_ORG_COMMON_ENTITIES(ID),
	[EOD_GRADE_ID] [int] foreign key references ENT_ORG_COMMON_ENTITIES(ID),
	[EOD_STATUS] [int] NULL,
	[EOD_ISDELETED] [bit] NULL,
	[EOD_DELETEDDATE] [datetime] NULL,
	[EOD_TYPE] [varchar](30) NULL,
	[EOD_WORKTYPE] [varchar](300) NULL,
	[EOD_CARD_PIN] [varchar](max) NULL,
	[IS_SYNC] [bit] NOT NULL DEFAULT 0,
	[PREVIOUS_EMPLOYEE_ID] [int] foreign key references ENT_EMPLOYEE_DTLS(EMPLOYEE_ID),
	--CONSTRAINT EmployeenameComposite UNIQUE(EPD_FIRST_NAME, EPD_MIDDLE_NAME,EPD_LAST_NAME,EOD_ISDELETED),
	CONSTRAINT EmployeeIDComposite UNIQUE(EOD_EMPID,EOD_COMPANY_ID)
)


CREATE TABLE [dbo].[ENT_EMPLOYEE_DTLS_HISTORY]
(
	[ROW_ID] int identity(1,1) primary key,
	[EMPLOYEE_ID] int foreign key references ENT_EMPLOYEE_DTLS(EMPLOYEE_ID),
	[EOD_EMPID] [nvarchar](15) NOT NULL,
	[EPD_SALUTATION] [nvarchar](50) NULL,
	[EPD_FIRST_NAME] [nvarchar](50) NULL,
	[EPD_MIDDLE_NAME] [nvarchar](50) NULL,
	[EPD_LAST_NAME] [nvarchar](50) NULL,
	[EPD_PERSO_FLAG] [varchar](5) NULL,
	[EPD_CARD_ID] [nvarchar](15) NULL,
	[EOD_JOINING_DATE] [datetime] NULL,
	[EOD_CONFIRM_DATE] [datetime] NULL,
	[EOD_LEFT_DATE] [datetime] NULL,
	[EOD_LEFT_REASON_ID] [int] foreign key references ENT_REASON(REASON_ID),
	[EOD_COMPANY_ID] int foreign key references ENT_COMPANY(COMPANY_ID),
	[EOD_LOCATION_ID] [int] foreign key references ENT_ORG_COMMON_ENTITIES(ID),
	[EOD_DIVISION_ID] [int] foreign key references ENT_ORG_COMMON_ENTITIES(ID),
	[EOD_DEPARTMENT_ID] [int] foreign key references ENT_ORG_COMMON_ENTITIES(ID),
	[EOD_DESIGNATION_ID] [int] foreign key references ENT_ORG_COMMON_ENTITIES(ID),
	[EOD_CATEGORY_ID] [int] foreign key references ENT_ORG_COMMON_ENTITIES(ID),
	[EOD_GROUP_ID] [int] foreign key references ENT_ORG_COMMON_ENTITIES(ID),
	[EOD_GRADE_ID] [int] foreign key references ENT_ORG_COMMON_ENTITIES(ID),
	[EOD_STATUS] [int] NULL,
	[EOD_ISDELETED] [bit] NULL,
	[EOD_DELETEDDATE] [datetime] NULL,
	[EOD_TYPE] [varchar](30) NULL,
	[EOD_WORKTYPE] [varchar](300) NULL,
	[EOD_CARD_PIN] [varchar](max) NULL,
	[IS_SYNC] [bit] NOT NULL DEFAULT 0,
	[PREVIOUS_EMPLOYEE_ID] [int] foreign key references ENT_EMPLOYEE_DTLS(EMPLOYEE_ID),
	[EMP_MODIFIEDDATE] [datetime] NULL,	
	[EMP_MODIFIEDBY] varchar(10) not null,	
)
	
CREATE TABLE [dbo].[ENT_EMPLOYEE_PERSONAL_DTLS]
(
	[EMPLOYEE_ID] int primary key,
	[EPD_GENDER] [int] NULL,
	[EPD_MARITAL_STATUS] [int] NULL,
	[EPD_DOB] [datetime] NULL,
	[EPD_DateOFMarriage] [datetime] NULL,
	[EPD_RELIGION] [int] NULL,
	[EPD_REFERENCE_ONE] [nvarchar](50) NULL,
	[EPD_REFERENCE_TWO] [nvarchar](50) NULL,
	[EPD_DOMICILE] [nvarchar](50) NULL,
	[EPD_BLOODGROUP] [int] NULL,
	[EPD_EMAIL] [nvarchar](50) NULL,
	[EPD_PAN] [nvarchar](15) NULL,	
	[EPD_PHOTOURL] [nvarchar](max) NULL,
	[EPD_AADHARCARD] [varchar](30) NULL,
	[EPD_UAN] [varchar](30) NULL,
	[EPD_ISDELETED] [bit] NULL,
	[EPD_DELETEDDATE] [datetime] NULL,
	[IS_SYNC] [bit] NOT NULL DEFAULT 0,
	CONSTRAINT PERSONAL_DTLS_EmployeeId foreign key(EMPLOYEE_ID) references ENT_EMPLOYEE_DTLS(EMPLOYEE_ID)
)


	
CREATE TABLE [dbo].[ENT_EMPLOYEE_ADDRESS]
(
	[EMPLOYEE_ADDRESS] int primary key identity,
	[EMPLOYEE_ID] int foreign key references ENT_EMPLOYEE_DTLS(EMPLOYEE_ID),
	[EAD_ADDRESS_TYPE] [nvarchar](20) NOT NULL,
	[EAD_ADDRESS] [nvarchar](200) NULL,
	[EAD_CITY] [nvarchar](50) NULL,
	[EAD_PIN] [nvarchar](10) NULL,
	[EAD_STATE] [nvarchar](50) NULL,
	[EAD_COUNTRY] [nvarchar](50) NULL,
	[EAD_PHONE_ONE] [nvarchar](50) NULL,
	[EAD_PHONE_TWO] [nvarchar](50) NULL,
	[EAD_ISDELETED] [bit] NULL,
	[EAD_DELETEDDATE] [datetime] NULL,
	[IS_SYNC] [bit] NOT NULL DEFAULT 0,
)
	


CREATE TABLE [dbo].[ENT_NOMINEE_TYPES](
	[NOMINEE_TYPE_ID] [int] IDENTITY(1,1) primary key,
	[NOMINEE_TYPE] varchar(50),
    [NOMINEE_ISDELETED] [bit] NOT NULL DEFAULT 0,
	[NOMINEE_DELETEDDATE] [datetime] NULL,
	[IS_SYNC] [bit] NOT NULL DEFAULT 0,
) 

INSERT INTO ENT_NOMINEE_TYPES(NOMINEE_TYPE) VALUES('PF'),('GRATUITY'),('ESIC'),('MEDICLAIM')

CREATE TABLE [dbo].[ENT_EMPLOYEE_Nomineedetails](
	[NOMINEE_DETAIL_ID] [int] IDENTITY(1,1) primary key,
	[EMPLOYEE_ID] int foreign key references ENT_EMPLOYEE_DTLS(EMPLOYEE_ID),
	[Nominee] [varchar](200) NULL,
	[NomineesAddress] [varchar](200) NULL,
	[Relation] [varchar](80) NULL,
	[BirthDate] [datetime] NULL,
	[SharePercent] decimal(12,2) NULL,
	[GuardianAddress] [varchar](200) NULL,
	[IS_SYNC] [bit] NOT NULL DEFAULT 0,
	[NOMINEE_TYPE_ID] int foreign key references ENT_NOMINEE_TYPES(NOMINEE_TYPE_ID)
) 

CREATE TABLE [dbo].[ENT_EMPLOYEE_FAMILY_DETAILS](
	[FAMILY_DETAIL_ID] [int] IDENTITY(1,1) primary key,
	[EMPLOYEE_ID] int foreign key references ENT_EMPLOYEE_DTLS(EMPLOYEE_ID),
	[FIRSTNAME] [varchar](200) NULL,
	[LASTNAME] [varchar](200) NULL,
	[BirthDate] [datetime] NULL,
	[Gender] [varchar](10) NULL,
	[RELATIONTYPE][varchar](2),
	[IS_SYNC] [bit] NOT NULL DEFAULT 0,	
) 


CREATE TABLE [dbo].[ENT_HierarchyDef](
	ENT_HierarchyDef_ID [int] IDENTITY(1,1) primary key,
	[Hier_Emp_ID] int foreign key references ENT_EMPLOYEE_DTLS(EMPLOYEE_ID),
	[Hier_Mgr_ID] int foreign key references ENT_EMPLOYEE_DTLS(EMPLOYEE_ID) null,
	[EOD_DESIGNATION_ID] [int] foreign key references ENT_ORG_COMMON_ENTITIES(ID),
	[Hier_IsDeleted] [bit] NULL,
	[Hier_DeletedDate] [datetime] NULL,
	[Hier_Entity] [varchar](15) NULL,
	[IS_SYNC] [bit] NOT NULL DEFAULT 0,
	CONSTRAINT [ck_hierarchy_eq] CHECK  (([Hier_Mgr_ID]<>[Hier_Emp_ID])),
	CONSTRAINT HierarchyDefComposite UNIQUE(Hier_Emp_ID, Hier_Mgr_ID,EOD_DESIGNATION_ID,Hier_IsDeleted),
)

CREATE TABLE [dbo].[ENT_EMPLOYEE_LEFT](
	[EL_ColumnID] [bigint] IDENTITY(1,1) primary key,
	[EL_EMP_ID] int foreign key references ENT_EMPLOYEE_DTLS(EMPLOYEE_ID),	
	[EL_LEFT_DATE] [datetime] NULL,	
	[EL_REASONID] [int] foreign key references ENT_REASON(REASON_ID),
	[EL_CREATEDDATE] [datetime] NULL,
	[EL_CREATEDBY] varchar(10),
	[EL_MODIFIEDDATE] [datetime] NULL,	
	[EL_MODIFIEDBY] varchar(10),
	[EL_ISDELETED] [bit] NULL,
	[EL_DELETEDDATE] [datetime] NULL,
	[EL_DELETEDBY] varchar(10)	,
	[IS_SYNC] [bit] NOT NULL DEFAULT 0
	)
--;
--go
--CREATE FUNCTION [dbo].[checkhierarchy](@t2 int)
--RETURNS int as
--begin
--declare @cc int
--select @cc=count(1) from ENT_HierarchyDef where Hier_Emp_ID=@t2
--return @cc
--end



create table ENT_MODULE_MASTER
(
	MODULE_ID int identity(1,1) primary key,
	MODULE_NAME varchar(50)
)


create table ENT_SUB_MODULE_MASTER
(
	SMODULE_ID int identity(1,1) primary key,
	SMODULE_NAME varchar(50),
	MODULE_ID int foreign key references  ENT_MODULE_MASTER(MODULE_ID)

)

---------- Commented by Sachin and added new query below --
----------insert into ENT_MODULE_MASTER values('Core Configuration');
----------insert into ENT_MODULE_MASTER values('Employee Self Service');
----------insert into ENT_MODULE_MASTER values ('Tempus');
----------insert into ENT_MODULE_MASTER values ('Sentinal' );
----------insert into ENT_MODULE_MASTER values('PERSO');
----------insert into ENT_MODULE_MASTER values('Vehicle Mangement');
----------insert into ENT_MODULE_MASTER values('Visitor Management');
---------- Commented by Sachin and added new query --

delete from [ENT_SUB_MODULE_MASTER]
delete from ENT_MODULE_MASTER
DBCC CHECKIDENT (ENT_MODULE_MASTER, RESEED, 0)
DBCC CHECKIDENT ([ENT_SUB_MODULE_MASTER], RESEED, 0)

SET IDENTITY_INSERT ENT_MODULE_MASTER ON

insert into ENT_MODULE_MASTER (MODULE_ID,MODULE_NAME) values 
                (1,'Core Configuration'),
				(2,'Employee Self Service'),(3,'Tempus'),
				(4,'Sentinal' ),(5,'PERSO'),
				(6,'Vehicle Mangement'),(7,'Visitor Management'),
				(8,'Asset Tracking System');
SET IDENTITY_INSERT ENT_MODULE_MASTER OFF

-- insert qurry for sub module

insert into ENT_SUB_MODULE_MASTER values('Core Configuration', (select MODULE_ID from ENT_MODULE_MASTER where MODULE_NAME ='Core Configuration'));
insert into ENT_SUB_MODULE_MASTER values('System Access Configuration', (select MODULE_ID from ENT_MODULE_MASTER where MODULE_NAME ='Core Configuration'));
insert into ENT_SUB_MODULE_MASTER values('System Maintenance', (select MODULE_ID from ENT_MODULE_MASTER where MODULE_NAME ='Core Configuration'));

insert into ENT_SUB_MODULE_MASTER values('Dashboard',(select MODULE_ID from ENT_MODULE_MASTER where MODULE_NAME ='Employee Self Service'));
insert into ENT_SUB_MODULE_MASTER values('Transactions',(select MODULE_ID from ENT_MODULE_MASTER where MODULE_NAME ='Employee Self Service'));
insert into ENT_SUB_MODULE_MASTER values('Reports' ,(select MODULE_ID from ENT_MODULE_MASTER where MODULE_NAME ='Employee Self Service'));

insert into ENT_SUB_MODULE_MASTER values('Core Configuration',(select MODULE_ID from ENT_MODULE_MASTER where MODULE_NAME ='Sentinal'));
insert into ENT_SUB_MODULE_MASTER values('Transactions',(select MODULE_ID from ENT_MODULE_MASTER where MODULE_NAME ='Sentinal'));
insert into ENT_SUB_MODULE_MASTER values('Reports' ,(select MODULE_ID from ENT_MODULE_MASTER where MODULE_NAME ='Sentinal'));

insert into ENT_SUB_MODULE_MASTER values('Core Configuration',(select MODULE_ID from ENT_MODULE_MASTER where MODULE_NAME ='Tempus'));
insert into ENT_SUB_MODULE_MASTER values('Transactions',(select MODULE_ID from ENT_MODULE_MASTER where MODULE_NAME ='Tempus'));
insert into ENT_SUB_MODULE_MASTER values('Reports' ,(select MODULE_ID from ENT_MODULE_MASTER where MODULE_NAME ='Tempus'));


insert into ENT_SUB_MODULE_MASTER values('Card Printing',(select MODULE_ID from ENT_MODULE_MASTER where MODULE_NAME ='PERSO'));
insert into ENT_SUB_MODULE_MASTER values('BEAS',(select MODULE_ID from ENT_MODULE_MASTER where MODULE_NAME ='PERSO'));
insert into ENT_SUB_MODULE_MASTER values('Card Personalisation' ,(select MODULE_ID from ENT_MODULE_MASTER where MODULE_NAME ='PERSO'));

insert into ENT_SUB_MODULE_MASTER values('VMS Admin',(select MODULE_ID from ENT_MODULE_MASTER where MODULE_NAME ='Visitor Management'));
insert into ENT_SUB_MODULE_MASTER values('Request Approval',(select MODULE_ID from ENT_MODULE_MASTER where MODULE_NAME ='Visitor Management'));
insert into ENT_SUB_MODULE_MASTER values('Reports' ,(select MODULE_ID from ENT_MODULE_MASTER where MODULE_NAME ='Visitor Management'));

insert into ENT_SUB_MODULE_MASTER values('Admin Operations',(select MODULE_ID from ENT_MODULE_MASTER where MODULE_NAME ='Vehicle Mangement'));
insert into ENT_SUB_MODULE_MASTER values('Enrollment',(select MODULE_ID from ENT_MODULE_MASTER where MODULE_NAME ='Vehicle Mangement'));
insert into ENT_SUB_MODULE_MASTER values('Reports' ,(select MODULE_ID from ENT_MODULE_MASTER where MODULE_NAME ='Vehicle Mangement'));

insert into ENT_SUB_MODULE_MASTER values('Enrollment',(select MODULE_ID from ENT_MODULE_MASTER where MODULE_NAME ='Asset Tracking System'));
insert into ENT_SUB_MODULE_MASTER values('Admin Operations',(select MODULE_ID from ENT_MODULE_MASTER where MODULE_NAME ='Asset Tracking System'));
insert into ENT_SUB_MODULE_MASTER values('Reports' ,(select MODULE_ID from ENT_MODULE_MASTER where MODULE_NAME ='Asset Tracking System'));

-- insert qurry for sub module End


create table ENT_MENU_MASTER
(
	MENU_ID int identity(1,1) primary key,
	MENU_NAME varchar(50),
	MENU_URL varchar(200),
    MENU_ITEMPOSITION float not null,
	MENU_IsDeleted [bit] NULL,
	MENU_DeletedDate [datetime] NULL,
	MODULE_ID int foreign key references  ENT_MODULE_MASTER(MODULE_ID),
	SMODULE_ID int foreign key references  ENT_SUB_MODULE_MASTER(SMODULE_ID),
)


create table MenuCompanyRelation
(
	RelationId int identity(1,1) primary key,
	MENU_ID int foreign key references  ENT_MENU_MASTER(MENU_ID),
	COMPANY_ID [int] foreign key references ENT_COMPANY(COMPANY_ID),
	CONSTRAINT  MenuCompanyRelation_MENU_ID_COMPANY_ID_Composite UNIQUE(MENU_ID,COMPANY_ID),
	IsDeleted [bit] NULL,
	DeletedDate [datetime] NULL	
)



USE [UNOMVC]
GO
SET IDENTITY_INSERT [dbo].[ENT_MENU_MASTER] ON 

GO
INSERT [dbo].[ENT_MENU_MASTER] ([MENU_ID], [MENU_NAME], [MENU_URL], [MENU_ITEMPOSITION], [MENU_IsDeleted], [MENU_DeletedDate], [MODULE_ID], [SMODULE_ID]) VALUES (1, N'Company', N'Company/index', 1, 0, NULL, 1, 1)
GO
INSERT [dbo].[ENT_MENU_MASTER] ([MENU_ID], [MENU_NAME], [MENU_URL], [MENU_ITEMPOSITION], [MENU_IsDeleted], [MENU_DeletedDate], [MODULE_ID], [SMODULE_ID]) VALUES (2, N'Common', N'CommonEntity/index', 2, 0, NULL, 1, 1)
GO
INSERT [dbo].[ENT_MENU_MASTER] ([MENU_ID], [MENU_NAME], [MENU_URL], [MENU_ITEMPOSITION], [MENU_IsDeleted], [MENU_DeletedDate], [MODULE_ID], [SMODULE_ID]) VALUES (3, N'Holiday', N'Holiday/index', 3, 0, NULL, 1, 1)
GO
INSERT [dbo].[ENT_MENU_MASTER] ([MENU_ID], [MENU_NAME], [MENU_URL], [MENU_ITEMPOSITION], [MENU_IsDeleted], [MENU_DeletedDate], [MODULE_ID], [SMODULE_ID]) VALUES (4, N'Reason', N'ReasonMaster/index', 4, 0, NULL, 1, 1)
GO
INSERT [dbo].[ENT_MENU_MASTER] ([MENU_ID], [MENU_NAME], [MENU_URL], [MENU_ITEMPOSITION], [MENU_IsDeleted], [MENU_DeletedDate], [MODULE_ID], [SMODULE_ID]) VALUES (5, N'Employee', N'Employee/index', 5, 0, NULL, 1, 1)
GO
INSERT [dbo].[ENT_MENU_MASTER] ([MENU_ID], [MENU_NAME], [MENU_URL], [MENU_ITEMPOSITION], [MENU_IsDeleted], [MENU_DeletedDate], [MODULE_ID], [SMODULE_ID]) VALUES (6, N'Employee Hierarchy', N'EmployeeHierarchy/index', 6, 0, NULL, 1, 1)
GO
INSERT [dbo].[ENT_MENU_MASTER] ([MENU_ID], [MENU_NAME], [MENU_URL], [MENU_ITEMPOSITION], [MENU_IsDeleted], [MENU_DeletedDate], [MODULE_ID], [SMODULE_ID]) VALUES (7, N'Employee Left', N'EmployeeLeft/index', 7, 0, NULL, 1, 1)
GO
INSERT [dbo].[ENT_MENU_MASTER] ([MENU_ID], [MENU_NAME], [MENU_URL], [MENU_ITEMPOSITION], [MENU_IsDeleted], [MENU_DeletedDate], [MODULE_ID], [SMODULE_ID]) VALUES (8, N'User', N'User/index', 1, 0, NULL, 1, 2)
GO
INSERT [dbo].[ENT_MENU_MASTER] ([MENU_ID], [MENU_NAME], [MENU_URL], [MENU_ITEMPOSITION], [MENU_IsDeleted], [MENU_DeletedDate], [MODULE_ID], [SMODULE_ID]) VALUES (9, N'newAssignMenu', N'AssignMenu/AssignMenu', 7, 1, CAST(0x0000A9740129DF14 AS DateTime), 4, 8)
GO
INSERT [dbo].[ENT_MENU_MASTER] ([MENU_ID], [MENU_NAME], [MENU_URL], [MENU_ITEMPOSITION], [MENU_IsDeleted], [MENU_DeletedDate], [MODULE_ID], [SMODULE_ID]) VALUES (10, N'controller', N'controller/index', 1, 0, NULL, 4, 7)
GO
INSERT [dbo].[ENT_MENU_MASTER] ([MENU_ID], [MENU_NAME], [MENU_URL], [MENU_ITEMPOSITION], [MENU_IsDeleted], [MENU_DeletedDate], [MODULE_ID], [SMODULE_ID]) VALUES (11, N'Testing Menu', N'TestingMenu1/Menu1', 5, 1, CAST(0x0000A98201340AFC AS DateTime), 5, 14)
GO
INSERT [dbo].[ENT_MENU_MASTER] ([MENU_ID], [MENU_NAME], [MENU_URL], [MENU_ITEMPOSITION], [MENU_IsDeleted], [MENU_DeletedDate], [MODULE_ID], [SMODULE_ID]) VALUES (12, N'Role Menu Access', N'RoleMenuAccess/index', 2, 0, NULL, 1, 2)
GO
INSERT [dbo].[ENT_MENU_MASTER] ([MENU_ID], [MENU_NAME], [MENU_URL], [MENU_ITEMPOSITION], [MENU_IsDeleted], [MENU_DeletedDate], [MODULE_ID], [SMODULE_ID]) VALUES (13, N'menu', N'Menu/index', 3, 0, NULL, 1, 2)
GO
INSERT [dbo].[ENT_MENU_MASTER] ([MENU_ID], [MENU_NAME], [MENU_URL], [MENU_ITEMPOSITION], [MENU_IsDeleted], [MENU_DeletedDate], [MODULE_ID], [SMODULE_ID]) VALUES (14, N'Zone', N'zone/index', 2, 0, NULL, 4, 7)
GO
INSERT [dbo].[ENT_MENU_MASTER] ([MENU_ID], [MENU_NAME], [MENU_URL], [MENU_ITEMPOSITION], [MENU_IsDeleted], [MENU_DeletedDate], [MODULE_ID], [SMODULE_ID]) VALUES (15, N'Access Level', N'accesslevel/index', 3, 0, NULL, 4, 7)
GO
INSERT [dbo].[ENT_MENU_MASTER] ([MENU_ID], [MENU_NAME], [MENU_URL], [MENU_ITEMPOSITION], [MENU_IsDeleted], [MENU_DeletedDate], [MODULE_ID], [SMODULE_ID]) VALUES (16, N'Employee Access Conguration', N'EmployeeAccessConfiguration/index', 4, 0, NULL, 4, 7)
GO
INSERT [dbo].[ENT_MENU_MASTER] ([MENU_ID], [MENU_NAME], [MENU_URL], [MENU_ITEMPOSITION], [MENU_IsDeleted], [MENU_DeletedDate], [MODULE_ID], [SMODULE_ID]) VALUES (17, N'Reader Message Template', N'ReaderTemplate/index', 5, 0, NULL, 4, 7)
GO
INSERT [dbo].[ENT_MENU_MASTER] ([MENU_ID], [MENU_NAME], [MENU_URL], [MENU_ITEMPOSITION], [MENU_IsDeleted], [MENU_DeletedDate], [MODULE_ID], [SMODULE_ID]) VALUES (18, N'Activity Browser', N'ActivityBrowser/index', 1, 0, NULL, 4, 8)
GO
INSERT [dbo].[ENT_MENU_MASTER] ([MENU_ID], [MENU_NAME], [MENU_URL], [MENU_ITEMPOSITION], [MENU_IsDeleted], [MENU_DeletedDate], [MODULE_ID], [SMODULE_ID]) VALUES (19, N'Event Viewer', N'EventBrowser/index', 2, 0, NULL, 4, 8)
GO
INSERT [dbo].[ENT_MENU_MASTER] ([MENU_ID], [MENU_NAME], [MENU_URL], [MENU_ITEMPOSITION], [MENU_IsDeleted], [MENU_DeletedDate], [MODULE_ID], [SMODULE_ID]) VALUES (20, N'Auto Card Lookup', N'AutoCardLookUp/index', 3, 0, NULL, 4, 8)
GO
INSERT [dbo].[ENT_MENU_MASTER] ([MENU_ID], [MENU_NAME], [MENU_URL], [MENU_ITEMPOSITION], [MENU_IsDeleted], [MENU_DeletedDate], [MODULE_ID], [SMODULE_ID]) VALUES (21, N'Locate Card Holder', N'LocateCardHolder/index', 4, 0, NULL, 4, 8)
GO
INSERT [dbo].[ENT_MENU_MASTER] ([MENU_ID], [MENU_NAME], [MENU_URL], [MENU_ITEMPOSITION], [MENU_IsDeleted], [MENU_DeletedDate], [MODULE_ID], [SMODULE_ID]) VALUES (22, N'Controller Status', N'ControllerStatus/index', 5, 0, NULL, 4, 8)
GO
INSERT [dbo].[ENT_MENU_MASTER] ([MENU_ID], [MENU_NAME], [MENU_URL], [MENU_ITEMPOSITION], [MENU_IsDeleted], [MENU_DeletedDate], [MODULE_ID], [SMODULE_ID]) VALUES (23, N'Category', N'Category/index', 1, 0, NULL, 3, 10)
GO
INSERT [dbo].[ENT_MENU_MASTER] ([MENU_ID], [MENU_NAME], [MENU_URL], [MENU_ITEMPOSITION], [MENU_IsDeleted], [MENU_DeletedDate], [MODULE_ID], [SMODULE_ID]) VALUES (24, N'shift', N'shift/index', 2, 0, NULL, 3, 10)
GO
INSERT [dbo].[ENT_MENU_MASTER] ([MENU_ID], [MENU_NAME], [MENU_URL], [MENU_ITEMPOSITION], [MENU_IsDeleted], [MENU_DeletedDate], [MODULE_ID], [SMODULE_ID]) VALUES (25, N'Leave File', N'LeaveFile/index', 3, 0, NULL, 3, 10)
GO
INSERT [dbo].[ENT_MENU_MASTER] ([MENU_ID], [MENU_NAME], [MENU_URL], [MENU_ITEMPOSITION], [MENU_IsDeleted], [MENU_DeletedDate], [MODULE_ID], [SMODULE_ID]) VALUES (26, N'Leave Rule', N'LeaveRule/index', 4, 0, NULL, 3, 10)
GO
INSERT [dbo].[ENT_MENU_MASTER] ([MENU_ID], [MENU_NAME], [MENU_URL], [MENU_ITEMPOSITION], [MENU_IsDeleted], [MENU_DeletedDate], [MODULE_ID], [SMODULE_ID]) VALUES (27, N'Operational Access', N'RoleDataAccess/create', 4, 0, NULL, 1, 2)
GO
INSERT [dbo].[ENT_MENU_MASTER] ([MENU_ID], [MENU_NAME], [MENU_URL], [MENU_ITEMPOSITION], [MENU_IsDeleted], [MENU_DeletedDate], [MODULE_ID], [SMODULE_ID]) VALUES (28, N'Weekend/Weekly Off', N'WeeklyOff/index', 5, 0, NULL, 3, 10)
GO
INSERT [dbo].[ENT_MENU_MASTER] ([MENU_ID], [MENU_NAME], [MENU_URL], [MENU_ITEMPOSITION], [MENU_IsDeleted], [MENU_DeletedDate], [MODULE_ID], [SMODULE_ID]) VALUES (29, N'Role Master', N'RoleMaster/index', 5, 0, NULL, 1, 2)
GO
INSERT [dbo].[ENT_MENU_MASTER] ([MENU_ID], [MENU_NAME], [MENU_URL], [MENU_ITEMPOSITION], [MENU_IsDeleted], [MENU_DeletedDate], [MODULE_ID], [SMODULE_ID]) VALUES (30, N'Company Menu Relation', N'Company_Level_Menu_Access/create', 6, 0, NULL, 1, 2)
GO
INSERT [dbo].[ENT_MENU_MASTER] ([MENU_ID], [MENU_NAME], [MENU_URL], [MENU_ITEMPOSITION], [MENU_IsDeleted], [MENU_DeletedDate], [MODULE_ID], [SMODULE_ID]) VALUES (31, N'shift pattern', N'shiftpattern/index', 6, 0, NULL, 3, 10)
GO
INSERT [dbo].[ENT_MENU_MASTER] ([MENU_ID], [MENU_NAME], [MENU_URL], [MENU_ITEMPOSITION], [MENU_IsDeleted], [MENU_DeletedDate], [MODULE_ID], [SMODULE_ID]) VALUES (32, N'Attendance Config', N'EmployeeTimeAttendance/index', 7, 0, NULL, 3, 10)
GO
SET IDENTITY_INSERT [dbo].[ENT_MENU_MASTER] OFF
GO



create table ENT_ROLE_MASTER
(
	ROLE_ID int identity(1,1) primary key,
	ROLE_NAME varchar(50),
	ROLE_IsDeleted [bit] DEFAULT 0,
	ROLE_DeletedDate [datetime] ,
	[COMPANY_ID] [int] foreign key references ENT_COMPANY(COMPANY_ID),
	[IS_SYNC] [bit] NOT NULL DEFAULT 0,
	CONSTRAINT ROLE_NAME_COMPANY_ID_Composite UNIQUE(ROLE_NAME,COMPANY_ID)
)


SELECT * FROM ENT_ROLE_MASTER

create table ENT_ROLE_MENU_ACCESS
(
	ROLE_ACCESS_ID int identity(1,1) primary key,
	ROLE_ADD bit not null,
	ROLE_DELETE bit not null,
	ROLE_EDIT bit not null,
	ROLE_VIEW bit not null,
	ROLE_IsDeleted [bit] NULL,
	ROLE_DeletedDate [datetime] NULL,
	[IS_SYNC] [bit] NOT NULL DEFAULT 0,
	ROLE_ID int foreign key references  ENT_ROLE_MASTER(ROLE_ID),
	MENU_ID int foreign key references  ENT_MENU_MASTER(MENU_ID)
)


CREATE TABLE [dbo].[ENT_User](
	[USER_ID] INT PRIMARY KEY IDENTITY,
	USER_CODE [nvarchar](15) NOT NULL,	
	[Password] [varchar](100) not NULL,
	ROLE_ID [int] foreign key references ENT_ROLE_MASTER(ROLE_ID),
	[EssEnabled] [bit] NULL,	
	[PASSWORD_RESET] [bit] NULL,
	[USER_CREATEDDATE] [datetime] NULL,
	[USER_CREATEDBY] varchar(10),
	[USER_MODIFIEDDATE] [datetime] NULL,	
	[USER_MODIFIEDBY] varchar(10),
	[USER_ISDELETED] [bit] NULL,
	[USER_DELETEDDATE] [datetime] NULL,
	[USER_DELETEDBY] varchar(10),
	[IS_SYNC] [bit] NOT NULL DEFAULT 0,
	[COMPANY_ID] [int] foreign key references ENT_COMPANY(COMPANY_ID),	
	CONSTRAINT USERCODE_COMPANYID_Composite UNIQUE(USER_CODE,COMPANY_ID)
	)
	declare @cmpId int
INSERT INTO ent_company ( COMPANY_CODE,COMPANY_NAME,COMPANY_CREATEDDATE,COMPANY_ISDELETED,COMPANY_EMP_CODE_LENGTH ) 
VALUES ('CMS', 'CMS Computers Ltd',getdate() ,0,4);
set @cmpid=@@identity
print @cmpId
INSERT INTO ENT_ROLE_MASTER(ROLE_NAME,COMPANY_ID) VALUES ('SYSADMIN',@cmpId),('ADMIN',@cmpId),('EMPLOYEE',@cmpId)
INSERT INTO ent_company_details 
(COMPANY_ADDRESS, COMPANY_CITY, COMPANY_PIN, COMPANY_PHONE1, COMPANY_PHONE2
, COMPANY_STATE, COMPANY_ID, ADDRESS_TYPE_ID, COMPANY_ISDELETED )
 VALUES ('', '', '', '', '', '',@cmpId ,1,0 ); 
	INSERT INTO ENT_USER(USER_CODE,[Password],ROLE_ID,EssEnabled,PASSWORD_RESET,COMPANY_ID,USER_ISDELETED) values('superuser','igrGXAJD9Sk=',1,0,0,@cmpId,0)

	select * from ent_user


	

USE [UNOMVC]
GO
SET IDENTITY_INSERT [dbo].[MenuCompanyRelation] ON 

GO
INSERT [dbo].[MenuCompanyRelation] ([RelationId], [MENU_ID], [COMPANY_ID], [IsDeleted], [DeletedDate]) VALUES (1, 1, 1, NULL, NULL)
GO
INSERT [dbo].[MenuCompanyRelation] ([RelationId], [MENU_ID], [COMPANY_ID], [IsDeleted], [DeletedDate]) VALUES (2, 2, 1, NULL, NULL)
GO
INSERT [dbo].[MenuCompanyRelation] ([RelationId], [MENU_ID], [COMPANY_ID], [IsDeleted], [DeletedDate]) VALUES (3, 3, 1, NULL, NULL)
GO
INSERT [dbo].[MenuCompanyRelation] ([RelationId], [MENU_ID], [COMPANY_ID], [IsDeleted], [DeletedDate]) VALUES (4, 4, 1, NULL, NULL)
GO
INSERT [dbo].[MenuCompanyRelation] ([RelationId], [MENU_ID], [COMPANY_ID], [IsDeleted], [DeletedDate]) VALUES (5, 5, 1, NULL, NULL)
GO
INSERT [dbo].[MenuCompanyRelation] ([RelationId], [MENU_ID], [COMPANY_ID], [IsDeleted], [DeletedDate]) VALUES (6, 6, 1, NULL, NULL)
GO
INSERT [dbo].[MenuCompanyRelation] ([RelationId], [MENU_ID], [COMPANY_ID], [IsDeleted], [DeletedDate]) VALUES (7, 7, 1, NULL, NULL)
GO
INSERT [dbo].[MenuCompanyRelation] ([RelationId], [MENU_ID], [COMPANY_ID], [IsDeleted], [DeletedDate]) VALUES (8, 8, 1, NULL, NULL)
GO
INSERT [dbo].[MenuCompanyRelation] ([RelationId], [MENU_ID], [COMPANY_ID], [IsDeleted], [DeletedDate]) VALUES (9, 9, 1, NULL, NULL)
GO
INSERT [dbo].[MenuCompanyRelation] ([RelationId], [MENU_ID], [COMPANY_ID], [IsDeleted], [DeletedDate]) VALUES (10, 10, 1, NULL, NULL)
GO
INSERT [dbo].[MenuCompanyRelation] ([RelationId], [MENU_ID], [COMPANY_ID], [IsDeleted], [DeletedDate]) VALUES (11, 11, 1, NULL, NULL)
GO
INSERT [dbo].[MenuCompanyRelation] ([RelationId], [MENU_ID], [COMPANY_ID], [IsDeleted], [DeletedDate]) VALUES (12, 12, 1, NULL, NULL)
GO
INSERT [dbo].[MenuCompanyRelation] ([RelationId], [MENU_ID], [COMPANY_ID], [IsDeleted], [DeletedDate]) VALUES (13, 13, 1, NULL, NULL)
GO
INSERT [dbo].[MenuCompanyRelation] ([RelationId], [MENU_ID], [COMPANY_ID], [IsDeleted], [DeletedDate]) VALUES (14, 14, 1, NULL, NULL)
GO
INSERT [dbo].[MenuCompanyRelation] ([RelationId], [MENU_ID], [COMPANY_ID], [IsDeleted], [DeletedDate]) VALUES (15, 15, 1, NULL, NULL)
GO
INSERT [dbo].[MenuCompanyRelation] ([RelationId], [MENU_ID], [COMPANY_ID], [IsDeleted], [DeletedDate]) VALUES (16, 16, 1, NULL, NULL)
GO
INSERT [dbo].[MenuCompanyRelation] ([RelationId], [MENU_ID], [COMPANY_ID], [IsDeleted], [DeletedDate]) VALUES (17, 17, 1, NULL, NULL)
GO
INSERT [dbo].[MenuCompanyRelation] ([RelationId], [MENU_ID], [COMPANY_ID], [IsDeleted], [DeletedDate]) VALUES (18, 18, 1, NULL, NULL)
GO
INSERT [dbo].[MenuCompanyRelation] ([RelationId], [MENU_ID], [COMPANY_ID], [IsDeleted], [DeletedDate]) VALUES (19, 19, 1, NULL, NULL)
GO
INSERT [dbo].[MenuCompanyRelation] ([RelationId], [MENU_ID], [COMPANY_ID], [IsDeleted], [DeletedDate]) VALUES (20, 20, 1, NULL, NULL)
GO
INSERT [dbo].[MenuCompanyRelation] ([RelationId], [MENU_ID], [COMPANY_ID], [IsDeleted], [DeletedDate]) VALUES (21, 21, 1, NULL, NULL)
GO
INSERT [dbo].[MenuCompanyRelation] ([RelationId], [MENU_ID], [COMPANY_ID], [IsDeleted], [DeletedDate]) VALUES (22, 22, 1, NULL, NULL)
GO
INSERT [dbo].[MenuCompanyRelation] ([RelationId], [MENU_ID], [COMPANY_ID], [IsDeleted], [DeletedDate]) VALUES (23, 23, 1, NULL, NULL)
GO
INSERT [dbo].[MenuCompanyRelation] ([RelationId], [MENU_ID], [COMPANY_ID], [IsDeleted], [DeletedDate]) VALUES (24, 24, 1, NULL, NULL)
GO
INSERT [dbo].[MenuCompanyRelation] ([RelationId], [MENU_ID], [COMPANY_ID], [IsDeleted], [DeletedDate]) VALUES (25, 25, 1, NULL, NULL)
GO
INSERT [dbo].[MenuCompanyRelation] ([RelationId], [MENU_ID], [COMPANY_ID], [IsDeleted], [DeletedDate]) VALUES (26, 26, 1, NULL, NULL)
GO
INSERT [dbo].[MenuCompanyRelation] ([RelationId], [MENU_ID], [COMPANY_ID], [IsDeleted], [DeletedDate]) VALUES (27, 27, 1, NULL, NULL)
GO
INSERT [dbo].[MenuCompanyRelation] ([RelationId], [MENU_ID], [COMPANY_ID], [IsDeleted], [DeletedDate]) VALUES (28, 28, 1, NULL, NULL)
GO
INSERT [dbo].[MenuCompanyRelation] ([RelationId], [MENU_ID], [COMPANY_ID], [IsDeleted], [DeletedDate]) VALUES (29, 29, 1, NULL, NULL)
GO
INSERT [dbo].[MenuCompanyRelation] ([RelationId], [MENU_ID], [COMPANY_ID], [IsDeleted], [DeletedDate]) VALUES (30, 30, 1, NULL, NULL)
GO
INSERT [dbo].[MenuCompanyRelation] ([RelationId], [MENU_ID], [COMPANY_ID], [IsDeleted], [DeletedDate]) VALUES (31, 31, 1, NULL, NULL)
GO
INSERT [dbo].[MenuCompanyRelation] ([RelationId], [MENU_ID], [COMPANY_ID], [IsDeleted], [DeletedDate]) VALUES (32, 32, 1, NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[MenuCompanyRelation] OFF
GO





CREATE TABLE ENT_ROLE_DATA_ACCESS
(
	DATA_ACCESS_ID INT PRIMARY KEY IDENTITY,
	USER_CODE int foreign key references ENT_User([USER_ID]),
	[COMMON_TYPES_ID] int foreign key references ENT_ORG_COMMON_TYPES(COMMON_TYPES_ID),	
	[MAPPED_ENTITY_ID] INT,
	[IS_SYNC] [bit] NOT NULL DEFAULT 0,
)



SET IDENTITY_INSERT [dbo].[ENT_ROLE_MENU_ACCESS] ON 

GO
INSERT [dbo].[ENT_ROLE_MENU_ACCESS] ([ROLE_ACCESS_ID], [ROLE_ADD], [ROLE_DELETE], [ROLE_EDIT], [ROLE_VIEW], [ROLE_IsDeleted], [ROLE_DeletedDate], [IS_SYNC], [ROLE_ID], [MENU_ID]) VALUES (1, 1, 1, 1, 1, 0, NULL, 0, 1, 1)
GO
INSERT [dbo].[ENT_ROLE_MENU_ACCESS] ([ROLE_ACCESS_ID], [ROLE_ADD], [ROLE_DELETE], [ROLE_EDIT], [ROLE_VIEW], [ROLE_IsDeleted], [ROLE_DeletedDate], [IS_SYNC], [ROLE_ID], [MENU_ID]) VALUES (2, 1, 1, 1, 1, 0, NULL, 0, 1, 2)
GO
INSERT [dbo].[ENT_ROLE_MENU_ACCESS] ([ROLE_ACCESS_ID], [ROLE_ADD], [ROLE_DELETE], [ROLE_EDIT], [ROLE_VIEW], [ROLE_IsDeleted], [ROLE_DeletedDate], [IS_SYNC], [ROLE_ID], [MENU_ID]) VALUES (3, 1, 1, 1, 1, 0, NULL, 0, 1, 3)
GO
INSERT [dbo].[ENT_ROLE_MENU_ACCESS] ([ROLE_ACCESS_ID], [ROLE_ADD], [ROLE_DELETE], [ROLE_EDIT], [ROLE_VIEW], [ROLE_IsDeleted], [ROLE_DeletedDate], [IS_SYNC], [ROLE_ID], [MENU_ID]) VALUES (4, 1, 1, 1, 1, 0, NULL, 0, 1, 4)
GO
INSERT [dbo].[ENT_ROLE_MENU_ACCESS] ([ROLE_ACCESS_ID], [ROLE_ADD], [ROLE_DELETE], [ROLE_EDIT], [ROLE_VIEW], [ROLE_IsDeleted], [ROLE_DeletedDate], [IS_SYNC], [ROLE_ID], [MENU_ID]) VALUES (5, 1, 1, 1, 1, 0, NULL, 0, 1, 5)
GO
INSERT [dbo].[ENT_ROLE_MENU_ACCESS] ([ROLE_ACCESS_ID], [ROLE_ADD], [ROLE_DELETE], [ROLE_EDIT], [ROLE_VIEW], [ROLE_IsDeleted], [ROLE_DeletedDate], [IS_SYNC], [ROLE_ID], [MENU_ID]) VALUES (6, 1, 1, 1, 1, 0, NULL, 0, 1, 6)
GO
INSERT [dbo].[ENT_ROLE_MENU_ACCESS] ([ROLE_ACCESS_ID], [ROLE_ADD], [ROLE_DELETE], [ROLE_EDIT], [ROLE_VIEW], [ROLE_IsDeleted], [ROLE_DeletedDate], [IS_SYNC], [ROLE_ID], [MENU_ID]) VALUES (7, 1, 1, 1, 1, 0, NULL, 0, 1, 7)
GO
INSERT [dbo].[ENT_ROLE_MENU_ACCESS] ([ROLE_ACCESS_ID], [ROLE_ADD], [ROLE_DELETE], [ROLE_EDIT], [ROLE_VIEW], [ROLE_IsDeleted], [ROLE_DeletedDate], [IS_SYNC], [ROLE_ID], [MENU_ID]) VALUES (8, 1, 1, 1, 1, 0, NULL, 0, 1, 8)
GO
INSERT [dbo].[ENT_ROLE_MENU_ACCESS] ([ROLE_ACCESS_ID], [ROLE_ADD], [ROLE_DELETE], [ROLE_EDIT], [ROLE_VIEW], [ROLE_IsDeleted], [ROLE_DeletedDate], [IS_SYNC], [ROLE_ID], [MENU_ID]) VALUES (9, 1, 1, 1, 1, 0, NULL, 0, 1, 9)
GO
INSERT [dbo].[ENT_ROLE_MENU_ACCESS] ([ROLE_ACCESS_ID], [ROLE_ADD], [ROLE_DELETE], [ROLE_EDIT], [ROLE_VIEW], [ROLE_IsDeleted], [ROLE_DeletedDate], [IS_SYNC], [ROLE_ID], [MENU_ID]) VALUES (10, 1, 1, 1, 1, 0, NULL, 0, 1, 10)
GO
INSERT [dbo].[ENT_ROLE_MENU_ACCESS] ([ROLE_ACCESS_ID], [ROLE_ADD], [ROLE_DELETE], [ROLE_EDIT], [ROLE_VIEW], [ROLE_IsDeleted], [ROLE_DeletedDate], [IS_SYNC], [ROLE_ID], [MENU_ID]) VALUES (11, 1, 1, 1, 1, 0, NULL, 0, 1, 11)
GO
INSERT [dbo].[ENT_ROLE_MENU_ACCESS] ([ROLE_ACCESS_ID], [ROLE_ADD], [ROLE_DELETE], [ROLE_EDIT], [ROLE_VIEW], [ROLE_IsDeleted], [ROLE_DeletedDate], [IS_SYNC], [ROLE_ID], [MENU_ID]) VALUES (12, 1, 1, 1, 1, 0, NULL, 0, 1, 12)
GO
INSERT [dbo].[ENT_ROLE_MENU_ACCESS] ([ROLE_ACCESS_ID], [ROLE_ADD], [ROLE_DELETE], [ROLE_EDIT], [ROLE_VIEW], [ROLE_IsDeleted], [ROLE_DeletedDate], [IS_SYNC], [ROLE_ID], [MENU_ID]) VALUES (13, 1, 1, 1, 1, 0, NULL, 0, 1, 13)
GO
INSERT [dbo].[ENT_ROLE_MENU_ACCESS] ([ROLE_ACCESS_ID], [ROLE_ADD], [ROLE_DELETE], [ROLE_EDIT], [ROLE_VIEW], [ROLE_IsDeleted], [ROLE_DeletedDate], [IS_SYNC], [ROLE_ID], [MENU_ID]) VALUES (14, 1, 1, 1, 1, 0, NULL, 0, 1, 14)
GO
INSERT [dbo].[ENT_ROLE_MENU_ACCESS] ([ROLE_ACCESS_ID], [ROLE_ADD], [ROLE_DELETE], [ROLE_EDIT], [ROLE_VIEW], [ROLE_IsDeleted], [ROLE_DeletedDate], [IS_SYNC], [ROLE_ID], [MENU_ID]) VALUES (15, 1, 1, 1, 1, 0, NULL, 0, 1, 15)
GO
INSERT [dbo].[ENT_ROLE_MENU_ACCESS] ([ROLE_ACCESS_ID], [ROLE_ADD], [ROLE_DELETE], [ROLE_EDIT], [ROLE_VIEW], [ROLE_IsDeleted], [ROLE_DeletedDate], [IS_SYNC], [ROLE_ID], [MENU_ID]) VALUES (16, 1, 1, 1, 1, 0, NULL, 0, 1, 16)
GO
INSERT [dbo].[ENT_ROLE_MENU_ACCESS] ([ROLE_ACCESS_ID], [ROLE_ADD], [ROLE_DELETE], [ROLE_EDIT], [ROLE_VIEW], [ROLE_IsDeleted], [ROLE_DeletedDate], [IS_SYNC], [ROLE_ID], [MENU_ID]) VALUES (17, 1, 1, 1, 1, 0, NULL, 0, 1, 17)
GO
INSERT [dbo].[ENT_ROLE_MENU_ACCESS] ([ROLE_ACCESS_ID], [ROLE_ADD], [ROLE_DELETE], [ROLE_EDIT], [ROLE_VIEW], [ROLE_IsDeleted], [ROLE_DeletedDate], [IS_SYNC], [ROLE_ID], [MENU_ID]) VALUES (18, 1, 1, 1, 1, 0, NULL, 0, 1, 18)
GO
INSERT [dbo].[ENT_ROLE_MENU_ACCESS] ([ROLE_ACCESS_ID], [ROLE_ADD], [ROLE_DELETE], [ROLE_EDIT], [ROLE_VIEW], [ROLE_IsDeleted], [ROLE_DeletedDate], [IS_SYNC], [ROLE_ID], [MENU_ID]) VALUES (19, 1, 1, 1, 1, 0, NULL, 0, 1, 19)
GO
INSERT [dbo].[ENT_ROLE_MENU_ACCESS] ([ROLE_ACCESS_ID], [ROLE_ADD], [ROLE_DELETE], [ROLE_EDIT], [ROLE_VIEW], [ROLE_IsDeleted], [ROLE_DeletedDate], [IS_SYNC], [ROLE_ID], [MENU_ID]) VALUES (20, 1, 1, 1, 1, 0, NULL, 0, 1, 20)
GO
INSERT [dbo].[ENT_ROLE_MENU_ACCESS] ([ROLE_ACCESS_ID], [ROLE_ADD], [ROLE_DELETE], [ROLE_EDIT], [ROLE_VIEW], [ROLE_IsDeleted], [ROLE_DeletedDate], [IS_SYNC], [ROLE_ID], [MENU_ID]) VALUES (21, 1, 1, 1, 1, 0, NULL, 0, 1, 21)
GO
INSERT [dbo].[ENT_ROLE_MENU_ACCESS] ([ROLE_ACCESS_ID], [ROLE_ADD], [ROLE_DELETE], [ROLE_EDIT], [ROLE_VIEW], [ROLE_IsDeleted], [ROLE_DeletedDate], [IS_SYNC], [ROLE_ID], [MENU_ID]) VALUES (22, 1, 1, 1, 1, 0, NULL, 0, 1, 22)
GO
INSERT [dbo].[ENT_ROLE_MENU_ACCESS] ([ROLE_ACCESS_ID], [ROLE_ADD], [ROLE_DELETE], [ROLE_EDIT], [ROLE_VIEW], [ROLE_IsDeleted], [ROLE_DeletedDate], [IS_SYNC], [ROLE_ID], [MENU_ID]) VALUES (23, 1, 1, 1, 1, 0, NULL, 0, 1, 23)
GO
INSERT [dbo].[ENT_ROLE_MENU_ACCESS] ([ROLE_ACCESS_ID], [ROLE_ADD], [ROLE_DELETE], [ROLE_EDIT], [ROLE_VIEW], [ROLE_IsDeleted], [ROLE_DeletedDate], [IS_SYNC], [ROLE_ID], [MENU_ID]) VALUES (24, 1, 1, 1, 1, 0, NULL, 0, 1, 24)
GO
INSERT [dbo].[ENT_ROLE_MENU_ACCESS] ([ROLE_ACCESS_ID], [ROLE_ADD], [ROLE_DELETE], [ROLE_EDIT], [ROLE_VIEW], [ROLE_IsDeleted], [ROLE_DeletedDate], [IS_SYNC], [ROLE_ID], [MENU_ID]) VALUES (25, 1, 1, 1, 1, 0, NULL, 0, 1, 25)
GO
INSERT [dbo].[ENT_ROLE_MENU_ACCESS] ([ROLE_ACCESS_ID], [ROLE_ADD], [ROLE_DELETE], [ROLE_EDIT], [ROLE_VIEW], [ROLE_IsDeleted], [ROLE_DeletedDate], [IS_SYNC], [ROLE_ID], [MENU_ID]) VALUES (26, 1, 1, 1, 1, 0, NULL, 0, 1, 26)
GO
INSERT [dbo].[ENT_ROLE_MENU_ACCESS] ([ROLE_ACCESS_ID], [ROLE_ADD], [ROLE_DELETE], [ROLE_EDIT], [ROLE_VIEW], [ROLE_IsDeleted], [ROLE_DeletedDate], [IS_SYNC], [ROLE_ID], [MENU_ID]) VALUES (27, 1, 1, 1, 1, 0, NULL, 0, 1, 27)
GO
INSERT [dbo].[ENT_ROLE_MENU_ACCESS] ([ROLE_ACCESS_ID], [ROLE_ADD], [ROLE_DELETE], [ROLE_EDIT], [ROLE_VIEW], [ROLE_IsDeleted], [ROLE_DeletedDate], [IS_SYNC], [ROLE_ID], [MENU_ID]) VALUES (28, 1, 1, 1, 1, 0, NULL, 0, 1, 28)
GO
INSERT [dbo].[ENT_ROLE_MENU_ACCESS] ([ROLE_ACCESS_ID], [ROLE_ADD], [ROLE_DELETE], [ROLE_EDIT], [ROLE_VIEW], [ROLE_IsDeleted], [ROLE_DeletedDate], [IS_SYNC], [ROLE_ID], [MENU_ID]) VALUES (29, 1, 1, 1, 1, 0, NULL, 0, 1, 29)
GO
INSERT [dbo].[ENT_ROLE_MENU_ACCESS] ([ROLE_ACCESS_ID], [ROLE_ADD], [ROLE_DELETE], [ROLE_EDIT], [ROLE_VIEW], [ROLE_IsDeleted], [ROLE_DeletedDate], [IS_SYNC], [ROLE_ID], [MENU_ID]) VALUES (30, 1, 1, 1, 1, 0, NULL, 0, 1, 30)
GO
INSERT [dbo].[ENT_ROLE_MENU_ACCESS] ([ROLE_ACCESS_ID], [ROLE_ADD], [ROLE_DELETE], [ROLE_EDIT], [ROLE_VIEW], [ROLE_IsDeleted], [ROLE_DeletedDate], [IS_SYNC], [ROLE_ID], [MENU_ID]) VALUES (31, 1, 1, 1, 1, 0, NULL, 0, 1, 31)
GO
INSERT [dbo].[ENT_ROLE_MENU_ACCESS] ([ROLE_ACCESS_ID], [ROLE_ADD], [ROLE_DELETE], [ROLE_EDIT], [ROLE_VIEW], [ROLE_IsDeleted], [ROLE_DeletedDate], [IS_SYNC], [ROLE_ID], [MENU_ID]) VALUES (32, 1, 1, 1, 1, 0, NULL, 0, 1, 32)
GO
SET IDENTITY_INSERT [dbo].[ENT_ROLE_MENU_ACCESS] OFF
GO


----------------------------TEMPUS

----Category

CREATE TABLE [dbo].[ENT_CATEGORY](
	[CATEGORY_ID] [int] IDENTITY(1,1) primary key,
	[ORG_CATEGORY_ID] [int] foreign key references ENT_ORG_COMMON_ENTITIES(ID),
	[EARLY_GOING] [datetime] NULL,
	[LATE_COMING] [datetime] NULL,
	[EXTRA_CHECK] [bit] NULL,
	[EXHRS_BEFORE_SHIFT_HRS] [datetime] NULL,
	[EXHRS_AFTER_SHIFT_HRS] [datetime] NULL,
	[COMPENSATORYOFF_CODE] [nvarchar](16) NULL,
	[DED_FROM_EXHRS_EARLY_GOING] [bit] NULL,
	[DED_FROM_EXHRS_LATE_COMING] [bit] NULL,
	[CREATEDDATE] [datetime] NULL,
	[CREATEDBY] varchar(10),
	[MODIFIEDDATE] [datetime] NULL,	
	[MODIFIEDBY] varchar(10),
	[ISDELETED] [bit] not NULL default 0,
	[DELETEDDATE] [datetime] NULL,
	[DELETEDBY] varchar(10)	,
	[COMPANY_ID] [int] foreign key references ENT_COMPANY(COMPANY_ID),
	[IS_SYNC] [bit] NOT NULL DEFAULT 0,
	unique(ORG_CATEGORY_ID,COMPANY_ID,ISDELETED)
)



---SHIFT
CREATE TABLE [dbo].[TA_SHIFT](
	[SHIFT_ID] [int] IDENTITY(1,1) PRIMARY KEY,
	[SHIFT_CODE] [nvarchar](15) NOT NULL,
	[SHIFT_DESCRIPTION] [nvarchar](50) NULL,
	[SHIFT_ALLOCATION_TYPE] [nvarchar](50) NULL,
	[SHIFT_AUTO_SEARCH_START] [datetime] NULL,
	[SHIFT_AUTO_SEARCH_END] [datetime] NULL,
	[SHIFT_TYPE] [nvarchar](10) NULL,
	[SHIFT_START] [datetime] NULL,
	[SHIFT_END] [datetime] NULL,
	[SHIFT_BREAK_START] [datetime] NULL,
	[SHIFT_BREAK_END] [datetime] NULL,
	[SHIFT_BREAK_HRS] [datetime] NULL,
	[SHIFT_WORKHRS] [datetime] NULL,
    [SHIFT_HALFDAYWORKHRS] [datetime] NULL,
	[SHIFT_FLAG_ADD_BREAK] [bit] NULL,
	[SHIFT_WEEKEND_DIFF_TIME] [bit] NULL,
	[SHIFT_WEEKEND_START] [datetime] NULL,
	[SHIFT_WEEKEND_END] [datetime] NULL,
	[SHIFT_WEEKEND_BREAK_START] [datetime] NULL,
	[SHIFT_WEEKEND_BREAK_END] [datetime] NULL,
	[SHIFT_EARLY_SEARCH_HRS] [datetime] NULL,
	[SHIFT_LATE_SEARCH_HRS] [datetime] NULL,		
	[SHIFT_START_EARLY_SEARCH_HRS] [datetime] NULL,
	[SHIFT_START_LATE_SEARCH_HRS] [datetime] NULL,
	[SHIFT_START_EARLY_VALID_HRS] [datetime] NULL,
	[SHIFT_START_LATE_VALID_HRS] [datetime] NULL,
	[SHIFT_BREAK_OUT_EARLY_SEARCH_HRS] [datetime] NULL,
	[SHIFT_BREAK_OUT_LATE_SEARCH_HRS] [datetime] NULL,
	[SHIFT_BREAK_OUT_EARLY_VALID_HRS] [datetime] NULL,
	[SHIFT_BREAK_OUT_LATE_VALID_HRS] [datetime] NULL,
	[SHIFT_BREAK_IN_EARLY_SEARCH_HRS] [datetime] NULL,
	[SHIFT_BREAK_IN_LATE_SEARCH_HRS] [datetime] NULL,
	[SHIFT_BREAK_IN_EARLY_VALID_HRS] [datetime] NULL,
	[SHIFT_BREAK_IN_LATE_VALID_HRS] [datetime] NULL,
	[SHIFT_END_EARLY_SEARCH_HRS] [datetime] NULL,
	[SHIFT_END_LATE_SEARCH_HRS] [datetime] NULL,
	[SHIFT_END_EARLY_VALID_HRS] [datetime] NULL,
	[SHIFT_END_LATE_VALID_HRS] [datetime] NULL,
	[SHIFT_BREAK_OUT_IN_MIN_DUR] [datetime] NULL,
	[SHIFT_CREATEDDATE] [datetime] NULL,
	[SHIFT_CREATEDBY] varchar(10),
	[SHIFT_MODIFIEDDATE] [datetime] NULL,	
	[SHIFT_MODIFIEDBY] varchar(10),
	[SHIFT_ISDELETED] [bit] not NULL default 0,
	[SHIFT_DELETEDDATE] [datetime] NULL,
	[SHIFT_DELETEDBY] varchar(10)	,
	[IS_SYNC] [bit] NOT NULL DEFAULT 0,
	[COMPANY_ID] [int] foreign key references ENT_COMPANY(COMPANY_ID),	
	CONSTRAINT SHIFT_CODE_COMPANYID_Composite UNIQUE(SHIFT_CODE,COMPANY_ID,SHIFT_ISDELETED)
	)


	
CREATE TABLE [dbo].[TA_SHIFT_PATTERN](
	[SHIFT_PATTERN_ID] [int] IDENTITY(1,1) PRIMARY KEY,
	[SHIFT_PATTERN_CODE] [nvarchar](15) NOT NULL,
	[SHIFT_PATTERN_DESCRIPTION] [nvarchar](50) NULL,
	[SHIFT_PATTERN_TYPE] [nvarchar](50) NULL,
	[SHIFT_PATTERN] [nvarchar](max) NULL,
	[SHIFT_ISDELETED] [bit] NULL,
	[SHIFT_DELETEDDATE] [datetime] NULL,
	[IS_SYNC] [bit] NOT NULL DEFAULT 0,
	[COMPANY_ID] [int] foreign key references ENT_COMPANY(COMPANY_ID),	
	CONSTRAINT SHIFT_PATTERN_CODE_CODE_COMPANYID_Composite UNIQUE(SHIFT_PATTERN_CODE,COMPANY_ID,SHIFT_ISDELETED)

)

CREATE TABLE [dbo].[SHIFT_SHIFT_PATTERN](
[SHIFT_ID] [int] constraint fk_SHIFT_SHIFT_PATTERN_SHIFT_ID foreign key references TA_SHIFT(SHIFT_ID),
[SHIFT_PATTERN_ID] [int] constraint fk_SHIFT_SHIFT_PATTERN_SHIFT_PATTERN_ID foreign key references TA_SHIFT_PATTERN(SHIFT_PATTERN_ID),
shiftorder int
)


	
CREATE TABLE [dbo].[TA_WKLYOFF](
	[MWK_CD] INT PRIMARY KEY IDENTITY,
	[MWK_DAY] [numeric](5, 0) NOT NULL,
	[MWK_OFF] [numeric](5, 0) NOT NULL,
	[MWK_PAT] [varchar](100) NULL,
	[MWK_DESC][varchar](30),
	[IS_SYNC] [bit] NOT NULL DEFAULT 0,
	[CREATEDDATE] [datetime] NULL,
	[CREATEDBY] varchar(10),
	[MODIFIEDDATE] [datetime] NULL,	
	[MODIFIEDBY] varchar(10),
	[IsDeleted] [bit] NOT NULL DEFAULT 0,
	[DELETEDDATE] [datetime] NULL,
	[DELETEDBY] varchar(10),
	[COMPANY_ID] [int] foreign key references ENT_COMPANY(COMPANY_ID),	
	
	)

CREATE TABLE [dbo].[TNA_EMPLOYEE_TA_CONFIG]
(
	[ETC_ROWID] [bigint] IDENTITY(1,1) PRIMARY KEY ,
	[ETC_EMP_ID] int foreign key references ENT_EMPLOYEE_DTLS(EMPLOYEE_ID),
	[ETC_MINIMUM_SWIPE] [nvarchar](5) NULL,
	[ETC_SHIFTCODE] int foreign key references TA_SHIFT_PATTERN(SHIFT_PATTERN_ID),
	[ETC_WEEKEND] int foreign key references TA_WKLYOFF(MWK_CD),
	[ETC_WEEKOFF] int foreign key references TA_WKLYOFF(MWK_CD),
	[ETC_SHIFT_START_DATE] [datetime] NULL,
	[ETC_ISDELETED] [bit] NULL,
	[ETC_DELETEDDATE] [datetime] NULL,
	[ScheduleType] [varchar](255) NULL,
	[ShiftType] [varchar](255) NULL,
	[IS_SYNC] [bit] NOT NULL DEFAULT 0,
	)



	create table tday_days
	(
		tdays varchar(2) not null unique,
		tday int not null unique
	)

	declare @i int
	set @i=1
	while(@i<32)
	begin
		insert into tday_days(tdays,tday) values (@i,@i)
		set @i=@i+1
	end 
	
	CREATE TABLE [dbo].[TA_Leave_File]
	(
	[Leave_File_ID] [int] IDENTITY(1,1) PRIMARY KEY,
	[Leave_CODE] [nvarchar](15) NOT NULL,
	[Leave_Description] [nvarchar](50) NULL,
	[Leave_IsPaid] [bit] NULL,
	[Leave_Group] [nvarchar](15) NULL,
	[Leave_ISDELETED] [bit] NULL,
	[Leave_DELETEDDATE] [datetime] NULL,
	[Leave_IsProDataBasiss] [bit] NOT NULL,
	[MAXCARRYFORWARD] INT, ----USER FOR LEAVE YEAR END CALCULATION
	[IS_SYNC] [bit] NOT NULL DEFAULT 0,
	[COMPANY_ID] [int] foreign key references ENT_COMPANY(COMPANY_ID),	
	CONSTRAINT Leave_CODE_COMPANYID_Composite UNIQUE(Leave_CODE,COMPANY_ID)
)



CREATE TABLE TA_LEAVE_ENTITIES
(
	LEAVE_ENTITIES_ID INT PRIMARY KEY IDENTITY,
	LEAVE_ENTITIES_CODE NVARCHAR(200),	
)

INSERT INTO TA_LEAVE_ENTITIES(LEAVE_ENTITIES_CODE) VALUES ('MAX'),('MIN'),('PREFIX'),('SUFFIX'),('COUNT'),('PREAPPLY'),('POSTAPPLY'),('COMBINED')
----PROVISIONING TO BE MADE FOR MATERNITY,PATERNITY
CREATE TABLE [dbo].[TA_LEAVE_RULE](
	[LEAVE_RULE_ID] [int] IDENTITY(1,1) PRIMARY KEY,
	[Leave_File_ID] [int] foreign key references TA_Leave_File(Leave_File_ID),	
	[LEAVE_ENTITIES_ID] [int] foreign key references TA_LEAVE_ENTITIES(LEAVE_ENTITIES_ID),	
	[LEAVE_VALUES] [nvarchar](200) NULL,	
	[LR_ISDELETED] [bit] NOT NULL DEFAULT 0,
	[LR_DELETEDDATE] [datetime] NULL,
	[IS_SYNC] [bit] NOT NULL DEFAULT 0,
	CONSTRAINT Leave_File_ID_LEAVE_ENTITIES_ID_Composite UNIQUE(Leave_File_ID,LEAVE_ENTITIES_ID,LR_ISDELETED)
)

CREATE TABLE [dbo].[TA_LEAVE_RULE_NEW](
	[LR_REC_ID] [bigint] IDENTITY(1,1) NOT NULL,
	[LR_CODE] [nvarchar](15) NULL,
	[LR_CATEGORYID] [nvarchar](30) NULL,
	[LR_ALLOTMENT] [numeric](18, 2) NULL,
	[LR_ACCUMULATION] [numeric](18, 2) NULL,
	[LR_ISDELETED] [bit] NULL,
	[LR_DELETEDDATE] [datetime] NULL,
	[LeaveID] [nvarchar](30) NULL,
	[LR_DAYS] [char](1) NULL,
	[LEAVE_RULE] [char](1) NULL,
	[LR_GreaterOrLesser] [char](1) NULL,
	[LR_MinDaysAllowed] [numeric](18, 2) NULL,
	[LR_AllotmentType] [varchar](2) NULL,
	[LR_AllotmentType_YE_PR] [varchar](2) NULL,
	[LR_MaxDaysAllowed] [numeric](18, 2) NULL,
	[COMPANY_ID] [int] foreign key references ENT_COMPANY(COMPANY_ID),
)

CREATE  TABLE TA_LEAVE_YEAR
(
LEAVE_YEAR_ID INT PRIMARY KEY IDENTITY,
FROMDATE DATETIME,
TODATE DATETIME,
[IS_SYNC] [bit] NOT NULL DEFAULT 0,
[COMPANY_ID] [int] foreign key references ENT_COMPANY(COMPANY_ID),	
[PREVIOUS_LEAVE_YEAR_ID] [int] foreign key references TA_LEAVE_YEAR(LEAVE_YEAR_ID),	
)

CREATE TABLE [dbo].[TA_LEAVE_SUMMARY](
	[LV_ID] [int] IDENTITY(1,1) PRIMARY KEY,
	[LV_EMP_ID] int foreign key references ENT_EMPLOYEE_DTLS(EMPLOYEE_ID),
	[LV_LEAVE_ID] int foreign key references TA_Leave_File(Leave_File_ID),
	[LV_OPENINGBAL] [decimal](12, 2) NULL,
	[LV_ALLOTMENT] [decimal](12, 2) NULL,
	[LV_AVAILABLE] [decimal](12, 2) NULL,
	[LV_AVAILED] [decimal](12, 2) NULL,
	[LV_ENCASHED] [decimal](12, 2) NULL,
	[LV_CUT] [decimal](12, 2) NULL,
	[LV_LAPSED] [decimal](12, 2) NULL,
	[LV_LEAVE_YEAR] int foreign key references TA_LEAVE_YEAR(LEAVE_YEAR_ID),
	[LV_ISDELETED] [bit] NULL,
	[LV_DELETEDDATE] [datetime] NULL,
	[IS_SYNC] [bit] NOT NULL DEFAULT 0,
)


------------------------------------------------------------------

CREATE TABLE [dbo].[TDAY_STATUS](
	[TDAY_STATUS_ID] INT PRIMARY KEY IDENTITY,
	[TDAY_STATUS] [varchar](50) NULL,
	[TDAY_STATUS_CODE] [varchar](20) NULL,
	[CLASSNAME]	[varchar](50) NULL,
)

insert into tday_status values ('ABSENT','AB','text-danger')
, ('PRESENT','PR','text-success') 
, ('WEEK OFF','ABWO','text-default')
, ('WEEK END' ,'ABW2','text-default')
,('HalfdayOnWeekend','PRABW2','text-danger'),
('HalfdayOnWeekoff','PRABWO','text-danger'),
('HalfdayOnHoliday','PRABHO','text-danger')

	CREATE TABLE [dbo].[TDAY](
	[TDAY_ID] INT constraint PK_TDAY_ID PRIMARY KEY IDENTITY,
	[TDAY_EMPCDE] int constraint PK_TDAY_EMPCDE foreign key references ENT_EMPLOYEE_DTLS(EMPLOYEE_ID),
	[TDAY_DATE] [datetime] NOT NULL,
	[TDAY_SFTASSG] [varchar](2) NULL,
	[TDAY_SFTREPO] [varchar](2) NULL,
	[TDAY_INTIME] [datetime] NULL,
	[TDAY_OUTIME] [datetime] NULL,
	[TDAY_LUNCH_OUT] [datetime] NULL,
	[TDAY_LUNCH_IN] [datetime] NULL,
	[TDAY_OUDATE] [datetime] NULL,
	[TDAY_EXHR] [datetime] NULL,
	[TDAY_STATUS_ID] int foreign key references TDAY_STATUS(TDAY_STATUS_ID),
	[TDAY_STATUS_ID_FH] int foreign key references TDAY_STATUS(TDAY_STATUS_ID),
	[TDAY_STATUS_ID_SH] int foreign key references TDAY_STATUS(TDAY_STATUS_ID),
	[TDAY_LATE] [datetime] NULL,
	[TDAY_EARLY] [datetime] NULL,
	[TDAY_WRKHR] [datetime] NULL,
	[TDAY_ERLNCH] [decimal](1, 0) NULL,
	[TDAY_LTLNCH] [decimal](1, 0) NULL,
	[TDAY_TOTIN] [varchar](3) NULL,
	[TDAY_TOTOUT] [varchar](3) NULL,
	[TDAY_ENTRY] [varchar](2) NULL,
	[TDAY_LVCUT] [varchar](5) NULL,
	[TDAY_LTOT] [datetime] NULL,
	[TDAY_EROT] [datetime] NULL,
	[TDAY_SHIFT_INDEX] [int] NULL,
	[TDAY_InDATE] [datetime] NULL,
	[TDAY_SHIFT_PATTERN_ID] [varchar](20) NULL,
	[isProcessed] [int] NULL,
	[TDAY_LEAVE_YEAR] int foreign key references TA_LEAVE_YEAR(LEAVE_YEAR_ID),	
	CONSTRAINT TDAY_TDAY_EMPCDE_TDAY_DATE_Composite UNIQUE(TDAY_EMPCDE,TDAY_DATE)
)
CREATE NONCLUSTERED INDEX IX_TDAY_DATE  
ON TDAY (TDAY_DATE)  

CREATE NONCLUSTERED INDEX IX_TDAY_EMPCDE  
ON TDAY (TDAY_EMPCDE)  

CREATE NONCLUSTERED INDEX IX_TDAY_STATUS  
ON TDAY (TDAY_STATUS_ID)  

CREATE NONCLUSTERED INDEX IX_TDAY_INTIME 
ON TDAY (TDAY_INTIME)  


CREATE NONCLUSTERED INDEX IX_TDAY_OUTIME  
ON TDAY (TDAY_OUTIME)  

CREATE NONCLUSTERED INDEX IX_TDAY_OUDATE
ON TDAY (TDAY_OUDATE)  

CREATE NONCLUSTERED INDEX IX_TDAY_TDAY_InDATE
ON TDAY (TDAY_InDATE)  



CREATE TABLE [dbo].[ESS_TA_LA](
	[ESS_LA_ID] [bigint] IDENTITY(1,1) primary key,
	[ESS_LA_EMPID] int foreign key references ENT_EMPLOYEE_DTLS(EMPLOYEE_ID),
	[ESS_LA_REQUESTDT] [datetime] NULL,
	[ESS_LA_FROMDT] [datetime] NULL,
	[ESS_LA_TODT] [datetime] NULL,
	[ESS_LA_LVCD] [varchar](15) NOT NULL,
	[ESS_LA_REASON_ID] [int] foreign key references ENT_REASON(REASON_ID),
	[ESS_LA_REMARK] [varchar](50) NULL,
	[ESS_LA_SANCID] [varchar](15) NULL,
	[ESS_LA_SANCDT] [datetime] NULL,
	[ESS_LA_SANC_REMARK] [varchar](50) NULL,
	[ESS_LA_ORDER] [numeric](18, 0) NULL,
	[ESS_LA_STATUS] [varchar](50) NULL,
	[ESS_LA_OLDSTATUS] [varchar](50) NULL,
	[ESS_LA_ISDELETED] [bit] NULL,
	[ESS_LA_DELETEDDATE] [datetime] NULL,
	[ESS_LA_LVDAYS] [numeric](12, 2) NULL,
	[ESS_REQUEST_TYPE] [varchar](10)	
)


CREATE TABLE [dbo].[ESS_TA_MA](
	[ESS_MA_ID] [bigint] IDENTITY(1,1) primary key,
	[ESS_MA_EMPID] int foreign key references ENT_EMPLOYEE_DTLS(EMPLOYEE_ID),
	[ESS_MA_REQUESTDT] [datetime] NULL,
	ESS_MA_FROMDT [datetime] NULL,
	ESS_MA_FROMTM [datetime] NULL,
	ESS_MA_TODT [datetime] NULL,
	ESS_MA_TOTM [datetime] NULL,
	[ESS_MA_REASON_ID] [int] foreign key references ENT_REASON(REASON_ID),
	[ESS_MA_REMARK] [varchar](50) NULL,
	[ESS_SANCTION_ID] int foreign key references ENT_User([USER_ID]),
	[ESS_MA_SANCDT] [datetime] NULL,
	[ESS_MA_SANC_REMARK] [varchar](50) NULL,
	[ESS_MA_ORDER] [numeric](18, 0) NULL,
	[ESS_MA_STATUS] [varchar](50) NULL,
	[ESS_MA_OLDSTATUS] [varchar](50) NULL,
	[ESS_MA_ISDELETED] [bit] NULL,
	[ESS_MA_DELETEDDATE] [datetime] NULL,
	[ESS_MA_LVDAYS] [numeric](12, 2) NULL,
	[ESS_REQUEST_TYPE] [varchar](10)	
)


CREATE TABLE [dbo].[ESS_TA_OD](
	[ESS_OD_ID] [bigint] IDENTITY(1,1) primary key,
	[ESS_OD_EMPID] int foreign key references ENT_EMPLOYEE_DTLS(EMPLOYEE_ID),
	ESS_OD_FROLOC varchar(200) NULL,
	ESS_OD_TOLOC varchar(200) NULL,
	ESS_OD_ODCD varchar(100),
	[ESS_OD_REQUESTDT] [datetime] NULL,
	ESS_OD_FROMDT [datetime] NULL,
	ESS_OD_FROMTM [datetime] NULL,
	ESS_OD_TODT [datetime] NULL,
	ESS_OD_TOTM [datetime] NULL,
	[ESS_OD_REASON_ID] [int] foreign key references ENT_REASON(REASON_ID),
	[ESS_OD_REODRK] [varchar](50) NULL,
	[ESS_SANCTION_ID] int foreign key references ENT_User([USER_ID]),
	[ESS_OD_SANCDT] [datetime] NULL,
	[ESS_OD_SANC_REODRK] [varchar](50) NULL,
	[ESS_OD_ORDER] [numeric](18, 0) NULL,
	[ESS_OD_STATUS] [varchar](50) NULL,
	[ESS_OD_OLDSTATUS] [varchar](50) NULL,
	[ESS_OD_ISDELETED] [bit] NULL,
	[ESS_OD_DELETEDDATE] [datetime] NULL,
	[ESS_OD_LVDAYS] [numeric](12, 2) NULL,
	[ESS_REQUEST_TYPE] [varchar](10)	
)


CREATE TABLE [dbo].[ESS_TA_CO](
	[ESS_CO_ID] [bigint] IDENTITY(1,1) primary key,
	[ESS_CO_EMPID] int foreign key references ENT_EMPLOYEE_DTLS(EMPLOYEE_ID),
	ESS_CO_DAYS numeric(4,2),
	ESS_CO_LEAVEAGANISTDATE [datetime] NULL,
	[ESS_CO_REQUESTDT] [datetime] NULL,
	ESS_CO_DT [datetime] NULL,
	[ESS_CO_REASON_ID] [int] foreign key references ENT_REASON(REASON_ID),
	[ESS_CO_REMARK] [varchar](50) NULL,
	[ESS_SANCTION_ID] int foreign key references ENT_User([USER_ID]),
	[ESS_CO_SANCDT] [datetime] NULL,
	[ESS_CO_SANC_RECORK] [varchar](50) NULL,
	[ESS_CO_STATUS] [varchar](50) NULL,
	[ESS_CO_ISDELETED] [bit] NULL,
	[ESS_CO_DELETEDDATE] [datetime] NULL,
	[ESS_REQUEST_TYPE] [varchar](10)	
)


CREATE TABLE [dbo].[ESS_TA_GP](
	[ESS_GP_ID] [bigint] IDENTITY(1,1) primary key,
	[ESS_GP_EMPID] int foreign key references ENT_EMPLOYEE_DTLS(EMPLOYEE_ID),
	ESS_GP_GPCD varchar(100),
	[ESS_GP_REQUESTDT] [datetime] NULL,
	ESS_GP_FROMDT [datetime] NULL,
	ESS_GP_FROMTM [datetime] NULL,
	ESS_GP_TODT [datetime] NULL,
	ESS_GP_TOTM [datetime] NULL,
	[ESS_GP_REASON_ID] [int] foreign key references ENT_REASON(REASON_ID),
	[ESS_GP_REMARK] [varchar](50) NULL,
	[ESS_SANCTION_ID] int foreign key references ENT_User([USER_ID]),
	[ESS_GP_SANCDT] [datetime] NULL,
	[ESS_GP_SANC_REMARK] [varchar](50) NULL,
	[ESS_GP_STATUS] [varchar](50) NULL,
	[ESS_GP_OLDSTATUS] [varchar](50) NULL,
	[ESS_GP_ISDELETED] [bit] NULL,
	[ESS_GP_DELETEDDATE] [datetime] NULL,
	[ESS_REQUEST_TYPE] [varchar](10)	
)


CREATE TABLE [dbo].[ESS_TA_OPT_HO](
	[ESS_OPT_HO_ID] [bigint] IDENTITY(1,1) primary key,
	[ESS_HO_EMPID] [varchar](15) NULL,
	[ESS_HO_REQ_DATE] [datetime] NULL,
	[ESS_HOLIDAYD_ID] [varchar](50) NULL,
	[ESS_HOLIDAY_DATE] [datetime] NULL,
	[ESS_HO_STATUS] [varchar](3) NULL,
	[ESS_HO_SANCID] [varchar](15) NULL,
	[ESS_HO_SANC_DATE] [datetime] NULL,
	[ESS_REMARK] [varchar](100) NULL,
	[ESS_HO_ISDELETED] [bit] NULL,
	[ESS_HO_DELETEDDATE] [datetime] NULL
) 



-----OD FROM TO LOCATION
--[dbo].[TA_Manual_Att]
--[dbo].[TA_LEAVE_REQ]
--[dbo].[TA_Outpass_Att]
--[dbo].[TA_GATEPASS]
--[dbo].[TA_COMP_OFF_REQ]
--[dbo].[TA_CO_USEDDETAIL]
----------------------------
--[dbo].[ESS_TA_MS]
--[dbo].[ESS_TA_MA]
--[dbo].[ESS_TA_OPT_HO]
--[dbo].[ESS_TA_LA]
--[dbo].[ESS_TA_GP]
--[dbo].[ESS_TA_CO]
--[dbo].[ESS_TA_OD]


----------------------Visitor -----------------------------------------------

CREATE TABLE [dbo].[Visitor_Appointment_Request](
	[RequestID] [int] primary key IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[Visitor_id] [varchar](100) NULL,
	[Approval_Authority_Code] [varchar](20) NULL,
	[approval_status] [varchar](10) NULL,
	[Aprroval_datetime] [datetime] NULL,
	[Approver_Remarks] [varchar](200) NULL,
	[Visitor_Allowed_From_time] [datetime] NULL,
	[visitor_Allowed_To_Time] [datetime] NULL,
	[appointment_from_date] [datetime] NULL,
	[appointment_to_date] [datetime] NULL,
	[nature_of_work] [varchar](30) NULL,
	[additional_Info] [varchar](200) NULL,
	[isDeleted] [bit] NULL,
	[isDeletedDate] [datetime] NULL,
	[Visitor_Name] [varchar](100) NULL,
	[VisitorCompany] [varchar](200) NULL,
	[mobileNo] [varchar](20) NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [varchar](15) NULL,
	[Designation] [varchar](200) NULL,
	[CheckedOutTime] [datetime] NULL,
	[PurposeOfVisit] [varchar](200) NULL,
	[Visitor_MiddleName] [varchar](100) NULL,
	[Visitor_LastName] [varchar](100) NULL,
	[visitor_nationality] [varchar](10) NULL,
	[visitor_location] [varchar](50) NULL,
	[Total_Count] [varchar](10) NULL,
        [IS_SYNC] [bit] NOT NULL DEFAULT 0, 
        [COMPANY_ID] [int] foreign key references ENT_COMPANY(COMPANY_ID),
) ON [PRIMARY]


CREATE TABLE [dbo].[Visitor_Master](
	[RowID] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[Visitor_ID] [varchar](100) NOT NULL,
	[Visitor_Name] [varchar](100) NULL,
	[Visitor_Photopath] [varchar](200) NULL,
	[VisitorCompany] [varchar](300) NULL,
	[VisitorCompAddress] [varchar](400) NULL,
	[VisitorCity] [varchar](50) NULL,
	[VisitorNationality] [varchar](50) NULL,
	[VisitorPassportDetails] [varchar](200) NULL,
	[Designation] [varchar](100) NULL,
	[visitor_type] [varchar](50) NULL,
	[Gender] [varchar](50) NULL,
	[address] [varchar](300) NULL,
	[Phone1] [varchar](20) NULL,
	[Phone2] [varchar](20) NULL,
	[mobileNo] [varchar](20) NULL,
	[VisitorFingerprintTemplate1] [varbinary](max) NULL,
	[VisitorFingerprintTemplate2] [varbinary](max) NULL,
	[IsDeleted] [bit] NULL,
	[IsDeletedDate] [datetime] NULL,
	[Employee_ID] [varchar](15) NULL,
	[VisitorPassportValidTill] [datetime] NULL,
	[VisitorPassportNo] [varchar](200) NULL,
	[VisitorFingerImageWarWick1] [varbinary](max) NULL,
	[VisitorFingerImageWarWick2] [varbinary](max) NULL,
	[CreatedOn] [datetime] NULL,
	[VisitorCardNo] [varchar](max) NULL,
	[Visitor_MiddleName] [varchar](100) NULL,
	[Visitor_LastName] [varchar](100) NULL,
	[VisitorImage] [varbinary](max) NULL,
	[VisitorSign] [varbinary](max) NULL,
        [IS_SYNC] [bit] NOT NULL DEFAULT 0, 
        [COMPANY_ID] [int] foreign key references ENT_COMPANY(COMPANY_ID),
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]


---------------------------------------------------------------------------------------------


CREATE TABLE ENT_PARAMS
(
	[PARAM_ID] int IDENTITY(1,1) primary key,
	[MODULE] [nvarchar](50) NULL,
	[IDENTIFIER] [nvarchar](50) NULL,
	[CODE] [nvarchar](50) NULL,
	[VALUE] [nvarchar](50) NULL,
	CONSTRAINT ENT_PARAMS_MODULE_IDENTIFIER_CODE_Composite UNIQUE(MODULE,IDENTIFIER,CODE)
)
INSERT INTO [dbo].[ENT_PARAMS]([MODULE],[IDENTIFIER],[CODE],[VALUE])
     VALUES 
	 ('TA','DAILYPROCESS','HD','04:00')
,('TA','DAILYPROCESS','FD','08:00')	 
	 ,('TA','SHIFT_PATTERN_TYPE','DL','DAILY')
	,('TA','SHIFT_PATTERN_TYPE','WK','WEEKLY')
	,('TA','SHIFT_PATTERN_TYPE','BW','BI-WEEKLY')
	,('TA','SHIFT_PATTERN_TYPE','MN','MONTHLY')
	 ,('ENT','BLOODGROUP','A-','A-')
	 ,('ENT','BLOODGROUP','A+','A+')
	 ,('ENT','BLOODGROUP','B-','B-')
	 ,('ENT','BLOODGROUP','B+','B+')
	 ,('ENT','BLOODGROUP','AB-','AB-')
	 ,('ENT','BLOODGROUP','AB+','AB+')
	 ,('ENT','BLOODGROUP','O-','O-')	 

	 ,('ENT','GENDER','F','FEMALE')
	 ,('ENT','GENDER','M','MALE')
	 ,('ENT','RELIGION','H','HINDU')
,('ENT','RELIGION','I','ISLAM')
,('ENT','RELIGION','C','CHRISTIAN')
,('ENT','RELIGION','S','SIKH')
,('ENT','RELIGION','B','BUDDHIST')
,('ENT','RELIGION','J','JAIN')
,('ENT','RELIGION','Z','ZOROASTRIAN')
	 ,('ENT','MARITALSTATUS','M','MARRIED')
,('ENT','MARITALSTATUS','U','SINGLE')
,('ENT','MARITALSTATUS','D','DIVORCED')
,('ENT','MARITALSTATUS','W','WIDOWED')
,('ENT','MARITALSTATUS','P','SEPARATED')
	 ,('ENT','EMPSTATUS','T','APPRENTICE/TRAINEE')
,('ENT','EMPSTATUS','C','CONFIRMED')
,('ENT','EMPSTATUS','B','PROBATION')
,('ENT','EMPSTATUS','D','DEPUTATION')
,('ENT','EMPSTATUS','CT','CONTRACT')
,('ENT','EMPSTATUS','TP','TEMPORARY')
,('ACS','EVENTTYPE','01','CARD')
,('ACS','EVENTTYPE','02','CARD')
,('ACS','EVENTTYPE','03','CARD')
	 ,('EVNT','EVENTMASTER','06','INCORRECT_AUTHENTICATION_MODE_MESSAGE')
,('EVNT','EVENTMASTER','07','INCORRECT_PIN')
,('EVNT','EVENTMASTER','05','MANAGER_VERIFICATION_FAILED')
,('EVNT','EVENTMASTER','04','USER_ENROLLED_SUCCESSFULLY')
,('EVNT','EVENTMASTER','03','USER_NOT_FOUND_MESSAGE')
,('EVNT','EVENTMASTER','02','USER_VERIFICATION_FAILED')
,('EVNT','EVENTMASTER','01','USER_VERIFIED')
	 ,('TA','LEAVE_TYPE','CL','CASUAL LEAVE')
	,('TA','LEAVE_TYPE','PL','PATERNITY LEAVE')
,('TA','LEAVE_TYPE','WP','WITHOUT PAY')
,('TA','LEAVE_TYPE','ML','MATERNITY LEAVE')
,('TA','LEAVE_TYPE','EL','EARNED LEAVE')
	 , ('ACS','CONTROLLERTYPE','DC','DC102')
	 , ('ACS','CONTROLLERTYPE','DCB','DC102BIO')
	 , ('ACS','CONTROLLERTYPE','MD4','4DC4')
	 , ('ACS','CONTROLLERTYPE','MD8','8DC8')
	 , ('ACS','CONTROLLERTYPE','HHR','HANDHELD READER')
	 , ('ACS','CONTROLLERTYPE','BH','BIOHANDY')
	 , ('ACS','CONTROLLERTYPE','BIOEDGE+','BIOEDGE+')
	 , ('ACS','CONFIGCHANGE','CTL','0')
	 , ('ACS','AUTHENTICATIONMODE','C','CARD')
	 , ('ACS','AUTHENTICATIONMODE','F','FINGER')
	 , ('ACS','AUTHENTICATIONMODE','CF','CARD + FINGER')
	 , ('ACS','READERTYPE','0','26 Bit HID')
	 , ('ACS','READERTYPE','1','34 Bit Mifare')
	 , ('ACS','READERTYPE','2','34 Bit HID (16 Bit Card ID)')
	 , ('ACS','READERTYPE','3','34 Bit HID (24 Bit Card ID)')
	 , ('ACS','READERTYPE','4','35 Bit HID (Corporate 1000)')
	 , ('ACS','READERTYPE','5','37 Bit HID')
	 , ('ACS','READERTYPE','6','ID08')
	 , ('ACS','READERMODE','I','IN')
	 , ('ACS','READERMODE','O','OUT')
	 , ('ACS','READERMODE','N','ALL')
	 , ('TA','READERMODE','IN','85')
	 , ('TA','READERMODE','OUT','86')
	 , ('TA','READERMODE','ALL','84')
	 , ('ACS','DOORTYPE','M','Magnetic')
	 , ('ACS','DOORTYPE','MF','Magnetic with Feedback')
	 , ('ACS','DOORTYPE','S','Strike')
	 , ('ACS','DOORTYPE','SF','Strike with Feedback')
	 , ('ACS','DOORTYPE','TF','Turnstile with Feedback')
	 , ('ACS','DOORTYPE','D','Dropbolt')
	 , ('ACS','DOORTYPE','DF','Dropbolt with Feedback')
	 

GO

INSERT INTO ENT_PARAMS (MODULE,IDENTIFIER,CODE,VALUE) VALUES('STATES','STATES','A.P','ANDHRA PRADESH')
INSERT INTO ENT_PARAMS (MODULE,IDENTIFIER,CODE,VALUE) VALUES('STATES','STATES','AND','ANDAMAN & NICOBAR')
INSERT INTO ENT_PARAMS (MODULE,IDENTIFIER,CODE,VALUE) VALUES('STATES','STATES','ARU','ARUNACHAL PRADESH')
INSERT INTO ENT_PARAMS (MODULE,IDENTIFIER,CODE,VALUE) VALUES('STATES','STATES','ASM','ASSAM')
INSERT INTO ENT_PARAMS (MODULE,IDENTIFIER,CODE,VALUE) VALUES('STATES','STATES','BHR','BIHAR')
INSERT INTO ENT_PARAMS (MODULE,IDENTIFIER,CODE,VALUE) VALUES('STATES','STATES','CHA','CHHATTISGARH')
INSERT INTO ENT_PARAMS (MODULE,IDENTIFIER,CODE,VALUE) VALUES('STATES','STATES','CHN','CHANDIGARH')
INSERT INTO ENT_PARAMS (MODULE,IDENTIFIER,CODE,VALUE) VALUES('STATES','STATES','DAD','DADAR & NAHARHAVELII')
INSERT INTO ENT_PARAMS (MODULE,IDENTIFIER,CODE,VALUE) VALUES('STATES','STATES','DAM','DAMAN DIYU')
INSERT INTO ENT_PARAMS (MODULE,IDENTIFIER,CODE,VALUE) VALUES('STATES','STATES','DEL','DELHI')
INSERT INTO ENT_PARAMS (MODULE,IDENTIFIER,CODE,VALUE) VALUES('STATES','STATES','GOA','GOA')
INSERT INTO ENT_PARAMS (MODULE,IDENTIFIER,CODE,VALUE) VALUES('STATES','STATES','GUJ','GUJARAT')
INSERT INTO ENT_PARAMS (MODULE,IDENTIFIER,CODE,VALUE) VALUES('STATES','STATES','H.P','HIMACHAL PRADESH')
INSERT INTO ENT_PARAMS (MODULE,IDENTIFIER,CODE,VALUE) VALUES('STATES','STATES','HAR','HARYANA')
INSERT INTO ENT_PARAMS (MODULE,IDENTIFIER,CODE,VALUE) VALUES('STATES','STATES','J&K','JAMMU & KASHMIR')
INSERT INTO ENT_PARAMS (MODULE,IDENTIFIER,CODE,VALUE) VALUES('STATES','STATES','JAR','JHARKHAND')
INSERT INTO ENT_PARAMS (MODULE,IDENTIFIER,CODE,VALUE) VALUES('STATES','STATES','KAR','KARNATAKA')
INSERT INTO ENT_PARAMS (MODULE,IDENTIFIER,CODE,VALUE) VALUES('STATES','STATES','KER','KERALA')
INSERT INTO ENT_PARAMS (MODULE,IDENTIFIER,CODE,VALUE) VALUES('STATES','STATES','LAK','LAKSHADWEEP')
INSERT INTO ENT_PARAMS (MODULE,IDENTIFIER,CODE,VALUE) VALUES('STATES','STATES','M.P','MADHYA PRADESH')
INSERT INTO ENT_PARAMS (MODULE,IDENTIFIER,CODE,VALUE) VALUES('STATES','STATES','MAH','MAHARASHTRA')
INSERT INTO ENT_PARAMS (MODULE,IDENTIFIER,CODE,VALUE) VALUES('STATES','STATES','MAN','MANIPUR')
INSERT INTO ENT_PARAMS (MODULE,IDENTIFIER,CODE,VALUE) VALUES('STATES','STATES','MEG','MEGHALAYA')
INSERT INTO ENT_PARAMS (MODULE,IDENTIFIER,CODE,VALUE) VALUES('STATES','STATES','MIS','MISSORAM')
INSERT INTO ENT_PARAMS (MODULE,IDENTIFIER,CODE,VALUE) VALUES('STATES','STATES','NAG','NAGALAND')
INSERT INTO ENT_PARAMS (MODULE,IDENTIFIER,CODE,VALUE) VALUES('STATES','STATES','ORS','ORISSA')
INSERT INTO ENT_PARAMS (MODULE,IDENTIFIER,CODE,VALUE) VALUES('STATES','STATES','PND','PONDICHERRI')
INSERT INTO ENT_PARAMS (MODULE,IDENTIFIER,CODE,VALUE) VALUES('STATES','STATES','PUN','PUNJAB')
INSERT INTO ENT_PARAMS (MODULE,IDENTIFIER,CODE,VALUE) VALUES('STATES','STATES','RAJ','RAJASTHAN')
INSERT INTO ENT_PARAMS (MODULE,IDENTIFIER,CODE,VALUE) VALUES('STATES','STATES','SIK','SIKKIM')
INSERT INTO ENT_PARAMS (MODULE,IDENTIFIER,CODE,VALUE) VALUES('STATES','STATES','T.N','TAMIL NADU')
INSERT INTO ENT_PARAMS (MODULE,IDENTIFIER,CODE,VALUE) VALUES('STATES','STATES','TLN','TELANGANA')
INSERT INTO ENT_PARAMS (MODULE,IDENTIFIER,CODE,VALUE) VALUES('STATES','STATES','TRI','TRIPURA')
INSERT INTO ENT_PARAMS (MODULE,IDENTIFIER,CODE,VALUE) VALUES('STATES','STATES','U.P','UTTAR PRADESH')
INSERT INTO ENT_PARAMS (MODULE,IDENTIFIER,CODE,VALUE) VALUES('STATES','STATES','UTK','UTTARAKHAND')
INSERT INTO ENT_PARAMS (MODULE,IDENTIFIER,CODE,VALUE) VALUES('STATES','STATES','UTT','UTTARANCHAL')
INSERT INTO ENT_PARAMS (MODULE,IDENTIFIER,CODE,VALUE) VALUES('STATES','STATES','W.B','WEST BENGAL')
INSERT INTO ENT_PARAMS (MODULE,IDENTIFIER,CODE,VALUE) VALUES('STATES','STATES','OT','Not In India')
GO

--------------------SENTINAL-------------------------
CREATE TABLE [dbo].[ACS_CONTROLLER](
	[ID] [bigint] IDENTITY(1,1) primary key NOT NULL,
	[CTLR_ID] int NOT NULL,
	[CTLR_DESCRIPTION] [nvarchar](50) NULL,
	[CTLR_TYPE]  nvarchar(20),
	[CTLR_IP] [nvarchar](15) NULL,
	[CTLR_SUBNET_MASK] [nvarchar](15) NULL,
	[CTLR_GATEWAY_IP] [nvarchar](15) NULL,
	[CTLR_SERVER_IP] [nvarchar](15) NULL,
	[CTLR_MAC_ID] [nvarchar](20) NULL,
	[CTLR_INCOMING_PORT] [nvarchar](6) NULL,
	[CTLR_OUTGOING_PORT] [nvarchar](6) NULL,
	[CTLR_FIRMWARE_VERSION_NO] [nvarchar](20) NULL,
	[CTLR_HARDWARE_VERSION_NO] [nvarchar](20) NULL,
	[CTLR_CHK_FACILITY_CODE] [bit] NULL,
	[CTLR_FACILITY_CODE1] [nvarchar](6) NULL,
	[CTLR_FACILITY_CODE2] [nvarchar](6) NULL,
	[CTLR_FACILITY_CODE3] [nvarchar](6) NULL,
	[CTLR_FACILITY_CODE4] [nvarchar](6) NULL,
	[CTLR_FACILITY_CODE5] [nvarchar](6) NULL,
	[CTLR_FACILITY_CODE6] [nvarchar](6) NULL,
	[CTLR_CHK_APB] [nvarchar](50) NULL,
	[CTLR_APB_TYPE] [nvarchar](2) NULL,
	[CTLR_APB_TIME] [nchar](10) NULL,
	[CTLR_AUTHENTICATION_MODE] nvarchar(4),
	[CTLR_CHK_TOC] [bit] NULL,
	[CTLR_EVENTS_STORED] [nvarchar](5) NULL,
	[CTLR_MAX_TRANSACTIONS] [nvarchar](5) NULL,
	[CTLR_CURRENT_USER_CNT] [nvarchar](4) NULL,
	[CTLR_MAX_USER_CNT] [nvarchar](4) NULL,
	[CTLR_ISDELETED] [bit] NULL,
	[CTLR_DELETEDDATE] [datetime] NULL,
	[CTLR_DELETEDBY] varchar(50),
	[CTLR_CREATEDDATE] [datetime] NULL,
	[CTLR_CREATEDBY] varchar(50),
	[CTLR_MODIFIEDDATE] [datetime] NULL,
	[CTLR_MODIFIEDBY] varchar(50),
	[CTLR_CONN_STATUS] [nvarchar](2) NULL,
	[CTLR_INACTIVE_DATETIME] [nvarchar](50) NULL,
	[CLTR_FOR_TA] [bit] NOT NULL DEFAULT 0,
	[Reinit_AL] [bit] NULL,
	[Reinit_TZ] [bit] NULL,
	[Reinit_AP] [bit] NULL,
	[CTLR_KEY_PAD] [bit] NULL,
	[CTLR_LOCATION_ID] [nvarchar](60) NULL,
	[EmptyReaderStatus] [bit] NULL,
	[CTLR_VISITOR] [bit] NULL,
	[COMPANY_ID] [int] foreign key references ENT_COMPANY(COMPANY_ID),
	[IS_SYNC] [bit] NOT NULL DEFAULT 0
	unique(CTLR_ID,COMPANY_ID,CTLR_ISDELETED),
	unique(COMPANY_ID,CTLR_IP)
	)
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'REF ENT_CONTROL TABLE' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACS_CONTROLLER', @level2type=N'COLUMN',@level2name=N'CTLR_TYPE'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'C-> CARD / F->FINGER/CF->CARD+FINGER' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACS_CONTROLLER', @level2type=N'COLUMN',@level2name=N'CTLR_FACILITY_CODE1'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'FROM CTRL TABLE' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACS_CONTROLLER', @level2type=N'COLUMN',@level2name=N'CTLR_APB_TYPE'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'C->CARD;F->FINGER;CF->CARD+FINGER' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACS_CONTROLLER', @level2type=N'COLUMN',@level2name=N'CTLR_AUTHENTICATION_MODE'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'IF MODE IS CARD + FINGER, CHK WHETHER IT IS TEMPLATE ON CARD' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACS_CONTROLLER', @level2type=N'COLUMN',@level2name=N'CTLR_CHK_TOC'
GO

CREATE TABLE [dbo].[ACS_READER](
	[RowID] [int] IDENTITY(1,1) primary key NOT NULL,
	[READER_ID] int NOT NULL,
	[READER_DESCRIPTION] [nvarchar](100) NULL,
	[CTLR_ID] int not null,
	[COMPANY_ID] [int] foreign key references ENT_COMPANY(COMPANY_ID),
	[READER_MODE] nvarchar(2),
	[READER_TYPE] nvarchar(20),
	[READER_PASSES_FROM] [nvarchar](15) NULL,
	[READER_PASSES_TO] [nvarchar](15) NULL,
	[READER_ISDELETED] [bit] not NULL,
	[READER_DELETEDDATE] [datetime] NULL,
	[IsActive] [bit] not NULL,
	[EntryReaderMode] [bigint] NULL,
	unique(READER_ID,CTLR_ID,COMPANY_ID)
	)
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'REF CTRL TABLE - IN,OUT,NULL' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACS_READER', @level2type=N'COLUMN',@level2name=N'READER_MODE'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'REF CTRL TABLE' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACS_READER', @level2type=N'COLUMN',@level2name=N'READER_TYPE'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ZONE ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACS_READER', @level2type=N'COLUMN',@level2name=N'READER_PASSES_FROM'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ZONE ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACS_READER', @level2type=N'COLUMN',@level2name=N'READER_PASSES_TO'
GO



CREATE TABLE [dbo].[ACS_DOOR](
	[DOOR_ID] int NOT NULL,
	[DOOR_DESCRIPTION] [nvarchar](50) NULL,
	[CTLR_ID] [bigint] foreign key references ACS_CONTROLLER(ID),
	[DOOR_TYPE] [nvarchar](50) NULL,
	[DOOR_OPEN_DURATION] [nvarchar](10) NULL,
	[DOOR_FEEDBACK_DURATION] [nvarchar](10) NULL,
	[DOOR_ISDELETED] [bit] not NULL,
	[DOOR_DELETEDDATE] [datetime] NULL,
	[READER_ID] int not NULL,
	[AP_FLAG] [bit] not NULL,
	unique(DOOR_ID,CTLR_ID)
	)
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'DOOR TYPE FROM CTRL TABLE' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACS_DOOR', @level2type=N'COLUMN',@level2name=N'DOOR_TYPE'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'IN SECONDS' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACS_DOOR', @level2type=N'COLUMN',@level2name=N'DOOR_OPEN_DURATION'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'IN SECONDS' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACS_DOOR', @level2type=N'COLUMN',@level2name=N'DOOR_FEEDBACK_DURATION'
GO

CREATE TABLE [dbo].[ACS_ACCESSPOINT_RELATION](
	[AP_ID] [decimal](18, 1) NOT NULL,
	[READER_ID] int NOT NULL,
	[DOOR_ID] int not NULL,
	[AP_CONTROLLER_ID] [bigint] foreign key references ACS_CONTROLLER(ID),
	[APR_ISDELETED] [bit] not NULL,
	[APR_DELETEDDATE] [datetime] NULL,
	[AccessPoint_Type] [bigint] NULL,
	[AP_FLAG] bit NOT NULL
)


CREATE TABLE [dbo].[ZONE](
	[ZONE_ID] [int] IDENTITY(1,1) primary key NOT NULL,
	[ZONE_DESCRIPTION] [nvarchar](50) not NULL,
	[ZONE_ISDELETED] [bit] not NULL DEFAULT 0,
	[ZONE_DELETEDDATE] [datetime] NULL,
	[ZONE_DELETEDBY] varchar(50) NULL,
	[ZONE_CREATEDDATE] [datetime] NULL,
	[ZONE_CREATEDBY]  varchar(50) NULL,
	[ZONE_MODIFIEDDATE] [datetime] NULL,
	[ZONE_MODIFIEDBY]  varchar(50) NULL,
	[COMPANY_ID] [int] foreign key references ENT_COMPANY(COMPANY_ID),
	[IS_SYNC] [bit] NOT NULL DEFAULT 0
	unique(ZONE_DESCRIPTION,COMPANY_ID)
)

CREATE TABLE [dbo].[ZONE_READER_REL](
	[ZONE_ID] int foreign key references ZONE(ZONE_ID),
	[READER_ID] int foreign key references ACS_READER(RowID),	
	[ZONER_ISDELETED] [bit] not NULL DEFAULT 0,
	[ZONER_DELETEDDATE] [datetime] NULL
)


CREATE TABLE [dbo].[ACS_TIMEZONE](
	[TZ_ID] [bigint] IDENTITY(1,1) constraint pk_ACS_TIMEZONE_TZ_ID primary key NOT NULL,	
	[TZ_DESCRIPTION] [nvarchar](50) constraint uq_ACS_TIMEZONE_TZ_DESCRIPTION unique not NULL,
	[TZ_NAME] [nvarchar](50) NULL,
	[TZ_ISDELETED] [bit] not NULL default 0,
	[TZ_DELETEDDATE] [datetime] NULL,
	[Period_id] [int] NULL
)


CREATE TABLE [dbo].[ACS_TIMEZONE_RELATION](
	[TZ_RELATION_ID] [bigint] IDENTITY(1,1) constraint pk_ACS_TIMEZONE_RELATION_TZ_RELATION_ID primary key NOT NULL,
	[TZ_ID] [bigint] constraint fk_ACS_TIMEZONE_RELATION_TZ_ID foreign key references ACS_TIMEZONE(TZ_ID),
	[TZ_TYPE] [nvarchar](10) NULL,
	[TZ_DAYOFWEEK] [nvarchar](15) NOT NULL,
	[TZ_FROMTIME] [datetime] NOT NULL,
	[TZ_TOTIME] [datetime] NOT NULL,
	[TZR_ISDELETED] [bit] not NULL default 0,
	[TZR_DELETEDDATE] [datetime] NULL,
	[Period_id] [bigint] NULL,
	[TZ_Flag] [bigint] NULL
)

declare @tzId int
INSERT INTO [dbo].[ACS_TIMEZONE]([TZ_DESCRIPTION],[TZ_NAME],[TZ_ISDELETED],[TZ_DELETEDDATE],[Period_id])
     VALUES('ALWAYS',null,0,null,null)
set @tzId=@@identity
print @tzId
INSERT INTO [dbo].[ACS_TIMEZONE_RELATION]([TZ_ID],[TZ_TYPE],[TZ_DAYOFWEEK],[TZ_FROMTIME],[TZ_TOTIME],[TZR_ISDELETED],[TZR_DELETEDDATE],[Period_id],[TZ_Flag])
VALUES(@tzId,'DOW','FRI','1900-01-01 00:00:00.000','1900-01-01 23:59:00.000',0,null,1,0);
INSERT INTO [dbo].[ACS_TIMEZONE_RELATION]([TZ_ID],[TZ_TYPE],[TZ_DAYOFWEEK],[TZ_FROMTIME],[TZ_TOTIME],[TZR_ISDELETED],[TZR_DELETEDDATE],[Period_id],[TZ_Flag])
VALUES(@tzId,'HO','H1','1900-01-01 00:00:00.000','1900-01-01 23:59:00.000',0,null,1,0);
INSERT INTO [dbo].[ACS_TIMEZONE_RELATION]([TZ_ID],[TZ_TYPE],[TZ_DAYOFWEEK],[TZ_FROMTIME],[TZ_TOTIME],[TZR_ISDELETED],[TZR_DELETEDDATE],[Period_id],[TZ_Flag])
VALUES(@tzId,'HO','H2','1900-01-01 00:00:00.000','1900-01-01 23:59:00.000',0,null,1,0);
INSERT INTO [dbo].[ACS_TIMEZONE_RELATION]([TZ_ID],[TZ_TYPE],[TZ_DAYOFWEEK],[TZ_FROMTIME],[TZ_TOTIME],[TZR_ISDELETED],[TZR_DELETEDDATE],[Period_id],[TZ_Flag])
VALUES(@tzId,'DOW','MON','1900-01-01 00:00:00.000','1900-01-01 23:59:00.000',0,null,1,0);
INSERT INTO [dbo].[ACS_TIMEZONE_RELATION]([TZ_ID],[TZ_TYPE],[TZ_DAYOFWEEK],[TZ_FROMTIME],[TZ_TOTIME],[TZR_ISDELETED],[TZR_DELETEDDATE],[Period_id],[TZ_Flag])
VALUES(@tzId,'DOW','SAT','1900-01-01 00:00:00.000','1900-01-01 23:59:00.000',0,null,1,0);
INSERT INTO [dbo].[ACS_TIMEZONE_RELATION]([TZ_ID],[TZ_TYPE],[TZ_DAYOFWEEK],[TZ_FROMTIME],[TZ_TOTIME],[TZR_ISDELETED],[TZR_DELETEDDATE],[Period_id],[TZ_Flag])
VALUES(@tzId,'DOW','SUN','1900-01-01 00:00:00.000','1900-01-01 23:59:00.000',0,null,1,0);
INSERT INTO [dbo].[ACS_TIMEZONE_RELATION]([TZ_ID],[TZ_TYPE],[TZ_DAYOFWEEK],[TZ_FROMTIME],[TZ_TOTIME],[TZR_ISDELETED],[TZR_DELETEDDATE],[Period_id],[TZ_Flag])
VALUES(@tzId,'DOW','THR','1900-01-01 00:00:00.000','1900-01-01 23:59:00.000',0,null,1,0);
INSERT INTO [dbo].[ACS_TIMEZONE_RELATION]([TZ_ID],[TZ_TYPE],[TZ_DAYOFWEEK],[TZ_FROMTIME],[TZ_TOTIME],[TZR_ISDELETED],[TZR_DELETEDDATE],[Period_id],[TZ_Flag])
VALUES(@tzId,'DOW','TUE','1900-01-01 00:00:00.000','1900-01-01 23:59:00.000',0,null,1,0);
INSERT INTO [dbo].[ACS_TIMEZONE_RELATION]([TZ_ID],[TZ_TYPE],[TZ_DAYOFWEEK],[TZ_FROMTIME],[TZ_TOTIME],[TZR_ISDELETED],[TZR_DELETEDDATE],[Period_id],[TZ_Flag])
VALUES(@tzId,'DOW','WED','1900-01-01 00:00:00.000','1900-01-01 23:59:00.000',0,null,1,0);



CREATE TABLE [dbo].[ACS_ACCESSLEVEL](
	[AL_ID] [bigint] IDENTITY(1,1) constraint pk_ACS_ACCESSLEVEL_AL_ID primary key NOT NULL,
	[AL_DESCRIPTION] [nvarchar](50) constraint uq_ACS_ACCESSLEVEL_AL_DESCRIPTION unique not NULL,
	[AL_TIMEZONE_ID] [bigint] constraint fk_ACS_ACCESSLEVEL_AL_TIMEZONE_ID foreign key references ACS_TIMEZONE(TZ_ID),
	[AL_ISDELETED] [bit]  not NULL default 0,
	[AL_DELETEDDATE] [datetime] NULL
)

CREATE TABLE [dbo].[ACS_ACCESSLEVEL_RELATION](
	[AL_ID] [bigint] constraint fk_ACS_ACCESSLEVEL_RELATION_AL_ID foreign key references ACS_ACCESSLEVEL(AL_ID),
	[RD_ZN_ID] int constraint fk_ACS_ACCESSLEVEL_RELATION_RD_ZN_ID foreign key references ACS_READER(RowID),
	[AL_ENTITY_TYPE] [nvarchar](10) NOT NULL,
	[CONTROLLER_ID] int NOT NULL,
	[ALR_ISDELETED] [bit] not NULL default 0,
	[ALR_DELETEDDATE] [datetime] NULL,
	[Action_flag] [int] NULL,
	[AccesLevelArray] [varchar](max) NULL,
	[ZoneId] int constraint fk_ACS_ACCESSLEVEL_RELATION_ZoneId foreign key references ZONE(ZONE_ID)
)


CREATE TABLE [dbo].[EAL_CONFIG](
	[EAL_ID] [bigint] IDENTITY(1,1) constraint pk_EAL_CONFIG_EAL_ID primary key NOT NULL,
	[ENTITY_TYPE] int foreign key references ENT_ORG_COMMON_TYPES(COMMON_TYPES_ID),
	[ENTITY_ID] [nvarchar](15) NULL,	
	[EMPLOYEE_CODE] int constraint fk_EAL_CONFIG_EMPLOYEE_CODE foreign key references ENT_EMPLOYEE_DTLS(EMPLOYEE_ID),
	[CARD_CODE] [nvarchar](15) NULL,
	[AL_ID] [bigint] constraint fk_EAL_CONFIG_AL_ID foreign key references ACS_ACCESSLEVEL(AL_ID),
	[FLAG] [nchar](10) NULL,
	[ISDELETED] [bit] not NULL default 0,
	[DELETEDDATE] [datetime] NULL,
	[CONTROLLER_ID] int NOT NULL,
	[TemplateFlag] [nvarchar](2) NULL,
	[ENT_PARAM_ID] int constraint fk_EAL_CONFIG_ENT_PARAM_ID foreign key references Ent_params(PARAM_ID)
	)

CREATE TABLE [dbo].[ENT_Reader_MessageTemplate](
	[RowID] [int] IDENTITY(1,1) NOT NULL primary key,
	[ControllerID] bigint foreign key references ACS_CONTROLLER(ID),	
	[EventID]  int foreign key references Ent_params(PARAM_ID),
	[EventMessage] [nvarchar](max) NOT NULL,
	[isdeleted] [bit] not NULL DEFAULT 0
	unique(ControllerID,EventID)
	)

	CREATE TABLE [dbo].[ACS_CARD_CONFIG](
	[RowID] [int] IDENTITY(1,1) NOT NULL primary key,
	[CC_EMP_ID]  int foreign key references ENT_EMPLOYEE_DTLS(EMPLOYEE_ID),
	[CARD_CODE] [nvarchar](15) NULL,
	[PIN] [nvarchar](50) NULL,
	[FingureForTA] [nvarchar](50) NULL,
	[USECOUNT] [int] NULL,
	[IGNORE_APB] [bit] NULL,
	[STATUS] [bit] NULL,	
	[ACTIVATION_DATE] [datetime] NULL,	
	[EXPIRY_DATE] [datetime] NULL,
	[ACE_isdeleted] [bit] NULL,
	[ACE_DELETEDDATE] [datetime] NULL,
	[RESET_APB] [bit] NULL,
	[AUTH_MODE] [nvarchar](5) NULL
	)


	create table UNOMVCLOGSHISTORY(Logid int IDENTITY(1,1) PRIMARY KEY,
queryused varchar(MAX),
ipaddress varchar(100),
activeruser int,
dateandtime datetime
)
------------------------------------TRANSACTION DATABASE---------------------------------------
USE MASTER;
IF DB_ID('UNOMVC_TRANSACTION') IS NOT NULL
BEGIN
ALTER DATABASE UNOMVC_TRANSACTION SET  SINGLE_USER WITH ROLLBACK IMMEDIATE
    DROP DATABASE UNOMVC_TRANSACTION
END
create database UNOMVC_TRANSACTION
GO

use UNOMVC_TRANSACTION;

IF OBJECT_ID (N'DBO.ACS_Events', N'U') IS NOT NULL  
DROP TABLE DBO.ACS_Events;


IF OBJECT_ID (N'DBO.TASC', N'U') IS NOT NULL  
DROP TABLE DBO.TASC;


CREATE TABLE [dbo].[ACS_Events](
	[Event_ID] [BIGint] IDENTITY(1,1) PRIMARY KEY,
	[Event_Type] [char](2) NULL,
	[Event_Datetime] [datetime] NULL,
	[Event_DayOfWeek]  AS (datename(weekday,CONVERT([datetime],[Event_Datetime],(103)))),
	[Event_Trace] [int] NULL,
	[Event_Employee_Code] [nvarchar](15) NULL,
	[Event_Card_Code] [nvarchar](15) NULL,
	[Event_Reader_ID] [numeric](18, 0) NULL,
	[Event_Controller_ID] [numeric](18, 0) NULL,
	[Event_Status] [numeric](18, 0) NULL,
	[Event_Alarm_Type] [numeric](18, 0) NULL,
	[Event_Alarm_Action] [char](2) NULL,
	[Event_Access_Point_ID] [char](3) NULL,
	[Event_EventCount] [numeric](18, 0) NULL,
	[EVENT_FLAG] [bit] NULL,
	[TASC_FLAG] [bit] NULL,
	[Event_mode] [nvarchar](50) NULL,	
	[LocationId] [varchar](30) NULL,
	[CenterId] [varchar](30) NULL,
	[COMPANY_ID] [int] --foreign key references ENT_COMPANY(COMPANY_ID),	
	)


	CREATE TABLE [dbo].[TASC](	
	TASC_ID [BIGint] IDENTITY(1,1) constraint pk_TAsc_Tasc_id PRIMARY KEY,
	[TAsc_empcode] [varchar](20) NOT NULL,
	[TAsc_terno] [varchar](4) NULL,
	[TAsc_time] [datetime] NULL,
	[TAsc_date] [datetime] NULL,
	[TAsc_Cocode] [varchar](50) NULL,
	[TAsc_Mode] [varchar](50) NULL,
	[Tasc_flag] [decimal](18, 0) NULL,
	[Tasc_swipe] [varchar](50) NULL,
	[TAsc_Rcode] [varchar](50) NULL,
	[Tsc_RDt] [datetime] NULL,
	[Tasc_ErrorCode] [decimal](18, 0) NULL,
	[Event_Trace] [int] NOT NULL,
	[Event_ID] [bigint] NOT NULL,
	[COMPANY_ID] [int]
	CONSTRAINT Tasc_TAsc_empcode_TAsc_time_Composite UNIQUE(TAsc_empcode,TAsc_time)	
)
CREATE NONCLUSTERED INDEX IX_Tasc_TAsc_empcode   
    ON Tasc (TAsc_empcode); 
	CREATE NONCLUSTERED INDEX IX_Tasc_TAsc_time   
    ON Tasc (TAsc_time); 
	CREATE NONCLUSTERED INDEX IX_Tasc_TAsc_date  
    ON Tasc (TAsc_date); 
	CREATE NONCLUSTERED INDEX IX_Tasc_Tasc_flag 
    ON Tasc (Tasc_flag); 
	CREATE NONCLUSTERED INDEX IX_Tasc_Event_ID
    ON Tasc (Event_ID); 
	CREATE NONCLUSTERED INDEX IX_Tasc_COMPANY_ID
    ON Tasc (COMPANY_ID); 


------------------------

--declare @dt datetime
--set @dt='2015-01-01'

--while(@dt<getdate())
--begin
--print @dt
--set @dt = @dt +1

--INSERT INTO [dbo].[TDAY]
--           ([TDAY_EMPCDE]
--           ,[TDAY_DATE]           
--           ,[TDAY_STATUS_ID] 
--		   )          
--     VALUES
--           (3
--           ,@dt
--           ,1
--           )
--end

------------------------------


--declare @dt datetime
--set @dt='2019-04-30'

--while(@dt<getdate())
--begin
--print @dt
--set @dt = cast(cast(@dt as date) as datetime) +1
--set @dt = @dt + cast(cast(getdate()+Rand() as time) as datetime)
--print @dt
--INSERT INTO [UNOMVC_TRANSACTION].dbo.TASC
--(
--TAsc_empcode,
--TAsc_time,
--TAsc_date,
--TAsc_Mode,
--Tasc_flag,
--Event_Trace,
--Event_ID,
--Company_id
--)
--VALUES
--(
--'abc111',
--@dt,
--cast(@dt as date),
--'N',
--0,
--100,
--100,
--1
--)
--end