<template>
  <div class="columnContainer">
    <div>
      <div class="container content">
        <section>
          <b-icon pack="fas" icon="tachometer-alt" type="is-danger" size="is-large"/>
          <div class="title is-2" style="display: inline-block; margin-left: 0.3em;">DASHBOARD</div>
          <div class="subtitle is-6" style="margin-top:1em; display: inline-block;">Records Overview</div>
          <button class="button is-info" style="float:right;" @click="changeView()">
            <b-icon
              pack="fas"
              icon="times-circle"
              type="is-white"
              custom-class="faa-parent animated-hover faa-flash animated"
              style="margin-right: 1em;"
            />Exit Dashboard
          </button>
        </section>
        <span class="dividerline" style="width:100%;"></span>
        <div class="tile is-ancestor" style="clear:both;">
          <div class="tile is-vertical is-8">
            <div class="tile">
              <div class="tile is-parent">
                <article class="tile is-child tileStyle notification is-info">
                  <div class="level-left">
                    <b-icon pack="fas" icon="calendar-alt" size="is-medium" type="is-light"></b-icon>
                    <p class="title is-4 createTitle" style="color:white;">Recent Activities</p>
                  </div>
                  <div style="margin-top:2em;">
                    <nav
                      class="panel"
                      style="background-color: white; color:black; font-size:10pt;"
                    >
                      <a
                        class="panel-block is-active"
                        v-for="(row, index) in this.allData"
                        v-bind:key="index"
                        v-bind:index="index"
                      >
                        <span class="panel-icon">
                          <b-icon
                            v-show="row.rowType == 2"
                            icon="clock"
                            aria-hidden="true"
                            type="is-success"
                          ></b-icon>
                          <b-icon
                            v-show="row.rowType == 3"
                            icon="clipboard"
                            aria-hidden="true"
                            type="is-danger"
                          ></b-icon>
                          <b-icon
                            v-show="row.rowType == 4"
                            pack="fas"
                            icon="user"
                            aria-hidden="true"
                            type="is-info"
                          ></b-icon>
                        </span>
                        <span style="margin:1em; width:100%;" v-show="row.rowType == 2">
                          <b>{{row.updatedBy}}</b>
                          updated Account Rate
                          <span
                            class="tag is-success"
                            style="float:right;"
                          >
                            <time-ago :refresh="60" :datetime="row.updatedAt"></time-ago>
                          </span>
                        </span>
                        <span style="margin:1em; width:100%;" v-show="row.rowType == 3">
                          <b>{{row.updatedBy}}</b> updated Account Detail
                          <span class="tag is-dark" style="float:right;">
                            <time-ago :refresh="60" :datetime="row.updatedAt"></time-ago>
                          </span>
                        </span>
                        <span style="margin:1em; width:100%;" v-show="row.rowType == 4">
                          <b>{{row.updatedBy}}</b> updated Customer Account
                          <span class="tag is-link" style="float:right;">
                            <time-ago :refresh="60" :datetime="row.updatedAt"></time-ago>
                          </span>
                        </span>
                      </a>
                    </nav>
                  </div>
                </article>
              </div>
              <div class="tile is-parent is-vertical">
                <article class="tile is-child tileStyle notification is-warning">
                  <div class="level-left">
                    <b-icon pack="fas" icon="clipboard" size="is-medium" type="is-danger"></b-icon>
                    <p class="title is-4 createTitle" style="color:white;">Session Synopses</p>
                  </div>

                  <section style="margin: 2em auto;">
                    <b-table
                      :data="isEmpty ? [] : dataSession"
                      :striped="isStriped"
                      :per-page="perPage"
                      :hoverable="isHoverable"
                      :current-page.sync="currentPage"
                      :default-sort-direction="defaultSortDirection"
                      default-sort="sessionName"
                    >
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
                        <b-table-column
                          field="sessionName"
                          label="Session Name"
                        >{{ props.row.sessionName }}</b-table-column>

                        <b-table-column
                          field="createdBy"
                          label="Created By"
                        >{{ props.row.createdBy }}</b-table-column>

                        <b-table-column field="updatedBy" label="Updated By">
                          <span>{{ props.row.updatedBy }}</span>
                        </b-table-column>

                        <b-table-column :visible="checkRole" field="visibility" label="Visible">
                          <b-icon pack="fas" icon="check" v-if="props.row.visibility"></b-icon>
                          <b-icon pack="fas" icon="times" v-else></b-icon>
                        </b-table-column>
                      </template>
                    </b-table>
                    <link
                      rel="stylesheet"
                      href="//cdn.materialdesignicons.com/2.5.94/css/materialdesignicons.min.css"
                    >
                    <link
                      rel="stylesheet"
                      href="https://use.fontawesome.com/releases/v5.2.0/css/all.css"
                    >
                  </section>
                  <router-link to="/ManageSessionSynopses">
                    <div
                      style="float:right; color:white;"
                      class="button is-warning"
                    >View More &nbsp;
                      <b-icon
                        pack="fas"
                        icon="arrow-right"
                        size="is-small"
                        type="is-danger"
                        custom-class="faa-parent animated-hover faa-horizontal animated"
                      ></b-icon>
                    </div>
                  </router-link>
                </article>
                <article class="tile tileStyle is-child notification is-primary">
                  <div class="level-left">
                    <b-icon pack="fas" icon="user" size="is-medium" type="is-warning"></b-icon>
                    <p class="title is-4 createTitle" style="color:white;">Customer Accounts</p>
                  </div>
                  <section style="margin: 2em auto;">
                    <b-table
                      :data="isEmpty ? [] : dataCustomers"
                      :striped="isStriped"
                      :per-page="perPage"
                      :hoverable="isHoverable"
                      :current-page.sync="currentPage"
                      :default-sort-direction="defaultSortDirection"
                      default-sort="updatedAt"
                    >
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
                        <b-table-column
                          field="accountName"
                          label="Customer"
                        >{{ props.row.accountName }}</b-table-column>

                        <b-table-column field="updatedBy" label="Updated By">
                          <span>{{ props.row.updatedBy }}</span>
                        </b-table-column>
                        <b-table-column field="updatedAt" label="Updated At" style="width:11%">
                          <span>{{ props.row.updatedAt}}</span>
                        </b-table-column>

                        <b-table-column :visible="checkRole" field="visibility" label="Visible">
                          <b-icon pack="fas" icon="check" v-if="props.row.visibility"></b-icon>
                          <b-icon pack="fas" icon="times" v-else></b-icon>
                        </b-table-column>
                      </template>
                    </b-table>
                  </section>
                  <router-link to="/ManageCustomerAccounts">
                    <div
                      style="float:right; color:white;"
                      class="button is-primary"
                    >View More &nbsp;
                      <b-icon
                        pack="fas"
                        icon="arrow-right"
                        size="is-small"
                        type="is-warning"
                        custom-class="faa-parent animated-hover faa-horizontal animated"
                      ></b-icon>
                    </div>
                  </router-link>
                </article>
              </div>
            </div>
            <!-- <div class="tile is-parent">
              <article class="tile is-child notification is-danger">
                <p class="title">Wide tile</p>
                <p class="subtitle">Aligned with the right tile</p>
                <div class="content">

                </div>
              </article>
            </div>-->
          </div>
          <div class="tile is-parent">
            <article class="tile is-child tileStyle notification is-dark">
              <div class="content">
                <div class="level-left">
                  <b-icon pack="fas" icon="clock" size="is-medium" type="is-warning"></b-icon>
                  <p class="title is-4 createTitle" style="color:white;">Account Rates/Details</p>
                </div>
                <div style="margin:2em auto;">
                  <b-datepicker
                    class="dateStyle"
                    inline
                    v-model="date"
                    :events="events"
                    indicators="dots"
                  ></b-datepicker>
                  <div style="clear:both; padding-top: 1em; font-size: 10pt; margin-left:2em;">
                    <span class="tag is-primary">&nbsp;</span>
                    <span class="tag is-info">&nbsp;</span>
                    Account Details: Start/End Dates
                  </div>
                  <div style="margin-top: 1em; font-size: 10pt; margin-left:2em;">
                    <span class="tag is-warning">&nbsp;</span>
                    <span class="tag is-danger">&nbsp;</span>
                    Account Rates: Start/End Dates
                  </div>
                </div>
              </div>
            </article>
          </div>
        </div>
      </div>
    </div>
    <link
      rel="stylesheet"
      href="node_modules\font-awesome-animation\dist\font-awesome-animation.min.css"
    >
  </div>
