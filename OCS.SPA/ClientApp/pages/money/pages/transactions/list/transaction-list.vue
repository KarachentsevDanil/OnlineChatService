<template>
    <div>
        <div class="page-header">
            <div class="page-header-content">
                <div class="page-title">
                    <h4><i class="icon-coins position-left"></i> <span class="text-semibold">Transactions</span></h4>
                    <a class="heading-elements-toggle"><i class="icon-more"></i></a><a class="heading-elements-toggle"><i class="icon-more"></i></a>
                </div>
            </div>
        </div>

        <div class="content">
            <div class="panel panel-flat without-header">
                <datatable v-bind="$data" :HeaderSettings="false" />
            </div>
        </div>
    </div>
</template>

<script>
import * as transactionService from "../api/transaction-service";

import * as authGetters from "../../../../auth/store/types/getter-types";
import * as authResources from "../../../../auth/store/resources";

import { mapGetters } from "vuex";

import Vue from "Vue";

export default {
  data: () => ({
    tblClass: "grid-table",
    pageSizeOptions: [10, 25, 50, 100],
    columns: [
      {
        title: "Amount",
        field: "FormattedAmount",
        sortable: false
      },
      {
        title: "Date",
        field: "FormattedCreatedOn",
        sortable: false
      }
    ],
    data: [],
    total: 0,
    query: {},
    xprops: {
      eventbus: new Vue()
    }
  }),
  watch: {
    query: {
      async handler() {
        await this.getTransactions();
      },
      deep: true
    }
  },
  computed: {
    ...mapGetters({
      getUser: authResources.AUTH_STORE_NAMESPACE.concat(
        authGetters.GET_USER_GETTER
      ),
      currentLanguage: "getCurrentLanguage"
    })
  },
  methods: {
    async getTransactions() {
      let data = (await transactionService.getUserTransactions()).data;

      this.data = data;
      this.total = data.length;
    }
  }
};
</script>