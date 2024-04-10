import { useContext } from "react";
import AuthContext from "../authentication/AuthContextProvider";

const useAuth = () => useContext(AuthContext);

export default useAuth;