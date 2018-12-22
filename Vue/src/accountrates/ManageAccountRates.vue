<template>
    <div class="panel container" style="margin-top:3em">
        <section class="subtitle is-2 level-left" style="margin-left:0.5em">
            <button class="button is-rounded is-info is-outlined" style="height:45px" @click="back()">
           <span class="icon is-info">
                <i class="fas fa-arrow-left"></i>
          </span>
            </button>
            <h1 style="margin-left:0.5em">Manage Account Rates</h1>
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
       
                <button class="button is-info" @click="createRate($route.params.customerAccountId)"><b-icon pack="fas" icon="plus"></b-icon><p>Add Account Rate</p></button>
          
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
                <b-table-column field="rateHour" label="Rate Per Hour" sortable>
                    {{ props.row.rateHour }}
                </b-table-column>

                <b-table-column field="startDate" label="Effective Start Date" sortable>
                    <span>
                        {{ new Date(props.row.startDate).toLocaleDateString() }}
                        <!--{{ new Date(props.row.date).toLocaleDateString() }}-->
                    </span>
                </b-table-column>
                <b-table-column field="endDate" label="Effective End Date" sortable style="width:10%">
                    <span>
                        {{ new Date(props.row.endDate).toLocaleDateString() }}
                        <!--{{ new Date(props.row.date).toLocaleDateString() }}-->
                    </span>
                </b-table-column>

               <b-table-column custom-key="actions">
                   <button class="button is-primary" @click="updateRate(props.row.rateId)">
                    <b-icon pack="fas" icon="edit"></b-icon>
                    <span>Update Rate</span>
                   </button>
                   <button class="button is-danger" @click="deleteRow(props.row.rateId)">
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
        .get("http://localhost:5000/api/CustomerAccounts/ManageAccountRates/" + this.$route.params.customerAccountId)
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
    updateRate(rateId) {
      router.push({ path: `/UpdateAccountRate/${rateId}` }); 
    },
    createRate(custId) {
      router.push({ path: `/CreateAccountRate/${custId}` }); 
    },
    back(){
        router.push({ path: `/ManageCustomerAccounts` }); 
    },
    deleteRow(accountId) {
        console.log(accountId);
      this.$dialog.confirm({
        title: "Deleting Rate Record",
        message:
          "Are you sure you want to <b>delete</b> this Account Rate? This action cannot be undone.",
        confirmText: "Delete Rate",
        type: "is-danger",
        hasIcon: true,
        onConfirm: () =>  {
            console.log(accountId);
            axios
            .delete("http://localhost:5000/api/CustomerAccounts/UpdateAccountRates/" + accountId)
            .then(response => {
              // JSON responses are automatically parsed.
              this.data.splice(accountId, 1);
              //this.$toast.open("Account Rate deleted!");
              this.$router.go(0);
              //return data;
            })
            .catch(e => {
              console.log(e.response);
            });
          
           }
          
    
      });
      //router.push({ path: `/DeleteSessionSynopsis/${sessionId}` }) // -> /DeleteSessionSynopses/123
    }
  }
};
</script>