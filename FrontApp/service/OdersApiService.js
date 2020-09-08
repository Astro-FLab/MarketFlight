import {
    postAsync,
    getAsync
} from "../helpers/apiHelper";

const endpoint = "/marketflight/orders";

class OrdersApiService {
    constructor() {}

    async GetAllOrders() {
        return await getAsync(`${endpoint}`);
    }

    async GetOrder(orderId) {
        return await getAsync(`${endpoint}/${orderId}`);
    }

    async CreateOrder(model) {
        return await postAsync(`${endpoint}`, model);
    }

    async UpdateOrder(orderId, model) {
        return await putAsync(`${endpoint}/${orderId}`, model);
    }

    async RemoveOrder(orderId) {
        return await deleteAsync(`${endpoint}/${orderId}`);
    }
}

export default new OrdersApiService();
