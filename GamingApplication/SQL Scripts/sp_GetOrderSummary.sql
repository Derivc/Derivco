USE [CFDProposalReview]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE object_id = OBJECT_ID(N'dbo.sp_GetOrderSummary'))
BEGIN
	DROP PROCEDURE [dbo].[sp_GetOrderSummary]
END
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_GetOrderSummary]
	 @start_date DATETIME,  
	 @end_date DATETIME,
	 @EmployeeID VARCHAR(100),
	 @CustomerID VARCHAR(100)
WITH EXEC AS CALLER
AS
BEGIN
 
		SELECT CONCAT(a.TitleOfCourtesy, '.', a.FirstName, ' ',a.LastName) fullName, a.shp_company_name, a.cust_company_name, a.order_nos, a.[date], a.tot_frieght_cost, a.number_different_products, a.ord_val
		FROM NorthWind a
		WHERE (@CustomerID IS NULL OR a.cust_id = @CustomerID)
		AND (@EmployeeID IS NULL OR a.emp_id = @EmployeeID)  
		AND a.CreateDate BETWEEN @start_date AND @end_date  
		GROUP BY a.[date], a.emp_id, a.cust_id, a.shp_company_name, a.TitleOfCourtesy, a.FirstName, a.LastName, a.cust_company_name, a.order_nos, a.tot_frieght_cost, a.number_different_products, a.ord_val
END  



