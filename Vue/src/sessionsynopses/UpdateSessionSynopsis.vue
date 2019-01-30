<template>
  <div class="panel container" style="margin-top:5em; width:50%">
    <div class="panel-heading">
      <h2 class="subtitle is-4">Update Session Synopsis</h2>
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
                type="text"
                placeholder="e.g. Practice"
              >
              <label
                :class="{errorSessionName : hasError }"
                v-if="hasError"
              >Invalid input. Session name required</label>
              <label
                :class="{errorSessionName : hasRegError }"
                v-if="hasRegError"
              >Invalid input, special characters or spaces only not allowed.</label>
              <p v-else></p>
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
            <b-switch v-model="model.visibility" type="is-warning"></b-switch>
          </div>
        </div>
      </div>
      <div class="field is-grouped is-grouped-centered">
        <p class="control">
          <button class="button is-primary" @click="updateSession()">Update</button>
        </p>
        <router-link class="control" to="/ManageSessionSynopses">
          <button class="button is-dark">Cancel</button>
        </router-link>
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
import { router, authHeader, authHeaderUrlencoded } from "../_helpers";
import api from "../_services/restful.service";

axios.defaults.headers["X-Requested-With"] = "XMLHttpRequest";

export default {
  data() {
    return {
      hasError: false,
      hasRegError: false,
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
      this.model = await api.get(
        "/SessionSynopses/" + this.$route.params.sessionId
      );
    },
    async updateSession() {
      this.model.sessionName = this.model.sessionName.trim();
      const nameReg = new RegExp("[^a-zA-Z0-9 ][^']*$");

      if (this.model.sessionName === "") {
        this.hasError = true;
        this.hasRegError = false;
      } else if (nameReg.test(this.model.sessionName) == true) {
        this.hasRegError = true;
        this.hasError = false;
      } else {
        this.hasError = false;
        this.hasRegError = false;

        try {
          await api.update(
            "/SessionSynopses/" + this.$route.params.sessionId,
            qs.stringify(this.model)
          );
        } finally {
          this.model = {
            sessionName: "",
            visibility: true
          };
          // return to previous page
          router.go(-1);
        }
      }
    }
  }
};
</script>

