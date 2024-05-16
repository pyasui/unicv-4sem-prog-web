import React, { useEffect, useState } from "react";
import api from "../../services/api";
import { ApiRoutes } from "../../services/apiRoute";
import APIService from "../../services/api";
import { Button, Container, Table } from "react-bootstrap";
import Actor from "../../models/actor";
import { MdDeleteOutline, MdEdit } from "react-icons/md";

const ActorList = () => {
    const [atores, setAtores] = useState([]);

    useEffect(() => {
        const carregarAtores = async () => {
            try {
                const apiService = new APIService();
                const dados = [];//  await apiService.obterDados(ApiRoutes.actor);

                const ator1 = new Actor(1, 'Brad Pitt');
                const ator2 = new Actor(2, 'Angelina Jolie');

                dados.push(ator1);
                dados.push(ator2);
                setAtores(dados);
            } catch (error) {
                console.error('Erro ao carregar atores:', error);
            }
        };

        carregarAtores();
    }, []); // O array vazio como segundo argumento garante que o efeito só será executado uma vez, equivalente ao componentDidMount

    const handleEditar = (id) => {
        console.log('Editar ator com ID:', id);
    };

    const handleExcluir = (id) => {
        console.log('Excluir ator com ID:', id);
    };

    return (
        <Container>
            <h1>Listagem de Atores</h1>

            <Table striped bordered hover size="sm">
                <thead>
                    <tr>
                        <th>#</th>
                        <th>Nome</th>
                        <th style={{ width: "200px" }}>Ações</th>
                    </tr>
                </thead>
                <tbody>
                    {atores.map((ator) => (
                        <tr key={ator.id}>
                            <td className="text-right">{ator.id}</td>
                            <td>{ator.nome}</td>
                            <td className="text-center">
                                <Button variant="primary" style={{ marginRight: '0.5rem' }} onClick={() => handleEditar(ator.id)}>
                                    <MdEdit />
                                </Button>
                                <Button variant="outline-danger" className="ml-2" onClick={() => handleExcluir(ator.id)}>
                                    <MdDeleteOutline />
                                </Button>
                            </td>
                        </tr>
                    ))}
                </tbody>
            </Table>
        </Container>
    );
};

export default ActorList;