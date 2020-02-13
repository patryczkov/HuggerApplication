import React, { Component } from 'react';
import { FlatList, Platform, StyleSheet, Text, View, Image, Dimensions, Animated, PanResponder } from 'react-native';
import axios from 'axios';

const { width, height } = Dimensions.get('window');

export default class App extends Component {

    constructor() {
        super()

        this.position = new Animated.ValueXY()
        this.state = {
            currentIndex: 0,
            users: [],
        }

        this.rotate = this.position.x.interpolate({
            inputRange: [-height / 2, 0, height / 2],
            outputRange: ['-10deg', '0deg', '10deg'],
            extrapolate: 'clamp'
        })

        this.rotateAndTranslate = {
            transform: [{
                rotate: this.rotate
            },
            ...this.position.getTranslateTransform()
            ]
        }

        this.likeOpacity = this.position.y.interpolate({
            inputRange: [-height / 2, 0, height / 2],
            outputRange: [0, 0, 1],
            extrapolate: 'clamp'
        })
        this.dislikeOpacity = this.position.y.interpolate({
            inputRange: [-height / 2, 0, height / 2],
            outputRange: [1, 0, 0],
            extrapolate: 'clamp'
        })

        this.nextCardOpacity = this.position.y.interpolate({
            inputRange: [-height / 2, 0, height / 2],
            outputRange: [1, 0.6, 1],
            extrapolate: 'clamp'
        })
        this.nextCardScale = this.position.y.interpolate({
            inputRange: [-height / 2, 0, height / 2],
            outputRange: [1, 0.8, 1],
            extrapolate: 'clamp'
        })

    }

    componentDidMount() {
        axios.get('https://localhost:50000/hugger/users')
            .then(result => {
                const users = result.data;
                this.setState({ users });

                console.log(users);
            })
            .catch((error) => {
                console.error(error);
                console.log(error)
            })
    }

    componentWillMount() {
        this.PanResponder = PanResponder.create({

            onStartShouldSetPanResponder: (evt, gestureState) => true,
            onPanResponderMove: (evt, gestureState) => {

                this.position.setValue({ x: gestureState.dx, y: gestureState.dy })
            },
            onPanResponderRelease: (evt, gestureState) => {

                if (gestureState.dy > 200) {
                    Animated.spring(this.position, {
                        toValue: { x: gestureState.dx, y: height + 100 }
                    }).start(() => {
                        this.setState({ currentIndex: this.state.currentIndex + 1 }, () => {
                            this.position.setValue({ x: 0, y: 0 })
                        })
                    })
                }
                else if (gestureState.dy < -200) {
                    Animated.spring(this.position, {
                        toValue: { x: gestureState.dx, y: -height - 100 }
                    }).start(() => {
                        this.setState({ currentIndex: this.state.currentIndex + 1 }, () => {
                            this.position.setValue({ x: 0, y: 0 })
                        })
                    })
                }
                else {
                    Animated.spring(this.position, {
                        toValue: { x: 0, y: 0 },
                        friction: 4
                    }).start()
                }
            }
        })
    }

    renderUsers = () => {
        let users = this.state.users

        return users.map((item, i) => {

            if (i < this.state.currentIndex) {
                return null
            } else if (i == this.state.currentIndex) {

                return (
                    <Animated.View
                        {...this.PanResponder.panHandlers}
                        key={item.id} style={[this.rotateAndTranslate, { height: height - 150, width: width, padding: 5, position: 'absolute' }]}>

                        <View style={{ position: 'absolute', top: 500, zIndex: 1000 }}>
                            <Text style={styles.titleText}> {item.login}, {item.age} </Text>
                        </View>
                        
                        <Animated.View style={{ opacity: this.likeOpacity, position: 'absolute', top: 100, right: 100, zIndex: 1000 }}>
                            <Text style={{ borderWidth: 2, borderColor: 'green', color: 'green', fontSize: 45, fontWeight: 'bold', padding: 10 }}>LIKE</Text>
                        </Animated.View>

                        <Animated.View style={{ opacity: this.dislikeOpacity, position: 'absolute', top: 470, right: 100, zIndex: 1000 }}>
                            <Text style={{ borderWidth: 2, borderColor: 'red', color: 'red', fontSize: 32, fontWeight: 'bold', padding: 10 }}>NOPE</Text>
                        </Animated.View>

                        <Image
                            style={{ flex: 1, height: null, width: null, resizeMode: 'cover', borderRadius: 20 }}
                            source={{ uri: require("./assets/profiles/1.jpg") }} />

                    </Animated.View>
                )
            } else {
                return (

                    <Animated.View
                        {...this.PanResponder.panHandlers}
                        key={item.id} style={[{
                            opacity: this.nextCardOpacity,
                            transform: [{ scale: this.nextCardScale }],
                            height: height - 120, width: width, padding: 10, position: 'absolute'
                        }]}>

                        <View style={{ position: 'absolute', top: 500, zIndex: 1000 }}>
                            <Text style={styles.titleText}>{item.name}, {item.age}</Text>
                        </View>

                        <Animated.View style={{ opacity: 0, transform: [{ rotate: '-30deg' }], position: 'absolute', top: 150, right: 150, zIndex: 1000 }}>
                            <Text style={{ borderWidth: 2, borderColor: 'green', color: 'green', fontSize: 45, fontWeight: 'bold', padding: 10 }}>LIKE</Text>

                        </Animated.View>

                        <Animated.View style={{ opacity: 0, transform: [{ rotate: '30deg' }], position: 'absolute', top: 470, right: 100, zIndex: 1000 }}>
                            <Text style={{ borderWidth: 2, borderColor: 'red', color: 'red', fontSize: 32, fontWeight: 'bold', padding: 10 }}>NOPE</Text>

                        </Animated.View>

                        <Image
                            style={{ flex: 1, height: null, width: null, resizeMode: 'cover', borderRadius: 20 }}
                            source={{ uri: require("./assets/profiles/1.jpg") }} />

                    </Animated.View>
                )
            }
        }).reverse()
    }

    render() {
        return (
            <View style={{ flex: 1 }}>
                <View style={{ height: 60 }}>
                </View>

                <View style={{ flex: 1 }}>
                    {this.renderUsers()}

                    <View style={{ top: 200, zIndex: -1 }}>
                        <Text style={{ color: 'black', fontSize: 40, fontWeight: 'bold', textAlign: 'center', }}> No more people in your city! </Text>
                    </View>

                </View>

                <View style={{ height: 60 }}>
                </View>

            </View>
        );
    }
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    justifyContent: 'center',
    alignItems: 'center',
    backgroundColor: '#F5FCFF',
    },

    baseText: {
        color: 'black',
        fontFamily: 'Roboto',
        fontSize: 25,
        fontWeight: '400',
        padding: 10,
        textAlign: 'left',
        right: 10
    },
    titleText: {
        color: 'white',
        fontFamily: 'Roboto',
        fontSize: 40,
        fontWeight: 'bold',
        padding: 10,
        textAlign: 'left',
        left: 15
    },
    item: {
        backgroundColor: '#f9c2ff',
        padding: 20,
        marginVertical: 8,
        marginHorizontal: 16,
    },
    title: {
        fontSize: 32,
    },
});
