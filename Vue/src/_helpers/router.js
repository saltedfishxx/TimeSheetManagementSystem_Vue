//this js file is used for routing between pages

import Vue from 'vue';
import Router from 'vue-router';

//Log in/out and home page
import HomePage from '../home/HomePage'
import LoginPage from '../login/LoginPage'
import Welcome from '../home/Welcome'

//Users
import ManageUsers from '../manageusers/ManageUsers'
import CreateUser from '../manageusers/CreateUser'
import UpdateUser from '../manageusers/UpdateUser'

//Session synopses
import ManageSessionSynopses from '../sessionsynopses/ManageSessionSynopses'
import CreateSessionSynopsis from '../sessionsynopses/CreateSessionSynopsis'
import UpdateSessionSynopsis from '../sessionsynopses/UpdateSessionSynopsis'

//Customer account
import CreateCustomerAccount from '../customeraccounts/CreateCustomerAccount'
import ManageCustomerAccounts from '../customeraccounts/ManageCustomerAccounts'
import UpdateGeneralInfo from '../customeraccounts/UpdateGeneralInfo'

//Account rates
import ManageAccountRates from '../accountrates/ManageAccountRates'
import CreateAccountRate from '../accountrates/CreateAccountRate'
import UpdateAccountRate from '../accountrates/UpdateAccountRate'


//Account Details
import CreateAccountDetail from '../accountdetails/CreateAccountDetail'
import ManageAccountDetails from '../accountdetails/ManageAccountDetails'
import UpdateAccountDetail from '../accountdetails/UpdateAccountDetail'

Vue.use(Router);

export const router = new Router({
  mode: 'history',
  routes: [
    {
      path: '/',
      name: 'Main',
      component: HomePage
    },
    {
      path: '/Welcome',
      name: 'Welcome',
      component: Welcome
    },
    {
      path: '/login',
      name: 'Login',
      component: LoginPage
    },
    {
      path: '/ManageSessionSynopses',
      name: 'ManageSessionSynopses',
      component: ManageSessionSynopses
    },
    {
      path: '/CreateSessionSynopsis',
      name: 'CreateSessionSynopsis',
      component: CreateSessionSynopsis
    },
    {
      path: '/UpdateSessionSynopsis/:sessionId',
      name: 'UpdateSessionSynopsis',
      component: UpdateSessionSynopsis
    },
    {
      path: '/CreateCustomerAccount',
      name: 'CreateCustomerAccount',
      component: CreateCustomerAccount
    },
    {
      path: '/ManageCustomerAccounts',
      name: 'ManageCustomerAccounts',
      component: ManageCustomerAccounts
    },
    {
      path: '/UpdateGeneralInfo/:customerAccountId',
      name: 'UpdateGeneralInfo',
      component: UpdateGeneralInfo
    },
    {
      path: '/ManageAccountRates/:customerAccountId',
      name: 'ManageAccountRates',
      component: ManageAccountRates
    },
    {
      path: '/CreateAccountRate/:customerAccountId',
      name: 'CreateAccountRate',
      component: CreateAccountRate
    },
    {
      path: '/UpdateAccountRate/:rateId',
      name: 'UpdateAccountRate',
      component: UpdateAccountRate
    },
    {
      path: '/ManageUsers',
      name: 'ManageUsers',
      component: ManageUsers
    },
    {
      path: '/CreateUser',
      name: 'CreateUser',
      component: CreateUser
    },
    {
      path: '/UpdateUser/:userid',
      name: 'UpdateUser',
      component: UpdateUser
    },
    {
      path: '/CreateAccountDetail/:customerAccountId',
      name: 'CreateAccountDetail',
      component: CreateAccountDetail
    },
    {
      path: '/ManageAccountDetails/:customerAccountId',
      name: 'ManageAccountDetails',
      component: ManageAccountDetails
    },
    {
      path: '/UpdateAccountDetail/:accId',
      name: 'UpdateAccountDetail',
      component: UpdateAccountDetail
    },
    // otherwise redirect to home
    { path: '*', redirect: '/' },
  ]
});

// redirect to login page if not logged in and trying to access a restricted page
router.beforeEach((to, from, next) => {
  let visited = false;
  //public pages such as login and signup does not require user to log in first
  const publicPages = ['/login', '/Welcome', '/CreateAccountDetail'];

  const authRequired = !publicPages.includes(to.path);
  const loggedIn = localStorage.getItem('user');

  //check if it is not a public page and there is no user in local storage
  if (authRequired && !loggedIn) {
    if (visited == false) {
      visited = true;
      return next('/Welcome');
    } else {
      return next('/login');
    }
  }

  next();
})