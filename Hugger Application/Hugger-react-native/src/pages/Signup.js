import React, { Component } from 'react';
import { StyleSheet, View, Text, TouchableOpacity, Dimensions, KeyboardAvoidingView } from 'react-native';
import { Field, reduxForm } from 'redux-form';
import { connect } from 'react-redux';
import { compose } from 'redux';
import { FontAwesome } from '@expo/vector-icons';

import Logo from '../components/Logo';
import InputText from '../components/InputText';
import { createNewUser } from '../actions/auth.actions.js';
import Loader from "../components/Loader";

import { Actions } from 'react-native-router-flux';

const { width, height } = Dimensions.get('window');

const styles = StyleSheet.create({
    container: {
        backgroundColor: '#634682',
        flex: 1,
        alignItems: 'center',
        justifyContent: 'center',
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
    icon: {
        paddingLeft: 10,
        paddingRight: 10
    },
    singupContainer: {
        flexGrow: 1,
        alignItems: 'flex-end',
        justifyContent: 'center',
        flexDirection: 'row',
        paddingBottom: 30,
    },
    signupText: {
        fontSize: 16,
        fontWeight: '500',
        color: 'rgba(255, 255, 255, 0.7)',
        textAlign: 'center',
    },
    signupButton: {
        fontSize: 16,
        fontWeight: '500',
        color: 'rgba(255, 255, 255, 0.9)',
    },
    errorText: {
        color: '#ffffff',
        fontSize: 12,
        paddingHorizontal: 16,
        fontWeight: '300',
    }
});

class Signup extends Component {

    goLoginScreen() {
        Actions.login()
    }

    createNewUser = (values) => {
        values.birthyear = parseInt(values.birthyear);
        values.localizationId = 1;
        this.props.dispatch(createNewUser(values));
    }

    onSubmit = (values) => {
        this.createNewUser(values);
    }

    renderTextInput = (field) => {
        const { meta: { touched, error }, label, secureTextEntry, maxLength, keyboardType, placeholder, input: { onChange, ...restInput } } = field;
        return (
            <View>
                <InputText
                    onChangeText={onChange}
                    maxLength={maxLength}
                    placeholder={placeholder}
                    keyboardType={keyboardType}
                    secureTextEntry={secureTextEntry}
                    label={label}
                    {...restInput} />
                {(touched && error) && <Text style={styles.errorText}>{error}</Text>}
            </View>
        );
    }

    render() {
        const { handleSubmit, createUser } = this.props
        return (
            <KeyboardAvoidingView
                style={styles.container}
                behavior="padding">
                {createUser.isLoading}
                <Logo />
                <Field
                    name="login"
                    placeholder="Login"
                    maxLength={30}
                    component={this.renderTextInput} />
                <Field
                    name="password"
                    placeholder="Password"
                    maxLength={30}
                    secureTextEntry={true}
                    component={this.renderTextInput} />
                <Field
                    name="email"
                    placeholder="Email"
                    maxLength={30}
                    keyboardType="email-address"
                    component={this.renderTextInput} />
                <Field
                    name="birthyear"
                    placeholder="Birth Year"
                    maxLength={4}
                    keyboardType="number-pad"
                    component={this.renderTextInput} />

                <TouchableOpacity style={[styles.button, styles.shadowBox]} onPress={handleSubmit(this.onSubmit)}>
                    <FontAwesome style={styles.icon} name="sign-in" size={25} color="white" />
                    <Text style={styles.buttonText}>Signup</Text>
                </TouchableOpacity>

                <View style={styles.singupContainer}>
                    <Text style={styles.signupText}>Already have an account?</Text>
                    <TouchableOpacity onPress={this.goLoginScreen}><Text style={styles.signupButton}> Sign in</Text></TouchableOpacity>
                </View>
            </KeyboardAvoidingView>
        );
    }
}

const validate = (values) => {
    const errors = {};

    if (!values.login) {
        errors.login = "Login is required!"
    }
    if (!values.password) {
        errors.password = "Password is required!"
    }
    if (!values.email) {
        errors.email = "Email is required!"
    }
    if (!values.birthyear) {
        errors.birthyear = "Birth year is required!"
    }
    if (values.birthyear < 1900 || values.birthyear > 2002) {
        errors.birthyear = "Very funny! Not acceptable birth year."
    }

    return errors;
};

mapStateToProps = (state) => ({
    createUser: state.authReducer.createUser
});

mapDispatchToProps = (dispatch) => ({
    dispatch
});

export default compose(
    connect(mapStateToProps, mapDispatchToProps),
    reduxForm({
        form: "register",
        validate
    })
)(Signup);