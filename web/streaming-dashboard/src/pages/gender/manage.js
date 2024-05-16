import React from "react";
import { useParams } from "react-router-dom";
 
const GenderManage = () => {
    let { id } = useParams();
    console.log('id', id);

    return (
        <div>
            <h1>Criação/edição de gêneros</h1>
        </div>
    );
};
 
export default GenderManage;