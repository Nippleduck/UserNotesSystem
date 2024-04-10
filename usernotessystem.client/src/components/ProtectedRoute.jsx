import React from "react";
import { Navigate, Outlet, useLocation } from "react-router-dom";
import { getUser } from "../authentication/storage";

const ProtectedRoute = ({ allowedRoles }) => {
    const currentUser = getUser();
    const location = useLocation();

    return allowedRoles.includes(currentUser?.role.toLowerCase()) ?
    <Outlet/> :
    <Navigate to={'/auth'} state={{'from': location}} replace/>
}

export default ProtectedRoute;