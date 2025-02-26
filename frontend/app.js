document.addEventListener('DOMContentLoaded', function () {
    carregarTodosUsuarios();
    document.querySelector('.close').addEventListener('click', fecharModal);

// clicar fora da modal fechará ela
    window.addEventListener('click', function(event) {
        const modal = document.getElementById('modalEdicao');
        if (event.target === modal) {
            fecharModal();
        }
    }
);
}
);






async function carregarTodosUsuarios() {
    try {
        const response = await fetch('http://localhost:5000/api/controladorusuario/todos');
        const usuarios = await response.json();
        const tabela = document.getElementById('tabelaUsuarios').getElementsByTagName('tbody')[0];
        tabela.innerHTML = ''; // Limpa a tabela antes de preencher

        usuarios.forEach(usuario => {
            const row = tabela.insertRow();
            row.insertCell(0).innerText = usuario.nome;
            row.insertCell(1).innerText = usuario.email;
            row.insertCell(2).innerText = usuario.telefone;                              
            row.insertCell(3).innerText = usuario.endereco;
            row.insertCell(4).innerText = new Date(usuario.dataNascimento).toLocaleDateString();

            // aqui é criado o botão de editar
            const cellAcoes = row.insertCell(5);
            const btnEditar = document.createElement('button');
            btnEditar.innerText = 'Editar';
            btnEditar.onclick = () => editarUsuario(usuario.id);
            cellAcoes.appendChild(btnEditar);
        });
    } catch (error) {
        console.error('Erro ao carregar usuários:', error);
    }
}

//função para abrir a modal de edição
function abrirModal() {
    document.getElementById('modalEdicao').style.display = 'block';
}

// Função para fechar a modal de edição
function fecharModal() {
    document.getElementById('modalEdicao').style.display = 'none';
}

// função para editar usuário
async function editarUsuario(id) {
    try {
        const response = await fetch(`http://localhost:5000/api/controladorusuario/${id}`);
        if (!response.ok) {
            throw new Error("Erro ao buscar usuário");
        }
        const usuario = await response.json();





        document.getElementById('editId').value = usuario.id;
        document.getElementById('editNome').value = usuario.nome;
        document.getElementById('editEmail').value = usuario.email;
        document.getElementById('editTelefone').value = usuario.telefone;
        document.getElementById('editEndereco').value = usuario.endereco;
        document.getElementById('editDataNascimento').value = usuario.dataNascimento.split('T')[0];






        // Abre a modal de edição
        abrirModal();
    } catch (error) {
        console.error("Erro ao editar usuário:", error);
    }
}

// Função para salvar a edição
async function salvarEdicao() {
    const id = document.getElementById('editId').value;
    const dadosEditados = {
        nome: document.getElementById('editNome').value,
        email: document.getElementById('editEmail').value,
        telefone: document.getElementById('editTelefone').value,
        endereco: document.getElementById('editEndereco').value,
        dataNascimento: document.getElementById('editDataNascimento').value
    };




    await fetch(`http://localhost:5000/api/controladorusuario/${id}`, {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(dadosEditados)
    });






    // vai carrearg a lista de usuários
    carregarTodosUsuarios();
    fecharModal();
}