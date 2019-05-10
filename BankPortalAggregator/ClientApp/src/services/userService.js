import { requestHelper } from '../helpers/RequestHelper';

export const userService = {
    login,
    logout
};

function login(code) {
    const requestOptions = {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ code: code })
    };

    return fetch('api/Account/Login', requestOptions)
        .then(requestHelper.handleResponse, requestHelper.handleError)
        .then(user => {
            console.log(user);
            if (user && user.token) {
                localStorage.setItem('user', JSON.stringify(user));
            }

            return user;
        });
}

function logout() {
    localStorage.removeItem('user');
}