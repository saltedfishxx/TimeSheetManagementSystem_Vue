<template>
  <div style="padding:2em;">
    <div>
      <b-icon
        class="iconStyle"
        pack="fas"
        icon="exclamation-triangle"
        size="is-large"
        type="is-warning"
      ></b-icon>
      <h3 style="text-align:center;">
        Saved account detail record.
        <br>
        <b>Warning:</b> The data might
        <b>overlap</b> with
      </h3>
    </div>
    <br>
    <b-table :data="data">
      <template slot="empty">
        <section class="section">
          <div class="content has-text-grey has-text-centered">
            <p>
              <b-icon icon="emoticon-sad" size="is-large"></b-icon>
            </p>
            <p>Nothing here.</p>
          </div>
        </section>
      </template>
      <template slot-scope="props">
        <b-table-column field="Day" label="Day" sortable>{{ props.row.dayName }}</b-table-column>
        <b-table-column field="startTimeMin" label="Start Time" sortable>
          <span>{{ props.row.startTime }}</span>
        </b-table-column>
        <b-table-column field="endTimeMin" label="End Time" sortable>
          <span>{{ props.row.endTime }}</span>
        </b-table-column>
        <b-table-column :visible="checkRole" field="visibility" label="Visible" sortable>
          <b-icon pack="fas" icon="check" v-if="props.row.visibility"></b-icon>
          <b-icon pack="fas" icon="times" v-else></b-icon>
        </b-table-column>
        <b-table-column field="startDate" label="Effective Start Date" sortable>
          <span>{{ props.row.startDate}}</span>
        </b-table-column>
        <b-table-column field="endDate" label="Effective End Date" sortable>
          <span>{{ props.row.endDate }}</span>
        </b-table-column>
      </template>
    </b-table>
    <div style="text-align: right">
      <button class="button is-primary" @click="close()">Ok</button>
    </div>
  </div>
</template>


<script>
import qs from "qs";
import axios from "axios";
import { router, authHeader, authHeaderUrlencoded } from "../_helpers";
import moment from "moment";

axios.defaults.headers["X-Requested-With"] = "XMLHttpRequest";

var data = [];

export default {
  data() {
    return {
      data,
      isEmpty: false,
      checkRole: false
    };
  },
  created() {
    let user = JSON.parse(localStorage.getItem("user"));
    if (user != null) {
      if (user.user.roles == "Admin") {
        this.checkRole = true;
      } else {
        this.checkRole = false;
      }
    }

    this.data = JSON.parse(localStorage.getItem("clashList"));
  },
  methods: {
    close() {
      this.$parent.close();
      router.go(-1);
    }
  }
};
</script>
