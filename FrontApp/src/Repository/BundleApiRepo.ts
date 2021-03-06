
import { EntityBuilder } from '@decahedron/entity';
import { Bundle } from '../Models';
import apiHelper from '../Helpers/apiHelper';

let endpoint: string;
let helper: apiHelper;

export default class BundleApiRepo {
    constructor() {
        helper = new apiHelper();
        endpoint = `http://marketflight.kuinox.io/marketflight/bundles`;
    }

    async GetAll(): Promise<Bundle[]> {
        return await helper.getAsync(`${endpoint}`).then(jsonData => EntityBuilder.buildManyAsync<Bundle>(Bundle, jsonData));
    }

    async GetBundle(bundleId: number): Promise<Bundle> {
        return await helper.getAsync(`${endpoint}/${bundleId}`).then(jsonData => EntityBuilder.buildOne<Bundle>(Bundle, jsonData));
    }

    async CreateBundle(price: number): Promise<Bundle> {
        return await helper.postAsync(`${endpoint}`, price).then(jsonData => EntityBuilder.buildOne<Bundle>(Bundle, jsonData));
    }

    async AddItem(bundleId: number, flightId: number): Promise<Bundle> {
        return await helper.postAsync(`${endpoint}/${bundleId}`, flightId).then(jsonData => EntityBuilder.buildOne<Bundle>(Bundle, jsonData));
    }

}
