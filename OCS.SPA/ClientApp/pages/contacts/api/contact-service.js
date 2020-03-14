import * as httpService from '../../../api/http-service'

const apiUserContacts = "/api/UserContact/";
const apiUser = "/api/User/";
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

export const getUsers = () => {
    let params = {
        url: apiUser+"query"
    }

    return httpService.getData(params);
}

export const createContact = (data) => {
    let params = {
        url: apiUserContacts,
        data: data
    }

    return httpService.postData(params);
}

export const createChat = (data) => {
    let params = {
        url: apiChats,
        data: data
    }

    return httpService.postData(params);
}