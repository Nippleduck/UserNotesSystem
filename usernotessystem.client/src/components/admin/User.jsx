const User = ({ user, onRemove, removeActive }) => (
    <li className="flex items-center justify-between mb-2">
      <span>{user.id} - {user.userName} - {user.role}</span>
      {
        removeActive && 
        <button
        className="bg-red-500 text-white px-2 py-1 rounded hover:bg-red-600"
        onClick={() => onRemove(user.id)}
      >
        Remove
      </button>
      }
    </li>
  );

  export default User;