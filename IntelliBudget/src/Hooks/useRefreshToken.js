import useAuth from "./useAuth";

const useRefreshToken = () => {
    const { auth } = useAuth();

    const refresh = async () => {
        return auth?.accessToken;
    }
    return refresh;
};

export default useRefreshToken;