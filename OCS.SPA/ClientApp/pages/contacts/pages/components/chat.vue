<template>
      <div class="content-wrapper">
        <!-- Content area -->
        <div class="content chat-border" v-if="chat">
          <!-- Basic layout -->
          <div class="panel panel-white">
            <div class="panel-heading">
              <h6 class="panel-title">
                {{getContact(chat).fullName}}
                <span v-if="getContact(chat).isOnline" class="label label-success position-right">Online</span>
                <span v-else class="label label-danger position-right">Offline</span>
              </h6>
            </div>

            <div class="panel-body">
              <ul class="media-list chat-list content-group">
                <li
                  :class="getStyleForCurrentUser(message.createdByUser.id)"
                  v-for="message in messages"
                  :key="message.Id"
                >
                  <div class="media-left" v-if="message.createdByUser.id != getUser.id">
                    <span class="btn bg-pink-400 btn-rounded btn-icon btn-xs legitRipple">
                      <span class="letter-icon">{{getNameIncon(message.createdByUser)}}</span>
                    </span>
                  </div>
                  <div class="media-body">
                    <div class="media-content">{{message.text}}</div>
                    <span class="media-annotation display-block mt-10">
                      {{message.createdAt | formatDate}}
                      <a href="#" v-if="message.createdByUser.id != getUser.id">
                        <i class="icon-bubbles2 position-right text-muted"></i>
                      </a>
                      <a href="#" v-else>
                        <i class="icon-bubbles4 position-right text-muted"></i>
                      </a>
                    </span>
                  </div>
                  <div class="media-right" v-if="message.createdByUser.id == getUser.id">
                    <span class="btn bg-pink-400 btn-rounded btn-icon btn-xs legitRipple">
                      <span class="letter-icon">{{getNameIncon(message.createdByUser)}}</span>
                    </span>
                  </div>
                </li>
              </ul>

              <textarea
                name="enter-message"
                class="form-control content-group"
                rows="3"
                cols="1"
                placeholder="Enter your message..."
                v-model="text"
              ></textarea>

              <div class="row">
                <div class="col-xs-6">
                </div>

                <div class="col-xs-6 text-right">
                  <button @click="sendMessageToUser" type="button" class="btn bg-teal-400 btn-labeled btn-labeled-right" :disabled="!text">
                    <b>
                      <i class="icon-circle-right2"></i>
                    </b> Send
                  </button>
                </div>
              </div>
            </div>
          </div>
          <!-- /basic layout -->
        </div>
        <!-- /content area -->
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
    messages: [],
    text: ""
  }),
  props: {
    chat:Object,
    newMessage:Object,
    sendMessage: Function
  },
  watch: {
    chat: {
      async handler() {
        console.log("Event triggered:", this.chat);
        await this.loadMessages();
      },
      deep: true
    },
    newMessage: {
      handler() {
        console.log("New message was added:", this.newMessage);
        if(this.newMessage.chat.id == this.chat.id){
          this.messages.push(this.newMessage);
          setTimeout(()=>{
          $('ul.chat-list').animate({ scrollTop: $('ul.chat-list').prop("scrollHeight")}, 100);
        }, 100);
        }
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
  methods: {
    getStyleForCurrentUser(userId) {
      return this.getUser.id == userId ? "media reversed" : "media";
    },
    getContact(contact){
      if(contact.createdByUserId != this.getUser.id){
        return {
          fullName: contact.createdByUserFullName,
          email: contact.createdByUserEmail,
          isOnline: contact.createdByUserIsOnline
        }
      }else{
        return {
          fullName: contact.invitedUserFullName,
          email: contact.invitedUserEmail,
          isOnline: contact.invitedUserIsOnline
        }
      }
    },
    getNameIncon(user) {
      return user.firstName[0] + user.lastName[0];
    },
    sendMessageToUser() {
      let newMessage = {        
        chatId: this.chat.id,
        text: this.text
      };

      this.sendMessage(newMessage);
      this.text = "";
    },
    async loadMessages() {
      if(this.chat){
        let query = {
          chatId: this.chat.id,
          skip: 0,
          take: 1000
        };

        let data = (await transactionService.getMessages(query)).data;
        this.messages = data.items;

        console.log("Messages:", this.messages);
        setTimeout(()=>{
          $('ul.chat-list').animate({ scrollTop: $('ul.chat-list').prop("scrollHeight")}, 100);
        }, 100);
      }
    }
  }
};
</script>