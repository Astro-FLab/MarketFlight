import { postAsync, getAsync } from "../helpers/apiHelper";
const endpoint = "/marketflight/users";

class UserApiService {
    constructor() {
    }

    async CreateUser(model) {
        return await postAsync(`${endpoint}`, model);
    }

    async GetAllUsers() {
        return await getAsync(`${endpoint}`);
    }

    async GetUser(userId) {
        return await getAsync(`${endpoint}/${userId}`);
    }

    async UpdateUser(userId) {
        return await putAsync(`${endpoint}/${userId}`);
    }

    async RemoveUser(userId) {
        return await deleteAsync(`${endpoint}/${userId}`);
    }
}

export default new UserApiService();