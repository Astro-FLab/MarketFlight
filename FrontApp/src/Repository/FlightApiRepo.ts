
import { Entity, EntityBuilder } from '@decahedron/entity';
import { Flight } from '../Models';
import apiHelper from '../Helpers/apiHelper';

let endpoint: string;
let helper: apiHelper;

export default class FlightApiRepo {
    constructor() {
        helper = new apiHelper();
        endpoint = `http://localhost:5000/marketflight/flights`;
    }


    async GetAllFlights() {
        return await helper.getAsync(`${endpoint}`).then(jsonData => EntityBuilder.buildOne<Flight>(Flight, jsonData));
    }

    async CreateFlight(model) {
        return await helper.postAsync(`${endpoint}`, model).then(jsonData => EntityBuilder.buildOne<Flight>(Flight, jsonData));
    }

    async GetFlight(flightId) {
        return await helper.getAsync(`${endpoint}/${flightId}`).then(jsonData => EntityBuilder.buildOne<Flight>(Flight, jsonData));
    }

    async UpdateFlight(flightId, model) {
        return await helper.putAsync(`${endpoint}/${flightId}`, model).then(jsonData => EntityBuilder.buildOne<Flight>(Flight, jsonData));
    }

    async RemoveFlight(flightId) {
        return await helper.deleteAsync(`${endpoint}/${flightId}`).then(jsonData => EntityBuilder.buildOne<Flight>(Flight, jsonData));
    }
}
