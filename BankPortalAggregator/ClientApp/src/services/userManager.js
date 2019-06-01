import { UserManager } from 'oidc-client';

var config = {
    authority: "https://localhost:44363/",
    client_id: "js",
    redirect_uri: "https://localhost:44345/callback",
    response_type: "code",
    scope: "openid profile",
    post_logout_redirect_uri: "https://localhost:44345/",
};

const userManager = createUserManager(config);

export default userManager;

function createUserManager(config) {
    return new UserManager(config);
}