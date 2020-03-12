<template>
<!-- Main navbar -->
	<div class="navbar navbar-default navbar-static-top header-highlight">
		<div class="navbar-collapse collapse" id="navbar-mobile">
			<div class="navbar-right">
				<p class="navbar-text">Hi, {{getUsername.firstName}} {{getUsername.lastName}}!</p>
				<p class="navbar-text"><span class="label bg-success">Online</span></p>
				
				<ul class="nav navbar-nav">				
					<li class="dropdown">
						<a href="#" class="dropdown-toggle" data-toggle="dropdown">
							<i class="icon-bell2"></i>
							<span class="visible-xs-inline-block position-right">Activity</span>
							<span class="status-mark border-pink-300"></span>
						</a>

						<div class="dropdown-menu dropdown-content">
							<div class="dropdown-content-heading">
								Activity
								<ul class="icons-list">
									<li><a href="#"><i class="icon-menu7"></i></a></li>
								</ul>
							</div>

							<ul class="media-list dropdown-content-body width-350">
								<li class="media">
									<div class="media-left">
										<a href="#" class="btn bg-success-400 btn-rounded btn-icon btn-xs"><i class="icon-mention"></i></a>
									</div>

									<div class="media-body">
										<a href="#">Taylor Swift</a> mentioned you in a post "Angular JS. Tips and tricks"
										<div class="media-annotation">4 minutes ago</div>
									</div>
								</li>

								<li class="media">
									<div class="media-left">
										<a href="#" class="btn bg-pink-400 btn-rounded btn-icon btn-xs"><i class="icon-paperplane"></i></a>
									</div>
									
									<div class="media-body">
										Special offers have been sent to subscribed users by <a href="#">Donna Gordon</a>
										<div class="media-annotation">36 minutes ago</div>
									</div>
								</li>

								<li class="media">
									<div class="media-left">
										<a href="#" class="btn bg-blue btn-rounded btn-icon btn-xs"><i class="icon-plus3"></i></a>
									</div>
									
									<div class="media-body">
										<a href="#">Chris Arney</a> created a new <span class="text-semibold">Design</span> branch in <span class="text-semibold">Limitless</span> repository
										<div class="media-annotation">2 hours ago</div>
									</div>
								</li>

								<li class="media">
									<div class="media-left">
										<a href="#" class="btn bg-purple-300 btn-rounded btn-icon btn-xs"><i class="icon-truck"></i></a>
									</div>
									
									<div class="media-body">
										Shipping cost to the Netherlands has been reduced, database updated
										<div class="media-annotation">Feb 8, 11:30</div>
									</div>
								</li>

								<li class="media">
									<div class="media-left">
										<a href="#" class="btn bg-warning-400 btn-rounded btn-icon btn-xs"><i class="icon-bubble8"></i></a>
									</div>
									
									<div class="media-body">
										New review received on <a href="#">Server side integration</a> services
										<div class="media-annotation">Feb 2, 10:20</div>
									</div>
								</li>

								<li class="media">
									<div class="media-left">
										<a href="#" class="btn bg-teal-400 btn-rounded btn-icon btn-xs"><i class="icon-spinner11"></i></a>
									</div>
									
									<div class="media-body">
										<strong>January, 2016</strong> - 1320 new users, 3284 orders, $49,390 revenue
										<div class="media-annotation">Feb 1, 05:46</div>
									</div>
								</li>
							</ul>
						</div>
					</li>
				</ul>
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
</style>