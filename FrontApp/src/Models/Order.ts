import { Entity } from '@decahedron/entity';

export class Order extends Entity {
    // We instantiate with null to ensure the property exists
    // at the time of hydration.
    public orderId: number = null;
    public userId: number = null;
    public orderDate: Date = null;
    public seatCount: number = null;
    public flightId: number = null;
    public departureAirportId: number = null;
    public departureAirportName: string = null;
    public arrivalAirportName: string = null;
}