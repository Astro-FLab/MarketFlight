IF NOT EXISTS ( select  *
                from    sys.schemas
                where   name = 'MF' )
begin
    exec ('create schema MF')
end
go
drop table if exists MF.tUser;
create table MF.tUser(
    UserId int not null identity(1,1) primary key,
    FirstName nvarchar(255) not null,
    LastName nvarchar(255) not null
);

drop table if exists MF.tAirport;
create table MF.tAirport(
    AirportId int not null identity(1,1) primary key,
    [Name] nvarchar(255) not null unique
);

drop table if exists MF.tFlight;
create table MF.tFlight(
    FlightId int not null identity(1,1) primary key,
    DepartureAirport int not null foreign key references MF.tAirport(AirportId),
    ArrivalAirport int not null foreign key references MF.tAirport(AirportId)
);

drop table if exists MF.tOrder;
create table MF.tOrder(
    OrderId int not null identity(1,1) primary key,
    UserId int not null foreign key references MF.tUser(UserId),
    OrderDate datetime2 not null,
    SeatCount int not null
);


