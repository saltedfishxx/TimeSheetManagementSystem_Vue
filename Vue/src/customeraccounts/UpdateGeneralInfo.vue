<template>
  <div class="panel container contentStyle">
    <div class="panel-heading level-left">
      <b-icon pack="fas" icon="plus" type="is-danger"></b-icon>
      <h2 class="subtitle is-4 updateTitle">Update Customer General Info</h2>
    </div>
    <div class="box boxStyle">
      <div class="formStyle">
        <label class="field-label is-medium">General Information</label>
       <div class="field is-horizontal fieldStyle">
          <div class="field-label is-normal">
            <label class="label">Account Name</label>
          </div>
          <div class="field-body">
            <div class="field">
              <div class="control">
                <input
                  v-model="customer.accountName"
                  class="input"
                  :class="{'input is-danger' : hasError }"
                  type="text"
                  placeholder="e.g. School"
                >
                <label :class="{'error' : hasError }" v-if="hasError">Account Name required</label>
              </div>
            </div>
          </div>
        </div>
       <div class="field is-horizontal fieldStyle2">
          <div class="field-label is-normal">
            <label class="label">Visibility</label>
          </div>
          <div class="field-body">
            <div class="field">
              <b-switch v-model="customer.visibility" type="is-info"></b-switch>
            </div>
          </div>
        </div>
        <div class="field is-horizontal fieldStyle">
          <div class="field-label is-normal">
            <label class="label">Comments</label>
          </div>
          <div class="field-body">
            <div class="field">
              <b-input aria-placeholder="comments" v-model="customer.comments" type="textarea"></b-input>
            </div>
          </div>
        </div>
      </div>
      <div class="field is-grouped is-grouped-centered buttonArrange">
        <p class="control">
          <button class="button is-primary" @click="updateGeneralInfo()">Update</button>
        </p>
        <router-link class="control" to="/ManageCustomerAccounts">
          <button class="button is-dark">Cancel</button>
        </router-link>
      </div>
    </div>
  </div>
</template>


<style>
.updateTitle {
  margin-left: 0.5em;
}
.contentStyle {
  margin-top: 5em; 
  margin-bottom: 2em; 
  width: 50%;
}
.boxStyle {
  margin-bottom: 5em;
}
.formStyle {
  width: 70%; 
  margin-left: 2em;
}
.fieldStyle {
  margin-top: 2em;
}
.fieldStyle2 {
  margin-top: 1em;
}
.buttonArrange {
  margin: 3em 0 1em;
}
.error {
  display: inline;
  color: #ff3860;
}
.labelError {
  margin-left: 20%;
}
</style>

<script>
import qs from "qs";
import axios from "axios";
import { router, authHeader, authHeaderUrlencoded } from "../_helpers";
import moment from "moment";

axios.defaults.headers["X-Requested-With"] = "XMLHttpRequest";

export default {
  data() {
    return {
      hasError: false,
      customer: {
        visibility: true
      }
    };
  },
  async created() {
    this.getCustomer();
  },
  methods: {
    getCustomer() {
      axios
        .get(
          "http://localhost:5000/api/CustomerAccounts/UpdateGeneralInfo/" +
            this.$route.params.customerAccountId,
          {
            headers: authHeader()
          }
        )
        .then(response => {
          // JSON responses are automatically parsed.
          this.customer = response.data;
        })
        .catch(e => {
          console.log(e);
        });
    },
    async updateGeneralInfo() {
      console.log("'" + JSON.stringify(this.customer) + "'");
      if (
        this.customer.accountName == null ||
        this.customer.accountName == ""
      ) {
        this.hasError = true;
      } else {
         this.hasError = false;
        axios
          .put(
            "http://localhost:5000/api/CustomerAccounts/UpdateGeneralInfo/" +
              this.$route.params.customerAccountId,

            qs.stringify(this.customer),
            {
              headers: authHeaderUrlencoded()
            }
          )
          .then(response => {
            console.log(response);
          })
          .catch(error => {
            console.log(error.response);
          });

        this.customer = {
          sessionName: "",
          comments: "",
          visibility: true
        };
        // return to manage customer page
        router.go(-1);
      }
    }
  }
};
</script>

