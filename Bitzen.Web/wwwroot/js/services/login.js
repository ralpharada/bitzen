class Login {
    static url = 'usuario';
    static fazerLogin = async (email, password) => {
        try {
            const resposta = await axios.post(`${API.urlApi}${this.url}/login`, {
                email: email,
                password: password
            });
            if (!resposta.data.success)
                return resposta.data.data;
            const expiresIn = resposta.data.data.expires_in;
            const dataExpiracao = new Date();
            dataExpiracao.setSeconds(dataExpiracao.getSeconds() + expiresIn);

            document.cookie = `jwt=${resposta.data.data.access_token};  expires=${dataExpiracao.toUTCString()}; path=/`;
            document.cookie = `rtk=${resposta.data.data.refresh_token}; path=/`;

            if (API.verificarJWTValido()) {
                window.location.href = '/';
            }
        } catch (erro) {
            console.error('Erro ao fazer login:', erro);
        }
    }
}