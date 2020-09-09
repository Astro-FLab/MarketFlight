import { Entity } from '@decahedron/entity';

export class Airport extends Entity {
    // We instantiate with null to ensure the property exists
    // at the time of hydration.
    public airportId: number = null;
    public name: string = null;
}