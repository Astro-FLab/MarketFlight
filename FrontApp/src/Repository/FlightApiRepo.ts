
import { Entity, EntityBuilder } from '@decahedron/entity';
import { Flight } from '../Models';
import apiHelper from '../Helpers/apiHelper';

let endpoint: string;
let helper: apiHelper;

export default class FlightApiRepo {
    constructor() {
        helper = new apiHelper();
        endpoint = `http://localhost:800/marketflight/flights`;
    }


    async GetAllFlights(): Promise<Flight[]> {
        let res = await helper.getAsync(`${endpoint}`);
        return EntityBuilder.buildMany<Flight>(Flight, res);
    }

    async CreateFlight(model: Flight) {
        return await helper.postAsync(`${endpoint}`, { DepartureAirportId: model.departureAirportId, ArrivalAirportId: model.arrivalAirportId, TotalSeatCount: 50 }).then(jsonData => EntityBuilder.buildOne<Flight>(Flight, jsonData));
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
