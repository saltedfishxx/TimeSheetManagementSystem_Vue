import Vue from 'vue'
import Router from 'vue-router'
import Hello from '@/components/Hello'
import ManageSessionSynopses from '@/components/ManageSessionSynopses'
import CreateSessionSynopsis from '@/components/CreateSessionSynopsis'
import UpdateSessionSynopsis from '@/components/UpdateSessionSynopsis'
import CreateCustomerAccount from '@/components/CreateCustomerAccount'
import ManageCustomerAccounts from '@/components/ManageCustomerAccounts'
import UpdateGeneralInfo from '@/components/UpdateGeneralInfo'
import ManageAccountRates from '@/components/ManageAccountRates'
import CreateAccountRate from '@/components/CreateAccountRate'
import UpdateAccountRate from '@/components/UpdateAccountRate'
import LoginPage from '../login/LoginPage'

Vue.use(Router)

//TODO: change index page to login page after finishing CA1 req
let router = new Router({
  mode: 'history',
  routes: [
    {
      path: '/',
      name: 'Main',
      component: Hello
    },
    { 
      path: '/login', 
      name: LoginPage,
      component: LoginPage },
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
})

router.beforeEach((to, from, next) => {
  // redirect to login page if not logged in and trying to access a restricted page
  const publicPages = ['/login'];
  const authRequired = !publicPages.includes(to.path);
  const loggedIn = localStorage.getItem('user');

  if (authRequired && !loggedIn) {
    return next('/Login');
  }

  next();
})

export default router
