<template>
  <div class="page-container">
    <div class="page-content">
        <contactList :newMessage="newMessage" :selectChat="setSelectedChat"></contactList>
        <chat :sendMessage="sendMessage" :chat="selectedChat" :newMessage="newMessage"></chat>
    </div>
  </div>
</template>

<script>
import * as transactionService from "../api/contact-service";

import * as authGetters from "../../auth/store/types/getter-types";
import * as authResources from "../../auth/store/resources";

import chat from "./components/chat";
import vue from "Vue";
import contactList from "./components/contact-list";
import * as signalR from '@aspnet/signalr';
import { mapGetters, mapActions } from "vuex";

// const connection = new signalR.HubConnectionBuilder()
//         .withUrl('https://dkarachatapi.azurewebsites.net/signalr/chats/',
//           { accessTokenFactory: () => localStorage.token }
//         )
//         .build();

// connection.start();
// // connection.start();

export default {
  components: {
    chat,
    contactList
  },
  data: () => ({
    selectedChat: null,
    newMessage: null,
    chats:[]
  }),
  methods: {
    setSelectedChat(contact) {
        console.log("Contact selected: ", contact);
        this.selectedChat = contact;
    },
    applyMessage(message){
        this.messageAdded(message);
        this.newMessage = message;
    },
    messageAdded(message){        
        if(this.selectedChat && this.selectedChat.id == message.chat.id){
            return;
        }

        let formatedMessage = `You've received message: ${message.text} from: ${message.createdByUser.firstName} ${message.createdByUser.lastName}`; 
        this.$noty.success(formatedMessage);
    },
    sendMessage(message){
        this.$chatHub.sendMessage(message);
    }
  },
  async created(){
      await this.$chatHub.startSignalR(localStorage.token);
      this.$chatHub.$on('message-added', this.applyMessage);
      // connection.on('ChatMessageHub', (message) => {
      //   this.messageAdded(message);
      //   this.newMessage = message;
      // })
  }
};
</script>