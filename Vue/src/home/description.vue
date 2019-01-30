<template>
  <div class="hello">
    <div>
      <div class="container content">
        <div class="blocks" id="left-block">
          <div class="sectionContent has-text-grey-dark">
            <h1 class="subtitle is-2 has-text-grey-dark">TimeSheet Management System</h1>
            <br>This TimeSheet Management solution is often needed by small business enterprises which provide:
            <br>
            <ul>
              <li>classes that centers around performing arts (dance, singing etc.)</li>
              <li>tuition on school subjects</li>
            </ul>
            <p></p>
          </div>
          <div>
            <br>
            <h3 class="subtitle is-4 has-text-grey-dark">User Role information</h3>
            <ul class="has-text-grey-dark">
              <li>
                The administrator user can logon to the system to manage class lesson records,
                account records, view instructor time sheet, manage class lesson schedules
              </li>
              <li>
                The officer user can logon to the system to submit their time-in and time-out
                information, view their class schedules.
              </li>
            </ul>
          </div>
        </div>
        <div class="blocks" id="right-block">
          <img src="src\assets\logo.png" width="400">
        </div>
      </div>
    </div>
    <div class="dashboardBtn divided">
      <span class="divider"></span>
      <a class="dashboardLink has-text-primary subtitle is-5" @click="changeView()">
        <b-icon
          pack="fas"
          icon="angle-double-down"
          type="is-info"
          custom-class="faa-parent animated-hover faa-bounce animated"
        ></b-icon>
        <span>View Dashboard</span>
      </a>
      <span class="divider"></span>
    </div>
    <link
      rel="stylesheet"
      href="node_modules\font-awesome-animation\dist\font-awesome-animation.min.css"
    >
  </div>
</template>

<script>
export default {
  data() {
    return {
      isDashboard: false
    };
  },
  computed: {
    user() {
      return this.$store.state.authentication.user;
    },
    users() {
      return this.$store.state.users.all;
    }
  },
  created() {
    this.$store.dispatch("users/getAll");
  },
  methods: {
    changeView() {
      this.isDashboard = true;
      EventBus.$emit("changeView", this.isDashboard);
    }
  }
};
</script>

<style>
.blocks {
  display: inline-block;
}
.sectionContent {
  padding-top: 4em;
}
.dashboardBtn {
  width: 100%;
  text-align: center;
  margin-top: 7em;
  clear: both;
}
.dashboardLink {
  padding-top: 1em;
  margin-bottom: 0;
}
#left-block {
  width: 60%;
  float: left;
}
#right-block {
  padding-top: 6em;
  padding-left: 2em;
}

.divided {
  display: flex;
  align-items: center;
}

.divider {
  flex-grow: 1;
  border-bottom: 1px solid lightgray;
  margin: 5px 20px;
}
</style>
