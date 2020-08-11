import { Auth } from "aws-amplify"

export const axiosRequestInterceptor = async config => {
    const session = await Auth.currentSession();

    const token = "Bearer " + session.getIdToken().getJwtToken();
    
    if (token) {
        config.headers.Authorization = token;
        // config.headers.post["Content-Type"] = "application/json";
        config.headers.common.Accept = "application/json; charset=utf-8";
    }
    return config;
};