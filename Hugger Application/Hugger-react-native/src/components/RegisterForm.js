import React, { Component } from 'react';
import { StyleSheet, Text, TextInput, View, Dimensions, TouchableOpacity } from 'react-native';
import { FontAwesome } from '@expo/vector-icons';

const { width, height } = Dimensions.get('window');

export default class RegisterForm extends Component {

    render() {
        return (
            <View style={styles.container}>

                <View style={styles.inputContainer}>
                    <TextInput
                        style={styles.inputField}
                        placeholder="Login"
                        placeholderTextColor="#ffffff"
                        underlineColorAndroid='transparent'
                        selectionColor="#9c9c9c"
                        onSubmitEditing={() => this.password.focus()}
                    />
                </View>

                <View style={styles.inputContainer}>
                    <TextInput
                        style={styles.inputField}
                        placeholder="Password"
                        placeholderTextColor="#ffffff"
                        secureTextEntry={true}
                        underlineColorAndroid='transparent'
                        selectionColor="#9c9c9c"
                        ref={(input) => this.password = input}
                        onSubmitEditing={() => this.email.focus()}
                    />
                </View>

                <View style={styles.inputContainer}>
                    <TextInput
                        style={styles.inputField}
                        placeholder="Email"
                        placeholderTextColor="#ffffff"
                        underlineColorAndroid='transparent'
                        selectionColor="#9c9c9c"
                        keyboardType="email-address"
                        ref={(input) => this.email = input}
                        onSubmitEditing={() => this.birthyear.focus()}
                    />
                </View>

                <View style={styles.inputContainer}>
                    <TextInput
                        style={styles.inputField}
                        placeholder="Birth Year"
                        placeholderTextColor="#ffffff"
                        underlineColorAndroid='transparent'
                        selectionColor="#9c9c9c"
                        keyboardType="number-pad"
                        maxLength={4}
                        ref={(input) => this.birthyear = input}
                    />
                </View>

                <TouchableOpacity style={[styles.button, styles.shadowBox]}>
                    <FontAwesome style={styles.icon} name="sign-in" size={25} color="white" />
                    <Text style={styles.buttonText}>{this.props.type}</Text>
                </TouchableOpacity>
            </View>
        );
    }
}

const styles = StyleSheet.create({
    container: {
        flex: 1,
        justifyContent: 'center',
        alignItems: 'center',
        marginTop: 40
    },
    button: {
        width: width * 0.3,
        backgroundColor: '#8260a7',
        borderRadius: 25,
        marginVertical: 10,
        paddingVertical: 10,
        flexDirection: 'row',
    },
    shadowBox: {
        shadowColor: "#000",
        shadowOffset: {
            width: 0,
            height: 2,
        },
        shadowOpacity: 0.25,
        shadowRadius: 3.84,
        elevation: 5,
    },
    buttonText: {
        fontSize: 16,
        fontWeight: '500',
        color: '#ffffff',
        textAlign: 'center'
    },
    inputContainer: {
        zIndex: 0,
        width: width * 0.7,
        height: 45,
        flexDirection: 'row',
        justifyContent: 'center',
        alignItems: 'center',
        backgroundColor: 'rgba(255, 255, 255, 0.2)',
        color: '#ffffff',
        borderRadius: 50,
        margin: 5,
    },
    inputField: {
        flex: 1,
        color: '#ffffff',
        paddingLeft: 20
    },
    icon: {
        zIndex: 1000,
        paddingLeft: 10,
        paddingRight: 10
    }
});