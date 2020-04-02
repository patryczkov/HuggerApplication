import PropTypes from "prop-types";
import React, { Component } from "react";
import { TextInput, View, StyleSheet, Dimensions } from "react-native";

const { width, height } = Dimensions.get('window');

const propTypes = {
    mapElement: PropTypes.func,
    onSubmitEditing: PropTypes.func,
    onChangeText: PropTypes.func,
    value: PropTypes.string,
    placeholder: PropTypes.string,
    maxLength: PropTypes.number,
    keyboardType: PropTypes.string,
    secureTextEntry: PropTypes.bool,
    label: PropTypes.string
};

const defaultProps = {
    mapElement: (n) => { },
    onSubmitEditing: () => { },
    onChangeText: () => { },
    value: "",
    placeholder: "",
    maxLength: 200,
    keyboardType: "default",
    secureTextEntry: false,
    label: ""
};

const styles = StyleSheet.create({
    inputBox: {
        width: width * 0.7,
        height: 45,
        backgroundColor: 'rgba(255, 255, 255, 0.2)',
        color: '#ffffff',
        borderRadius: 50,
        paddingHorizontal: 16,
        marginVertical: 5
    }
});

class InputText extends Component {
    render() {
        const { placeholder, secureTextEntry, keyboardType, maxLength, value, onChangeText, onSubmitEditing } = this.props;

        return (
            <View>
                <TextInput
                    style={styles.inputBox}
                    underlineColorAndroid='transparent'
                    placeholder={placeholder}
                    placeholderTextColor='rgba(255, 255, 255, 0.8)'
                    selectionColor="#9c9c9c"
                    secureTextEntry={secureTextEntry}
                    keyboardType={keyboardType}
                    maxLength={maxLength}
                    returnKeyType="next"
                    onChangeText={onChangeText} />
            </View>
            );
    }
}

InputText.defaultProps = defaultProps;
InputText.propTypes = propTypes;

export default InputText;