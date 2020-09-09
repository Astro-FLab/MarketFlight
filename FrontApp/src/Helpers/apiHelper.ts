export default class apiHelper {
    async checkErrors(resp) {
        if (resp.ok) return resp;

        let errorMsg = `ERROR ${resp.status} (${resp.statusText})`;
        let serverText = await resp.text();
        if (serverText) errorMsg = `${errorMsg}: ${serverText}`;

        var error = new Error(errorMsg);
        throw error;
    }

    async toJSON(resp) {
        const result = await resp.text();
        if (result) return JSON.parse(result);
    }

    async postAsync(url, data) {
        return await fetch(url, {
            method: 'POST',
            body: JSON.stringify(data),
            headers: {
                'Content-Type': 'application/json'
            }
        })
            .then(this.checkErrors);
    }

    async postInQueryAsync(url, params) {
        return await fetch(url + '?' + new URLSearchParams(params), {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            }
        })
            .then(this.checkErrors);
    }

    async putAsync(url, data) {
        return await fetch(url, {
            method: 'PUT',
            body: JSON.stringify(data),
            headers: {
                'Content-Type': 'application/json'
            }
        })
            .then(this.checkErrors)
            .then(this.toJSON);
    }

    public async getAsync(url) {
        return await fetch(url, {
            method: 'GET'
        })
            .then(this.checkErrors)
            .then(this.toJSON);
    }

    async deleteAsync(url) {
        return await fetch(url, {
            method: 'DELETE'
        })
            .then(this.checkErrors)
            .then(this.toJSON);
    }
}
