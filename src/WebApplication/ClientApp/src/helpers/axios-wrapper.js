import { useRef, useEffect } from 'react';
import axios from 'axios';
import { useAuth0 } from '../auth/react-auth0-spa';
import { myConfig } from '../configurations';

export default () => {
  const { getTokenSilently, loginWithRedirect } = useAuth0();
  const api = useRef(
    axios.create({
      baseURL: myConfig.backUrl
    })
  );
  useEffect(() => {
    const currentAPI = api.current;
    const requestInterceptorId = currentAPI.interceptors.request.use(async config => {
      const token = await getTokenSilently();
      config.headers.authorization = `Bearer ${token}`;
      config.cancelToken = axios.CancelToken.source().token;
      return config;
    });
    const responseInterceptorId = currentAPI.interceptors.response.use(null, async error => {
      if (error.config && error.response && error.response.status === 401) {
        await loginWithRedirect({
          redirect_uri: window.location.origin
        });
      }

      return Promise.reject(error);
    });
    return () => {
      currentAPI.interceptors.request.eject(requestInterceptorId);
      currentAPI.interceptors.response.eject(responseInterceptorId);
    };
  });
  return api.current;
};