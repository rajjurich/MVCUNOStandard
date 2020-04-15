alter procedure USP_GETCommonDetails
@ActionCommand varchar(200)=''
as
if(@ActionCommand='CAT')
select * from ENT_ORG_COMMON_ENTITIES where COMMON_TYPES_ID=4
