
ALTER TABLE ent_company  DROP UQ__ENT_COMP__BD25F02B4F82BDB0;

alter table ent_company add constraint ENT_COMPANY_COMPANY_CODE_COMPANY_ISDELETED_Composite unique(COMPANY_CODE,COMPANY_ISDELETED);





alter table ent_org_common_entities add constraint 
OCE_ID_CommonType_COMPANY_ID_Composite 
unique(OCE_ID, COMMON_TYPES_ID, COMPANY_ID,OCE_ISDELETED);