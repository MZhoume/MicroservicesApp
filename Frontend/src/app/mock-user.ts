/**
 * Created by User on 02/01/2017.
 */
import {User} from './user'

export const USER_Response1 = {
    flag:"success",
    user: {
        uid: "12",
        firstname: "hf",
        JWT: "inocandiuiabgd",
    },
    reason: "",
};

export const USER_Response2 = {
    flag:"fail",
    user: {
        uid: "",
        firstname: "",
        JWT: "",
    },
    reason: "wrong pwd",
};

