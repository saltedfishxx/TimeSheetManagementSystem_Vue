<template>
<div class="app">
  <header>
  <nav class="navbar is-primary" role="navigation" aria-label="main navigation">
    <div class="navbar-brand">
      <!--icon -->
      <router-link class="navbar-item" to="/" >
         <b-icon pack="fas" icon="clock" size="is-medium"></b-icon>
         <h2 style="padding-left:0.5em">Timesheet Management System</h2>
      </router-link>

      <a role="button" class="navbar-burger burger" aria-label="menu" aria-expanded="false" data-target="navbarBasicExample">
        <span aria-hidden="true"></span>
        <span aria-hidden="true"></span>
        <span aria-hidden="true"></span>
      </a>
    </div>

    <div id="navbarBasicExample" class="navbar-menu">
      <div class="navbar-start" style="padding-left:1em">
        <router-link class="navbar-item" to="/">
          Home
        </router-link>
        <div class="navbar-item has-dropdown is-hoverable">
          <router-link class="navbar-link" to="/ManageSessionSynopses">
            Session Synopses
          </router-link>
          
          <div class="navbar-dropdown">
            <a class="navbar-item" href="/ManageSessionSynopses">
              Manage Session Sypnopses
            </a>
            <hr class="navbar-divider">
            <router-link class="navbar-item" to="/CreateSessionSynopsis">
              Create Session Sypnopsis
            </router-link>
          </div>
      </div>
       <div class="navbar-item has-dropdown is-hoverable">
          <router-link class="navbar-link" to='/ManageCustomerAccounts'>
            Customer Accounts
          </router-link>
          
          <div class="navbar-dropdown">
            <router-link class="navbar-item" to="/ManageCustomerAccounts">
              Manage Customer Accounts
            </router-link>
            <hr class="navbar-divider">
            <router-link class="navbar-item" to="/CreateCustomerAccount">
              Create Customer Account
            </router-link>
          </div>
      </div>
    </div>

    <div class="navbar-end">
     <div class="navbar-item buttons">
       <a href="#" class="button is-light" @click.prevent="login" v-if="!activeUser">Login</a>
       <a href="#" class="button is-light" @click.prevent="logout" v-else>Logout </a>
       </div>
  
    </div>
    </div>
  </nav>
  </header>
  <main>
    <div class="col-sm-6 offset-sm-3">
     <div v-if="alert.message" :class="`alert ${alert.type}`">{{alert.message}}</div>
    <transition name="fade" mode="out-in">
    <router-view>

    </router-view>
    </transition>
     </div>
  </main>
  <link rel="stylesheet" href="//cdn.materialdesignicons.com/2.5.94/css/materialdesignicons.min.css">
  <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.2.0/css/all.css">
  </div>
</template>

<script>
export default {
  name: "app",
  computed: {
        alert () {
            return this.$store.state.alert
        }
    },
     watch:{
        $route (to, from){
            // clear alert on location change
            this.$store.dispatch('alert/clear');
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

