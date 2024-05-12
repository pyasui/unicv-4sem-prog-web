import logo from './logo.svg';
import './App.css';
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import CategoryList from './pages/category';
import GenderList from './pages/gender';
import HomePage from './pages/home';
import { NavbarBootstrap } from './components/navbar/navbar-bootstrap';
import DirectorList from './pages/director';
import ActorList from './pages/actor';

function App() {
  return (
    <Router>
        <NavbarBootstrap />
        <Routes>
            <Route exact path="/" element={<HomePage />} />
            <Route path="/categorias" element={<CategoryList />} />
            <Route path="/generos" element={<GenderList />} />
            <Route path="/atores" element={<ActorList />} />
            <Route path="/diretores" element={<DirectorList />} />
        </Routes>
    </Router>
);
}

export default App;
