import { Link, useLocation } from "react-router-dom";
import useAuth from "../hooks/useAuth";

const Header = () => {
    const { currentUser, signOut } = useAuth();
    const location = useLocation();

    return (
        location.pathname === "/" ? <></> :
        <header className="bg-gray-800 p-4 flex justify-between items-center">
        < div className="flex justify-center items-center">
            <Link to="/home" className="text-white mr-4">Home</Link>
            <Link to="/notes" className="text-white mr-4">Notes</Link>
            { currentUser.isAdmin && <Link to="/admin" className="text-white">Users</Link> }
        </div>
        <div>
            <button onClick={signOut} className="bg-red-600 text-white px-4 py-2 rounded hover:bg-red-700">Sign Out</button>
        </div>
    </header>
    )
}

export default Header;