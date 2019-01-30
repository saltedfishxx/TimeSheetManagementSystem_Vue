<template>
  <div class="panel container contentStyle">
    <div class="panel-heading level-left">
      <b-icon pack="fas" icon="plus" type="is-danger"></b-icon>
      <h2 class="subtitle is-4 createTitle">Create Customer Account</h2>
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
        <label class="field-label is-medium">Rate per Hour</label>
        <div class="field is-horizontal fieldStyle">
          <div class="field-label is-normal">
            <label class="label">Rate per Hour</label>
          </div>
          <div class="field-body">
            <div class="field">
              <div class="control">
                <input
                  v-model="customer.rateHour"
                  class="input"
                  :class="{'input is-danger' : hasRateError }"
                  type="number"
                  placeholder="e.g. 100"
                  min="0"
                  step="0.50"
                  pattern="[0-9]"
                >
                <label :class="{'error' : hasRateError }" v-if="hasRateError">Rate hour required</label>
              </div>
            </div>
          </div>
        </div>
        <div class="field is-horizontal fieldStyle">
          <div class="field-label is-normal">
            <label class="label">Effective Start Date</label>
          </div>
          <div>
            <div class="field-body">
              <b-datepicker
                v-model="customer.startDate"
                placeholder="Click to select..."
                :min-date="minDate"
                icon="calendar-today"
              ></b-datepicker>
            </div>
            <div class="labelError">
              <label
                :class="{'error' : hasStartDateError }"
                v-if="hasStartDateError"
              >Start Date required</label>
              <label
                :class="{'error' : hasClashError }"
                v-if="hasClashError"
              >End Date is eariler than start date</label>
            </div>
          </div>
        </div>
        <div class="field is-horizontal fieldStyle">
          <div class="field-label is-normal">
            <label class="label">Effective End Date</label>
          </div>
          <div>
            <div class="field-body">
              <b-datepicker
                v-model="customer.endDate"
                placeholder="Click to select..."
                :min-date="customer.startDate"
                icon="calendar-today"
              ></b-datepicker>
            </div>
            <div class="labelError">
              <label
                :class="{'error' : hasClashError }"
                v-if="hasClashError"
              >End Date is eariler than start date</label>
            </div>
          </div>
        </div>
      </div>
      <div class="field is-grouped is-grouped-centered buttonArrange">
        <p class="control">
          <button class="button is-primary" @click="createAccount()">Create</button>
        </p>
        <router-link class="control" to="/ManageCustomerAccounts">
          <button class="button is-dark">Cancel</button>
        </router-link>
      </div>
    </div>
  </div>
</template>


<style>
.createTitle {
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
.box {
  margin-top: 0;
}
</style>

<script>
import qs from "qs";
import { router, authHeader, authHeaderUrlencoded } from "../_helpers";
import moment from "moment";
import api from "../_services/restful.service";
import ManageCustomerAccountsVue from "./ManageCustomerAccounts.vue";

export default {
  data() {
    const today = new Date();
    return {
      hasError: false,
      hasRateError: false,
      hasStartDateError: false,
      hasClashError: false,
      minDate: new Date(today.getFullYear(), today.getMonth(), today.getDate()),
      customer: {
        visibility: true
      }
    };
  },
  methods: {
    async createAccount() {
      var dateA = moment(this.customer.startDate, "MM-DD-YYYY"); // replace format by your one
      var dateB = moment(this.customer.endDate, "MM-DD-YYYY");
      const numOnly = new RegExp("^[0-9]*$");

      //validation
      if (numOnly.test(this.customer.rateHour) == false) {
        this.hasRateError = true;
      } else {
        this.hasError = false;
      }
      if (
        this.customer.accountName == null ||
        this.customer.accountName == ""
      ) {
        this.hasError = true;
      } else {
        this.hasError = false;
      }
      if (
        this.customer.rateHour == null ||
        this.customer.rateHour == 0 ||
        this.customer.rateHour == ""
      ) {
        this.hasRateError = true;
      } else {
        this.hasRateError = false;
      }
      if (this.customer.startDate == null) {
        this.hasStartDateError = true;
      } else {
        this.hasStartDateError = false;
      }
      if (dateA.diff(dateB) > 0) {
        this.hasClashError = true;
      } else {
        this.hasClashError = false;
      }
      if (
        this.hasRateError == true ||
        this.hasStartDateError == true ||
        this.hasClashError == true
      ) {
      } else {
        this.hasRateError = false;
        this.hasStartDateError = false;
        this.hasClashError = false;

        try {
          await api
            .create("/CustomerAccounts/", qs.stringify(this.customer))
            .catch(error => {
              console.log(error);
              this.$snackbar.open({
                duration: 3000,
                message:
                  error.message +
                  ". There is an account with the existing name.",
                type: "is-warning",
                position: "is-top"
              });
            });
        } finally {
          this.customer = {
            sessionName: "",
            comments: "",
            rateHour: "",
            startDate: null,
            endDate: null,
            visibility: true
          };
          // go back to previous page
          router.push("/ManageCustomerAccounts");
        }
      }
    }
  }
};
</script>

