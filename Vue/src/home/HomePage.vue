<template>
  <div class="hello">
    <div class="container content">
      <div class="blocks" id="left-block">
      <div style="padding-top:2em">
    <h1 class="subtitle is-2">TimeSheet Management System</h1><br />
   This TimeSheet Management solution is often needed by small business enterprises which provide:<br />
        <ul>
            <li>classes that centers around performing arts (dance, singing etc.)</li>
            <li>tuition on school subjects</li>
        </ul>
    <p></p>
      </div>
      <div >
        <br/>
    <h3 class="subtitle is-4">User Role information</h3>
        <ul>
            <li> The administrator user can logon to the system to manage class lesson records, 
              account records, view instructor time sheet, manage class lesson schedules</li>
            <li> The officer user can logon to the system to submit their time-in and time-out
               information, view their class schedules.</li>
        </ul>
      </div>
      </div>
      <div class="blocks" id="right-block">
      <img src="src\assets\k_round.jpg" width="400">
    </div>
    </div>
    
    
  </div>
</template>

<script>
export default {
    computed: {
        user () {
            return this.$store.state.authentication.user;
        },
        users () {
            return this.$store.state.users.all;
        }
    },
    created () {
        this.$store.dispatch('users/getAll');
    }
};
</script>

<style>
   .blocks{
     display: inline-block;
   }
   #left-block {
     width:60%;
     float: left;
   }
   #right-block {
       padding-top: 5em
   }
</style>
