<template>
    <div class="panel container" style="margin-top:5em; margin-bottom:2em; width:50%">
        <div class="panel-heading level-left">
            <b-icon pack="fas" icon="plus" type="is-danger"></b-icon>
            <h2 class="subtitle is-4" style="margin-left:0.5em">
              Create Customer Account 
            </h2>
        </div>
        <div class="box" style="margin-bottom:5em">
          <div style="width:70%; margin-left:2em">
          <label class="field-label is-medium">
            General Information
          </label>
        <div class="field is-horizontal" style="margin-top:2em">
            <div class="field-label is-normal">
             <label class="label">Account Name</label>
            </div>
        <div class="field-body">
            <div class="field">
                <div class="control">
                    <input v-model="customer.accountName" class="input" :class="{'input is-danger' : hasError }" type="text" placeholder="e.g. School">
                     <label :class="{'error' : hasError }" hidden>Account Name required</label>
                </div>
            </div>
        </div>
        </div>
         <div class="field is-horizontal" style="margin-top:1em">
            <div class="field-label is-normal">
             <label class="label">Visibility</label>
            </div>
        <div class="field-body">
           <div class="field">
            <b-switch v-model="customer.visibility"
                type="is-warning">
            </b-switch>
        </div>
        </div>
        </div>
         <div class="field is-horizontal" style="margin-top:2em">
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
    <div class="field is-grouped is-grouped-centered" style="margin:3em 0 1em">
            <p class="control">
                <button class="button is-primary" @click="updateGeneralInfo()">
                Update
                </button>
            </p>
            <a class="control" href="/ManageCustomerAccounts">
                <button class="button is-dark">
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
        .get("http://localhost:5000/api/CustomerAccounts/UpdateGeneralInfo/" + this.$route.params.customerAccountId)
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
    async updateGeneralInfo() {
      console.log("'" + JSON.stringify(this.customer) + "'");
      if(this.customer.accountName == null || this.customer.accountName == ""){
          this.hasError = true;
      }else {
      axios
        .put(
          "http://localhost:5000/api/CustomerAccounts/UpdateGeneralInfo/" + this.$route.params.customerAccountId,
          
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
        sessionName: "",
        comments: "",
        visibility: true
      };
      // object
      router.push({ path: '/ManageCustomerAccounts' })
    }
    }
  }
};
</script>

