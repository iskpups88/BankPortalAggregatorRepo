import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { ProductList } from './components/ProductList';
import { Callback } from './components/CallbackComponent';
import { Login } from './components/Login';
import { SilientRenew } from './components/SilentRenew';
import { LogoutCallback } from './components/LogoutCallback';

export default class App extends Component {
    displayName = App.name

    render() {
        return (
            <Layout>           
                <Route path='/products' component={ProductList} />
                <Route exact={true} path='/callback' component={Callback} />
                <Route path='/login' component={Login} />
                <Route path='/silentRenew' component={SilientRenew} />
                <Route path='/logoutCallback' component={LogoutCallback} />
            </Layout>
        );
    }
}
