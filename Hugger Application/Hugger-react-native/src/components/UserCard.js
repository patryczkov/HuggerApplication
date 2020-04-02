import React, { Component } from 'react';
import { StyleSheet, View, Text, TouchableOpacity, Image, Dimensions } from 'react-native';
import axios from 'axios';
import * as SecureStore from 'expo-secure-store';

const { width, height } = Dimensions.get('window');

export default class UserCard extends Component {

    constructor() {
        super()
        this.state = {
            user: []
        }
    }

    async componentDidMount() {
        const user_id = await SecureStore.getItemAsync('user_id');
        axios.get('http://huggerdev-001-site1.ctempurl.com/hugger/users/' + user_id)
            .then(res => {
                const user = res.data;
                this.setState({ user });
            })
            .catch(function (error) {
                console.log(error);
            });
    }
    
    resizeImage() {
        
    }

    render() {
        let user = this.state.user
        var path = user.folderId;

        var currentTime = new Date();
        var year = currentTime.getFullYear();

        return (
            <View style={styles.container}>

                <TouchableOpacity onPress={this.resizeImage}
                    flexGrow={1}
                    flex={1}>
                    <Image
                        source={{ uri: 'http://drive.google.com/uc?export=view&id=' + path }}
                        style={{ width: width / 1.2, height: height / 3, resizeMode: 'contain' }} />
                </TouchableOpacity>

                <View style={styles.descContainer}>
                    <Text style={styles.titleText}> {user.login}, {year - user.birthYear}</Text>
                    <Text style={styles.baseText}> {user.description}</Text>
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
    descContainer: {
        marginTop: 5,
        flex: 1,
        borderWidth: 1,
        borderBottomColor: 'white',
        borderTopColor: 'transparent',
        borderStyle: 'dotted'
    },
    baseText: {
        color: 'white',
        fontFamily: 'Roboto',
        fontSize: 25,
        fontWeight: '400',
        padding: 10,
        textAlign: 'left',
    },
    titleText: {
        color: 'white',
        fontFamily: 'Roboto',
        fontSize: 25,
        fontWeight: 'bold',
        padding: 10,
        textAlign: 'left',
    },
});
