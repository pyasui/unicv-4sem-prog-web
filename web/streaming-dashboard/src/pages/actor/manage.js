import React from "react";
import { useParams } from "react-router-dom";
 
const ActorManage = () => {
    let { id } = useParams();

    let mensagem = id ? 'Editar': 'Novo';
    console.log(id, mensagem);

    return (
        <div>
            <h1>{ mensagem } Ator</h1>
        </div>
    );
};
 
export default ActorManage;