import useAuth from "../../hooks/useAuth";

const HomePage = () => {
    const { currentUser } = useAuth();

    return (
        <div className="flex justify-center items-center h-screen">
      <div className="bg-gray-200 p-6 rounded-lg shadow-lg">
        <h1 className="text-3xl font-bold mb-4">User Information</h1>
        <div className="text-lg mb-2">
          <p>
            <span className="font-bold">User ID:</span> {currentUser.id}
          </p>
          <p>
            <span className="font-bold">Username:</span> {currentUser.userName}
          </p>
          <p>
            <span className="font-bold">Role:</span> {currentUser.role}
          </p>
        </div>
      </div>
    </div>
    )
}

export default HomePage;