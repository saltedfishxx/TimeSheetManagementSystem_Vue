<template>
  <div class="row">
    <div class="col-sm-6 offset-sm-3">
      <div class="box">
        <h2>Sign Up</h2>
        <form @submit.prevent="handleSubmit">
          <div class="form-group">
            <label for="username">Username</label>
            <input
              type="text"
              v-model="user.username"
              name="username"
              class="form-control"
              :class="{ 'is-invalid': submitted && !user.username }"
            >
            <div v-show="submitted && !user.username" class="invalid-feedback">Username is required</div>
          </div>
          <div class="form-group">
            <label for="password">Password</label>
            <input
              type="password"
              v-model="user.password"
              name="password"
              class="form-control"
              :class="{ 'is-invalid': submitted && !user.password }"
            >
            <div v-show="submitted && !user.password" class="invalid-feedback">Password is required</div>
          </div>
          <div class="form-group">
            <label for="firstname">First Name</label>
            <input
              type="text"
              v-model="user.firstname"
              name="firstname"
              class="form-control"
              :class="{ 'is-invalid': submitted && !user.firstname }"
            >
            <div v-show="submitted && !user.firstname" class="invalid-feedback">First Name is required</div>
          </div>
          <div class="form-group">
            <label for="lastname">Last Name</label>
            <input
              type="text"
              v-model="user.lastname"
              name="lastname"
              class="form-control"
              :class="{ 'is-invalid': submitted && !user.lastname }"
            >
            <div v-show="submitted && !user.lastname" class="invalid-feedback">Last Name is required</div>
          </div>
          <div class="form-group">
            <button class="button is-primary">Sign Up</button>
           
          </div>
        </form>
      </div>
    </div>
  </div>
</template>

<script>
import qs from "qs";
import axios from "axios";
import { router } from '../_helpers';
import moment from 'moment';

axios.defaults.headers["X-Requested-With"] = "XMLHttpRequest";

export default {
  data() {
    return {
    user: {
      firstname: "",
      lastname: "",
      username: "",
      password: ""
    },
      submitted: false
    };
  },
  created () {
        // reset login status
        this.$store.dispatch('authentication/logout');
    },
  methods: {
    signup(collectedUser){
      console.log('data', qs.stringify(collectedUser));
        axios
        .post(
          "http://localhost:5000/api/Users/signup",
          
            
          qs.stringify(collectedUser),
            
        
          {
            headers: {
              "content-type": "application/x-www-form-urlencoded",
              'Access-Control-Allow-Origin': '*'
            }
          }
        )
        .then(response => {
          console.log(response);
        })
        .catch(error => {
          console.log(error);
        });

      this.user = {
         firstname: "",
      lastname: "",
      username: "",
      password: ""
      };
      this.submitted = false;
      // object
        router.push({ path: '/login' })
    },
        handleSubmit(e) {
      this.submitted = true;
      if (this.user.username == null && this.user.password == null) {
        this.submitted = false;
      }else{
          this.signup(this.user);
      }
    }
  }
};
</script>
