import './App.css';
import { Routes, Route } from "react-router-dom";
import LoginPage from './components/login/LoginPage';
import HomePage from './components/home/HomePage';
import Notes from './components/notes/Notes';
import AdminPage from './components/admin/AdminPage';
import ProtectedRoute from './components/ProtectedRoute';
import Header from './components/Header';

function App() {
  return (
    <>
    <Header/>
    <Routes>
      <Route exact path='/' element={<LoginPage/>}/>
        <Route element={<ProtectedRoute allowedRoles={["regular", "administrator"]}/>}>
          <Route path='/home' element={<HomePage/>}/>
        </Route>
        <Route element={<ProtectedRoute allowedRoles={["regular", "administrator"]}/>}>
          <Route path='/notes' element={<Notes/>}/>
        </Route>
        <Route element={<ProtectedRoute allowedRoles={["administrator"]}/>}>
          <Route path='/admin' element={<AdminPage/>}/>
        </Route>
    </Routes>
    </>
  )
}

export default App;
