<template>
  <div>
    <nav class="navbar is-primary is-transparent" role="navigation" aria-label="main navigation">
      <div class="navbar-brand">
        <router-link class="navbar-item" to="/">
          <b-icon pack="fas" icon="clock" size="is-medium"></b-icon>
          <h2 style="padding-left:0.5em">Timesheet Management System</h2>
        </router-link>

        <a
          role="button"
          class="navbar-burger navbar-end"
          aria-label="menu"
          @click="showNav = !showNav"
          :class="{ 'is-active': showNav }"
          data-target="navbarBasicExample"
        >
          <span aria-hidden="true"></span>
          <span aria-hidden="true"></span>
          <span aria-hidden="true"></span>
        </a>
      </div>

      <div id="navbarBasicExample" class="navbar-menu" :class="{ 'is-active': showNav }">
        <div class="navbar-start" style="padding-left:1em" v-show="activeUser">
          <router-link class="navbar-item" to="/">Home</router-link>
          <router-link class="navbar-item" to="/ManageUsers" v-show="isAdmin">Users</router-link>
          <div class="navbar-item has-dropdown is-hoverable">
            <router-link class="navbar-link" to="/ManageSessionSynopses">Session Synopses</router-link>

            <div class="navbar-dropdown">
              <router-link class="navbar-item" to="/ManageSessionSynopses">Manage Session Sypnopses</router-link>
              <hr class="navbar-divider">
              <router-link class="navbar-item" to="/CreateSessionSynopsis">Create Session Sypnopsis</router-link>
            </div>
          </div>
          <div class="navbar-item has-dropdown is-hoverable">
            <router-link class="navbar-link" to="/ManageCustomerAccounts">Customer Accounts</router-link>

            <div class="navbar-dropdown">
              <router-link class="navbar-item" to="/ManageCustomerAccounts">Manage Customer Accounts</router-link>
              <hr class="navbar-divider">
              <router-link class="navbar-item" to="/CreateCustomerAccount">Create Customer Account</router-link>
            </div>
          </div>
        </div>

        <div class="navbar-end">
          <div class="navbar-item">
            <b-dropdown ref="dropdown" position="is-bottom-left" v-show="activeUser">
              <a class="navbar-item" slot="trigger">
                <b-icon
                  v-if="this.notifData != null"
                  pack="fas"
                  icon="bell"
                  type="is-white"
                  custom-class="faa-parent animated-hover  faa-ring animated"
                ></b-icon>
                <b-icon v-else pack="fas" icon="bell" type="is-white"></b-icon>
              </a>

              <b-dropdown-item custom style="text-align:right">
                <p v-if="this.notifData == null">You have no notifications.</p>
                <b-message
                  size="is-small"
                  style="overflow-wrap: break-word;"
                  v-for="(row, index) in this.notifData"
                  v-bind:key="index"
                  v-bind:index="index"
                  :type="{'is-info': row.notifType == 1 || row.notifType == 2, 'is-warning': row.notifType == 3}"
                >{{ row.message}}</b-message>
                <button
                  v-if="this.notifData != null"
                  class="button is-danger"
                  @click="checkOpen()"
                >Clear</button>
              </b-dropdown-item>
            </b-dropdown>
          </div>
          <div class="navbar-item">
            <p>{{ currentUser() }}</p>
          </div>
          <div class="navbar-item buttons">
            <router-link
              to="/login"
              class="button is-light"
              @click.prevent="login"
              v-if="!activeUser"
            >Login</router-link>
            <p to="/login" class="button is-light" @click="logout" v-else>Logout</p>
          </div>
        </div>
      </div>
    </nav>
    <div class="container">
      <transition name="fade" mode="out-in">
        <router-view></router-view>
      </transition>
    </div>
    <link
      rel="stylesheet"
      href="//cdn.materialdesignicons.com/2.5.94/css/materialdesignicons.min.css"
    >
    <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.2.0/css/all.css">
  </div>
</template>

<script>
import api from "../_services/restful.service";
import moment from "moment";

