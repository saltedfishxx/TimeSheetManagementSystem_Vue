export function authHeader() {
    // this function returns a http request header with jwt token for authorisation
    
    //token is retrieved from local storage when user first logged in
    let user = JSON.parse(localStorage.getItem('user'));

    if (user && user.token) {
        return { 'Authorization': "Bearer " + user.token,
        "content-type": "application/json",
        'Access-Control-Allow-Origin': '*' };
    } else {
        return {};
    }
}

export function authHeaderUrlencoded() {
    // this function returns a http request header with jwt token for authorisation
    
    //token is retrieved from local storage when user first logged in
    let user = JSON.parse(localStorage.getItem('user'));

    if (user && user.token) {
        return { 'Authorization': "Bearer " + user.token,
        "content-type": "application/x-www-form-urlencoded",
        'Access-Control-Allow-Origin': '*'};
    } else {
        return {};
    }
}
