import config from 'config';
import { authHeader, authHeaderUrlencoded } from '../_helpers';
import Vue from 'vue'
import axios from 'axios'

//This file is used to handle requests for various controller services such as adding/updating
//session synopses/customer account/account rate/ account detail



const client = axios.create({
    baseURL: `${config.apiUrl}/api/`
})

export default {
    async execute(method, resource, data) {
        return client({
            method,
            url: resource,
            data,
            headers: authHeader()
        }).then(req => {
            console.log(req.data)
            return req.data
        }).catch(error => {
            console.log(error);
        })
    },
    async executeWithData(method, resource, data) {
        return client({
            method,
            url: resource,
            data,
            headers: authHeaderUrlencoded()
        }).then(req => {
            console.log(req.data)
            return req.data
        }).catch(error => {
            console.log(error);
        })
    },
    getAll(url) {
        return this.execute('get', url)
    },
    get(url) {
        return this.execute('get', url)
    },
    create(url, data) {
        return this.executeWithData('post', url, data)
    },
    update(url, data) {
        return this.executeWithData('put', url, data)
    },
    delete(url) {
        return this.execute('delete', url)
    }
}