import { Entity } from '@decahedron/entity';

export class Flight extends Entity {
    // We instantiate with null to ensure the property exists
    // at the time of hydration.
    public flightId: number = null;
    public departureAirportId: number = null;
    public departureAirportName: string = null;
    public arrivalAirportId: number = null;
    public arrivalAirportName: string = null;
}