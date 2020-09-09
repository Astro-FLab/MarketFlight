
import { Entity, EntityBuilder } from '@decahedron/entity';
import { User } from '../Models';
import apiHelper from '../Helpers/apiHelper';

let endpoint: string;
let helper: apiHelper;

class UserApiRepo {
    constructor() {
        helper = new apiHelper();
        endpoint = `http://localhost:800/marketflight/users`;
    }

    async CreateUser(model) {
        return await helper.postAsync(`${endpoint}`, model);
    }

    async GetAllUsers() {
        return await helper.getAsync(`${endpoint}`).then(jsonData => EntityBuilder.buildOne<User>(User, jsonData));
    }

    async GetUser(userId) {
        return await helper.getAsync(`${endpoint}/${userId}`).then(jsonData => EntityBuilder.buildOne<User>(User, jsonData));
    }

    async UpdateUser(userId, model) {
        return await helper.putAsync(`${endpoint}/${userId}`, model).then(jsonData => EntityBuilder.buildOne<User>(User, jsonData));
    }

    async RemoveUser(userId) {
        return await helper.deleteAsync(`${endpoint}/${userId}`).then(jsonData => EntityBuilder.buildOne<User>(User, jsonData));
    }

    async GetUserFlights(userId) {
        return await helper.getAsync(`${endpoint}/${userId}/flights`).then(jsonData => EntityBuilder.buildOne<User>(User, jsonData));
    }
}

export default new UserApiRepo();
