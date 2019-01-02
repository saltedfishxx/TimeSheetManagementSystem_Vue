import config from 'config';
import {authHeader} from '../_helpers';
import qs from 'qs';

//This js file is used to handle user services namely login/logout functions 
//as well as handling responses

export const userService = {
    login,
    logout,
    getAll
};

function login(loginUser) {

    console.log('datainqs', qs.stringify(loginUser));
    const requestOptions = {
        method: 'POST',
        headers: {
            "content-type": "application/x-www-form-urlencoded",
            'Access-Control-Allow-Origin': '*'
          },
        body: qs.stringify(loginUser)
    };

    return fetch(`${config.apiUrl}/api/Users/signin`, requestOptions)
        .then(handleResponse)
        .then(user => {
            // login successful if there's a jwt token in the response
            if (user.token) {
                // store user details and jwt token in local storage to keep user logged in between page refreshes
                localStorage.setItem('user', JSON.stringify(user));
            }

            return user;
        });
}

function logout() {
    // remove user from local storage to log user out
    localStorage.removeItem('user');
}

function getAll() {
    const requestOptions = {
        method: 'GET',
        headers: authHeader()
    };

    return fetch(`${config.apiUrl}/api/Users`, requestOptions).then(handleResponse).catch(error => {
        if (!error.response) {
            // network error
            this.errorStatus = 'Error: Network Error';
        } else {
            this.errorStatus = error.response.data.message;
        }
        return Promise.reject(errorStatus)
      })
}

function handleResponse(response) {
    return response.text().then(text => {
        const data = text && JSON.parse(text);
        if (!response.ok) {
            if (response.status === 401) {
                // auto logout if 401 response returned from api
                logout();
                location.reload(true);
            }
            if(response.status === 404){
                const error = (data && data.message) || response.statusText || "Cannot connect to server";
                return Promise.reject(error);
            }
            const error = (data && data.message) || response.statusText;
            return Promise.reject(error);
        }

        return data;
    });
}