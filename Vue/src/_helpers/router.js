import Vue from 'vue';
import Router from 'vue-router';

import HomePage from '../home/HomePage'
import LoginPage from '../login/LoginPage'
import SignupPage from '../login/SignupPage'
import ManageSessionSynopses from '../sessionsynopses/ManageSessionSynopses'
import CreateSessionSynopsis from '../sessionsynopses/CreateSessionSynopsis'
import UpdateSessionSynopsis from '../sessionsynopses/UpdateSessionSynopsis'
import CreateCustomerAccount from '../customeraccounts/CreateCustomerAccount'
import ManageCustomerAccounts from '../customeraccounts/ManageCustomerAccounts'
import UpdateGeneralInfo from '../customeraccounts/UpdateGeneralInfo'
import ManageAccountRates from '../accountrates/ManageAccountRates'
import CreateAccountRate from '../accountrates/CreateAccountRate'
import UpdateAccountRate from '../accountrates/UpdateAccountRate'


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
      path: '/login', 
      name: 'Login', 
      component: LoginPage 
    },
    { 
      path: '/signup', 
      name: 'SignUp', 
      component: SignupPage 
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

    // otherwise redirect to home
    { path: '*', redirect: '/' }
  ]
});

router.beforeEach((to, from, next) => {
  // redirect to login page if not logged in and trying to access a restricted page
  const publicPages = ['/login', '/signup'];
  const authRequired = !publicPages.includes(to.path);
  const loggedIn = localStorage.getItem('user');

  if (authRequired && !loggedIn) {
    return next('/login');
  }

  next();
})