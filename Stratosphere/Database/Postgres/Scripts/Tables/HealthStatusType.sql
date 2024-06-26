/*
	postgres inserts

	description: 
	simple table to hold status id's and their definitions
*/

create table if not exists Stratosphere.HealthStatusType (
	HealthStatusTypeId uuid not null,
	CreatedBy varchar(255) not null,
	CreatedDate timestamp not null,
	ModifiedBy varchar(255) null,
	ModifiedDate timestamp null,
	Name varchar(255) not null,
	Description varchar(1000) not null,
	primary key (HealthStatusTypeId)
);