<template>
  <div class="panel container contentStyle">
    <div class="panel-heading level-left">
      <b-icon pack="fas" icon="plus" type="is-danger"></b-icon>
      <h2 class="subtitle is-4 updateTitle">Update Account Rate</h2>
    </div>
    <div class="box boxStyle">
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
                  pattern="[0-9]"
                  placeholder="e.g. 100"
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
          <button
            class="button is-primary"
            @click="updateAccount(customer.customerAccountId)"
          >Update</button>
        </p>
        <div class="control">
          <button class="button is-dark" @click="back(customer.customerAccountId)">Cancel</button>
        </div>
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
import api from "../_services/restful.service";
import { router, authHeader, authHeaderUrlencoded } from "../_helpers";
import moment, { now } from "moment";

export default {
  data() {
    const today = new Date();
    return {
      customer: {},
      accountRates: [],
      collectedEndDate: null,
      minDate: new Date(today.getFullYear(), today.getMonth(), today.getDate()),
      hasRateError: false,
      hasStartDateError: false,
      hasClashError: false,
      hasDateClash: false
    };
  },
  async created() {
    this.get();
  },
  methods: {
    async getAll() {
      try {
        this.accountRates = await api.getAll(
          "/CustomerAccounts/ManageAccountRates/" +
            this.customer.customerAccountId
        );
      } finally {
      }
    },
    async get() {
      try {
        this.customer = await api.get(
          "/CustomerAccounts/UpdateAccountRates/" + this.$route.params.rateId
        );
      } finally {
        // convert to date
        this.customer.startDate = new Date(this.customer.startDate);
        if (this.customer.endDate == null || this.customer.endDate == "") {
          this.customer.endDate = null;
        } else {
          this.customer.endDate = new Date(this.customer.endDate);
        }
        this.getAll();
      }
    },
    async updateAccount(customerAccountId) {
      console.log("'" + JSON.stringify(this.customer) + "'");

      var dateA = moment(this.customer.startDate, "MM-DD-YYYY"); // replace format by your one
      var dateB = moment(this.customer.endDate, "MM-DD-YYYY");

      var accRateStart = null;
      var accRateEnd = null;

      //validation
      for (let i = 0; i < this.accountRates.length; i++) {
        if (this.accountRates[i].rateId != this.$route.params.rateId) {
          accRateStart = moment(this.accountRates[i].startDate, "DD-MM-YYYY");
          accRateEnd = moment(this.accountRates[i].endDate, "DD-MM-YYYY");

          console.log(accRateStart, accRateEnd);
          console.log(dateA, dateB);

          this.hasDateClash = false;
          if (dateB != null || dateB != "") {
            if (dateA.diff(accRateEnd) <= 0 && accRateStart.diff(dateB) <= 0) {
            }
          } else {
            this.hasDateClash = false;
          }
        }
      }

      if (this.customer.rateHour == null || this.customer.rateHour == 0) {
        this.hasRateError = true;
      } else {
        this.hasRateError = false;
      }
      if (this.customer.startDate === null) {
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

        try {
          await api.update(
            "/CustomerAccounts/UpdateAccountRates/" + this.$route.params.rateId,
            qs.stringify(this.customer)
          );
        } finally {
          this.customer = {
            rateHour: "",
            startDate: null,
            endDate: null
          };
          // go back to previous page (manage account rate)
          router.go(-1);
        }
      }
    },
    back(customerAccountId) {
      router.go(-1);
    }
  }
};
</script>

