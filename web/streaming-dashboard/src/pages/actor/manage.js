import React from "react";
import { useParams } from "react-router-dom";
 
const ActorManage = () => {
    let { id } = useParams();
    console.log('id', id);

    return (
        <div>
            <h1>Criação/edição de atores</h1>
        </div>
    );
};
 
export default ActorManage;