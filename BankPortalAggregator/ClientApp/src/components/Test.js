import React, { Component } from 'react';
import { User } from './User';

export class Test extends Component {
    state = {
        name: null,
        imgUrl: null,
    }
    componentDidMount() {
        const _onInit = auth2 => {
            console.log('init OK', auth2)
        }
        const _onError = err => {
            console.log('error', err)
        }
        window.gapi.load('auth2', function () {
            window.gapi.auth2
                .init({
                    client_id: process.env.REACT_APP_GOOGLE_CLIENT_ID,
                    access_type:'offline'
                })
                .then(_onInit, _onError)
        })
    }
    signIn = () => {
        const auth2 = window.gapi.auth2.getAuthInstance()
        auth2.signIn().then(googleUser => {
            const profile = googleUser.getBasicProfile()
            this.setState({
                name: profile.getName(),
                imgUrl: profile.getImageUrl(),
            })
        })
    }
    signOut = () => {
        const auth2 = window.gapi.auth2.getAuthInstance()
        auth2.signOut().then(() => {
            this.setState({
                name: null,
                imgUrl: null,
            })
        })
    }
    render() {
        const { name, imgUrl } = this.state
        return (
            <div className="App">
                <header className="App-header">
                    {!name && <button onClick={this.signIn}>Log in</button>}
                    {!!name && <button onClick={this.signOut}>Log out</button>}
                    {!!name && <User name={name} imgUrl={imgUrl} />}
                </header>
            </div>
        )
    }
}
