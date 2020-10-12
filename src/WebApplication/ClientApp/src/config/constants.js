const prod = {
    url: {
        API_URL: "https://prtg-api.it-one.com.ar:23443/api/"
    }
};
const dev = {
    url: {
        API_URL: "https://localhost:44316/api/"
    }
};

export const config = process.env.NODE_ENV === 'development' ? dev : prod;