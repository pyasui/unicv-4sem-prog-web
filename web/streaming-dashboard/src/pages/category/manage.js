import React from "react";
import { useParams } from "react-router-dom";
 
const CategoryManage = () => {
    let { id } = useParams();
    console.log('id', id);

    return (
        <div>
            <h1>Criação/edição de categorias</h1>
        </div>
    );
};
 
export default CategoryManage;