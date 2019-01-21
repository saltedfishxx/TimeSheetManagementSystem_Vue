<template>
  <div class="panel container contentStyle">
    <div class="panel-heading level-left">
      <b-icon pack="fas" icon="plus" type="is-danger"></b-icon>
      <h2 class="subtitle is-4 createTitle">Create Account Detail</h2>
    </div>
    <div class="box boxStyle">
      <div style="text-align:center; margin-top: 1em;">
        <button
          v-show="CheckAdmin"
          class="button is-dark"
          @click="cardModal()"
        >Check existing Account Details</button>
      </div>
      <div class="formStyle">
        <div class="field is-horizontal fieldStyle">
          <div class="field-label is-normal">
            <label class="label">Day of week</label>
          </div>
          <div>
            <div class="field-body">
              <b-select
                v-model="accountDetail.dayOfWeek"
                placeholder="Select a day"
                style="padding-right:5.5em"
              >
                <option value="2">Monday</option>
                <option value="3">Tuesday</option>
                <option value="4">Wednesday</option>
                <option value="5">Thursday</option>
                <option value="6">Friday</option>
                <option value="7">Saturday</option>
                <option value="1">Sunday</option>
              </b-select>
            </div>
            <div class="labelError">
              <label :class="{'error' : hasDayError }" v-if="hasDayError">Please select a day</label>
            </div>
          </div>
        </div>
        <!-- todo: add form input -->
        <div class="field is-horizontal fieldStyle">
          <div class="field-label is-normal">
            <label class="label">Start Time</label>
          </div>
          <div>
            <div class="field-body">
              <b-timepicker
                v-model="accountDetail.startTime"
                placeholder="Click to select..."
                hour-format="12"
                icon="clock"
              >
                <div class="buttons">
                  <button class="button is-primary" @click="accountDetail.startTime = new Date()">
                    <b-icon icon="clock"></b-icon>
                    <span>Now</span>
                  </button>
                  
                  <button class="button is-danger" @click="accountDetail.startTime = null">
                    <b-icon icon="close"></b-icon>
                    <span>Clear</span>
                  </button>
                </div>
              </b-timepicker>
            </div>
            <div class="labelError">
              <label
                :class="{'error' : hasstartTimeError }"
                v-if="hasstartTimeError"
              >Please enter the start time</label>
            </div>
          </div>
        </div>

        <div class="field is-horizontal fieldStyle">
          <div class="field-label is-normal">
            <label class="label">End Time</label>
          </div>
          <div>
            <div class="field-body">
              <b-timepicker
                v-model="accountDetail.endTime"
                placeholder="Click to select..."
                :min-time="accountDetail.startTime"
                hour-format="12"
                icon="clock"
              >
                <div class="buttons">
                  <button class="button is-primary" @click="accountDetail.endTime = new Date()">
                    <b-icon icon="clock"></b-icon>
                    <span>Now</span>
                  </button>
                  
                  <button class="button is-danger" @click="accountDetail.endTime = null">
                    <b-icon icon="close"></b-icon>
                    <span>Clear</span>
                  </button>
                </div>
              </b-timepicker>
            </div>
            <div class="labelError">
              <label
                :class="{'error' : hasendTimeError }"
                v-if="hasendTimeError"
              >Please enter the end time</label>
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
                v-model="accountDetail.startDate"
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
                v-model="accountDetail.endDate"
                placeholder="Click to select..."
                :min-date="accountDetail.startDate"
                icon="calendar-today"
              ></b-datepicker>
            </div>
            <div class="labelError">
              <label
                :class="{'error' : hasStartDateError }"
                v-if="hasStartDateError"
              >Start Date required</label>
            </div>
          </div>
        </div>
      </div>
      <div class="field is-horizontal fieldStyle2">
        <div class="field-label is-normal" style="margin-left:3.5em">
          <label class="label">Visibility</label>
        </div>
        <div class="field-body">
          <div class="field">
            <b-switch v-model="accountDetail.visibility" type="is-info"></b-switch>
          </div>
        </div>
      </div>
      <div class="field is-grouped is-grouped-centered buttonArrange">
        <p class="control">
          <button class="button is-primary" @click="createDetail()">Create</button>
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
  padding-left: 4em;
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
.buttons {
  margin-top: 1em;
}
.iconStyle {
  float: left;
}
</style>

<script>
import qs from "qs";
import axios from "axios";
import { router, authHeader, authHeaderUrlencoded } from "../_helpers";
import moment from "moment";
import ModalTable from "./ModalTable";
import ClashTable from "./ClashTable";

axios.defaults.headers["X-Requested-With"] = "XMLHttpRequest";

const overlapDetails = [];
var startingTime = null;
var endingTime = null;

