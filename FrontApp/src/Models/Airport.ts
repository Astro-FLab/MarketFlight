import { Entity } from '@decahedron/entity';

class Airport extends Entity {
    // We instantiate with null to ensure the property exists
    // at the time of hydration.
    public AirportId: number = null;
    public Name: string = null;
}