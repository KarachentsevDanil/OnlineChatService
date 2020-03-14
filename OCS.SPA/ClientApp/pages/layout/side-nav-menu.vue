<template>
<!-- Main navbar -->
	<div class="navbar navbar-default navbar-static-top header-highlight">
		<div class="navbar-collapse collapse" id="navbar-mobile">
			<div class="navbar-right">
				<p class="navbar-text">Hi, {{getUsername.firstName}} {{getUsername.lastName}}!</p>
				<p class="navbar-text"><span class="label bg-success">Online</span></p>
				
				<p class="navbar-text">
				<i @click="logout" class="icon-exit"></i>
				</p>
	
			</div>
		</div>
	</div>
	<!-- /main navbar -->
</template>

<script>
import * as authGetters from "../auth/store/types/getter-types";
import * as authActions from "../auth/store/types/action-types";
import * as authResources from "../auth/store/resources";
import { mapGetters } from "vuex";
import * as navBarConstants from "../../constants/navBarConstants.js";

import * as mainStoreGetters from "../../store/types/action-types";

export default {
  data() {
    return {
    };
  },
  computed: {
    ...mapGetters({
      getUsername: authResources.AUTH_STORE_NAMESPACE.concat(
        authGetters.GET_USER_GETTER
      )
    }),
    getNameIncon() {
      return this.getUsername.firstName[0] + this.getUsername.lastName[0];
    },
    getNavbarItems() {
      return navBarConstants.navBarConstants;
    }
  },
  methods: {
    isPanelActive(item) {
      let isActive = false;

      isActive = item.url == this.$route.path;

      if (isActive) {
        return true;
      }

      isActive = !item.children
        ? item.url == this.$route.path
        : item.children.some(t => t.url == this.$route.path);

      return isActive;
    },
    logout() {
      this.$store.dispatch(
        authResources.AUTH_STORE_NAMESPACE.concat(authActions.LOGOUT_ACTION)
      );

      this.$router.push("/login");
    }
  }
};
</script>

<style>
.user-name {
  font-size: 15px;
}
.icon-exit:hover{
	cursor: pointer;
}
</style>