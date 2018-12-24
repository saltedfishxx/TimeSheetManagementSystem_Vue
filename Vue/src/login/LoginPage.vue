<template>
<div class="row">
        <div class="col-sm-6 offset-sm-3">
    <div class="box">
        <section class="sectionTitle level-left">
        <b-icon pack="fas" icon="user" type="is-danger" size="is-medium"></b-icon>
        <h2 class="title is-3 loginTitle">Login</h2>
        </section>
        <div v-if="alert.message" class="alertMessage" :class="`alert ${alert.type}`">{{alert.message}}</div>
        <div class="alert alert-info">
            Username: test<br />
            Password: test
        </div>
        <form @submit.prevent="handleSubmit">
            <div class="form-group">
                <label for="username">Username</label>
                <input type="text" v-model="username" name="username" class="form-control" :class="{ 'is-invalid': submitted && !username }" />
                <div v-show="submitted && !username" class="invalid-feedback">Username is required</div>
            </div>
            <div class="form-group">
                <label htmlFor="password">Password</label>
                <input type="password" v-model="password" name="password" class="form-control" :class="{ 'is-invalid': submitted && !password }" />
                <div v-show="submitted && !password" class="invalid-feedback">Password is required</div>
            </div>
            <div class="form-group level">
                <button class="button is-primary arrange" :disabled="loggingIn">Login</button>
                <p>Have not created an account? <router-link to="/signup">Sign Up</router-link></p>
            </div>
        </form>
    </div>
        </div>
</div>
</template>

<script>
export default {
    data () {
        return {
            username: '',
            password: '',
            submitted: false
        }
    },
    computed: {
        loggingIn () {
            return this.$store.state.authentication.status.loggingIn;
        },
        activeUser () {
            return this.$store.state.authentication.user;
        },
    alert() {
      return this.$store.state.alert;
  },
  watch: {
    $route(to, from) {
      // clear alert on location change
      this.$store.dispatch("alert/clear");
    }
  }
    },
    created () {
        // reset login status
        this.$store.dispatch('authentication/logout');
    },
    methods: {
        handleSubmit (e) {
            this.submitted = true;
            const { username, password } = this;
            const { dispatch } = this.$store;
            if (username && password) {
                dispatch('authentication/login', { username, password });
            }
        }
    }
};
</script>

<style>
.box{
    margin-top: 5em;
}
.arrange {
  float: left;
}
.sectionTitle{
    margin-left: 0.2em;
    margin-bottom: 1.2em;
}
.loginTitle{
    margin-left: 0.5em;
}
</style>
