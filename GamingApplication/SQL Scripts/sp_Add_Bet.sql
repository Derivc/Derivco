USE [CFDProposalReview]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE object_id = OBJECT_ID(N'dbo.sp_Add_Bet'))
BEGIN
	DROP PROCEDURE [dbo].[sp_Add_Bet]
END
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_Add_Bet]
	 @rou_rolled VARCHAR(100),  
	 @money INT,
	 @player VARCHAR(100)
WITH EXEC AS CALLER
AS
BEGIN
 
		INSERT INTO Placebet(rou_rolled, [money], player, CreateDate)
		VALUES(@rou_rolled, @money, @player, CURRENT_TIMESTAMP)
END  




