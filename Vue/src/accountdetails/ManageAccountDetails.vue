<template>
  <div class="panel container contentStyle2">
    <section class="subtitle is-2 level-left" style="margin-left:0.5em">
      <button class="button is-rounded is-info is-outlined" style="height:45px" @click="back()">
        <span class="icon is-info">
          <i class="fas fa-arrow-left"></i>
        </span>
      </button>
      <h1 class="sectionStyle">Manage Account Details</h1>
    </section>
    <nav class="breadcrumb" aria-label="breadcrumbs">
      <ul>
        <li>
          <router-link to="/">Home</router-link>
        </li>
        <li>
          <router-link to="/ManageCustomerAccounts" aria-current="page">Manage Customer Accounts</router-link>
        </li>
        <li class="is-active">
          <router-link to="/" aria-current="page">Manage Account Details</router-link>
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
          <button
            class="button is-info"
            @click="createAccountDetail($route.params.customerAccountId)"
          >
            <b-icon pack="fas" icon="plus"></b-icon>
            <p>Add Account Detail</p>
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
        default-sort="dayOfWeek"
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
          <b-table-column field="status" label="is Active" sortable>
            <b-icon
              v-if="isrunning(props.row.accountDetailId, props.row.startDate, props.row.endDate)"
              pack="fas"
              icon="cog"
              type="is-success"
              custom-class="faa-parent animated-hover faa-spin animated"
            ></b-icon>
            <b-icon v-else pack="fas" icon="cog" type="is-danger"></b-icon>
          </b-table-column>
          <b-table-column field="Day" label="Day" sortable>{{ props.row.dayName }}</b-table-column>
          <b-table-column field="startTimeMin" label="Start Time" sortable>
            <span>{{ props.row.startTime }}</span>
          </b-table-column>
          <b-table-column field="endTimeMin" label="End Time" sortable>
            <span>{{ props.row.endTime }}</span>
          </b-table-column>
          <b-table-column :visible="checkRole" field="visibility" label="Visible" sortable>
            <b-icon pack="fas" icon="check" v-if="props.row.visibility"></b-icon>
            <b-icon pack="fas" icon="times" v-else></b-icon>
          </b-table-column>
          <b-table-column field="startDate" label="Effective Start Date" sortable>
            <span>{{ props.row.startDate}}</span>
          </b-table-column>
          <b-table-column field="endDate" label="Effective End Date" sortable>
            <span>{{ props.row.endDate }}</span>
          </b-table-column>

          <b-table-column custom-key="actions">
            <button class="button is-primary" @click="updateRate(props.row.accountDetailId)">
              <b-icon pack="fas" icon="edit"></b-icon>
              <span>Update Detail</span>
            </button>
            <button
              class="button is-danger"
              :disabled="isOne(props.row.accountDetailId, props.row.startDate, props.row.endDate)"
              @click="deleteRow(props.row.accountDetailId, $route.params.customerAccountId, props.row)"
            >
              <b-icon pack="fas" icon="trash-alt"></b-icon>
              <span>Delete</span>
            </button>
          </b-table-column>
        </template>
      </b-table>
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
import api from "../_services/restful.service";
import { router, authHeader } from "../_helpers";
import moment from "moment";

export default {
  data() {
    const data = [];
    return {
      data,
      checkRole: false,
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

    isrunning(id, startDate, endDate) {
      var starting = moment(startDate, "DD-MM-YYYY"); // replace format by your one
      var ending = moment(endDate, "DD-MM-YYYY");
      var today = moment(new Date());
      if (today.diff(starting) >= 0 || today.diff(ending) >= 0) {
        console.log("is running");
        return true;
      } else {
        return false;
      }
    },
    async getAll() {
      try {
        this.data = await api.getAll(
          "/AccountDetails/" + this.$route.params.customerAccountId
        );
      } finally {
        //loop through list to do conversion
        for (var index in this.data) {
          var row = this.data[index];

          //convert start time min to HH:MM AM/PM
          var startMin = row.startTimeMin;
          row.startTime = moment()
            .startOf("day")
            .add(startMin, "minutes")
            .format("hh:mm A");
          //console.log(row.startTime);

          //convert end time min to HH:MM AM/PM
          var endMin = row.endTimeMin;
          row.endTime = moment()
            .startOf("day")
            .add(endMin, "minutes")
            .format("hh:mm A");
          //console.log(row.endTime);

          //convert day number to day name
          switch (row.dayOfWeek) {
            case 1:
              row.dayName = "Sunday";
              break;
            case 2:
              row.dayName = "Monday";
              break;
            case 3:
              row.dayName = "Tuesday";
              break;
            case 4:
              row.dayName = "Wednesday";
              break;
            case 5:
              row.dayName = "Thursday";
              break;
            case 6:
              row.dayName = "Friday";
              break;
            case 7:
              row.dayName = "Saturday";
              break;
            default:
              row.dayName = "Unknown";
              break;
          }
        }
        let user = JSON.parse(localStorage.getItem("user"));
        if (user != null) {
          if (user.user.roles == "Admin") {
            this.checkRole = true;
          } else {
            this.checkRole = false;
          }
        }
      }
    },
    updateRate(rateId) {
      router.push({ path: `/UpdateAccountDetail/${rateId}` });
    },
    createAccountDetail(custId) {
      router.push({ path: `/CreateAccountDetail/${custId}` });
    },
    back() {
      router.push({ path: `/ManageCustomerAccounts` });
    },
    async deleteRow(accountDetailId, custId, row) {
      console.log(accountDetailId);
      this.$dialog.confirm({
        title: "Deleting Account Detail Record",
        message:
          "Are you sure you want to <b>delete</b> this Account Detail? This action cannot be undone.",
        confirmText: "Delete Account Detail",
        type: "is-danger",
        hasIcon: true,
        onConfirm: async () => {
          console.log(accountDetailId);

          try {
            await api.delete("/AccountDetails/" + accountDetailId);
          } finally {
            this.$toast.open({
              duration: 1000,
              message: "Account Detail deleted!"
            });
            let index = this.data.findIndex(
              row => row.accountDetailId === accountDetailId
            );

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