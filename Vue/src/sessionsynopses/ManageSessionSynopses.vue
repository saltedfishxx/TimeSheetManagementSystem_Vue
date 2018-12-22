<template>
    <div class="panel container" style="margin-top:3em">
        <section class="subtitle is-2 level-left" style="margin-left:0.5em">
            <b-icon pack="fas" icon="clipboard" type="is-danger" size="is-small"></b-icon>
            <h1 style="margin-left:0.5em">Manage Session Synopses</h1>
        </section>
        <section>
            <div class="level panel-heading" style="margin-top:2em">
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
            <a class="control" href="/CreateSessionSynopsis">
                <button class="button is-info"><b-icon pack="fas" icon="plus"></b-icon><p>Add Session</p></button>
            </a>
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
            default-sort="sessionName">

            <template slot="empty">
                <section class="section">
                    <div class="content has-text-grey has-text-centered">
                        <p>
                            <b-icon
                                icon="emoticon-sad"
                                size="is-large">
                            </b-icon>
                        </p>
                        <p>Nothing here.</p>
                    </div>
                </section>
            </template>
            <template slot-scope="props">
                <b-table-column field="sessionName" label="Session Name" sortable>
                    {{ props.row.sessionName }}
                </b-table-column>

                <b-table-column field="createdBy" label="Created By" sortable>
                    {{ props.row.createdBy }}
                </b-table-column>

                <b-table-column field="updatedBy" label="Updated By" sortable>
                    <span>
                        {{ props.row.updatedBy }}
                        <!--{{ new Date(props.row.date).toLocaleDateString() }}-->
                    </span>
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
                <!-- <b-table-column custom-key="actionsDelete">
                   
                </b-table-column> -->
            </template>
        </b-table>
        <link rel="stylesheet" href="//cdn.materialdesignicons.com/2.5.94/css/materialdesignicons.min.css">
        <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.2.0/css/all.css">
    </section>
    </div>
</template>

<script>
//import api from '@/SessionSynopsesApiService';
import axios from "axios";
import { router } from '../_helpers';

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
      perPage: 5
    };
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
      axios
        .get("http://localhost:5000/api/SessionSynopses/")
        .then(response => {
          // JSON responses are automatically parsed.
          this.data = response.data;
          //return data;
        })
        .catch(e => {
          console.log(e);
        });
      // .catch((error) => {
      //   console.log(error)
      // })
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
        onConfirm: () =>  {
            console.log(sessionId);
            axios
            .delete("http://localhost:5000/api/SessionSynopses/" + sessionId)
            .then(response => {
              // JSON responses are automatically parsed.
              this.data.splice(sessionId, -1);
              this.$router.go(0);
              //return data;
            })
            .catch(e => {
              console.log(e);
            });
          this.$toast.open("Session deleted!");
          router.push('/ManageSessionSynopses' );
           }
          
    
      });
      //router.push({ path: `/DeleteSessionSynopsis/${sessionId}` }) // -> /DeleteSessionSynopses/123
    }
  }
};
</script>