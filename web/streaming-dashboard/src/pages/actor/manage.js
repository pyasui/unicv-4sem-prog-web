import React, { useState } from "react";
import { Button, Container, Form } from "react-bootstrap";
import { useParams } from "react-router-dom";
import { ApiRoutes } from "../../services/apiRoute";
import APIService from "../../services/api";

const ActorManage = () => {
    let { id } = useParams();
    const isNew = id == null;
    let mensagem = !isNew ? 'Editar' : 'Novo';

    const [name, setName] = useState('');
    const [profile, setProfile] = useState('');

    const saveClick = async () => {
        try {
            // const apiService = new APIService();
            // const route = ApiRoutes.actor;

            // if (isNew){
            //     // await apiService.postData(route, obj);
            // }
            // else {
            //     // await apiService.putData(`${route}/${id}`, obj);
            // }
        }
        catch (error) {
            console.log(error);
            alert('erro ao salvar');
        }
    };

    return (
        <Container>
            <h3>{mensagem} Ator</h3>
            <Form>
                <Form.Group className="mb-3" controlId="formName">
                    <Form.Label>Nome</Form.Label>
                    <Form.Control type="text" placeholder="Nome do ator" />
                </Form.Group>

                <Form.Group className="mb-3" controlId="formProfile">
                    <Form.Label>Perfil</Form.Label>
                    <Form.Control as="textarea" rows={3} type="text" placeholder="Perfil do ator" />
                </Form.Group>

                <Button variant="primary" type="submit" onClick={saveClick}>
                    Salvar
                </Button>
                <Button variant="secondary" className="ms-2">
                    Cancelar
                </Button>
            </Form>
        </Container>
    );
};

export default ActorManage;