export const requestHelper = {
    authHeader,
    handleResponse,
    handleError
};

function authHeader() {
    let token = localStorage.getItem('access_token');
    if (token) {
        return { 'Authorization': 'Bearer ' + token };
    } else {
        return {};
    }
}

function handleResponse(response) {
    return new Promise((resolve, reject) => {
        if (response.ok) {
            var contentType = response.headers.get("content-type");
            if (contentType && contentType.includes("application/json")) {
                response.json().then(json => resolve(json));
            } else {
                resolve();
            }
        } else {
            response.text().then(text => reject(text));
        }
    });
}

function handleError(error) {
    return Promise.reject(error && error.message);
}