</template>

<script>
import api from "../_services/restful.service";
import moment from "moment";
import Vue from "vue";
import TimeAgo from "vue2-timeago";

const thisMonth = new Date().getMonth();

export default {
  components: {
    TimeAgo
  },
  data() {
    const dataSession = [];
    const dataCustomers = [];
    const dataAccountRates = [];
    const dataAccountDetails = [];
    const dataCustomersWithDate = [];
    const allData = [];
    const events = [];
    return {
      isDashboard: true,
      dataSession,
      dataCustomers,
      dataCustomersWithDate,
      allData,
      isFullPage: true,
      isEmpty: false,
      isPaginated: true,
      isStriped: true,
      isHoverable: true,
      isPaginationSimple: false,
      defaultSortDirection: "desc",
      currentPage: 1,
      perPage: 3,
      checkRole: false,
      date: new Date(),
      events
    };
  },
  created() {
    this.getAll();
  },
  methods: {
    changeView() {
      this.isDashboard = false;
      EventBus.$emit("changeView", this.isDashboard);
    },
    async getAll() {
      try {
        this.dataSession = await api.getAll("/SessionSynopses/");
        this.dataCustomers = await api.getAll("/CustomerAccounts/");
        this.dataCustomersWithDate = await api.getAll(
          "/CustomerAccounts/CustomerList/"
        );
        this.dataAccountRates = await api.getAll(
          "/CustomerAccounts/GetAllRates/"
        );
        this.dataAccountDetails = await api.getAll(
          "/AccountDetails/GetAllDetails/"
        );
      } finally {
        let user = JSON.parse(localStorage.getItem("user"));

        if (user != null) {
          if (user.user.roles == "Admin") {
            this.checkRole = true;
          } else {
            this.checkRole = false;
          }
        }
        const colorsAccRate = ["is-primary", "is-info"];
        const colorsAccDetail = ["is-danger", "is-warning"];
        var counterA = 0;
        var counterB = 0;
        for (var index in this.dataAccountDetails) {
          var detail = this.dataAccountDetails[index];

          detail.startDate = new Date(detail.startDate);
          if (detail.endDate == null || detail.endDate == "") {
            detail.endDate = null;
          } else {
            detail.endDate = new Date(detail.endDate);
            this.events.push({
              date: detail.endDate,
              type: colorsAccDetail[counterA]
            });
          }
          this.events.push({
            date: detail.startDate,
            type: colorsAccDetail[counterA]
          });
          counterA += 1;
          if (counterA > colorsAccDetail.indexOf(colorsAccDetail[counterA])) {
            counterA = 0;
          }
        }
        for (var index in this.dataAccountRates) {
          var rate = this.dataAccountRates[index];

          rate.startDate = new Date(rate.startDate);
          if (rate.endDate == null || rate.endDate == "") {
            rate.endDate = null;
          } else {
            rate.endDate = new Date(rate.endDate);
            this.events.push({
              date: rate.endDate,
              type: colorsAccRate[counterB]
            });
          }
          this.events.push({
            date: rate.startDate,
            type: colorsAccRate[counterB]
          });
          counterB += 1;
          if (counterB > colorsAccRate.indexOf(colorsAccRate[counterB])) {
            counterB = 0;
          }
        }

        this.allData = this.allData.concat(
          // this.dataSession,
          this.dataCustomersWithDate,
          this.dataAccountRates,
          this.dataAccountDetails
        );

        //assign a row type to each row in the list
        // 1 = session synopses type, 2 = account rate type etc
        for (var index in this.allData) {
          var row = this.allData[index];
          if (row.sessionId != null) {
            row.rowType = 1;
          } else if (row.rateId != null) {
            row.rowType = 2;
          } else if (row.accountDetailId != null) {
            row.rowType = 3;
          } else {
            row.rowType = 4;
          }
        }
        this.allData = this.allData.sort((a, b) => {
          const aDate = new Date(a.updatedAt);
          const bDate = new Date(b.updatedAt);

          return bDate.getTime() - aDate.getTime();
        });

        //as the tile has limited space, only 8 recent activites will be shown
        this.allData = this.allData.slice(0, 7);

        console.log(this.allData);
      }
    }
  }
};
</script>

<style>
.createTitle {
  margin-left: 1em;
}
.columnContainer {
  margin: 2em 0;
}
.columnItem {
  margin: 0 2em;
  padding: 0 auto;
  max-height: 500px;
}
.dateStyle {
  margin-left: 1em;
}
.panelColor {
  background-color: darkcyan;
  color: white;
}
.tileStyle {
  box-shadow: 3px 5px 15px -3px #8b8b8b;
}

.dividerline {
  display: block;
  height: 1px;
  border: 0;
  border-top: 1px solid #ccc;
  margin: 0 auto 1.5em;
  padding: 0;
}
</style>

