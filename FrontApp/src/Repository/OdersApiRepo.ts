
import { Entity, EntityBuilder } from '@decahedron/entity';
import { Order } from '../Models';
import apiHelper from '../Helpers/apiHelper';

let endpoint: string;
let helper: apiHelper;


class OrdersApiRepo {
    constructor() {
        helper = new apiHelper();
        endpoint = `http://localhost:5000/marketflight/orders`;
    }

    async GetAllOrders() {
        return await helper.getAsync(`${endpoint}`).then(jsonData => EntityBuilder.buildOne<Order>(Order, jsonData));
    }

    async GetOrder(orderId) {
        return await helper.getAsync(`${endpoint}/${orderId}`).then(jsonData => EntityBuilder.buildOne<Order>(Order, jsonData));
    }

    async CreateOrder(model) {
        return await helper.postAsync(`${endpoint}`, model).then(jsonData => EntityBuilder.buildOne<Order>(Order, jsonData));
    }

    async UpdateOrder(orderId, model) {
        return await helper.putAsync(`${endpoint}/${orderId}`, model).then(jsonData => EntityBuilder.buildOne<Order>(Order, jsonData));
    }

    async RemoveOrder(orderId) {
        return await helper.deleteAsync(`${endpoint}/${orderId}`).then(jsonData => EntityBuilder.buildOne<Order>(Order, jsonData));
    }
}

export default new OrdersApiRepo();
