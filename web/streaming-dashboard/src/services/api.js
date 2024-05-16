import axios from 'axios';

class APIService {
    constructor() {
        this.baseURL = 'https://api-streaming-unicv.azurewebsites.net/api';
    }

    async obterDados(route) {
        const url = `${this.baseURL}/${route}`;
        try {
            const response = await axios.get(url);
            return response.data;
        } catch (error) {
            console.error('Erro:', error);
            throw error;
        }
    }

    async enviarDados(route, dados) {
        const url = `${this.baseURL}/${route}`; // Use a rota definida
        try {
            const response = await axios.post(url, dados);
            return response.data;
        } catch (error) {
            console.error('Erro:', error);
            throw error;
        }
    }
}

export default APIService;