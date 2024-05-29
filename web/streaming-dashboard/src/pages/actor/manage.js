import React, { useState, useEffect } from "react";
import { Button, Container, Form, Row } from "react-bootstrap";
import { useParams, useNavigate } from "react-router-dom";
import { ApiRoutes } from "../../services/apiRoute";
import APIService from "../../services/api";
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faArrowLeft } from '@fortawesome/free-solid-svg-icons';

const ActorManage = () => {
    const { id } = useParams();
    const isNew = !id;
    const message = isNew ? 'Novo' : 'Editar';
    const apiService = new APIService();

    const [model, setModel] = useState({ name: '', profile: '' });

    const navigate = useNavigate();

    useEffect(() => {
        if (!isNew) {
            const loadData = async () => {
                try {
                    const route = `${ApiRoutes.actor}/${id}`;
                    const response = await apiService.getData(route);
                    setModel(response);
                } catch (error) {
                    alert('Erro ao carregar dados do ator');
                }
            };
            loadData();
        }
    }, []);

    const saveClick = async (event) => {
        event.preventDefault();
        try {
            const route = ApiRoutes.actor;

            if (isNew) {
                await apiService.postData(route, model);
            } else {
                await apiService.putData(`${route}/${id}`, model);
            }

            backPage();
        } catch (error) {
            console.log(error);
            alert('Erro ao salvar');
        }
    };

    const handleChange = (event) => {
        const { name, value } = event.target;
        setModel(prevModel => ({ ...prevModel, [name]: value }));
    };

    const backPage = () => {
        navigate('/atores');

    }
    return (
        <Container>
            <div className="d-flex align-items-center mb-3">
                <Button variant="link" className="p-0 me-2" onClick={() => backPage()}>
                    <FontAwesomeIcon icon={faArrowLeft} size="lg" />
                </Button>
                <h3 className="mb-0">{message} Ator</h3>
            </div>
            <Form onSubmit={saveClick}>
                <Form.Group className="mb-3" controlId="formName">
                    <Form.Label>Nome</Form.Label>
                    <Form.Control
                        type="text"
                        name="name"
                        placeholder="Nome do ator"
                        value={model.name}
                        onChange={handleChange}
                    />
                </Form.Group>

                <Form.Group className="mb-3" controlId="formProfile">
                    <Form.Label>Perfil</Form.Label>
                    <Form.Control
                        as="textarea"
                        rows={3}
                        type="text"
                        name="profile"
                        placeholder="Perfil do ator"
                        value={model.profile}
                        onChange={handleChange}
                    />
                </Form.Group>

                <Button variant="primary" type="submit">
                    Salvar
                </Button>
                <Button variant="secondary" className="ms-2" onClick={backPage}>
                    Cancelar
                </Button>
            </Form>
        </Container>
    );
};

export default ActorManage;
