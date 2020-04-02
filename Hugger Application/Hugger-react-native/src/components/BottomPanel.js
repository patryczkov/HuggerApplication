import React, { Component } from 'react';
import { StyleSheet, View, Text, TouchableOpacity } from 'react-native';
import { FontAwesome } from '@expo/vector-icons';

import { Actions } from 'react-native-router-flux';
import { hug } from '../actions/hug';

export default class BottomPanel extends Component {

    goUserPanel() {
        Actions.user()
    }

    likeProfile() {
        hug.like()
    }

    dislikeProfile() {
        hug.dislike()
    }

    render() {
        return (
            <View style={styles.container}>
                <TouchableOpacity onPress={this.goUserPanel}>
                    <FontAwesome style={styles.icon_small} name="user" size={40} color="white" />
                </TouchableOpacity>
                <TouchableOpacity onPress={this.likeProfile}>
                    <FontAwesome style={styles.icon_big} name="check-circle" size={60} color="white" />
                </TouchableOpacity>
                <TouchableOpacity onPress={this.dislikeProfile}>
                    <FontAwesome style={styles.icon_big} name="times-circle" size={60} color="white" />
                </TouchableOpacity>
                <TouchableOpacity>
                    <FontAwesome style={styles.icon_small} name="comments" size={40} color="white" />
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
    },
    icon_big: {
        flex: 1,
        zIndex: 1000,
        paddingLeft: 10,
        paddingRight: 10,
    }
});