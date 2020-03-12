import ContactsChat from "../pages/contacts/pages/contacts-chat";

import * as routeGuards from "./route-guards";

export default [
    {
        path: "/chat",
        component: ContactsChat,
        beforeEnter: (to, from, next) => {
            routeGuards.validateRoute(to, from, next);
        }
    }
];
