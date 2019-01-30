<template>
  <div class="panel container contentStyle2">
    <section class="subtitle is-2 level-left" style="margin-left:0.5em">
      <button class="button is-rounded is-info is-outlined" style="height:45px" @click="back()">
        <span class="icon is-info">
          <i class="fas fa-arrow-left"></i>
        </span>
      </button>
      <h1 class="sectionStyle">Manage Account Rates</h1>
    </section>
    <nav class="breadcrumb" aria-label="breadcrumbs">
      <ul>
        <li>
          <a href="/">Home</a>
        </li>
        <li>
          <a href="/ManageCustomerAccounts" aria-current="page">Manage Customer Accounts</a>
        </li>
        <li class="is-active">
          <a href="/" aria-current="page">Manage Account Rates</a>
        </li>
      </ul>
    </nav>
    <section>
      <div class="level panel-heading panelStyle">
        <div class="level-left">
          <b-field grouped group-multiline>
            <b-select v-model="defaultSortDirection">
              <option value="asc">Default sort direction: ASC</option>
              <option value="desc">Default sort direction: DESC</option>
            </b-select>
            <b-select v-model="perPage" :disabled="!isPaginated">
              <option value="5">5 per page</option>
              <option value="10">10 per page</option>
              <option value="15">15 per page</option>
              <option value="20">20 per page</option>
            </b-select>
          </b-field>
        </div>
        <div class="level-right">
          <button class="button is-info" @click="createRate($route.params.customerAccountId)">
            <b-icon pack="fas" icon="plus"></b-icon>
            <p>Add Account Rate</p>
          </button>
        </div>
      </div>
      <b-table
        :data="isEmpty ? [] : data"
        :paginated="isPaginated"
        :per-page="perPage"
        :striped="isStriped"
        :hoverable="isHoverable"
        :current-page.sync="currentPage"
        :default-sort-direction="defaultSortDirection"
        default-sort="accountName"
      >
        <template slot="empty">
          <section class="section">
            <div class="content has-text-grey has-text-centered">
              <p>
                <b-icon icon="emoticon-sad" size="is-large"></b-icon>
              </p>
              <p>Nothing here.</p>
            </div>
          </section>
        </template>
        <template slot-scope="props">
          <b-table-column field="rateHour" label="Rate Per Hour" sortable>{{ props.row.rateHour }}</b-table-column>

          <b-table-column field="startDate" label="Effective Start Date" sortable>
            <span>{{ props.row.startDate}}</span>
          </b-table-column>
          <b-table-column field="endDate" label="Effective End Date" sortable>
            <span>{{ props.row.endDate }}</span>
          </b-table-column>

          <b-table-column custom-key="actions">
            <button class="button is-primary" @click="updateRate(props.row.rateId)">
              <b-icon pack="fas" icon="edit"></b-icon>
              <span>Update Rate</span>
            </button>
            <button
              class="button is-danger"
              :disabled="isOne(props.row.rateId, props.row.startDate, props.row.endDate)"
              @click="deleteRow(props.row.rateId, $route.params.customerAccountId, props.row)"
            >
              <b-icon pack="fas" icon="trash-alt"></b-icon>
              <span>Delete</span>
            </button>
          </b-table-column>
        </template>
      </b-table>
      <link
        rel="stylesheet"
        href="//cdn.materialdesignicons.com/2.5.94/css/materialdesignicons.min.css"
      >
      <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.2.0/css/all.css">
    </section>
  </div>
</template>

<style>
.contentStyle2 {
  margin-top: 3em;
}
.sectionStyle {
  margin-left: 0.5em;
}
.manageTitle {
  margin-left: 1em;
}
.panelStyle {
  margin: 0;
}
</style>

<script>
import { router, authHeader } from "../_helpers";
import api from "../_services/restful.service";
import moment from "moment";

export default {
  data() {
    const data = [];
    return {
      data,
      isFullPage: true,
      isEmpty: false,
      isPaginated: true,
      isStriped: true,
      isHoverable: true,
      isPaginationSimple: false,
      defaultSortDirection: "desc",
      currentPage: 1,
      perPage: 5
    };
  },
  watch: {
    $route(to, from) {
      this.getAll();
    }
  },
  async created() {
    const loadingComponent = this.$loading.open({
      container: this.isFullPage ? null : this.$refs.element.$el
    });
    setTimeout(() => loadingComponent.close(), 0.5 * 1000);
    this.getAll();

    var custAccId = this.$route.params.customerAccountId;
  },
  computed: {},
  methods: {
    isOne(id, startDate, endDate) {
      if (this.data.length == 1) {
        return true;
      } else {
        var starting = moment(startDate, "DD-MM-YYYY"); // replace format by your one
        var ending = moment(endDate, "DD-MM-YYYY");
        var today = moment(new Date());
        if (today.diff(starting) >= 0 || today.diff(ending) >= 0) {
          console.log("is true");
          return true;
        } else {
          console.log("is false");
          return false;
        }
      }
    },
    async getAll() {
      try {
        this.data = await api.getAll(
          "/CustomerAccounts/ManageAccountRates/" +
            this.$route.params.customerAccountId
        );
      } finally {
      }
    },
    updateRate(rateId) {
      router.push({ path: `/UpdateAccountRate/${rateId}` });
    },
    createRate(custId) {
      router.push({ path: `/CreateAccountRate/${custId}` });
    },
    back() {
      router.push({ path: `/ManageCustomerAccounts` });
    },
    async deleteRow(accountId, custId, row) {
      console.log(accountId);
      this.$dialog.confirm({
        title: "Deleting Rate Record",
        message:
          "Are you sure you want to <b>delete</b> this Account Rate? This action cannot be undone.",
        confirmText: "Delete Rate",
        type: "is-danger",
        hasIcon: true,
        onConfirm: async () => {
          console.log(accountId);
          try {
            await api.delete(
              "/CustomerAccounts/UpdateAccountRates/" + accountId
            );
          } finally {
            this.$toast.open({
              duration: 1000,
              message: "Account Rate deleted!"
            });
            let index = this.data.findIndex(row => row.rateId === accountId);

            this.data.splice(index, 1);
            this.$forceUpdate();
            console.log("data", this.data);
          }
        }
      });
    }
  }
};
</script>