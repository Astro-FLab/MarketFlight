
import { EntityBuilder } from '@decahedron/entity';
import { Order } from '../Models';
import apiHelper from '../Helpers/apiHelper';

let endpoint: string;
let helper: apiHelper;

export default class OrdersApiRepo {
    constructor() {
        helper = new apiHelper();
        endpoint = `http://marketflight.kuinox.io/marketflight/orders`;
    }

    async GetAllOrders() {
        return await helper.getAsync(`${endpoint}`).then(jsonData => EntityBuilder.buildOne<Order>(Order, jsonData));
    }

    async GetOrder(orderId: number) {
        return await helper.getAsync(`${endpoint}/${orderId}`).then(jsonData => EntityBuilder.buildOne<Order>(Order, jsonData));
    }

    async CreateOrder(model: Order): Promise<Order> {
        return await helper.postAsync(`${endpoint}`, { UserId: model.userId, FlightId: model.flightId, OrderDate: model.orderDate, SeatCount: model.seatCount }).then(jsonData => EntityBuilder.buildOne<Order>(Order, jsonData));
    }

    async UpdateOrder(orderId: number, model: Order) {
        return await helper.putAsync(`${endpoint}/${orderId}`, model).then(jsonData => EntityBuilder.buildOne<Order>(Order, jsonData));
    }

    async RemoveOrder(orderId: number) {
        return await helper.deleteAsync(`${endpoint}/${orderId}`).then(jsonData => EntityBuilder.buildOne<Order>(Order, jsonData));
    }
}
