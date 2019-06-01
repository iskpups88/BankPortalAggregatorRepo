//import { IDENTITY_CONFIG, METADATA_OIDC } from "../utils/authConst";
import { UserManager, WebStorageStateStore, Log } from "oidc-client";

var config = {
    authority: "https://localhost:44363/",
    client_id: "js",
    redirect_uri: "https://localhost:44345/callback",
    response_type: "code",
    scope: "openid profile API IntegrationService",
    post_logout_redirect_uri: "https://localhost:44345/logoutCallback",
    silent_redirect_uri: "https://localhost:44345/silentRenew",
    revokeAccessTokenOnSignout : true
};

export default class AuthService {
    UserManager;
    accessToken;

    static instance = null;
    static createInstance() {
        var object = new AuthService();
        return object;
    }

    static getInstance() {
        if (!AuthService.instance) {
            AuthService.instance = AuthService.createInstance();
        }
        return AuthService.instance;
    }

    constructor() {
        this.UserManager = new UserManager({
            ...config,
            userStore: new WebStorageStateStore({ store: window.localStorage }),
        });
        // Logger
        Log.logger = console;
        Log.level = Log.DEBUG;

        this.UserManager.events.addUserLoaded(user => {
            this.accessToken = user.access_token;
            localStorage.setItem("access_token", user.access_token);
            localStorage.setItem("id_token", user.id_token);
            this.setUserInfo({
                accessToken: this.accessToken,
                idToken: user.id_token
            });
            //if (window.location.href.indexOf("signin-oidc") !== -1) {
            //    this.navigateToScreen();
            //}
        });
        this.UserManager.events.addSilentRenewError(e => {
            console.log("silent renew error", e.message);
        });

        this.UserManager.events.addAccessTokenExpired(() => {
            console.log("token expired");
            this.signinSilent();
        });
    }

    signinRedirectCallback = () => {
        return this.UserManager.signinRedirectCallback();
    };

    getUser = async () => {
        const user = await this.UserManager.getUser();

        if (!user) {
            return await this.UserManager.signinRedirectCallback();
        }
        return user;
    };

    parseJwt = token => {
        const base64Url = token.split(".")[1];
        const base64 = base64Url.replace("-", "+").replace("_", "/");
        return JSON.parse(window.atob(base64));
    };

    setUserInfo = authResult => {
        //const data = this.parseJwt(this.accessToken);
        this.setSessionInfo(authResult);
        //this.setUser(data);
    };

    signinRedirect = () => {
        localStorage.setItem("redirectUri", window.location.pathname);
        this.UserManager.signinRedirect({});
    };

    setUser = data => {
        localStorage.setItem("userId", data.sub);
    };

    navigateToScreen = () => {
        const redirectUri = !!localStorage.getItem("redirectUri")
            ? localStorage.getItem("redirectUri")
            : "/en/dashboard";
        const language = "/" + redirectUri.split("/")[1];

        window.location.replace(language + "/dashboard");
    };

    setSessionInfo(authResult) {
        localStorage.setItem("access_token", authResult.accessToken);
        localStorage.setItem("id_token", authResult.idToken);
    }

    isAuthenticated = () => {
        const access_token = localStorage.getItem("access_token");
        return !!access_token;
    };

    signinSilent = () => {
        this.UserManager.signinSilent()
            .then(user => {
                console.log("signed in", user);
            })
            .catch(err => {
                console.log(err);
            });
    };

    signinSilentCallback = () => {
        return this.UserManager.signinSilentCallback();
    };

    createSigninRequest = () => {
        return this.UserManager.createSigninRequest();
    };

    logout = () => {
        this.UserManager.signoutRedirect({
            id_token_hint: localStorage.getItem("id_token")
        });
        //this.UserManager.clearStaleState();
        //localStorage.clear();
    };

    signoutRedirectCallback = () => {
        return this.UserManager.signoutRedirectCallback();
        //window.location.replace(process.env.REACT_APP_PUBLIC_URL);
        
        //this.UserManager.clearStaleState();
    };
}