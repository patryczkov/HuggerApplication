import React, { Component } from 'react';
import { StyleSheet, Image, Text, View, Dimensions } from 'react-native';

const { width, height } = Dimensions.get('window');

export const LOGO_HEIGHT = height / 9;
export const LOGO_HEIGHT_SMALL = height / 7;

export default class Logo extends Component {

    render() {
        return (
            <View style={styles.container}>
                <Image style={styles.logo}
                    source={require('../../assets/logo.png')} />
            </View>
        );
    }
}

const styles = StyleSheet.create({
    container: {
        flexGrow: 1,
        justifyContent: 'flex-end',
        alignItems: 'center',
        padding: 20
    },
    logoText: {
        marginVertical: 15,
        fontSize: 18,
        color:'rgba(255, 255, 255, 0.7)'
    },
    logo: {
        width: width - 100,
        height: LOGO_HEIGHT
    }
});