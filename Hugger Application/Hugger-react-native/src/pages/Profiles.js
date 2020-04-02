import React, { Component } from 'react';
import { StyleSheet, View } from 'react-native';

import Card from '../components/Card';
import Panel from '../components/BottomPanel';

export default class Profiles extends Component {

    render() {
        return (
            <View style={styles.container}>
                <View style={{ height: 60 }}>
                </View>

                <Card />

                <View style={{ height: 80 }}>
                    <Panel />
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
});