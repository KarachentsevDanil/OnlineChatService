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
                <input type="search" class="form-control" placeholder="Search contact..." />
                <div class="form-control-feedback">
                  <i class="icon-search4 text-size-base text-muted"></i>
                </div>
              </div>
              <div class="form-control">
                <input type="checkbox" /> Search outside of a contacts
              </div>
            </div>
            <div class="category-content no-padding">
              <ul class="media-list media-list-linked">
                <li class="media" v-for="item in data" v-bind:key="item.id" @click="contactSelected(item)">
                  <a href="#" class="media-link">
                    <div class="media-left media-middle">
														<a href="#" class="btn bg-success-400 btn-rounded btn-icon btn-xs legitRipple">
															<span class="letter-icon">{{item.contact.firstName[0]}}{{item.contact.lastName[0]}}</span>
														</a>
											</div>
                    <div class="media-body">
                      <span class="media-heading text-semibold">{{item.contact.firstName}} {{item.contact.lastName}}</span>
                      <span class="text-size-small text-muted display-block">{{item.contact.email}}</span>
                    </div>
                    <div class="media-right media-middle">
                      <span class="status-mark" v-bind:class="{ 'bg-success': item.contact.isOnline, 'bg-danger': !item.contact.isOnline }"></span>
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

import Vue from "Vue";

export default {
  data: () => ({
    data: [],
    total: 0
  }),
  props: {
    selectContact:Function
  },
  methods: {
    async loadContacts() {
      let data = (await transactionService.getContacts()).data;

      this.data = data;
    },
    contactSelected(contact){
      console.log("Raise contact selected event: ", contact);
      this.selectContact(contact);
    }
  },
  beforeMount() {
    this.loadContacts();
  }
};
</script>