export default {
  name: "app",
  data() {
    const notifData = [];
    return {
      showNav: false,
      detailsActive: false,
      ratesActive: false,
      notifData
    };
  },
  computed: {
    activeUser() {
      console.log(this.$store.state.authentication.user);
      return this.$store.state.authentication.user;
    },
    isAdmin() {
      let user = this.$store.state.authentication.user;
      if (user != null) {
        if (user.user.roles == "Admin") {
          return true;
        } else {
          return false;
        }
      }
    }
  },
  methods: {
    login() {},
    async logout() {
      this.$dialog.confirm({
        title: "Log Out",
        message: "Are you sure you want to <b>Logout</b>?",
        confirmText: "Logout",
        type: "is-danger",
        hasIcon: true,
        onConfirm: () => {
          this.$toast.open("Logging out...");
          this.$router.push("/login");
        }
      });
    },
    currentUser: function() {
      // `this` points to the vm instance
      let user = JSON.parse(localStorage.getItem("user"));
      if (user != null) {
        console.log(user);
        if (this.$store.state.authentication.user) {
          return "Welcome, " + user.user.userName;
        } else {
          return "";
        }
      } else {
        return "";
      }
    },
    checkOpen() {
      console.log("clear notif");
      this.notifData = null;
    },
    async notifications() {
      //get all list with customer name
      var accountDetailsList = [];
      var accountRatesList = [];
      var specificDetails = [];

      //get number of active records
      var numActiveDetails = 0;
      var numActiveRates = 0;
      var numOverlap = 0;

      //get customer name of active records
      var activeDetails = "";
      var activeDetailsList = [];
      var activeRates = "";
      var activeRatesList = [];
      var overlapCust = "";
      var overlapCustList = [];

      //notification message
      var notifAccountDetail = "";
      var notifAccountRate = "";
      var notifOverlap = "";

      //get data using api
      accountRatesList = await api.getAll("/CustomerAccounts/GetAllRates/");
      accountDetailsList = await api.getAll("/AccountDetails/GetAllDetails/");

      //iterate through account rate list to check for active records
      for (var index in accountRatesList) {
        var rate = accountRatesList[index];

        var starting = moment(rate.startDate, "DD-MM-YYYY"); // replace format by your one
        var ending = moment(rate.endDate, "DD-MM-YYYY");
        var today = moment(new Date());
        if (today.diff(starting) >= 0 || today.diff(ending) >= 0) {
          numActiveRates += 1;
          activeRatesList.push(rate.accountName);
        }
      }

      //iterate through account details list to check for active records
      for (var index in accountDetailsList) {
        var detail = accountDetailsList[index];

        var starting = moment(detail.startDate, "DD-MM-YYYY"); // replace format by your one
        var ending = moment(detail.endDate, "DD-MM-YYYY");
        var today = moment(new Date());
        if (today.diff(starting) >= 0 || today.diff(ending) >= 0) {
          numActiveDetails += 1;
          activeDetailsList.push(detail.accountName);
        }
      }

      // check for clashing records
      //get list of cust id
      for (var index in accountDetailsList) {
        specificDetails.push(accountDetailsList[index].custID);
      }

      //check for clashing records per cust
      for (var name in specificDetails) {
        var filteredArray = await api.getAll(
          "/AccountDetails/GetFiltered/" + name
        );
        for (var i = 0; i < filteredArray.length; i++) {
          var a = [];
          var b = [];
          if (i + 1 != filteredArray.length) {
            a = filteredArray[i];
            b = filteredArray[i + 1];
            var starttimeA = moment()
              .startOf("day")
              .add(a.startTimeMin, "minutes")
              .format("hh:mm A");

            var endtimeA = moment()
              .startOf("day")
              .add(a.endTimeMin, "minutes")
              .format("hh:mm A");

            var starttimeB = moment()
              .startOf("day")
              .add(b.startTimeMin, "minutes")
              .format("hh:mm A");

            var endtimeB = moment()
              .startOf("day")
              .add(b.endTimeMin, "minutes")
              .format("hh:mm A");

            if (
              starttimeA.localeCompare(endtimeB) <= 0 &&
              starttimeB.localeCompare(endtimeA) <= 0 &&
              a.dayOfWeek == b.dayOfWeek
            ) {
              numOverlap += 1;
              if (a.accountName != null || a.accountName != undefined) {
                overlapCustList.push(a.accountName);
              }
            }
          }
        }
      }

      //Generate notif message
      //remove duplicates
      activeDetailsList = activeDetailsList.filter(function(item, i, ar) {
        return ar.indexOf(item) === i;
      });
      activeRatesList = activeRatesList.filter(function(item, i, ar) {
        return ar.indexOf(item) === i;
      });
      overlapCustList = overlapCustList.filter(function(item, i, ar) {
        return ar.indexOf(item) === i;
      });

      //add them into a string
      for (var i = 0; i < activeDetailsList.length; i++) {
        if (activeDetails != "") {
          activeDetails += ", " + activeDetailsList[i];
        } else {
          activeDetails = activeDetailsList[i];
        }
      }

      for (var i = 0; i < activeRatesList.length; i++) {
        if (activeRates != "") {
          activeRates += ", " + activeRatesList[i];
        } else {
          activeRates = activeRatesList[i];
        }
      }

      for (var i = 0; i < overlapCustList.length; i++) {
        if (overlapCust != "") {
          overlapCust += ", " + overlapCustList[i];
        } else {
          overlapCust = overlapCustList[i];
        }
      }

      notifAccountDetail =
        "You have " +
        numActiveDetails +
        " Account Details that are active in " +
        activeDetails;
      notifAccountRate =
        "You have " +
        numActiveRates +
        " Account Rates that are active in " +
        activeRates;

      if (numOverlap != 0) {
        notifOverlap =
          "You have " +
          numOverlap +
          " overlapping Account Details in " +
          overlapCust;
      }
      if (numActiveDetails != 0 || numActiveRates != 0 || numOverlap != 0) {
        this.notifData = [
          {
            notifType: 1,
            number: numActiveDetails,
            message: notifAccountDetail
          },
          {
            notifType: 2,
            number: numActiveRates,
            message: notifAccountRate
          },
          { notifType: 3, number: numOverlap, message: notifOverlap }
        ];
      }
    }
  },
  created() {
    this.notifications();
  },
  watch: {
    $route(to, from) {
      const pages = [
        "/ManageAccountDetails/" + this.$route.params.customerAccountId,
        "/CreateAccountRate/" + this.$route.params.customerAccountId,
        "/CreateAccountDetail/" + this.$route.params.customerAccountId,
        "/UpdateAccountDetail/" + +this.$route.params.customerAccountId
      ];
      const needsRefresh = pages.includes(to.path);

      if (needsRefresh) {
        this.notifications();
      }
    }
  }
};
</script>

<style lang ="scss">
@import "~bulma/sass/utilities/_all";
// Import Bulma and Buefy styles
@import "~bulma";
@import "~buefy/src/scss/buefy";

.fade-enter-active,
.fade-leave-active {
  transition-duration: 0.3s;
  transition-property: opacity;
  transition-timing-function: ease;
}

.fade-enter,
.fade-leave-active {
  opacity: 0;
}
</style>