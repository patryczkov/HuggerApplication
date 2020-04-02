import React, { Component } from 'react';
import { StyleSheet, View, Text, TouchableOpacity } from 'react-native';
import Bar from '../components/BottomBar';

import UserCard from '../components/UserCard';

export default class UserPage extends Component {

    render() {
        return (
            <View style={styles.container}>

                <View style={{ height: 60 }}>
                </View>

                <UserCard />

                <View style={{ height: 80 }}>
                    <Bar />
                </View>
            </View>
        );
    }
}

const styles = StyleSheet.create({
    container: {
        backgroundColor: '#634682',
        flex: 1,
    },
    icon_small: {
        flex: 1,
        zIndex: 1000,
        paddingLeft: 25,
        paddingRight: 25,
        marginTop: 10
    },
});
