const user = "user";
const token = "token";

const storeUser = (data) => {
    const { id, role, userName, token } = data;
    const isAdmin = role?.toLowerCase() === "administrator" ?? false;
    const userData = { id: id, userName, role: role, isAdmin };
    localStorage.setItem(token, token);
    localStorage.setItem(user, JSON.stringify(userData));
    return {userData, token};
}

const removeUser = () => {
    localStorage.removeItem(user);
    localStorage.removeItem(token);
};

const getUser = () => JSON.parse(localStorage.getItem(user));

const getToken = () => JSON.parse(localStorage.getItem(token));

export { 
    storeUser,
    removeUser,
    getUser,
    getToken
 }