export default {
  components: {
    ClashTable: ClashTable,
    ModalTable: ModalTable
  },
  data() {
    const today = new Date();
    return {
      ClashTable,
      ModalTable,
      accountDetail: {
        visibility: true,
        startTimeinMin: 0,
        endTimeinMin: 0
      },
      hasstartTimeError: false,
      hasClashError: false,
      hasDayError: false,
      hasendTimeError: false,
      hasStartDateError: false,
      accountDetailList: [],
      overlapDetails,
      isOverlap: false,
      minDate: new Date(today.getFullYear(), today.getMonth(), today.getDate())
    };
  },
  computed: {
    CheckAdmin() {
      let user = JSON.parse(localStorage.getItem("user"));
      if (user != null) {
        if (user.user.roles == "Admin") {
          return true;
        } else {
          return false;
        }
      }
    }
  },
  methods: {
    cardModal() {
      this.$modal.open({
        parent: this,
        component: ModalTable,
        hasModalCard: false
      });
    },
    getAll() {
      axios
        .get(
          "http://localhost:5000/api/AccountDetails/" +
            this.$route.params.customerAccountId,
          {
            headers: authHeader()
          }
        )
        .then(response => {
          localStorage.setItem(
            "customerID",
            JSON.stringify(this.$route.params.customerAccountId)
          );

          // JSON responses are automatically parsed.
          this.accountDetailList = response.data;
          //return data;
          console.log(this.accountDetailList);
          for (var index in this.accountDetailList) {
            var row = this.accountDetailList[index];

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

          // check for overlapping time
          if (startingTime != null || endingTime != null) {
            var timeA = moment()
              .startOf("day")
              .add(startingTime, "minutes")
              .format("hh:mm A");

            var timeB = moment()
              .startOf("day")
              .add(endingTime, "minutes")
              .format("hh:mm A");

            console.log(timeA);

            if (
              timeA.localeCompare(row.endTime) <= 0 &&
              row.startTime.localeCompare(timeB) <= 0 &&
              row.dayOfWeek == this.accountDetail.dayOfWeek
            ) {
              this.overlapDetails.push(row);
              this.isOverlap = true;
              localStorage.setItem(
                "clashList",
                JSON.stringify(this.overlapDetails)
              );
            }

            if (this.isOverlap == true) {
              this.warningClash(this.overlapDetails);
            }
          }
        })
        .catch(e => {
          console.log(e);
        });
    },
    warningClash(overlapList) {
      this.$modal.open({
        parent: this,
        component: ClashTable,
        hasModalCard: false
      });
    },
    async createDetail() {
      console.log("'" + JSON.stringify(this.accountDetail) + "'");
      var dateA = moment(this.accountDetail.startDate, "DD-MM-YYYY"); // replace format by your one
      var dateB = moment(this.accountDetail.endDate, "DD-MM-YYYY");
      const numOnly = new RegExp("^[0-9]*$");

      //validation
      if (
        this.accountDetail.startDate == null ||
        this.accountDetail.startDate == ""
      ) {
        this.hasStartDateError = true;
      } else {
        this.hasStartDateError = false;
      }
      if (
        this.accountDetail.dayOfWeek == null ||
        this.accountDetail.dayOfWeek == ""
      ) {
        this.hasDayError = true;
      } else {
        this.hasDayError = false;
      }
      if (
        this.accountDetail.startTime == null ||
        this.accountDetail.startTime == ""
      ) {
        this.hasstartTimeError = true;
      } else {
        this.hasstartTimeError = false;
      }
      if (
        this.accountDetail.endTime == null ||
        this.accountDetail.endTime == ""
      ) {
        this.hasendTimeError = true;
      } else {
        this.hasendTimeError = false;
      }
      if (
        this.hasstartTimeError == true ||
        this.hasendTimeError == true ||
        this.hasDayError == true ||
        this.hasStartDateError == true
      ) {
      } else {
        this.hasstartTimeError = false;
        this.hasendTimeError = false;
        this.hasDayError = false;
        this.hasStartDateError = false;

        //convert time to time in minutes
        var startTimeHour = parseInt(
          new Date(this.accountDetail.startTime).getHours().toString()
        );
        var startTimeMin = parseInt(
          new Date(this.accountDetail.startTime).getMinutes().toString()
        );
        startingTime = startTimeHour * 60 + startTimeMin;
        this.accountDetail.startTimeinMin = startingTime;

        console.log(this.accountDetail.startTimeinMin);

        var endTimeHour = parseInt(
          new Date(this.accountDetail.endTime).getHours().toString()
        );
        var endTimeMin = parseInt(
          new Date(this.accountDetail.endTime).getMinutes().toString()
        );
        endingTime = endTimeHour * 60 + endTimeMin;
        this.accountDetail.endTimeinMin = endingTime;
        console.log(this.accountDetail.endTimeinMin);

        this.getAll();

        //send request to web api to post data
        axios
          .post(
            "http://localhost:5000/api/AccountDetails/CreateAccountDetail/" +
              this.$route.params.customerAccountId,

            qs.stringify(this.accountDetail),
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

        this.accountDetail = {
          dayOfWeek: null,
          startTime: null,
          endTime: null,
          startTimeinMin: null,
          endTimeinMin: null,
          startDate: null,
          endDate: null,
          visibility: false
        };
        // redirect to previous page
        if (this.isOverlap != true) {
          router.go(-1);
        }
      }
    }
  }
};
</script>

