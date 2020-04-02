import React, { Component } from 'react';
import { StyleSheet, View, Text, TouchableOpacity } from 'react-native';
import { FontAwesome } from '@expo/vector-icons';

import { Actions } from 'react-native-router-flux';
import { connect } from 'react-redux';

import { logoutUser } from "../actions/auth.actions";

class BottomBar extends Component {

    goProfiles() {
        Actions.profiles()
    }

    goMessages() {
        Actions.profiles()
    }

    goPreferences() {
        Actions.profiles()
    }

    goSettings() {
        Actions.profiles()
    }

    logoutUser = () => {
        this.props.dispatch(logoutUser());
        Actions.login()
    }

    render() {
        return (
            <View style={styles.container}>
                <TouchableOpacity onPress={this.goProfiles}>
                    <FontAwesome style={styles.icon_small} name="users" size={40} color="white" />
                </TouchableOpacity>
                <TouchableOpacity>
                    <FontAwesome style={styles.icon_small} name="comments" size={40} color="white" />
                </TouchableOpacity>
                <TouchableOpacity>
                    <FontAwesome style={styles.icon_small} name="filter" size={40} color="white" />
                </TouchableOpacity>
                <TouchableOpacity>
                    <FontAwesome style={styles.icon_small} name="cog" size={40} color="white" />
                </TouchableOpacity>
                <TouchableOpacity onPress={this.logoutUser}>
                    <FontAwesome style={styles.icon_small} name="sign-out" size={40} color="white" />
                </TouchableOpacity>
            </View>
        );
    }
}

const styles = StyleSheet.create({
    container: {
        backgroundColor: '#634682',
        flex: 1,
        alignItems: 'center',
        justifyContent: 'center',
        flexDirection: 'row',
    },
    icon_small: {
        flex: 1,
        zIndex: 1000,
        paddingLeft: 15,
        paddingRight: 15,
        marginTop: 10
    }
});


mapStateToProps = (state) => ({
    getUser: state.userReducer.getUser
});

mapDispatchToProps = (dispatch) => ({
    dispatch
});

export default connect(mapStateToProps, mapDispatchToProps)(BottomBar);