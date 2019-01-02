<template>
  <div>
    <nav class="navbar is-primary is-transparent" role="navigation" aria-label="main navigation">
      <div class="navbar-brand">
        <!--icon -->
        <router-link class="navbar-item" to="/">
          <b-icon pack="fas" icon="clock" size="is-medium"></b-icon>
          <h2 style="padding-left:0.5em">Timesheet Management System</h2>
        </router-link>

        <a
          role="button"
          class="navbar-burger burger"
          aria-label="menu"
          aria-expanded="false"
          data-target="navbarBasicExample"
        >
          <span aria-hidden="true"></span>
          <span aria-hidden="true"></span>
          <span aria-hidden="true"></span>
        </a>
      </div>

      <div id="navbarBasicExample" class="navbar-menu">
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
        <router-view :key="$route.fullPath"></router-view>
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
// global.jQuery = require('jquery');
// var $ = global.jQuery;
// widows.$ = $;

export default {
  name: "app",
  computed: {
    activeUser() {
      console.log(this.$store.state.authentication.user)
      return this.$store.state.authentication.user;
    },
    isAdmin () {
      let user = this.$store.state.authentication.user;
      if (user != null) {
        if(user.user.roles == "Admin"){
          return true;
        }else {
          return false;
        }
      }
    }
  },
  // created(){
  // $(".navbar-burger").click(function() {
  
  //       // Toggle the "is-active" class on both the "navbar-burger" and the "navbar-menu"
  //       $(".navbar-burger").toggleClass("is-active");
  //       $(".navbar-menu").toggleClass("is-active");
  
  //   });
  
  // },
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
    }
  },
  watch: {
    $route(to, from) {
      // clear alert on location change
      this.$store.dispatch("alert/clear");
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