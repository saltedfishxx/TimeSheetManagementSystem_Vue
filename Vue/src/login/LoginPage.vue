<template>
  <div class="row">
    <div class="loginContainer level">
      <div id = "walk">
      <img src="src/assets/Annoying_Dog.gif" class="doggo lefty level-left">
      </div>
      <div class="box login">
        <section class="sectionTitle level-left">
          <b-icon pack="fas" icon="user" type="is-danger" size="is-medium"></b-icon>
          <h2 class="title is-3 loginTitle">Login</h2>
        </section>
        <div
          v-if="alert.message"
          class="alertMessage"
          :class="`alert ${alert.type}`"
        >{{alert.message}}</div>
        <div class="alert alert-info">
          <div class="level">
            <div class="level-left">Instructor Username: test
              <br>Password: test
            </div>
            <div class="level-right">Admin Username: admin
              <br>Password: admin
            </div>
          </div>
        </div>
        <form @submit.prevent="handleSubmit">
          <div class="form-group">
            <label for="username">Username</label>
            <input
              type="text"
              v-model="username"
              name="username"
              class="form-control"
              :class="{ 'is-invalid': submitted && !username }"
            >
            <div v-show="submitted && !username" class="invalid-feedback">Username is required</div>
          </div>
          <div class="form-group">
            <div class="field is-horizontal">
            <label for="password">Password</label>
            <div type="password" @click="switchVisibility">
              <b-icon v-if="eyeToggle" pack="fas" icon="eye-slash" size="is-small" style="margin-left:1em;"></b-icon>
              <b-icon v-else pack="fas" icon="eye" size="is-small" style="margin-left:1em;"></b-icon>
            </div>
            </div>
            <input
              :type="passwordFieldType"
              v-model="password"
              name="password"
              class="form-control"
              :class="{ 'is-invalid': submitted && !password }"
            >
            <div v-show="submitted && !password" class="invalid-feedback">Password is required</div>
          </div>
          <div class="form-group level">
            <button class="button is-primary arrange" :disabled="loggingIn">Login</button>
            <p>Note that only administrators can create accounts</p>
          </div>
        </form>
      </div>
      <img src="src/assets/Annoying_Dog_Sleeping.gif" class="doggo righty level-right">
    </div>
    
  </div>
</template>

<script>
export default {
  data() {
    return {
      username: "",
      password: "",
      submitted: false,
      passwordFieldType: 'password',
      eyeToggle: false
    };
  },
  computed: {
    loggingIn() {
      return this.$store.state.authentication.status.loggingIn;
    },
    activeUser() {
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
  created() {
    // reset login status
    this.$store.dispatch("authentication/logout");
  },
  methods: {
    switchVisibility() {
      this.passwordFieldType = this.passwordFieldType === 'password' ? 'text' : 'password'
      this.eyeToggle = this.eyeToggle === false ? true : false
    },
    handleSubmit(e) {
      this.submitted = true;
      const { username, password } = this;
      const { dispatch } = this.$store;
      if (username && password) {
        dispatch("authentication/login", { username, password });
      }
    }
  }
}

</script>

<style>
.login {
  margin-top: 5em;
  width: 600px;
}
.arrange {
  float: left;
}
.sectionTitle {
  margin-left: 0.2em;
  margin-bottom: 1.2em;
}
.loginTitle {
  margin-left: 0.5em;
}
.righty {
  position: relative;
    width:10%;
  height: auto;
  right: 10%;
    top: 35%;
}
.loginContainer{
  margin: 0 auto;
}

#walk {
  width: 10%;
  position: relative;
  top:30%;
  z-index: 2;
-webkit-animation:linear infinite;
-webkit-animation-name: run;
-webkit-animation-duration: 20s;
}     

@-webkit-keyframes run {
    0% { right: -120%;}
    39% {right: 18%; transform: rotateY(0deg);}
    40% {right: 20%; transform: rotateY(180deg);}
    55% {right: 20%;}
    89% {right: -109%; transform: rotateY(180deg);}
    90% {right: -120%; transform: rotateY(0deg)}
    100% {right:-120%;}
}
</style>
