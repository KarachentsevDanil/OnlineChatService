<template>
  <div>
    <div class="panel panel-body login-form">
      <div class="text-center">
        <div class="icon-object border-success text-success">
          <i class="icon-plus3"></i>
        </div>
        <h5 class="content-group">
          <span>Create new account</span>
        </h5>
      </div>

      <div class="form-group has-feedback has-feedback-left">
        <input
          type="text"
          class="form-control"
          v-model="user.email"
          :error-messages="emailErrors"
          @input="$v.user.email.$touch()"
          @blur="$v.user.email.$touch()"
          placeholder="Email"
          required
        >

        <div class="form-control-feedback">
          <i class="icon-user text-muted"></i>
        </div>
      </div>

      <div class="form-group has-feedback has-feedback-left">
        <input
          type="text"
          class="form-control"
          placeholder="First Name"
          v-model="user.firstName"
          :error-messages="firstNameErrors"
          @input="$v.user.firstName.$touch()"
          @blur="$v.user.firstName.$touch()"
          required
        >

        <div class="form-control-feedback">
          <i class="icon-user text-muted"></i>
        </div>
      </div>

      <div class="form-group has-feedback has-feedback-left">
        <input
          type="text"
          class="form-control"
          placeholder="Last Name"
          v-model="user.lastName"
          :error-messages="lastNameErrors"
          @input="$v.user.lastName.$touch()"
          @blur="$v.user.lastName.$touch()"
          required
        >

        <div class="form-control-feedback">
          <i class="icon-user text-muted"></i>
        </div>
      </div>

      <div class="form-group has-feedback has-feedback-left">
        <input
          type="password"
          class="form-control"
          v-model="user.password"
          :error-messages="passwordErrors"
          placeholder="Password"
          @input="$v.user.password.$touch()"
          @blur="$v.user.password.$touch()"
          required
        >
        <div class="form-control-feedback">
          <i class="icon-user-lock text-muted"></i>
        </div>
      </div>
      <div class="form-group">
        <button
          type="submit"
          :disabled="isInvaild"
          @click="submit"
          class="btn bg-teal btn-block btn-lg"
        >
          <span>Register</span>
          <i class="icon-circle-right2 position-right"></i>
        </button>
      </div>
      <div class="content-divider text-muted form-group">
        <span>Already have account&</span>
      </div>
      <router-link
        to="/login"
        class="btn btn-default btn-block content-group"
      >Sign In</router-link>
    </div>
  </div>
</template>

<script>
import * as authenticationService from "../api/authentication-service";
import * as authTextResources from "../resources/resources";
import * as mainStoreActions from "../../../store/types/action-types";

import { mapGetters } from "vuex";
import { validationMixin } from "vuelidate";
import { required, minLength, email } from "vuelidate/lib/validators";

export default {
  mixins: [validationMixin],

  validations: {
    user: {
      email: { required, email },
      password: { required, minLength: minLength(6) },
      firstName: { required },
      lastName: { required }
    }
  },
  data: () => ({
    user: {
      email: "",
      password: "",
      firstName: "",
      lastName: ""
    },
    labels: {
      ...authTextResources.lables
    },
    menu: false
  }),
  methods: {
    async submit() {
      let data = {
        Email: this.user.email,
        Password: this.user.password,
        FirstName: this.user.firstName,
        LastName: this.user.lastName
      };

      this.$store.dispatch(
        mainStoreActions.START_LOADING_ACTION,
        "Account is creating ..."
      );

      let response = (await authenticationService.registr(data));

      if (response.status == 200) {
        this.$noty.success(
          authTextResources.popupMessages.accountCreatedMessage
        );
        this.$router.push("/login");
      } else {
        this.$noty.error(
          authTextResources.popupMessages.accountCreationFailedMessage
        );
      }

      this.$store.dispatch(mainStoreActions.STOP_LOADING_ACTION);
    }
  },
  computed: {
    isInvaild() {
      if (this.$v.$invalid) {
        return true;
      }

      return false;
    },
    passwordErrors() {
      const errors = [];
      if (!this.$v.user.password.$dirty) return errors;
      !this.$v.user.password.minLength &&
        errors.push(
          authTextResources.lables.validationMessages.passwordLengthMessage
        );
      !this.$v.user.password.required &&
        errors.push(
          authTextResources.lables.validationMessages.passwordRequiredMessage
        );
      return errors;
    },
    emailErrors() {
      const errors = [];
      if (!this.$v.user.email.$dirty) return errors;
      !this.$v.user.email.email &&
        errors.push(authTextResources.lables.validationMessages.emailMessage);
      !this.$v.user.email.required &&
        errors.push(
          authTextResources.lables.validationMessages.emailRequiredMessage
        );
      return errors;
    },
    firstNameErrors() {
      const errors = [];
      if (!this.$v.user.firstName.$dirty) return errors;
      !this.$v.user.firstName.required &&
        errors.push(
          authTextResources.lables.validationMessages.firstNameRequiredMessage
        );
      return errors;
    },
    lastNameErrors() {
      const errors = [];
      if (!this.$v.user.lastName.$dirty) return errors;
      !this.$v.user.lastName.required &&
        errors.push(
          authTextResources.lables.validationMessages.lastNameRequiredMessage
        );
      return errors;
    }
  }
};
</script>
