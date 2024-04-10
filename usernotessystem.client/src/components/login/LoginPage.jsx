import { useState } from "react";
import { useNavigate } from "react-router-dom";
import useAuth from "../../hooks/useAuth";
import axios from "../../api/axiosApi";

const LoginPage = () => {
    const { signIn } = useAuth();

    const navigate = useNavigate();

    const [userName, setUserName] = useState("");
    const [password, setPassword] = useState("");
    const [error, setError] = useState("");

    const handleSubmit = async() => {
        setError("");
        try {
            const response = await axios.post('/users/authenticate',
                JSON.stringify({ userName, password }),
                {
                    headers: { 'Content-Type': 'application/json' },
                    withCredentials: true
                }
            );
            signIn(response.data);
            navigate("/home");
        } catch (error) {
            setError('Login failed');
        } finally {
            setUserName('');
            setPassword('');
        }
    }

    return (
        <div className="flex items-center justify-center h-screen">
        <div className="bg-gray-100 p-8 rounded-lg shadow-lg">
          <form 
            onSubmit={e => {
                e.preventDefault();
                handleSubmit();
            }} 
            className="space-y-4"
            >
            {error.length !== 0 && <p className='text-red-500'>{error}</p>}
            <div>
              <label htmlFor="email" className="block font-medium">Username:</label>
              <input 
                type="text"
                className="w-full border rounded-md p-2"
                onChange={e => setUserName(e.target.value)}
                value={userName}
                required
              />
            </div>
            <div>
              <label htmlFor="password" className="block font-medium">Password:</label>
              <input 
                type="password" 
                className="w-full border rounded-md p-2" 
                onChange={e => setPassword(e.target.value)}
                value={password}
                required
              />
            </div>
            <button type="submit" className="w-full bg-blue-500 text-white font-bold py-2 px-4 rounded">Log in</button>
          </form>
        </div>
      </div>
    )
}

export default LoginPage