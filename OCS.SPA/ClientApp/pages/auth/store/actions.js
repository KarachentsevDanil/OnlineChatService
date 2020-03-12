import * as authenticationService from "../api/authentication-service";
import * as mutations from "./types/mutators-types";
import * as authResources from "../resources/resources";
import Vue from "vue";

export default {
    async login(context, data) {
        let response = (await authenticationService.login(data.user));
        
        if (response.status == 200) {
            let userDate = response.data;

            let token = {
                value: userDate.accessToken,
                expireData: userDate.expiresIn
            };
            
            Vue.prototype.startSignalR(userDate.accessToken);

            context.commit(mutations.SET_TOKEN_MUTATOR, token);
            context.commit(mutations.SET_USER_MUTATOR, userDate.user);
            
            data.router.push("/chat");
        } else {
            data.notification.error(
                authResources.popupMessages.loginFailedMessage
            );
        }
    },
    refreshContext(){
        Vue.prototype.startSignalR(localStorage.token);
    },
    logout(context) {
        context.commit(mutations.USER_LOGOUT_MUTATOR);
    }
};
