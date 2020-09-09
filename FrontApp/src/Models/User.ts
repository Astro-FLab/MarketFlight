import { Entity } from '@decahedron/entity';

export class User extends Entity {
    // We instantiate with null to ensure the property exists
    // at the time of hydration.
    public UserId: number = null;
    public FirstName: string = null;
    public LastName: string = null;
}