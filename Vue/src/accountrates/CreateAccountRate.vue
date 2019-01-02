<template>
  <div class="panel container contentStyle">
    <div class="panel-heading level-left">
      <b-icon pack="fas" icon="plus" type="is-danger"></b-icon>
      <h2 class="subtitle is-4 createTitle">Create Account Rate</h2>
    </div>
    <div class="box boxStyle">
      <label class="field-label is-medium">Rate per Hour</label>
      <div class="formStyle">
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
              <label
                :class="{'error' : hasDateClash }"
                v-if="hasDateClash"
              >Dates overlap with existing account rates in customer.</label>
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
              <label
                :class="{'error' : hasDateClash }"
                v-if="hasDateClash"
              >Dates overlap with existing account rates in customer.</label>
            </div>
          </div>
        </div>
      </div>
      <div class="field is-grouped is-grouped-centered buttonArrange">
        <p class="control">
          <button class="button is-primary" @click="createAccount()">Create</button>
        </p>
        <a class="control" @click="$router.go(-1)">
          <button class="button is-dark">Cancel</button>
        </a>
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
    const today = new Date();
    return {
      customer: {},
      accountRates: [],
      hasDateClash: false,
      hasRateError: false,
      hasStartDateError: false,
      hasClashError: false,
      minDate: new Date(today.getFullYear(), today.getMonth(), today.getDate())
    };
  },
  created() {
    this.getAll();
  },
  methods: {
    getAll() {
      axios
        .get(
          "http://localhost:5000/api/CustomerAccounts/ManageAccountRates/" +
            this.$route.params.customerAccountId,
          {
            headers: authHeader()
          }
        )
        .then(response => {
          // JSON responses are automatically parsed.
          this.accountRates = response.data;
          //return data;
        })
        .catch(e => {
          console.log(e);
        });
    },
    async createAccount() {
      console.log("'" + JSON.stringify(this.customer) + "'");

      var dateA = moment(this.customer.startDate, "DD-MM-YYYY"); // replace format by your one
      var dateB = moment(this.customer.endDate, "DD-MM-YYYY");
      const numOnly = new RegExp("^[0-9]*$");
      var accRateStart;
      var accRateEnd;

      //validation
      this.hasDateClash = false;
      for (let i = 0; i < this.accountRates.length; i++) {
        accRateStart = moment(this.accountRates[i].startDate, "DD-MM-YYYY");
        accRateEnd = moment(this.accountRates[i].endDate, "DD-MM-YYYY");
        console.log(accRateStart, accRateEnd);
        console.log(dateA, dateB);
 
        if (dateB != null || dateB != "") {
          if (dateA.diff(accRateEnd) <= 0 && accRateStart.diff(dateB) <= 0) {
            this.hasDateClash = true;
          }
        } else {
          this.hasDateClash = false;
        }
      }

      if (numOnly.test(this.customer.rateHour) == false) {
        this.hasRateError = true;
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
        this.hasClashError == true ||
        this.hasDateClash == true
      ) {
      } else {
        this.hasRateError = false;
        this.hasStartDateError = false;
        this.hasClashError = false;
        this.hasDateClash == false;
        //send request to web api to post data
        axios
          .post(
            "http://localhost:5000/api/CustomerAccounts/CreateAccountRate/" +
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
          rateHour: "",
          startDate: null,
          endDate: null
        };
        // redirect to previous page
        router.go(-1);
      }
    }
  }
};
</script>

