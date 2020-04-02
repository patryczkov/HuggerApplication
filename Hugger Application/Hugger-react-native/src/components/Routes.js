import React, { Component } from 'react';
import { Router, Scene } from 'react-native-router-flux';

import Login from '../pages/Login';
import Signup from '../pages/Signup';
import Profiles from '../pages/Profiles';
import User from '../pages/UserPage';

export default class Routes extends Component {
    render() {
        return (
            <Router>
                <Scene>
                    <Scene key="root" hideNavBar={true} initial={!this.props.isLoggedIn}>
                        <Scene key="login" component={Login} initial={true} />
                        <Scene key="signup" component={Signup} />
                    </Scene>
                    <Scene key="app" hideNavBar={true} initial={this.props.isLoggedIn}>
                        <Scene key="user" component={User} initial={true} />
                        <Scene key="profiles" component={Profiles} />
                    </Scene>
                </Scene>
            </Router>
        )
    }
}