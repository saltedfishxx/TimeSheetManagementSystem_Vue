<template>
    <div class="panel container" style="margin-top:3em">
        <section class="subtitle is-2 level-left" style="margin-left:0.5em">
            <b-icon pack="fas" icon="users" type="is-danger" size="is-small"></b-icon>
            <h1 style="margin-left:1em">Manage Customer Accounts</h1>
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
            <a class="control" href="/CreateCustomerAccount">
                <button class="button is-info"><b-icon pack="fas" icon="plus"></b-icon><p>Add Customer</p></button>
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
            default-sort="accountName">

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
                <b-table-column field="accountName" label="Account Name" sortable>
                    {{ props.row.accountName }}
                </b-table-column>

                <b-table-column field="comments" label="Comments" sortable style="width:25%">
                    {{ props.row.comments }}
                </b-table-column>

                <b-table-column field="updatedBy" label="Updated By" sortable>
                    <span>
                        {{ props.row.updatedBy }}
                        <!--{{ new Date(props.row.date).toLocaleDateString() }}-->
                    </span>
                </b-table-column>
                <b-table-column field="updatedAt" label="Updated At" sortable style="width:10%">
                    <span>
                        {{ new Date(props.row.updatedAt).toLocaleDateString() }}
                        <!--{{ new Date(props.row.date).toLocaleDateString() }}-->
                    </span>
                </b-table-column>

               <b-table-column custom-key="actions">
                   <button class="button is-primary" @click="updateGeneralInfo(props.row.customerAccountId)">
                    <b-icon pack="fas" icon="edit"></b-icon>
                    <span>Update General Info</span>
                   </button>
                   <button class="button is-info" @click="manageAccountRates(props.row.customerAccountId)">
                    <b-icon pack="fas" icon="dollar-sign"></b-icon>
                    <span>Manage Account Rates</span>
                   </button>
                   <button class="button is-danger" @click="deleteRow(props.row.customerAccountId)">
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
        .get("http://localhost:5000/api/CustomerAccounts/",
        {
            headers: 
                authHeader()
                })
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
    updateGeneralInfo(customerAccountId) {
      router.push({ path: `/UpdateGeneralInfo/${customerAccountId}` }); 
    },
    manageAccountRates(customerAccountId) {
      router.push({ path: `/ManageAccountRates/${customerAccountId}` });
    },
    deleteRow(accountId) {
        console.log(accountId);
      this.$dialog.confirm({
        title: "Deleting Customer Record",
        message:
          "Are you sure you want to <b>delete</b> this Customer Account? This action cannot be undone.",
        confirmText: "Delete Account",
        type: "is-danger",
        hasIcon: true,
        onConfirm: () =>  {
            console.log(accountId);
            axios
            .delete("http://localhost:5000/api/CustomerAccounts/" + accountId)
            .then(response => {
              // JSON responses are automatically parsed.
              this.data.splice(accountId, -1);
              this.$router.go(0);
              //return data;
            })
            .catch(e => {
              console.log(e.response);
            });
          this.$toast.open("Session deleted!");
         
           }
          
    
      });
      //router.push({ path: `/DeleteSessionSynopsis/${sessionId}` }) // -> /DeleteSessionSynopses/123
    }
  }
};
</script>