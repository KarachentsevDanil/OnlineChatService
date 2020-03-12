import TransactionListPage from "../pages/money/pages/transactions/list/transaction-list";

import * as routeGuards from "./route-guards";

export default [
    {
        path: "/transactions",
        component: TransactionListPage,
        beforeEnter: (to, from, next) => {
            routeGuards.validateRoute(to, from, next);
        }
    }
];
