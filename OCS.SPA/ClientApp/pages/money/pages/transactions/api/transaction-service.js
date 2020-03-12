import * as httpService from '../../../../../api/http-service'

const apiTransaction = "/api/MoneyTransaction/";

export const getUserTransactions = () => {
    let params = {
        url: apiTransaction
    }

    return httpService.getData(params);
}