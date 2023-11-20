class Motorista {
    static url = 'motorista';

    static getListarAxios = async () => {
        API.authBearer();
        return await axios.get(`${API.urlApi}${this.url}/listar`);
    }
    static postAxios = async (body) => {
        API.authBearer();
        return await axios.post(`${API.urlApi}${this.url}`, body);
    }
    static deleteAxios = async (id) => {
        API.authBearer();
        return await axios.delete(`${API.urlApi}${this.url}/${id}`);
    }
    static putAxios = async (body) => {
        API.authBearer();
        return await axios.put(`${API.urlApi}${this.url}`, body);
    }
}