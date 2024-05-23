import axios from 'axios';

class APIService {
    constructor() {
        this.baseURL = 'https://api-streaming-unicv.azurewebsites.net/api';
    }

    async getData(route) {
        const url = `${this.baseURL}/${route}`;
        try {
            const response = await axios.get(url);
            return response.data;
        } catch (error) {
            console.error('Erro:', error);
            throw error;
        }
    }

    async postData(route, data) {
        const url = `${this.baseURL}/${route}`;
        try {
            const response = await axios.post(url, data);
            return response.data;
        } catch (error) {
            console.error('Erro:', error);
            throw error;
        }
    }

    async putData(route, data) {
        const url = `${this.baseURL}/${route}`;
        try {
            const response = await axios.put(url, data);
            return response.data;
        } catch (error) {
            console.error('Erro:', error);
            throw error;
        }
     }

    async deleteData(route) { 
        const url = `${this.baseURL}/${route}`;
        try {
            const response = await axios.delete(url);
            return response.data;
        } catch (error) {
            console.error('Erro:', error);
            throw error;
        }
    }
}

export default APIService;