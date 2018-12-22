<template>
    <div class="panel container" style="margin-top:5em; margin-bottom:2em; width:50%">
        <div class="panel-heading level-left">
            <b-icon pack="fas" icon="plus" type="is-danger"></b-icon>
            <h2 class="subtitle is-4" style="margin-left:0.5em">
              Update Account Rate
            </h2>
        </div>
        <div class="box" style="margin-bottom:5em">
          <div style="width:70%; margin-left:2em">
          <label class="field-label is-medium">
            Rate per Hour
          </label>
        <div class="field is-horizontal" style="margin-top:2em">
            <div class="field-label is-normal">
             <label class="label">Rate per Hour</label>
            </div>
        <div class="field-body">
            <div class="field">
                <div class="control">
                    <input v-model="customer.rateHour" class="input" :class="{'input is-danger':hasRateError}" type="number" placeholder="e.g. 100">
                    <label :class="{'error' : hasRateError }" hidden>Rate hour required</label>
                </div>
            </div>
        </div>
        </div>
        <div class="field is-horizontal" style="margin-top:2em">
            <div class="field-label is-normal">
             <label class="label">Effective Start Date</label>
            </div>
        <div class="field-body">
        <b-datepicker v-model="customer.startDate"
            placeholder="Click to select..." :min-date="minDate"
            icon="calendar-today">
        </b-datepicker>
       
        </div>
        </div>
         <div class="labelError">
            <label :class="{'error' : hasStartDateError }" hidden>Start Date required</label>
            <label :class="{'error' : hasClashError }" hidden>End Date is eariler than start date</label>   
        </div>
        <div class="field is-horizontal" style="margin-top:2em">
            <div class="field-label is-normal">
             <label class="label">Effective End Date</label>
            </div>
        <div class="field-body">
                <b-datepicker v-model="customer.endDate"
            placeholder="Click to select..." :min-date="minDate"
            icon="calendar-today">
        </b-datepicker>
            
        </div>
        </div>
         <div class="labelError">
        <label :class="{'error' : hasClashError }" hidden>End Date is eariler than start date</label>
        </div>
        
    </div>
    <div class="field is-grouped is-grouped-centered" style="margin:3em 0 1em">
            <p class="control">
                <button class="button is-primary" @click="updateAccount(customer.customerAccountId)">
                Update
                </button>
            </p>
            <a class="control">
                <button class="button is-dark" @click="back(customer.customerAccountId)">
                Cancel
                </button>
            </a>
            </div>
        </div>
    </div>
</template>


<style>
.error {
  display: inline;
  color: #ff3860;
}
.labelError{
    margin-left: 20%;
}
</style>

<script>
import qs from "qs";
import axios from "axios";
import { router } from '../_helpers';
import moment from 'moment';

axios.defaults.headers["X-Requested-With"] = "XMLHttpRequest";

export default {
  data() {
    const today = new Date()
    return {
      customer: {
       
      },
      minDate: new Date(today.getFullYear(), today.getMonth(), today.getDate()),
      hasRateError: false,
      hasStartDateError: false,
      hasClashError: false
    };
  },
  async created() {
    this.getAll();
  },
  methods: {
     getAll() {
      axios
        .get("http://localhost:5000/api/CustomerAccounts/UpdateAccountRates/" + this.$route.params.rateId)
        .then(response => {
          // JSON responses are automatically parsed.
          this.customer = response.data;
          
          //return data;
        })
        .catch(e => {
          console.log(e);
        });
      // .catch((error) => {
      //   console.log(error)
      // })
    },
    async updateAccount(customerAccountId) {
      console.log("'" + JSON.stringify(this.customer) + "'");

        var dateA = moment(this.customer.startDate, "MM-DD-YYYY"); // replace format by your one
        var dateB = moment(this.customer.endDate, "MM-DD-YYYY");
      if (this.customer.rateHour == null || this.customer.rateHour == 0) {
        this.hasRateError = true;
      } 
      if (this.customer.startDate == null) {
        this.hasStartDateError = true;
      } 
      if (dateA.diff(dateB) > 0) {
        this.hasClashError = true;
      }
      if (this.hasRateError == true || this.hasStartDateError == true || this.hasClashError == true) {

      }
      else {

      axios
        .put(
          "http://localhost:5000/api/CustomerAccounts/UpdateAccountRates/" + this.$route.params.rateId,
          
            "'" + JSON.stringify(
              this.customer)
             +"'"
          ,
          {
            headers: {
              "content-type": "application/json",
              'Access-Control-Allow-Origin': '*'
            }
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
      // object
      router.push({ path: `/ManageAccountRates/${customerAccountId}` })
      }
    },
     back(customerAccountId) {
      router.push({ path: `/ManageAccountRates/${customerAccountId}` }); 
    },
  }
};
</script>

