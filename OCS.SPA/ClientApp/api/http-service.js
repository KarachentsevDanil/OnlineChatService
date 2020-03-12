import axios from "axios";
import router from "../router";

const baseApiUrl = "https://localhost:5005";

axios.interceptors.response.use(
    response => {
        return response;
    },
    error => {
        if (error.response.status === 401) {
            router.push("/login");
        }

        return error;
    }
);

let getHeaders = () => {
    let token = localStorage.getItem("token");
    let headers = {};

    if (token) {
        headers = { Authorization: `Bearer ${localStorage.token}` };
    }

    return headers;
};

export const getData = params => {
    const headers = getHeaders();

    return axios.get(baseApiUrl + params.url, {
        headers: headers,
        params: params.data
    });
};

export const postData = params => {
    const headers = getHeaders();
    
    return axios.post(baseApiUrl + params.url, params.data, {
        headers: headers
    });
};

export const putData = params => {
    const headers = getHeaders();

    return axios.put(baseApiUrl + params.url, params.data, {
        headers: headers
    });
};

export const deleteData = params => {
    const headers = getHeaders();

    return axios.delete(baseApiUrl + params.url, {
        headers: headers,
        params: params.data
    });
};
