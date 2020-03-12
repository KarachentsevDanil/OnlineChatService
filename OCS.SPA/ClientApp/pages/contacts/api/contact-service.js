import * as httpService from '../../../api/http-service'

const apiUserContacts = "/api/UserContact/";
const apiChats = "/api/PrivateChat/";

export const getContacts = () => {
    let params = {
        url: apiUserContacts
    }

    return httpService.getData(params);
}

export const getMessages = (data) => {
    let params = {
        url: apiChats + "messages",
        data: data
    }

    return httpService.getData(params);
}

export const getChats = () => {
    let params = {
        url: apiChats
    }

    return httpService.getData(params);
}