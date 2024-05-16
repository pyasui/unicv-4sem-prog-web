import React from "react";
import { Button, ButtonGroup, Container, Table } from "react-bootstrap";
import { FaThumbsUp } from 'react-icons/fa';

const CategoryList = () => {
    return (
        <Container>
            <Table striped bordered hover size="sm">
                <thead>
                    <tr>
                        <th>#</th>
                        <th>Nome</th>
                        <th>Ações</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td>1</td>
                        <td>Comédia</td>
                        <td>
                            <Button variant="primary" size="sm">
                                <FaThumbsUp />
                            </Button>
                        </td>
                    </tr>
                    <tr>
                        <td>2</td>
                        <td>Comédia Romântica</td>
                        <td>
                            <ButtonGroup aria-label="Basic example">
                                <Button size="sm" variant="secondary">Editar</Button>
                                <Button size="sm" variant="danger">Excluir</Button>
                            </ButtonGroup>
                        </td>
                    </tr>
                    <tr>
                        <td>3</td>
                        <td>Suspense</td>
                    </tr>
                </tbody>
            </Table>
        </Container>
    );
};

export default CategoryList;