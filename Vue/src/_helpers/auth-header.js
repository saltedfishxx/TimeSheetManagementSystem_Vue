export function authHeader() {
    // return authorization header with jwt token
    let user = JSON.parse(localStorage.getItem('user'));

    if (user && user.token) {
        return { 'Authorization': "Bearer " + user.token,
        "content-type": "application/json",
        'Access-Control-Allow-Origin': '*' };
    } else {
        return {};
    }
}

export function authHeader2() {
    // return authorization header with jwt token
    let user = JSON.parse(localStorage.getItem('user'));

    if (user && user.token) {
        return { 'Authorization': "Bearer " +  user.token,
        "content-type": "application/x-www-form-urlencoded",
            'Access-Control-Allow-Origin': '*'};
    } else {
        return {};
    }
}