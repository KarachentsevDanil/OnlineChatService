import Vue from "vue";
import VueRouter from "vue-router";
import LoginPage from "./pages/auth/pages/login";

import authorizationRoutes from "./routes/authorization-routes";
import moneyRoutes from "./routes/money-routes";
import conctactRoutes from "./routes/contact-routes";

Vue.use(VueRouter);

const routes = [
    ...authorizationRoutes,
    ...moneyRoutes,
    ...conctactRoutes,
    {
        path: "*",
        component: LoginPage
    }
];

let router = new VueRouter({
    mode: "history",
    routes
});

export default router;
