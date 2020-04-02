import React, { Component } from 'react';
import { Field, reduxForm } from 'redux-form';
import { StyleSheet, View, Text, TouchableOpacity, Dimensions, KeyboardAvoidingView } from 'react-native';
import { compose } from 'redux';
import { connect } from 'react-redux';
import { FontAwesome } from '@expo/vector-icons';

import Logo from '../components/Logo';
import InputText from '../components/InputText';

import { Actions } from 'react-native-router-flux';
import { loginUser } from '../actions/auth.actions.js';


const { width, height } = Dimensions.get('window');

const styles = StyleSheet.create({
    container: {
        backgroundColor: '#634682',
        flex: 1,
        alignItems: 'center',
        justifyContent: 'center'
    },
    button: {
        width: width * 0.3,
        backgroundColor: '#8260a7',
        borderRadius: 25,
        marginVertical: 10,
        paddingVertical: 10,
        flexDirection: 'row',
        zIndex: 10000
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
        fontWeight: '600',
        color: 'rgba(255, 255, 255, 0.9)',
    },
    errorText: {
        color: '#ffffff',
        fontSize: 12,
        paddingHorizontal: 16,
        fontWeight: '300',
    }
});

class Login extends Component {

    goSignupScreen() {
        Actions.signup()
    }

    loginUser = (values) => {
        console.log(values);
        this.props.dispatch(loginUser(values));
    }

    onSubmit = (values) => {
        this.loginUser(values);
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
        const { handleSubmit } = this.props
        return (
            <KeyboardAvoidingView
                style={styles.container}
                behavior="padding">
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

                <TouchableOpacity style={[styles.button, styles.shadowBox]} onPress={handleSubmit(this.onSubmit)}>
                    <FontAwesome style={styles.icon} name="sign-in" size={25} color="white" />
                    <Text style={styles.buttonText}>Login</Text>
                </TouchableOpacity>

                <View style={styles.singupContainer}>
                    <Text style={styles.signupText}>Need an account?</Text>
                    <TouchableOpacity onPress={this.goSignupScreen}><Text style={styles.signupButton}> Signup</Text></TouchableOpacity>
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
        form: "login",
        validate
    })
)(Login);