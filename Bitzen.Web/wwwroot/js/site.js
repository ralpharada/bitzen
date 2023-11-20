class API {
    static urlApi = 'https://localhost:5001/api/';

    static authBearer = async () => {
        let jwt = this.obterJWT();
        if (!jwt) {
            const refresh = this.obterRefreshToken();
            if (!refresh) {
                window.location.reload();
                return;
            }
            jwt = await this.obterNovoAccessToken(refresh);
            window.location.reload();
        }
        axios.defaults.headers.common['Authorization'] = `Bearer ${jwt}`;
    }
    static verificarJWTValido = async () => {
        const jwt = this.obterJWT();
        if (!this.isTokenExpirado(jwt)) {
            const refresh = this.obterRefreshToken();
            if (!refresh) return false;
            await this.obterNovoAccessToken(refresh);
            return true;
        }
        return jwt && jwt !== 'undefined';
    }

    // Função para obter o JWT do cookie
    static obterJWT = () => {
        const cookies = document.cookie.split(';');
        for (const cookie of cookies) {
            const [nome, valor] = cookie.trim().split('=');
            if (nome === 'jwt') {
                return valor;
            }
        }
        return null;
    }
    static obterRefreshToken = () => {
        const cookies = document.cookie.split(';');
        for (const cookie of cookies) {
            const [nome, valor] = cookie.trim().split('=');
            if (nome === 'rtk') {
                return valor;
            }
        }
        return null;
    }
    static isTokenExpirado = (jwt) => {
        try {
            const payload = jwt.split('.')[1];
            const decodedPayload = atob(payload);
            const expirationDate = new Date(JSON.parse(decodedPayload).exp * 1000);

            return expirationDate > new Date();
        } catch (ex) { return false; }
    }
    static obterNovoAccessToken = async (refreshToken) => {
        const resposta = await axios.post(`${this.urlApi}usuario/refreshtoken`, {
            refreshToken: refreshToken
        });
        const expiresIn = resposta.data.data.expires_in;
        const dataExpiracao = new Date();
        dataExpiracao.setSeconds(dataExpiracao.getSeconds() + expiresIn);
        document.cookie = `jwt=${resposta.data.data.access_token};  expires=${dataExpiracao.toUTCString()}; path=/`;
        document.cookie = `rtk=${resposta.data.data.refresh_token}; path=/`;
        return resposta.data.data.access_token;
    }

    static abrirModal = () => {
        var modal = document.getElementById('minhaModal');
        modal.classList.remove('hidden');
    }

    static formatarValor = (valor) => {
        const valorFormatado = valor.toLocaleString('pt-BR', {
            style: 'currency',
            currency: 'BRL'
        });

        return valorFormatado;
    }
    static formatarData = (dataString) => {
        const data = new Date(dataString);

        const dia = String(data.getDate()).padStart(2, '0');
        const mes = String(data.getMonth() + 1).padStart(2, '0'); // Meses começam do zero
        const ano = data.getFullYear();
        const horas = String(data.getHours()).padStart(2, '0');
        const minutos = String(data.getMinutes()).padStart(2, '0');
        const segundos = String(data.getSeconds()).padStart(2, '0');

        const dataFormatada = `${dia}/${mes}/${ano} ${horas}:${minutos}:${segundos}`;

        return dataFormatada;
    }
    static sair = () => {
        this.limparCookies();
        window.location.reload();
    }
    static limparCookies = () => {
        const cookies = document.cookie.split(';');
        for (let i = 0; i < cookies.length; i++) {
            const cookie = cookies[i];
            const igualPosicao = cookie.indexOf('=');
            const nome = igualPosicao > -1 ? cookie.substr(0, igualPosicao) : cookie;
            document.cookie = `${nome}=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;`;
        }
    }
}
document.addEventListener('DOMContentLoaded', async function () {
    const pathname = window.location.pathname;
    const verificarJWTValido = await API.verificarJWTValido();
    if (verificarJWTValido) {
        if (pathname == '/login')
            window.location.href = '/';
    } else {
        if (pathname != '/login')
            window.location.href = '/login';
    }
    var overlay = document.querySelector('.overlay');
    overlay.style.display = 'none';

    const botao = document.getElementById('sair');
    if (botao) {
        botao.addEventListener('click', function () {
            API.sair();
        });
    }
});