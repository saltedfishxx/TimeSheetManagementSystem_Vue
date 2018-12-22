import Vue from 'vue';

import { store } from './_store';
import { router } from './_helpers';
import App from './app/App';
import babelPolyfill from 'babel-polyfill'
import Buefy from 'buefy'


Vue.config.productionTip = false
Vue.use(Buefy)


// setup fake backend
//import { configureFakeBackend } from './_helpers';
//configureFakeBackend();

new Vue({
    el: '#app',
    router,
    store,
    render: h => h(App)
});