import { fetchApi } from "../service/api";
import * as SecureStore from 'expo-secure-store';

export const createNewUser = (payload) => {
    return async (dispatch) => {

        try {
            dispatch({
                type: "CREATE_USER_LOADING"
            });
            const response = await fetchApi("/account/register", "POST", payload, 200);

            if (response.success) {
                dispatch({
                    type: "CREAT_USER_SUCCESS"
                });
                dispatch({
                    type: "AUTH_USER_SUCCESS",
                    token: response.responseBody.token
                });
                dispatch({
                    type: "GET_USER_SUCCESS",
                    payload: response.responseBody
                });

                return response;
            } else {
                throw response;
            }

        } catch (error) {
            dispatch({
                type: "CREAT_USER_FAIL",
                payload: error.responseBody
            });
            return error;
        }
    }
}

export const loginUser = (payload) => {
    return async (dispatch) => {

        try {
            dispatch({
                type: "LOGIN_USER_LOADING"
            });
            const response = await fetchApi("/account/login", "POST", payload, 200);

            if (response.success) {

                await SecureStore.setItemAsync('user_id', response.responseBody.id.toString());

                dispatch({
                    type: "LOGIN_USER_SUCCESS",
                });
                dispatch({
                    type: "AUTH_USER_SUCCESS",
                    token: response.responseBody.token
                });
                dispatch({
                    type: "GET_USER_SUCCESS",
                    payload: response.responseBody
                });

                return response;
            } else {
                throw response;
            }

        } catch (error) {
            dispatch({
                type: "LOGIN_USER_FAIL",
                payload: error.responseBody
            });
            return error;
        }
    }
}

export const logoutUser = () => {
    return async (dispatch, getState) => {
        const state = getState();
        try {
            const { authReducer: { authData: { token } } } = state;
            const response = await fetchApi("/user/logout", "DELETE", null, 200, token);
            console.log(response);
            await SecureStore.deleteItemAsync('user_id');

            dispatch({
                type: "USER_LOGGED_OUT_SUCCESS"
            });
        } catch (e) {
            console.log(e);
        }
    }
}