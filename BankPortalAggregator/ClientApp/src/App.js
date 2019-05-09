import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { FetchData } from './components/FetchData';
import { Counter } from './components/Counter';
import { ProductList } from './components/ProductList';
import { GoogleLoginPage } from './components/GoogleLoginPage';
import { Test } from './components/Test';

export default class App extends Component {
    displayName = App.name

    render() {
        return (
            <Layout>
                <Route exact path='/' component={Home} />
                <Route path='/counter' component={Counter} />
                <Route path='/fetchdata' component={FetchData} />
                <Route path='/products' component={ProductList} />
                <Route path='/login' component={GoogleLoginPage} />
                <Route path='/test' component={Test} />
            </Layout>
        );
    }
}
