<template>
    <div id="app" v-cloak>
        <div>
          <div v-if="getToken">
                <BlockUI v-if="blockUiOptions && blockUiOptions.isLoading" :message="blockUiOptions.message" :html="blockUiOptions.icon"></BlockUI>
                <main>
                    <!-- Main navbar -->
                    <side-nav-menu></side-nav-menu>
                    <!-- /main navbar -->
                    <router-view></router-view>
                </main>
            </div>
            <div v-else>
                <div class="login-container">
                    <div class="page-container">
                        <div class="page-content">
                            <div class="content-wrapper">
                                <div class="content">
                                    <router-view></router-view>
                                    <app-footer></app-footer>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>
<script>
import Vue from "vue";
import appFooter from "./app-footer";
import sideNavMenu from "./side-nav-menu";

import "../../assets/limitless/icons/icomoon/styles.css";
import "../../assets/limitless/css/bootstrap.css";
import "../../assets/limitless/css/core.css";
import "../../assets/limitless/css/components.css";
import "../../assets/limitless/css/colors.css";
import "../../assets/limitless/css/engage.css";

import "../../assets/limitless/js/pace.js";
import "bootstrap/dist/js/bootstrap.js";
import "../../assets/limitless/js/app.js";
import "../../assets/limitless/js/ripple.min.js";

import * as authGetters from "../auth/store/types/getter-types";
import * as authResources from "../auth/store/resources";

import * as mainStoreGetters from "../../store/types/action-types";

import { mapGetters } from "vuex";

export default {
  components: {
    appFooter: appFooter,
    sideNavMenu: sideNavMenu
  },
  data() {
    return {
    };
  },
  destroyed(){
    // Vue.prototype.stopSignalR();
  },
  computed: {
    ...mapGetters({
      getUsername: authResources.AUTH_STORE_NAMESPACE.concat(
        authGetters.GET_USER_GETTER
      ),
      getToken: authResources.AUTH_STORE_NAMESPACE.concat(
        authGetters.GET_TOKEN_GETTER
      ),
      blockUiOptions: "getLoaderOptions"
    })
  }
};
</script>

<style>
.chat-border{
  padding: 0px !important;
}
div.sidebar{
  max-height: 700px;
}
ul.media-list.chat-list{
  max-height: 450px;
}
div.sidebar{
  max-height: 700px;
  overflow: auto;
}
div.sidebar-content{
  height: 700px;
}
div.search-block{
  padding: 20px;
}
div.category-title{
  padding-bottom: 8px !important;
}
</style>