import React from "react";
import { useParams } from "react-router-dom";
 
const DirectorManage = () => {
    let { id } = useParams();
    console.log('id', id);

    return (
        <div>
            <h1>Criação/edição de diretores</h1>
        </div>
    );
};
 
export default DirectorManage;