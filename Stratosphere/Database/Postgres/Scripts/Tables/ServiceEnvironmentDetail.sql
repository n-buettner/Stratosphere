/*
	postgres inserts

	description: 
	helper table to store the settings for a given service by environment. used to support how health checks are treated as example as each env could
	have a different setting value that qualifies it for specific alert details.
*/

create table if not exists Stratosphere.ServiceEnvironmentDetail (
	ServiceId int not null,
	EnvironmentId int not null,
	AutomaticRestartEligible boolean not null default false,
	MinimumHealthStatusTypeIdForAction int not null,
	ConsecutiveFailuresBeforeAlert int not null default 10,
	ConsecutiveFailuresBeforeRestart int not null default 20,
	CreatedBy varchar(255) not null,
	CreatedDate timestamp not null,
	ModifiedBy varchar(255) null,
	ModifiedDate timestamp null,
	primary key (ServiceId, EnvironmentId),
	foreign key (ServiceId) references Stratosphere.Service(ServiceId),
	foreign key (EnvironmentId) references Stratosphere.Environment(EnvironmentId),
	foreign key (MinimumHealthStatusTypeIdForAction) references Stratosphere.HealthStatusType(HealthStatusTypeId)
);