set nocount on

SELECT rtrim(ltrim(Event_Employee_Code))as Event_Employee_Code
,Event_Datetime AS EVENT_TIME
,cast(cast(Event_Datetime as date) as datetime) As EVENT_DATE

,event_mode
,Event_Trace
,Event_ID
,COMPANY_ID

   INTO #TEMP
	FROM [UNOMVC_TRANSACTION].dbo.ACS_EVENTS WHERE TASC_FLAG=0 

	INSERT INTO [UNOMVC_TRANSACTION].dbo.TASC
	(TAsc_empcode,TAsc_time,TAsc_date,TAsc_Mode,Tasc_flag,Event_Trace,Event_ID,COMPANY_ID) 
	SELECT Event_Employee_Code,EVENT_TIME,EVENT_DATE,event_mode,0,Event_Trace,Event_ID,COMPANY_ID  FROM #TEMP


    UPDATE [UNOMVC_TRANSACTION].dbo.ACS_EVENTS
	SET TASC_FLAG=1
	WHERE Event_ID IN (SELECT Event_ID FROM #TEMP)


    DROP TABLE #TEMP

	select @@rowcount