export const axiosRequestInterceptor = async config => {

    const token = "Bearer ";
    
    if (token) {
        config.headers.common.Accept = "application/json; charset=utf-8";
    }
    return config;
};