<template>
      <div class="sidebar sidebar-secondary sidebar-default">
        <div class="sidebar-content">
          <!-- Online users -->
          <div class="sidebar-category">
            <div class="category-title">
              <h6>Contacts</h6>
            </div>
            <div class="search-block">
              <p>Search contacts:</p>
              <div class="has-feedback has-feedback-left">
                <input type="search" class="form-control" placeholder="Search contact..." v-model="searchContactTerm" @keyup="filterContacts"/>
                <div class="form-control-feedback">
                  <i class="icon-search4 text-size-base text-muted"></i>
                </div>
              </div>
              <div class="form-control">
                <input type="checkbox" v-model="addContacts"/> Add new contacts
              </div>
            </div>
            <div class="category-content no-padding">
              <ul class="media-list media-list-linked" v-if="!addContacts">
                <li class="media" v-for="item in data" v-bind:key="item.id" @click="chatSelected(item)">
                  <a href="#" class="media-link">
                    <div class="media-left media-middle">
														<a href="#" class="btn bg-success-400 btn-rounded btn-icon btn-xs legitRipple">
															<span class="letter-icon">{{getContact(item).fullName | fullNameIcon}}</span>
														</a>
											</div>
                    <div class="media-body">
                      <span class="media-heading text-semibold">{{getContact(item).fullName}}</span>
                      <span class="text-size-small text-muted display-block">
                        <span v-if="item.lastMessageCreatedByUserId == getUser.id">You:</span> {{getContact(item).lastMessage}}
                      </span>
                    </div>
                    <div class="media-right media-middle">
                      <span class="status-mark" v-bind:class="{ 'bg-success': getContact(item).isOnline, 'bg-danger': !getContact(item).isOnline }"></span>
                    </div>
                  </a>
                </li>
              </ul>
              <ul class="media-list media-list-linked user-search-result" v-else>
                <li class="media" v-for="item in filteredUsers" v-bind:key="item.id">
                  <a href="#" class="media-link">
                    <div class="media-left media-middle">
														<a href="#" class="btn bg-success-400 btn-rounded btn-icon btn-xs legitRipple">
															<span class="letter-icon">{{item.firstName[0]}}{{item.lastName[0]}}</span>
														</a>
											</div>
                    <div class="media-body">
                      <span class="media-heading text-semibold">{{item.firstName}} {{item.lastName}}</span>
                      <span class="text-size-small text-muted display-block">
                        {{item.email}}
                      </span>
                    </div>
                    <div class="media-right media-middle">
                      <button type="button" class="btn btn-primary btn-icon btn-rounded legitRipple" @click="addNewContact(item)">
                        <i class="icon-plus"></i>
                      </button>
                    </div>
                  </a>
                </li>
              </ul>
            </div>
          </div>
          <!-- /online users -->
        </div>
      </div>
</template>

<script>
import * as transactionService from "../../api/contact-service";

import * as authGetters from "../../../auth/store/types/getter-types";
import * as authResources from "../../../auth/store/resources";

import * as mainStoreGetters from "../../../../store/types/action-types";

import { mapGetters } from "vuex";

import Vue from "Vue";

export default {
  data: () => ({
    data: [],
    users: [],
    filteredUsers: [],
    total: 0,
    addContacts: false,
    searchContactTerm: ""
  }),
  props: {
    selectChat:Function,
    newMessage: Object
  },
  methods: {
    async loadContacts() {
      let data = (await transactionService.getChats()).data;

      this.data = data;
      
      let users = (await transactionService.getUsers()).data;

      this.users = users;
      this.filteredUsers = users;
    },
    chatSelected(contact){
      this.selectChat(contact);
    },
    filterContacts(){
      let search = this.searchContactTerm.toLowerCase();

      let result = this.users
              .filter(t=> `${t.firstName} ${t.lastName}`.toLowerCase().includes(search) ||
              t.email.toLowerCase().includes(search));

      this.filteredUsers = result;
    },
    async addNewContact(item){
      let newContact = {
        contactId : item.id
      };

      await transactionService.createContact(newContact);
      await transactionService.createChat(newContact);

      this.$noty.success(`${item.firstName} ${item.lastName} successfully added to contact!`);

      this.filteredUsers = this.filteredUsers.filter(t => t.id != item.id);
      this.users = this.users.filter(t => t.id != item.id);

      let items = (await transactionService.getChats()).data;
      this.data = items;
    },
    getContact(contact){
      if(contact.createdByUserId != this.getUser.id){
        return {
          fullName: contact.createdByUserFullName,
          email: contact.createdByUserEmail,
          isOnline: contact.createdByUserIsOnline,
          lastMessage: contact.lastMessageText ? contact.lastMessageText.substring(0, 20) : "",
          lastMessageCreatedByUserId: contact.lastMessageCreatedByUserId
        }
      }else{
        return {
          fullName: contact.invitedUserFullName,
          email: contact.invitedUserEmail,
          isOnline: contact.invitedUserIsOnline,
          lastMessage: contact.lastMessageText ? contact.lastMessageText.substring(0, 20) : "",
          lastMessageCreatedByUserId: contact.lastMessageCreatedByUserId
        }
      }
    }
  },
  watch: {
    newMessage: {
      async handler() {
        let items = (await transactionService.getChats()).data;
        this.data = items;
      },
      deep: true
    }
  },
   computed: {
    ...mapGetters({
      getUser: authResources.AUTH_STORE_NAMESPACE.concat(
        authGetters.GET_USER_GETTER
      ),
      getToken: authResources.AUTH_STORE_NAMESPACE.concat(
        authGetters.GET_TOKEN_GETTER
      ),
      blockUiOptions: "getLoaderOptions"
    })
  },
  beforeMount() {
    this.loadContacts();
  }
};
</script>