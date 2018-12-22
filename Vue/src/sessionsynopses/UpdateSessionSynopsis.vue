<template>
    <div class="panel container" style="margin-top:5em; width:50%">
        <div class="panel-heading">
            <h2 class="subtitle is-4">
            Update Session Synopsis 
            </h2>
        </div>
        <div class="box">
        <div class="field is-horizontal" style="margin-top:2em">
            <div class="field-label is-normal">
             <label class="label">Session Name</label>
            </div>
        <div class="field-body">
            <div class="field">
                <div class="control">
                    <input v-model="model.sessionName" class="input" :class="{'input is-danger':hasError}" type="text" placeholder="e.g. Practice">
                    <label :class="{'errorSessionName' : hasError }" hidden>Session name required</label>
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
            <b-switch v-model="model.visibility"
                type="is-warning">
            </b-switch>
        </div>
        </div>
        </div>
        <div class="field is-grouped is-grouped-centered">
            <p class="control">
                <button class="button is-primary" @click="updateSession()">
                Update
                </button>
            </p>
            <a class="control" href="/ManageSessionSynopses">
                <button class="button is-dark">
                Cancel
                </button>
            </a>
            </div>
    </div>
    </div>
</template>

<style>
.errorSessionName {
  display: inline;
  color: #ff3860;
}
</style>

<script>
import qs from "qs";
import axios from "axios";
import { router } from '../_helpers';

axios.defaults.headers["X-Requested-With"] = "XMLHttpRequest";

export default {
  data() {
    return {
      hasError: false,
      isVisible: "Yes",
      model: {
        sessionName: "",
        visibility: true
      }
    };
  },
  async created() {
    this.getSession();
  },
  methods: {
    async getSession() {
      axios
        .get(
          "http://localhost:5000/api/SessionSynopses/" +
            this.$route.params.sessionId
        )
        .then(response => {
          // JSON responses are automatically parsed.
          this.model = response.data;
          //return data;
        })
        .catch(e => {
          console.log(e);
        });
    },
    async updateSession() {
      console.log("'" + JSON.stringify(this.model) + "'");
      if (this.model.sessionName == null || this.model.sessionName == "") {
        this.hasError = true;
      } else {
        axios
          .put(
            "http://localhost:5000/api/SessionSynopses/" +
              this.$route.params.sessionId,

            "'" + JSON.stringify(this.model) + "'",
            {
              headers: {
                "content-type": "application/json"
              }
            }
          )
          .then(response => {
            console.log(response);
          })
          .catch(error => {
            console.log(error.response);
          });

        this.model = {
          sessionName: "",
          visibility: true
        };
        // object
        router.push({ path: "/ManageSessionSynopses" });
      }
    }
  }
};
</script>

