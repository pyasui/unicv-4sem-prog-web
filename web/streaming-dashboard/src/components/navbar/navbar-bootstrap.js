import { Navbar, Container, Nav, NavDropdown } from "react-bootstrap";
import { NavLink } from "react-router-dom";

export const NavbarBootstrap = () => {
    return (
        <>
            <Navbar expand="lg" className="bg-body-tertiary">
                <Container>
                    <Navbar.Brand href="#home">Dashboard - Streaming</Navbar.Brand>
                    <Navbar.Toggle aria-controls="basic-navbar-nav" />
                    <Navbar.Collapse id="basic-navbar-nav">
                        <Nav className="me-auto">
                            <Nav.Link href="/">Home</Nav.Link>

                            <NavDropdown title="Cadastros" id="basic-nav-dropdown">
                                <NavDropdown.Item href="/categorias">Categorias</NavDropdown.Item>
                                <NavDropdown.Item href="/generos">Gêneros</NavDropdown.Item>
                                <NavDropdown.Item href="/diretores">Diretores</NavDropdown.Item>
                                <NavDropdown.Item href="/atores">Atores</NavDropdown.Item>
                                <NavDropdown.Divider />
                                <NavDropdown.Item href="#action/3.4">
                                    Separated link
                                </NavDropdown.Item>
                            </NavDropdown>
                        </Nav>
                    </Navbar.Collapse>
                </Container>
            </Navbar>
        </>
    );
}