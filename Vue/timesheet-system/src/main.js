// The Vue build version to load with the `import` command
// (runtime-only or standalone) has been set in webpack.base.conf with an alias.
import Vue from 'vue'
import App from './App'
import { store } from './_store';
import { router } from './_helpers';

//3rd party imports
import Buefy from 'buefy'


Vue.config.productionTip = false
Vue.use(Buefy)



/* eslint-disable no-new */
new Vue({
  el: '#app',
  router,
  store,
  render: h => h(App)
});