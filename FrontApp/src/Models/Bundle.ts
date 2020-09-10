import { Entity } from '@decahedron/entity';
import type { Flight } from '.';

export class Bundle extends Entity {
    // We instantiate with null to ensure the property exists
    // at the time of hydration.
    public BundleId: number = null;
    public Price: number = null;
    public Flights: Flight[] = null;
}