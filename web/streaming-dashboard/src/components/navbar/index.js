import React from "react";
import { Nav, NavLink, NavMenu } from "./NavbarElements";

const Navbar = () => {
	return (
		<>
			<Nav>
				<NavMenu>
					<NavLink to="/" activeStyle>
						Home
					</NavLink>
					<NavLink to="/categorias" activeStyle>
						Categorias
					</NavLink>
					<NavLink to="/generos" activeStyle>
						Gêneros
					</NavLink>
				</NavMenu>
			</Nav>
		</>
	);
};

export default Navbar;
