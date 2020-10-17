const prod = {
    url: {
        API_URL: "https://prtg-api.it-one.com.ar/api/"
    }
};
const dev = {
    url: {
        API_URL: "https://localhost:5001/api/"
    }
};

export const config = process.env.NODE_ENV === 'development' ? dev : prod;