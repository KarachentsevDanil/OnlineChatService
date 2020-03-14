import Vue from 'vue'
import axios from 'axios'
import router from './router'
import store from './store'
import { sync } from 'vuex-router-sync'
import pagination from './assets/vue-pagination/vue-pagination'
import vueSlider from 'vue-slider-component';
import BlockUI from 'vue-blockui'
import VueNoty from 'vuejs-noty'
import Datetime from 'vue-datetime'
import Datatable from 'vue2-datatable-component';
import grid from './pages/plugins/datatable/datatable';
import moment from 'moment'
import ChatHub from './signalR/chat-hub';

window.paceOptions = {
    ajax: false,
    restartOnRequestAfter: false,
};

Vue.filter('formatDate', function(value) {
  if (value) {
    return moment(String(value)).format('MM/DD/YYYY hh:mm')
  }});

Vue.filter('fullNameIcon', function(value) {
    let items = value.split(" ");
    return items[0][0] + items[1][0];
  });

import App from './pages/layout/app-root'

import 'vue2-dropzone/dist/vue2Dropzone.min.css'
import 'vuejs-noty/dist/vuejs-noty.css'
import 'vue-datetime/dist/vue-datetime.css'

Vue.prototype.$http = axios;

sync(store, router);

Vue.use(VueNoty);
Vue.use(BlockUI);
Vue.use(Datatable);
Vue.use(Datetime);
Vue.use(ChatHub)

Vue.component('pagination', pagination);
Vue.component('vueSlider', vueSlider);
Vue.component('grid', grid);

const app = new Vue({
    store,
    router,
    ...App
});

export {
    app,
    router,
    store
}
