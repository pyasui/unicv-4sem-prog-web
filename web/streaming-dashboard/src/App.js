import logo from './logo.svg';
import './App.css';
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import CategoryList from './pages/category';
import GenderList from './pages/gender';
import HomePage from './pages/home';
import Navbar from './components/navbar';

function App() {
  return (
    <Router>
        <Navbar />
        <Routes>
            <Route exact path="/" element={<HomePage />} />
            <Route path="/categorias" element={<CategoryList />} />
            <Route path="/generos" element={<GenderList />} />
        </Routes>
    </Router>
);
}

export default App;
