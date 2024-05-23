import React, { useEffect, useState } from "react";
import api from "../../services/api";
import { ApiRoutes } from "../../services/apiRoute";
import APIService from "../../services/api";
import { Badge, Button, Container, Table } from "react-bootstrap";
import Actor from "../../models/actor";
import { MdDeleteOutline, MdEdit } from "react-icons/md";
import { Link } from "react-router-dom";

const ActorList = () => {
    const [actors, setActors] = useState([]);

    useEffect(() => {
        loadData();
    }, []);

    const loadData = async () => {
        try {
            const apiService = new APIService();
            const data = await apiService.getData(ApiRoutes.actor);

            setActors(data);
        } catch (error) {
            console.error('Erro ao carregar atores:', error);
        }
    };

    const deleteActorClick = async (id) => {
        var confirm = window.confirm('Deseja excluir este registro?');
        if (confirm) {
            const apiService = new APIService();
            await apiService.deleteData(`${ApiRoutes.actor}/${id}`);

            loadData();
        }
    };

    return (
        <Container>
            <div className="d-flex justify-content-between align-items-center mt-4 mb-4">
                <h3>Listagem de atores</h3>
                <Link to={`/atores/gerenciar/`}>
                    <Button color="primary">Novo</Button>
                </Link>
            </div>
            <Table striped bordered hover size="sm">
                <thead>
                    <tr>
                        <th>#</th>
                        <th>Nome</th>
                        <th style={{ width: "200px" }}>Ações</th>
                    </tr>
                </thead>
                <tbody>
                    {actors.map((ator) => (
                        <tr key={ator.id}>
                            <td className="text-right">{ator.id}</td>
                            <td>{ator.name}</td>
                            <td className="text-center">
                                <Link to={`/atores/gerenciar/${ator.id}`}>
                                    <Badge bg="primary" pill>
                                        <MdEdit />
                                    </Badge>
                                </Link>
                                &nbsp;|&nbsp;
                                <Badge bg="danger" pill onClick={() => deleteActorClick(ator.id)} >
                                    <MdDeleteOutline />
                                </Badge>
                            </td>
                        </tr>
                    ))}
                </tbody>
            </Table>
        </Container>
    );
};

export default ActorList;