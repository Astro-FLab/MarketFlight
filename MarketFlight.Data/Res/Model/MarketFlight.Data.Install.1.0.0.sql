IF NOT EXISTS ( select  *
                from    sys.schemas
                where   name = 'MF' )
begin
    exec ('create schema MF')
end
go
create table MF.tUser(
    UserId int not null identity(1,1) primary key,
    FirstName nvarchar(255) not null,
    LastName nvarchar(255) not null
);

create table MF.tAirport(
    AirportId int not null identity(1,1) primary key,
    [Name] nvarchar(255) not null unique
);

create table MF.tFlight(
    FlightID int not null identity(1,1) primary key,
    DepartureAirport int not null foreign key references MF.tAirport(AirportId),
    ArrivalAirport int not null foreign key references MF.tAirport(AirportId)
);

create table MF.tOrder(
    OrderId int not null identity(1,1) primary key,
    UserId int not null foreign key references MF.tUser(UserId),
    OrderDate datetime2 not null,
    SeatCount int not null
);


