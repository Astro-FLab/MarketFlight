
import { EntityBuilder } from '@decahedron/entity';
import { User, Order } from '../Models';
import apiHelper from '../Helpers/apiHelper';

let endpoint: string;
let helper: apiHelper;

export default class UserApiRepo {
    constructor() {
        helper = new apiHelper();
        endpoint = `http://localhost:800/marketflight/users`;
    }

    async CreateUser(model: User): Promise<User> {
        return await helper.postAsync(`${endpoint}`, model);
    }

    async CreateUserIfNotExist(model: Partial<User>): Promise<number> {
        return await helper.postInQueryAsync(`${endpoint}/createUserIfNotExist`, "firstName=" + model.FirstName + "&lastName=" + model.LastName);
    }

    async GetAllUsers(): Promise<User[]> {
        return await helper.getAsync(`${endpoint}`).then(jsonData => EntityBuilder.buildMany<User>(User, jsonData));
    }

    async GetUser(userId: number) {
        return await helper.getAsync(`${endpoint}/${userId}`).then(jsonData => EntityBuilder.buildOne<User>(User, jsonData));
    }

    async UpdateUser(userId: number, model: User) {
        return await helper.putAsync(`${endpoint}/${userId}`, model).then(jsonData => EntityBuilder.buildOne<User>(User, jsonData));
    }

    async RemoveUser(userId: number) {
        return await helper.deleteAsync(`${endpoint}/${userId}`).then(jsonData => EntityBuilder.buildOne<User>(User, jsonData));
    }

    async GetUserOrders(userId: number): Promise<Order[]> {
        return await helper.getAsync(`${endpoint}/${userId}/orders`).then(jsonData => EntityBuilder.buildMany<Order>(Order, jsonData));
    }
}
