import { EntityBuilder } from '@decahedron/entity';
import { Airport } from '../Models';
import apiHelper from '../Helpers/apiHelper';

let endpoint: string;
let helper: apiHelper;

export default class AirportsApiRepo {
    constructor() {
        helper = new apiHelper();
        endpoint = `http://localhost:800/marketflight/airports`;
    }

    async GetAllAirPorts() {
        return await helper.getAsync(`${endpoint}/getAllAirports`).then(jsonData => EntityBuilder.buildMany<Airport>(Airport, jsonData));
    }

    async CreateAirport(model: Airport) {
        return await helper.postAsync(`${endpoint}`, model).then(jsonData => EntityBuilder.buildOne<Airport>(Airport, jsonData));
    }

    async GetAirportById(airportId: number) {
        return await helper.getAsync(`${endpoint}/${airportId}`).then(jsonData => EntityBuilder.buildOne<Airport>(Airport, jsonData));
    }

    async GetAirportByName(airportName: string): Promise<Airport> {
        return await helper.getAsync(`${endpoint}/${airportName}/byName`).then(jsonData => EntityBuilder.buildOne<Airport>(Airport, jsonData));
    }

    async UpdateAirport(airportId: number, model: Airport) {
        return await helper.putAsync(`${endpoint}/${airportId}`, model).then(jsonData => EntityBuilder.buildOne<Airport>(Airport, jsonData));
    }

    async RemoveAirport(airportId: number) {
        return await helper.deleteAsync(`${endpoint}/${airportId}`).then(jsonData => EntityBuilder.buildOne<Airport>(Airport, jsonData));
    }
}
