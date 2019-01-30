import Vue from 'vue';

import { store } from './_store';
import { router } from './_helpers';
import App from './app/App';
import Buefy from 'buefy';
import 'babel-polyfill';
import TimeAgo from 'vue2-timeago'



Vue.config.productionTip = false
Vue.use(Buefy)
window.EventBus = new Vue();
Vue.use(TimeAgo)

new Vue({
    el: '#app',
    router,
    store,
    render: h => h(App)
});