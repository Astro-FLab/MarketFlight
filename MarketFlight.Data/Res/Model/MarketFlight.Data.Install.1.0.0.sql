IF NOT EXISTS ( select  *
                from    sys.schemas
                where   name = 'MF' )
begin
    exec ('create schema MF')
end
go
drop table if exists MF.tOrder;
drop table if exists MF.tFlight;
drop table if exists MF.tAirport;
drop table if exists MF.tUser;
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
    FlightId int not null identity(1,1) primary key,
    DepartureAirport int not null foreign key references MF.tAirport(AirportId),
    ArrivalAirport int not null foreign key references MF.tAirport(AirportId),
    TotalSeatCout int not null
);

create table MF.tOrder(
    OrderId int not null identity(1,1) primary key,
    UserId int not null foreign key references MF.tUser(UserId),
    FlightId int not null foreign key references MF.tFlight(FlightId),
    OrderDate datetime2 not null,
    SeatCount int not null
);
go
drop view if exists MF.vFlight;
go;
create view MF.vFlight as
    select FlightId,
        tf.DepartureAirport as 'DepartureAirportId', ta1.[Name] as 'DepartureAirportName',
        tf.ArrivalAirport as 'ArrivalAirportId', ta2.[Name] as 'ArrivalAirportName'
        from MF.tFlight tf
        join MF.tAirport ta1 on tf.DepartureAirport = ta1.AirportId
        join MF.tAirport ta2 on tf.ArrivalAirport = ta2.AirportId;
go;
drop view if exists MF.vOrder;
go;
create view MF.vOrder as
    select * from MF.tOrder tor join MF.vFlight tf on tor.FlightId = tf.FlightId;
go;
drop procedure if exists MF.pCreateOrder;
go
create procedure MF.pCreateOrder
    @UserId int not null,
    @FlightId int not null,
    @OrderDate datetime2 not null,
    @SeatCount int not null,
    @OrderId int not null output
as
begin
    declare @TotalSeatCount int;
    select @TotalSeatCount = TotalSeatCount from MF.tFlight where FlightId = @FlightId;
    declare @CurrentTakenSeat int;
    select @CurrentTakenSeat = sum(SeatCount) from MF.tOrder where FlightId = @FlightId;
    declare @RemainingSeatCount int = @TotalSeatCount - @CurrentTakenSeat;
    if @RemainingSeatCount < @SeatCount
    begin
        set @OrderId = -1;
        return;
    end
    insert into MF.tOrder (UserId, FlightId, OrderDate, SeatCount)
    output INSERTED.OrderId into @OrderId
    values(@UserId, @FlightId, @OrderDate, @SeatCount)
end
go;
