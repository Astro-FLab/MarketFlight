import {
    postAsync,
    getAsync,
    putAsync,
    deleteAsync
} from "../helpers/apiHelper";

const endpoint = "/marketflight/flights";

class FlightApiService {
    constructor() {}

    async GetAllFlights() {
        return await getAsync(`${endpoint}`);
    }

    async CreateFlight(model) {
        return await postAsync(`${endpoint}`, model);
    }

    async GetFlight(flightId) {
        return await getAsync(`${endpoint}/${flightId}`);
    }

    async UpdateFlight(flightId) {
        return await putAsync(`${endpoint}/${flightId}`);
    }

    async RemoveFlight(flightId) {
        return await deleteAsync(`${endpoint}/${flightId}`);
    }
}

export default new FlightApiService();
