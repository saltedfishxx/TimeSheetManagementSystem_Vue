<template>
  <div class="panel container" style="margin-top:3em">
    <section class="subtitle is-2 level-left sectionTitle">
      <b-icon pack="fas" icon="users" type="is-danger" size="is-small"></b-icon>
      <h1 class="manageTitle">Manage Users</h1>
    </section>
    <section>
      <div class="level panel-heading panelStyle">
        <div class="level-left">
          <b-field grouped group-multiline>
            <b-select v-model="defaultSortDirection">
              <option value="asc">Default sort direction: ASC</option>
              <option value="desc">Default sort direction: DESC</option>
            </b-select>
            <b-select v-model="perPage" :disabled="!isPaginated">
              <option value="5">5 per page</option>
              <option value="10">10 per page</option>
              <option value="15">15 per page</option>
              <option value="20">20 per page</option>
            </b-select>
          </b-field>
        </div>
        <div class="level-right">
          <router-link class="control" to="/CreateUser">
            <button class="button is-info">
              <b-icon pack="fas" icon="plus"></b-icon>
              <p>Add User</p>
            </button>
          </router-link>
        </div>
      </div>
      <b-table
        :data="isEmpty ? [] : data"
        :paginated="isPaginated"
        :per-page="perPage"
        :striped="isStriped"
        :hoverable="isHoverable"
        :current-page.sync="currentPage"
        :default-sort-direction="defaultSortDirection"
        default-sort="firstName"
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
          <b-table-column field="firstName" label="First Name" sortable>{{ props.row.firstName }}</b-table-column>

          <b-table-column field="lastName" label="Last Name" sortable>{{ props.row.lastName }}</b-table-column>

          <b-table-column field="username" label="Username" sortable>
            <span>{{ props.row.username }}</span>
          </b-table-column>

          <b-table-column field="roles" label="Roles" sortable>
            <span>{{ props.row.roles }}</span>
          </b-table-column>

          <b-table-column custom-key="actions">
            <button class="button is-info" @click="editRow(props.row.userId)">
              <b-icon pack="fas" icon="edit"></b-icon>
              <span>Update</span>
            </button>
            <button class="button is-danger" @click="deleteRow(props.row.userId)">
              <b-icon pack="fas" icon="trash-alt"></b-icon>
              <span>Delete</span>
            </button>
          </b-table-column>
        </template>
      </b-table>
      <link
        rel="stylesheet"
        href="//cdn.materialdesignicons.com/2.5.94/css/materialdesignicons.min.css"
      >
      <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.2.0/css/all.css">
    </section>
  </div>
</template>

<script>
import axios from "axios";
import { router, authHeader, authHeaderUrlencoded } from "../_helpers";
import { userService } from "../_services";

export default {
  data() {
    const data = [];
    return {
      data,
      isFullPage: true,
      isEmpty: false,
      isPaginated: true,
      isStriped: true,
      isHoverable: true,
      isPaginationSimple: false,
      defaultSortDirection: "asc",
      currentPage: 1,
      perPage: 5
    };
  },
  watch: {
    $route(to, from) {
      this.getAll();
    }
  },
  async created() {
    const loadingComponent = this.$loading.open({
      container: this.isFullPage ? null : this.$refs.element.$el
    });
    setTimeout(() => loadingComponent.close(), 0.5 * 1000);
    this.getAll();
  },
  methods: {
    getAll() {
      userService.getAll().then(res => {
        this.data = res;
      });
    },
    editRow(userid) {
      router.push({ path: `/UpdateUser/${userid}` }); // -> /UpdateSessionSynopses/123
    },
    deleteRow(userid) {
      console.log(userid);
      this.$dialog.confirm({
        title: "Deleting User Account",
        message:
          "Are you sure you want to <b>delete</b> this User Account? This action cannot be undone.",
        confirmText: "Delete User",
        type: "is-danger",
        hasIcon: true,
        onConfirm: () => {
          console.log(userid);
          userService
            .deleteUser(userid)
            .then(response => {
              // JSON responses are automatically parsed.
              let index = this.data.findIndex(row => row.userId === userid);
              this.data.splice(index, 1);
            })
            .catch(e => {
              console.log(e);
            });
          this.$toast.open("User deleted!");
        }
      });
    }
  }
};
</script>

<style>
.sectionTitle,
.manageTitle {
  margin-left: 1em;
}
.panelStyle {
  margin-top: 2em;
}
</style>
