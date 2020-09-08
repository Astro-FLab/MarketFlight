import { Entity } from '@decahedron/entity';

class Flight extends Entity {
    // We instantiate with null to ensure the property exists
    // at the time of hydration.
    public FlightId: number = null;
    public DepartureAirportId: number = null;
    public DepartureAirportName: string = null;
    public ArrivalAirportId: number = null;
    public ArrivalAirportName: string = null;
}