import { Entity, Type } from '@decahedron/entity';
import { Flight } from '.';

export class Bundle extends Entity {
    // We instantiate with null to ensure the property exists
    // at the time of hydration.
    public bundleId: number = null;
    public price: number = null;
    @Type(Flight)
    public flights: Flight[] = null;
}