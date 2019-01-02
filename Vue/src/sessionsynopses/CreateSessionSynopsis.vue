<template>
  <div class="panel container" style="margin-top:5em; width:50%">
    <div class="panel-heading level-left">
      <b-icon pack="fas" icon="plus" type="is-danger"></b-icon>
      <h2 class="subtitle is-4 createTitle">Create Session Synopsis</h2>
    </div>
    <div class="box">
      <div class="field is-horizontal" style="margin-top:2em">
        <div class="field-label is-normal">
          <label class="label">Session Name</label>
        </div>
        <div class="field-body">
          <div class="field">
            <div class="control">
              <input
                v-model="model.sessionName"
                class="input"
                :class="{'input is-danger':hasError || hasRegError}"
                name="sessionName"
                type="text"
                placeholder="e.g. Practice"
              >
               <label :class="{errorSessionName : hasError }" v-if="hasError" >Invalid input. Session name required</label>
               <label :class="{errorSessionName : hasRegError }" v-if="hasRegError" >Invalid input, special characters or spaces only not allowed.</label>
            
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
            <b-switch v-model="model.visibility" type="is-info"></b-switch>
          </div>
        </div>
      </div>
      <div class="field is-grouped is-grouped-centered">
        <p class="control">
          <button class="button is-primary" @click="createSession()">Create</button>
        </p>
        <router-link class="control" to="/ManageSessionSynopses">
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
.errorSessionName {
  display: inline-block;
  color: #ff3860;
}
</style>

<script>
import qs from "qs";
import axios from "axios";
import { router, authHeader, authHeaderUrlencoded } from "../_helpers";

axios.defaults.headers["X-Requested-With"] = "XMLHttpRequest";

export default {
  data() {
    return {
      isVisible: "Yes",
      hasError: false,
      hasRegError: false,
      model: {
        sessionName: "",
        visibility: true
      }
    };
  },
  methods: {
    async createSession() {
      this.model.sessionName = this.model.sessionName.trim();
      const nameReg = new RegExp("[^a-zA-Z0-9 ][^']*$")
      console.log("'" + JSON.stringify(this.model) + "'");
      if (this.model.sessionName === "") {
        this.hasError = true;
        this.hasRegError = false;
      }
      else if ( nameReg.test(this.model.sessionName) == true) {
        this.hasRegError = true;
        this.hasError = false;

      } else {
        this.hasError = false;
        this.hasRegError = false;
        axios
          .post(
            "http://localhost:5000/api/SessionSynopses/",

           qs.stringify(this.model),
            {
              headers: authHeaderUrlencoded()
            }
          )
          .then(response => {
            console.log(response);
            
        this.model = {
          sessionName: "",
          visibility: true
        };
        // go back to manage session synopses
        router.push("/ManageSessionSynopses");
          })
          .catch(error => {
            console.log(error);
                     this.$snackbar.open({
                    duration: 3000,
                    message: "Error: Session cannot be created",
                    type: 'is-warning',
                    position: 'is-top',  
                });
            
          });

      }
    }
  }
};
</script>

