import axios from 'axios';
import * as SecureStore from 'expo-secure-store';

export const like = (id) => {
    console.log("like");

    /*return async () => {
        axios.post('http://huggerdev-001-site1.ctempurl.com/hugger/users/{userId}/Hugs', {

        })
            .catch((error) => {
                console.error(error);
                console.log(error)
            })
    }*/
}

export const dislike = () => {
    return async () => {
        axios.post('http://huggerdev-001-site1.ctempurl.com/hugger/users/{userId}/Hugs', {

        })
            .catch((error) => {
                console.error(error);
                console.log(error)
            })
    }
}