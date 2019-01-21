<template>
  <div style="padding: 2em;">
    <b-table :data="isEmpty ? [] : data">
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
  </div>
</template>


<script>
import qs from "qs";
import axios from "axios";
import { router, authHeader, authHeaderUrlencoded } from "../_helpers";
import moment from "moment";

axios.defaults.headers["X-Requested-With"] = "XMLHttpRequest";

export default {
  data: function() {
    return {
      data: [],
      isEmpty: false,
      checkRole: true
    };
  },
  created() {
    this.getAll();
    console.log("invoked create method");
  },
  methods: {
    getAll() {
      let custId = JSON.parse(localStorage.getItem("customerID"));
      axios
        .get("http://localhost:5000/api/AccountDetails/" + custId, {
          headers: authHeader()
        })
        .then(response => {
          // JSON responses are automatically parsed.
          this.data = response.data;
          //return data;
          console.log(this.data);
          for (var index in this.data) {
            var row = this.data[index];

            //convert start time min to HH:MM AM/PM
            var startMin = row.startTimeMin;
            row.startTime = moment()
              .startOf("day")
              .add(startMin, "minutes")
              .format("hh:mm A");
            //console.log(row.startTime);

            //convert end time min to HH:MM AM/PM
            var endMin = row.endTimeMin;
            row.endTime = moment()
              .startOf("day")
              .add(endMin, "minutes")
              .format("hh:mm A");
            //console.log(row.endTime);

            //convert day number to day name
            switch (row.dayOfWeek) {
              case 1:
                row.dayName = "Sunday";
                break;
              case 2:
                row.dayName = "Monday";
                break;
              case 3:
                row.dayName = "Tuesday";
                break;
              case 4:
                row.dayName = "Wednesday";
                break;
              case 5:
                row.dayName = "Thursday";
                break;
              case 6:
                row.dayName = "Friday";
                break;
              case 7:
                row.dayName = "Saturday";
                break;
              default:
                row.dayName = "Unknown";
                break;
            }
          }
          let user = JSON.parse(localStorage.getItem("user"));
          if (user != null) {
            if (user.user.roles == "Admin") {
              this.checkRole = true;
            } else {
              this.checkRole = false;
            }
          }
        })
        .catch(e => {
          console.log(e);
        });
    }
  }
};
</script>
