/*
	postgres inserts

	description: 
	reference table to store queue feeds created by the referenced cloudamqp service. 
*/

create table if not exists Stratosphere.Queue (
	QueueId uuid not null,
	CreatedBy varchar(255) not null,
	CreatedDate timestamp not null,
	ModifiedBy varchar(255) null,
	ModifiedDate timestamp null,
	QueueName varchar(255) not null,
	VirtualHostId uuid not null,
	EnvironmentId uuid not null,
	primary key (QueueId),
	foreign key (VirtualHostId) references Stratosphere.VirtualHost(VirtualHostId),
	foreign key (EnvironmentId) references Stratosphere.Environment(EnvironmentId)
);