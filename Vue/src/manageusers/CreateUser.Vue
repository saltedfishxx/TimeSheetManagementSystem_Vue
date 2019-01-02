<template>
  <div class="row">
    <div class="col-sm-6 offset-sm-3">
      <div class="box signup">
        <section class="sectionTitle level-left">
          <b-icon pack="fas" icon="user-plus" type="is-danger" size="is-medium"></b-icon>
          <h2 class="title is-3 loginTitle">Add User</h2>
        </section>
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
          <div class="form-group">
            <label for="firstname">First Name</label>
            <input
              type="text"
              v-model="user.firstname"
              name="firstname"
              class="form-control"
              :class="{ 'is-invalid': submitted && !user.firstname }"
            >
            <div
              v-show="submitted && !user.firstname"
              class="invalid-feedback"
            >First Name is required</div>
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
            <label for="roles">Role</label>
            <select
              v-model="user.roles"
              name="role"
              class="form-control"
              :class="{ 'is-invalid': submitted && !user.roles }"
            >
              <option disabled value>Please select one</option>
              <option>Instructor</option>
              <option>Admin</option>
            </select>

            <div v-show="submitted && !user.roles" class="invalid-feedback">Role is required</div>
          </div>
          <div class="form-group">
            <br>
            <button class="button is-primary arrange">Create User</button>
              <router-link class="control" to="/ManageUsers">
          <button class="button is-dark" style="margin-left: 1em">Cancel</button>
        </router-link>
          </div>
          
        </form>
      </div>
    </div>
  </div>
</template>

<script>
import qs from "qs";
import axios from "axios";
import { router, authHeader, authHeaderUrlencoded } from "../_helpers";
import moment from "moment";

axios.defaults.headers["X-Requested-With"] = "XMLHttpRequest";

export default {
  data() {
    return {
      user: {
        firstname: "",
        lastname: "",
        username: "",
        password: "",
        roles: ""
      },
      passwordFieldType: 'password',
      eyeToggle: false,
      submitted: false
    };
  },
  methods: {
    signup(collectedUser) {
      console.log("data", qs.stringify(collectedUser));
      axios
        .post(
          "http://localhost:5000/api/Users/signup",

          qs.stringify(collectedUser),

          {
            headers: authHeaderUrlencoded()
          }
        )
        .then(response => {
          console.log(response);
        })
        .catch(error => {
          console.log(error);
           this.$snackbar.open({
                    duration: 3000,
                    message: error.response,
                    type: 'is-danger',
                    position: 'is-top',
                    indefinite: true,
                   
                })
        });

      this.user = {
        firstname: "",
        lastname: "",
        username: "",
        password: "",
        roles: ""
      };
      this.submitted = false;
      // object
      router.push({ path: "/ManageUsers" });
    },
    switchVisibility() {
      this.passwordFieldType = this.passwordFieldType === 'password' ? 'text' : 'password'
      this.eyeToggle = this.eyeToggle === false ? true : false
    },
    handleSubmit(e) {
      this.submitted = true;
      if (this.user.username == null || this.user.password == null || this.user.roles == "") {
        this.submitted = false;
         this.$snackbar.open({
                    duration: 3000,
                    message: "Please fill in all fields",
                    type: 'is-danger',
                    position: 'is-top',
                    indefinite: true,
                   
                })
      } else {
        this.signup(this.user);
      }
    }
  }
};
</script>

<style>
.signup {
  margin: 5em 0;
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
</style>

