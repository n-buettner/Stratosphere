/*
	sqlserver inserts

	description: 
	Holds historical information related to alerts raised for a long term log. Useful for tracking and reporting.
*/

create table [Stratosphere].[AlertHistory] (
	[CreatedBy] varchar(255) not null,
	[CreatedDate] datetime2(7) not null,
	[ModifiedBy] varchar(255) null,
	[ModifiedDate] datetime2(7) null
);