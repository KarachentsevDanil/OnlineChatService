<template>
  <div class="page-container">
    <div class="page-content">
        <contactList :selectContact="setSelectedContact"></contactList>
        <chat :sendMessage="sendMessage" :contact="selectedContact" :chat="selectedChat" :newMessage="newMessage"></chat>
    </div>
  </div>
</template>

<script>
import * as transactionService from "../api/contact-service";

import * as authGetters from "../../auth/store/types/getter-types";
import * as authResources from "../../auth/store/resources";

import chat from "./components/chat";
import contactList from "./components/contact-list";
import * as signalR from '@aspnet/signalr';
import { mapGetters, mapActions } from "vuex";

const connection = new signalR.HubConnectionBuilder()
        .withUrl('https://localhost:5005/signalr/chats/',
          { accessTokenFactory: () => localStorage.token }
        )
        .build();

connection.start();

export default {
  components: {
    chat,
    contactList
  },
  data: () => ({
    selectedContact: null,
    selectedChat: null,
    newMessage: null,
    chats:[]
  }),
  watch: {
    selectedContact: {
      handler() {
        console.log("Watch triggered: ", this.selectedContact, this.chats);
        
        if (this.selectedContact && this.chats) {
            let userId = this.selectedContact.contact.id;
            let selectedChat = this.chats.find(t => t.createdByUser.id == userId || t.invitedUser.id == userId);
            this.selectedChat = selectedChat;
        }
      },
      deep: true
    }
  },
  methods: {
    async loadChats() {
        let data = (await transactionService.getChats()).data;
        this.chats = data;
    },
    setSelectedContact(contact) {
        console.log("Contact selected: ", contact);
        this.selectedContact = contact;
    },
    messageAdded(message){        
        if(this.selectedChat && this.selectedChat.id == message.chat.id){
            return;
        }
        
        let formatedMessage = `You've received message: ${message.text} from: ${message.createdByUser.firstName} ${message.createdByUser.lastName}`; 
        this.$noty.success(formatedMessage);
    },
    sendMessage(message){
        connection.invoke('SendPrivateMessageAsync', message);
    }
  },
  beforeMount(){
  },
  mounted(){
      connection.on('ChatMessageHub', (message) => {
        this.messageAdded(message);
        this.newMessage = message;
      })
  },
  beforeMount(){
    this.loadChats();
  }
};
</script>