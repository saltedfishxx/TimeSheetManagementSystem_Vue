<template>
  <div class="panel container" style="margin-top:3em">
    <section class="subtitle is-2 level-left sectionTitle">
      <b-icon pack="fas" icon="clipboard" type="is-danger" size="is-small"></b-icon>
      <h1 class="manageTitle">Manage Session Synopses</h1>
    </section>
    <nav class="breadcrumb" aria-label="breadcrumbs">
      <ul>
        <li>
          <router-link to="/">Home</router-link>
        </li>
        <li class="is-active">
          <router-link to="/ManageSessionSynopses" aria-current="page">Manage Session Synopses</router-link>
        </li>
      </ul>
    </nav>
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
          <router-link class="control" to="/CreateSessionSynopsis">
            <button class="button is-info">
              <b-icon pack="fas" icon="plus"></b-icon>
              <p>Add Session</p>
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
            sortable
          >{{ props.row.sessionName }}</b-table-column>

          <b-table-column field="createdBy" label="Created By" sortable>{{ props.row.createdBy }}</b-table-column>

          <b-table-column field="updatedBy" label="Updated By" sortable>
            <span>{{ props.row.updatedBy }}</span>
          </b-table-column>

          <b-table-column :visible="checkRole" field="visibility" label="Visible" sortable>
            <b-icon pack="fas" icon="check" v-if="props.row.visibility"></b-icon>
            <b-icon pack="fas" icon="times" v-else></b-icon>
          </b-table-column>

          <b-table-column custom-key="actions">
            <button class="button is-info" @click="editRow(props.row.sessionId)">
              <b-icon pack="fas" icon="edit"></b-icon>
              <span>Update</span>
            </button>
            <button class="button is-danger" @click="deleteRow(props.row.sessionId)">
              <b-icon pack="fas" icon="trash-alt"></b-icon>
              <span>Delete</span>
            </button>
          </b-table-column>
        </template>
      </b-table>
    </section>
  </div>
</template>

<script>
import { router, authHeader, authHeaderUrlencoded } from "../_helpers";
import api from "../_services/restful.service";

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
      defaultSortDirection: "desc",
      currentPage: 1,
      perPage: 5,
      checkRole: false
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
    async getAll() {
      try {
        this.data = await api.getAll("/SessionSynopses/");
      } finally {
        let user = JSON.parse(localStorage.getItem("user"));

        if (user != null) {
          if (user.user.roles == "Admin") {
            this.checkRole = true;
          } else {
            this.checkRole = false;
          }
        }
      }
    },
    editRow(sessionId) {
      router.push({ path: `/UpdateSessionSynopsis/${sessionId}` }); // -> /UpdateSessionSynopses/123
    },
    deleteRow(sessionId) {
      console.log(sessionId);
      this.$dialog.confirm({
        title: "Deleting Session Record",
        message:
          "Are you sure you want to <b>delete</b> this Session Record? This action cannot be undone.",
        confirmText: "Delete Session",
        type: "is-danger",
        hasIcon: true,
        onConfirm: async () => {
          console.log(sessionId);
          try {
            await api.delete("/SessionSynopses/" + sessionId);
          } finally {
            let index = this.data.findIndex(row => row.sessionId === sessionId);
            this.data.splice(index, 1);

            this.$toast.open({
              duration: 1000,
              message: "Session deleted!"
            });
          }
        }
      });
    }
  }
};
</script>

<style>
.sectionTitle,
.manageTitle {
  margin-left: 0.5em;
}
.panelStyle {
  margin: 0;
}
</style>
