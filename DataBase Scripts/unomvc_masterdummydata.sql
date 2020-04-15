USE [UNOMVC]
GO

INSERT INTO [dbo].[ENT_COMPANY]
           ([COMPANY_CODE]
           ,[COMPANY_NAME]
           ,[COMPANY_CREATEDDATE]
           ,[COMPANY_CREATEDBY]
           ,[COMPANY_MODIFIEDDATE]
           ,[COMPANY_MODIFIEDBY]
           ,[COMPANY_ISDELETED]
           ,[COMPANY_DELETEDDATE]
           ,[COMPANY_DELETEDBY]
           ,[IS_SYNC])
     VALUES
           ('CMSCOMP'
           ,'CMS COMPUTERS LTD'
           ,null
           ,null
           ,null
           ,null
           ,0
           ,null
           ,null
           ,0)
GO

------------------------------------------

insert into ENT_ORG_COMMON_ENTITIES(
COMMON_TYPES_ID,
OCE_ID,
OCE_DESCRIPTION,
COMPANY_ID) 
values(4,'CAT1','OfficeStaff',1)