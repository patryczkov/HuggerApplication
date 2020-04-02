import React, { Component } from 'react';
import { StyleSheet, View, Text, ActivityIndicator } from 'react-native';

export default class Loader extends Component {

    render() {
        return (
            <View style={styles.container}>
                <ActivityIndicator color="#ffffff" size="large" />
            </View>
        );
    }
}

const styles = StyleSheet.create({
    container: {
        backgroundColor: 'rgba(0, 0, 0, 0.4)',
        position: 'absolute',
        width: "100%",
        height: "100%",
        zIndex: 1000,
        justifyContent: 'center'
    },
});