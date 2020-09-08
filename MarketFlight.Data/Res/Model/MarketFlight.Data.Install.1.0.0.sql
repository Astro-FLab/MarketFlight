IF NOT EXISTS ( select  *
                from    sys.schemas
                where   name = 'MF' )
begin
    exec ('create schema MF')
end
go
drop table if exists MF.tFlightToSell;
drop table if exists MF.tBundleItems;
drop table if exists MF.tOrder;
drop table if exists MF.tBundle;
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
    TotalSeatCount int not null,
    DepartureAirportId int not null foreign key references MF.tAirport(AirportId),
    ArrivalAirportId int not null foreign key references MF.tAirport(AirportId),
);

create table MF.tBundle(
    BundleId int not null identity(1,1) primary key,
    Price money not null
);

create table MF.tBundleItems(
    BundleId int not null foreign key references MF.tBundle(BundleId),
    FlightId int not null foreign key references MF.tFlight(FlightId)
);

create table MF.tOrder(
    OrderId int not null identity(1,1) primary key,
    UserId int not null foreign key references MF.tUser(UserId),
    BundleId int not null foreign key references MF.tBundle(BundleId),
    OrderDate datetime2 not null,
    BoughtPrice money not null
);
go
drop view if exists MF.vFlight;
go
create view MF.vFlight as
    select FlightId as 'FlightId',
        tf.DepartureAirportId, ta1.[Name] as 'DepartureAirportName',
        tf.ArrivalAirportId, ta2.[Name] as 'ArrivalAirportName'
        from MF.tFlight tf
        join MF.tAirport ta1 on tf.DepartureAirportId = ta1.AirportId
        join MF.tAirport ta2 on tf.ArrivalAirportId = ta2.AirportId;
go
drop view if exists MF.vOrder;
go
drop procedure if exists MF.pCreateOrder;
go
--create procedure MF.pCreateOrder
--    @UserId int,
--    @FlightId int,
--    @OrderDate datetime2,
--    @SeatCount int,
--    @OrderId int output
--as
--begin
--    declare @TotalSeatCount int;
--    select @TotalSeatCount = TotalSeatCount from MF.tFlight f where f.FlightId = @FlightId;
--    declare @CurrentTakenSeat int;
--    select @CurrentTakenSeat = sum(SeatCount) from MF.tOrder f where f.FlightId = @FlightId;
--    declare @RemainingSeatCount int = @TotalSeatCount - @CurrentTakenSeat;
--    if @RemainingSeatCount < @SeatCount
--    begin
--        set @OrderId = -1;
--        return;
--    end
--	declare @Output table(OrderId int);
--    insert into MF.tOrder (UserId, FlightId, OrderDate, SeatCount)
--    output INSERTED.OrderId into @Output(OrderId)
--    values(@UserId, @FlightId, @OrderDate, @SeatCount)
--	select @OrderId = OrderId from @Output;
--end
go
