<template>
  <div class="panel container contentStyle2">
    <section class="subtitle is-2 level-left sectionStyle">
      <b-icon pack="fas" icon="users" type="is-danger" size="is-small"></b-icon>
      <h1 class="manageTitle">Manage Customer Accounts</h1>
    </section>
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
          <router-link class="control" to="/CreateCustomerAccount">
            <button class="button is-info">
              <b-icon pack="fas" icon="plus"></b-icon>
              <p>Add Customer</p>
            </button>
          </router-link>
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
          <b-table-column
            field="accountName"
            label="Customer"
            sortable
          >{{ props.row.accountName }}</b-table-column>

          <b-table-column
            field="comments"
            label="Comments"
            sortable
            style="width:17%"
          >{{ props.row.comments }}</b-table-column>

          <b-table-column
            field="numAccRates"
            label="No. Rates"
            sortable
            style="width:7%"
          >{{ props.row.numAccRates }}</b-table-column>

          <b-table-column field="updatedBy" label="Updated By" sortable>
            <span>{{ props.row.updatedBy }}</span>
          </b-table-column>
          <b-table-column field="updatedAt" label="Updated At" sortable style="width:11%">
            <span>{{ props.row.updatedAt}}</span>
          </b-table-column>

          <b-table-column :visible="checkRole" field="visibility" label="Visible" sortable>
            <b-icon pack="fas" icon="check" v-if="props.row.visibility"></b-icon>
            <b-icon pack="fas" icon="times" v-else></b-icon>
          </b-table-column>

          <b-table-column custom-key="actions">
            <button
              class="button is-primary buttonStyle"
              @click="updateGeneralInfo(props.row.customerAccountId)"
            >
              <b-icon pack="fas" icon="edit"></b-icon>
              <span>Update Cust Info</span>
            </button>
            <button class="button is-info buttonStyle" @click="manageAccountRates(props.row.customerAccountId)">
              <b-icon pack="fas" icon="dollar-sign"></b-icon>
              <span>Manage Rates/Hour</span>
            </button>
            <button class="button is-link buttonStyle" @click="manageAccountDetail(props.row.customerAccountId)">
              <b-icon pack="fas" icon="clipboard"></b-icon>
              <span>Manage Account Details</span>
            </button>
            <button class="button is-danger buttonStyle" @click="deleteRow(props.row.customerAccountId)">
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

<script>
import axios from "axios";
import { router, authHeader } from "../_helpers";

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
    watch:{
    $route (to, from){
        this.getAll();
    }
 },
  async created() {
    const loadingComponent = this.$loading.open({
      container: this.isFullPage ? null : this.$refs.element.$el
    });
    setTimeout(() => loadingComponent.close(), 0.5 * 1000);
    this.getAll();
  },
  methods: {
    getAll() {
      axios
        .get("http://localhost:5000/api/CustomerAccounts/", {
          headers: authHeader()
        })
        .then(response => {
          // JSON responses are automatically parsed.
          this.data = response.data;
            let user = JSON.parse(localStorage.getItem('user'));

          if (user != null) {
            if (user.user.roles == "Admin") {
              this.checkRole = true;
            } else {
              this.checkRole =  false;
            }
          }
        })
        .catch(e => {
          console.log(e);
        });
    },
    updateGeneralInfo(customerAccountId) {
      router.push({ path: `/UpdateGeneralInfo/${customerAccountId}` });
    },
    manageAccountRates(customerAccountId) {
      router.push({ path: `/ManageAccountRates/${customerAccountId}` });
    },
    manageAccountDetail(customerAccountId){
      router.push({ path: `/ManageAccountDetails/${customerAccountId}` });
    },
    deleteRow(accountId) {
      console.log(accountId);
      this.$dialog.confirm({
        title: "Deleting Customer Record",
        message:
          "Are you sure you want to <b>delete</b> this Customer Account? This action cannot be undone.",
        confirmText: "Delete Account",
        type: "is-danger",
        hasIcon: true,
        onConfirm: () => {
          console.log(accountId);
          axios
            .delete("http://localhost:5000/api/CustomerAccounts/" + accountId, {
              headers: authHeader()
            })
            .then(response => {
              // JSON responses are automatically parsed.
              let index = this.data.findIndex(
                row => row.customerAccountId === accountId
              );
              this.data.splice(index, 1);
            })
            .catch(e => {
              console.log(e.response);
            });
          this.$toast.open({
              duration: 1000,
              message: "Customer Account deleted!"
          });
        }
      });
    }
  }
};
</script>

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
  margin-top: 2em;
}
.buttonStyle {
  margin-top: 0.5em;
  width: 250px;
}
</style>
