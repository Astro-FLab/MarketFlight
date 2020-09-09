import { Entity } from '@decahedron/entity';

export class Order extends Entity {
    // We instantiate with null to ensure the property exists
    // at the time of hydration.
    public OrderId: number = null;
    public UserId: number = null;
    public OrderDate: Date = null;
    public SeatCount: number = null;
    public FlightId: number = null;
    public DepartureAirportId: number = null;
    public DepartureAirportName: string = null;
    public ArrivalAirportName: string = null;
}