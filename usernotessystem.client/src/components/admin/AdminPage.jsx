import { useEffect, useState } from "react";
import User from "./User";
import useAuthAxios from "../../hooks/useAuthAxios";
import useAuth from "../../hooks/useAuth";

const AdminPage = () => {
    const [users, setUsers] = useState([]);
    const [newUserName, setNewUserName] = useState('');
    const [newUserPassword, setNewUserPassword] = useState('');
    const [newUserRole, setNewUserRole] = useState('');
    const [error, setError] = useState('');

    const axios = useAuthAxios();
    const { currentUser } = useAuth();

    useEffect(() => {
        const fetchUsers = async () => {
            const response = await axios.get("/users");
            setUsers(response.data);
        };

        fetchUsers();
    }, [axios]);

    const handleAddUser = async () => {
        if (newUserName.trim() === '' || newUserPassword.trim() === '' || newUserRole.trim() === '') {
        return;
        }

        const passwordRegex = /^(?=.*[A-Z])(?=.*\W)/;
        if (!passwordRegex.test(newUserPassword)) {
        setError('Password should contain at least one non-alphanumeric and uppercase character.');
        return;
        }

        const roles = {
            "administrator" : 1,
            "regular": 0
        };
        const newUser = { userName: newUserName, password: newUserPassword };
        const response = await axios
            .post("/users", { ...newUser, role: roles[newUserRole]})
            .catch(error => setError(error));

        setUsers([...users, { id: response.data, ...newUser, role: newUserRole }]);
        setNewUserName('');
        setNewUserPassword('');
        setNewUserRole('');
    };

    const handleRemoveUser = async (id) => {
        await axios.delete(`/users/${id}`)
            .catch(error => setError(error));
        const updatedUsers = users.filter(user => user.id !== id);
        setUsers(updatedUsers);
    };

    return (
        <div className="container mx-auto mt-8">
        <h1 className="text-2xl font-bold mb-4">Admin Page</h1>
        <div className="mb-4">
            <input
            type="text"
            className="border border-gray-400 px-4 py-2 mr-2"
            placeholder="Enter user name"
            value={newUserName}
            onChange={(e) => setNewUserName(e.target.value)}
            />
            <input
            type="text"
            className="border border-gray-400 px-4 py-2 mr-2"
            placeholder="Enter password"
            value={newUserPassword}
            onChange={(e) => setNewUserPassword(e.target.value)}
            />
            <select
            className="border border-gray-400 px-4 py-2 mr-2"
            value={newUserRole}
            onChange={(e) => setNewUserRole(e.target.value)}
            >
            <option value="">Select role</option>
            <option value="administrator">Administrator</option>
            <option value="regular">Regular</option>
            </select>
            <button
            className="bg-blue-500 text-white px-4 py-2 rounded hover:bg-blue-600"
            onClick={handleAddUser}
            >
            Add User
            </button>
        </div>
        {error.length !== 0 && <p className='text-red-500'>{error}</p>}
        <div>
            {users.length > 0 ? (
            <ul>
                {users.map(user => (
                <User 
                    key={user.id} 
                    user={user} 
                    onRemove={handleRemoveUser} 
                    removeActive={ user.id !== currentUser.id } />
                ))}
            </ul>
            ) : (
            <p>No users found.</p>
            )}
        </div>
        </div>
    );
}

export default AdminPage;