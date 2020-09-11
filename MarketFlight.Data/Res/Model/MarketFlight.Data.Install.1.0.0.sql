EXEC('CREATE SCHEMA [MF]');
go;
create type IntList as table ( Number int );
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
    SeatCount int not null,
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
    BuyerId int not null foreign key references MF.tUser(UserId),
    BundleId int not null foreign key references MF.tBundle(BundleId),
    OrderDate datetime2 not null,
    BoughtPrice money not null
);

create table MF.tOrderItem(
    OrderId int not null identity(1,1) primary key,
    TicketOwnerId int not null foreign key references MF.tUser(UserId)
);
go

go
create view MF.vFlight as
    select FlightId as 'FlightId',
        tf.DepartureAirportId, ta1.[Name] as 'DepartureAirportName',
        tf.ArrivalAirportId, ta2.[Name] as 'ArrivalAirportName'
        from MF.tFlight tf
        join MF.tAirport ta1 on tf.DepartureAirportId = ta1.AirportId
        join MF.tAirport ta2 on tf.ArrivalAirportId = ta2.AirportId;
go

go
go
create procedure MF.pCreateOrder
    @BuyerId int,
    @BundleId int,
    @OrderDate datetime2,
	@BoughtPrice money,
	@TicketHolders IntList readonly,
    @OrderId int output
as
begin
	declare @MinCount int;
	select @MinCount = min(f.SeatCount) from MF.tBundleItems bi join MF.tFlights f on bi.FlightId = f.FlightId where bi.BundleId = @BundleId;
	if @MinCount < (select count(*) from IntList)
	begin
		set @OrderId = -1;
		return;
	end
	update f --substract place count from flights.
		set SeatCount = SeatCount - (select count(*) from IntList)
	from MF.tFlights f
		join MF.tBundleItems bi
		on bi.FlightId = f.FlightId where bi.BundleId = @BundleId;
	declare @Output table(OrderId int);
	insert into MF.tOrder(BuyerId, BundleId, OrderDate, BoughtPrice)
    output INSERTED.OrderId into @Output(OrderId)
	values(@BuyerId, @BundleId, @OrderDate, @BoughtPrice);--create the order
	select @OrderId = OrderId from @Output;
	insert into MF.tOrderItem(OrderId, TicketOwnerId) 
	select @OrderId, Number from @TicketHolders;
end
go
create procedure MF.pEnsureUser
    @FirstName nvarchar(255),
    @LastName nvarchar(255),
    @UserId int output
as
begin
    select UserId from MF.tUser where FirstName = @FirstName and LastName = @LastName;
    if @UserId = null
    begin
        declare @Output table(UserId int);
        insert into MF.tUser (FirstName, LastName)
        output INSERTED.UserId into @Output(UserId)
        values(@FirstName, @LastName);
        select @UserId = UserId from @Output;
    end
end
