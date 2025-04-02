const API_DOMAIN = "https://localhost:7016/api";

const ApiService = {
    async get(endpoint, params = {}, headers = {}) {
        const url = new URL(`${API_DOMAIN}/${endpoint}`);
        Object.keys(params).forEach((key) =>
            url.searchParams.append(key, params[key])
        );

        const finalUrl = decodeURIComponent(url.toString());
        console.log(finalUrl);

        return $.ajax({
            url: finalUrl,
            method: "GET",
            dataType: "json",
            headers: {
                Authorization: `Bearer ${localStorage.getItem("jwtToken")}`,
                ...headers,
            },
        }).fail((jqXHR, textStatus, errorThrown) => {
            console.error(`GET ${endpoint} failed:`, textStatus, errorThrown);
        });
    },

    async post(endpoint, data, headers = {}) {
        return new Promise((resolve, reject) => {
            $.ajax({
                url: `${API_DOMAIN}/${endpoint}`,
                method: "POST",
                contentType: "application/json",
                data: JSON.stringify(data),
                dataType: "json",
                headers: {
                    Authorization: `Bearer ${localStorage.getItem("jwtToken")}`,
                    ...headers,
                },
                success: function (response) {
                    resolve(response);
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    console.error(`POST ${endpoint} failed:`, jqXHR.status, errorThrown);
                    reject({ status: jqXHR.status, message: jqXHR.responseText });
                },
            });
        });
    },

    async put(endpoint, data, headers = {}) {
        return $.ajax({
            url: `${API_DOMAIN}/${endpoint}`,
            method: "PUT",
            contentType: "application/json",
            data: JSON.stringify(data),
            dataType: "json",
            headers: {
                Authorization: `Bearer ${localStorage.getItem("jwtToken")}`,
                ...headers,
            },
        }).fail((jqXHR, textStatus, errorThrown) => {
            console.error(`PUT ${endpoint} failed:`, textStatus, errorThrown);
        });
    },

    async delete(endpoint, id, headers = {}) {
        return $.ajax({
            url: `${API_DOMAIN}/${endpoint}/${id}`,
            method: "DELETE",
            dataType: "json",
            headers: {
                Authorization: `Bearer ${localStorage.getItem("jwtToken")}`,
                ...headers,
            },
        }).fail((jqXHR, textStatus, errorThrown) => {
            console.error(`DELETE ${endpoint} failed:`, textStatus, errorThrown);
        });
    },
};

export default ApiService;
