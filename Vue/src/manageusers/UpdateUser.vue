<template>
  <div class="row">
    <div class="col-sm-6 offset-sm-3">
      <div class="box signup">
        <section class="sectionTitle level-left">
          <b-icon pack="fas" icon="user-plus" type="is-danger" size="is-medium"></b-icon>
          <h2 class="title is-3 loginTitle">Update User</h2>
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

            <div v-show="submitted && !user.role" class="invalid-feedback">Role is required</div>
          </div>
          <div class="form-group">
            <button class="button is-primary arrange">Update User</button>
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
import { router, authHeader, authHeaderUrlencoded } from "../_helpers";
import { userService } from "../_services";
import moment from "moment";

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
      submitted: false
    };
  },
  created() {
    this.getUser();
  },
  methods: {
    getUser() {
      userService
        .get(this.$route.params.userid)
        .then(response => {
          // JSON responses are automatically parsed.
          this.user = response;
        })
        .catch(e => {
          console.log(e);
        });
    },
    update(collectedUser) {
      console.log("data", qs.stringify(this.user));
      userService
        .updateUser(collectedUser, this.$route.params.userid)
        .then(response => {
          console.log(response);
        })
        .catch(error => {
          console.log(error);
          this.$snackbar.open({
            duration: 3000,
            message: "Error: user could not be updated",
            type: "is-danger",
            position: "is-top",
            indefinite: true
          });
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
    handleSubmit(e) {
      this.submitted = true;
      if (
        this.user.lastname == null ||
        this.user.firstname == null ||
        this.user.roles == ""
      ) {
        this.submitted = false;
        this.$snackbar.open({
          duration: 3000,
          message: "Please fill in all the required fields",
          type: "is-danger",
          position: "is-top",
          indefinite: true
        });
      } else {
        this.update(this.user);
      }
    }
  }
};
</script>

<style>
.signup {
  margin-top: 5em;
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


