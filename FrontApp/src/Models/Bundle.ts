import { Entity } from '@decahedron/entity';
import type { Flight } from '.';

export class Bundle extends Entity {
    // We instantiate with null to ensure the property exists
    // at the time of hydration.
    public bundleId: number = null;
    public price: number = null;
    public flights: Flight[] = null;
}