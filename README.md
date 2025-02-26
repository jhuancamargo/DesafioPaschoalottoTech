# Relatório de Usuários - Paschoalotto

Este projeto foi desenvolvido como parte de um desafio técnico para a Paschoalotto, com o objetivo de criar uma aplicação backend em C# (.NET) integrada a um banco de dados PostgreSQL e um frontend em HTML, CSS (*extra*) e JavaScript para exibir e editar dados de usuários gerados pela API [Random User Generator](https://randomuser.me/).

A aplicação permite:
- Gerar usuários aleatórios e armazená-los no banco de dados.
- Exibir os usuários em uma tabela no frontend.
- Editar os dados dos usuários diretamente na interface.

## 🛠️ Tecnologias Utilizadas

- **Backend**:
  - C# (.NET 6)
  - Entity Framework Core
  - PostgreSQL
  - Swagger (Documentação da API)
- **Frontend**:
  - HTML5
  - CSS3
  - JavaScript (Vanilla)
- **Ferramentas**:
  - Visual Studio Code
  - Git e GitHub
  - Postman (Testes de API)

## 🛠️ Funcionalidades

1. **Geração de Usuários**:
   - Consumo da API Random User Generator para criar usuários aleatórios.
   - Armazenamento dos dados no banco de dados PostgreSQL.

2. **Listagem de Usuários**:
   - Exibição dos usuários em uma tabela no frontend.
   - Design responsivo e moderno.

3. **Edição de Usuários**:
   - Modal interativa para editar os dados dos usuários.
   - Atualização dos dados no banco de dados em tempo real.

## 🧠 Desafios e Soluções

### Desafio 1: Integração com o Banco de Dados
- **Problema**: Configurar o Entity Framework Core para se conectar ao PostgreSQL e criar a tabela de usuários.
- **Solução**: Utilizei o pacote `Npgsql.EntityFrameworkCore.PostgreSQL` e configurei a string de conexão no `appsettings.json`. Criei migrações para gerar a tabela `Usuarios`.

### Desafio 2: Comunicação entre Frontend e Backend
- **Problema**: O frontend não conseguia se comunicar com o backend devido a problemas de CORS.
- **Solução**: Configurei a política `PermitirTudo` no backend para permitir requisições de qualquer origem.

### Desafio 3: Formulário de Edição
- **Problema**: O formulário de edição inicialmente abria abaixo da tabela, o que não era ideal.
- **Solução**: Transformei o formulário em uma modal centralizada, com um overlay escuro, melhorando a experiência do usuário.

## 🖥️ Como Executar o Projeto

### Pré-requisitos
- .NET 6 SDK
- Node.js (opcional, para servir o frontend)
- PostgreSQL

### Passos
1. **Clone o repositório**:
   git clone https://github.com/seu-usuario/paschoalotto-desafio.git
   cd paschoalotto-desafio
   
3. **Configure o Banco de Dados**:
   Crie um banco de dados PostgreSQL chamado paschoalotto_db.
   Utilizando pgAdmin4 | Servers > PostgresSQL > Databases> Schemas > Tables > Botão direito - Create para criar uma tabela. No mesmo local que deverá criar sua tabela, você pode adicionar Columns com suas respectivas necessidades.
   Atualize a string de conexão no arquivo appsettings.json com as credenciais do seu banco de dados.
   **Vídeo Tutorial** https://www.youtube.com/watch?v=L_2l8XTCPAE&ab_channel=HashtagPrograma%C3%A7%C3%A3o
   
5. **Execute o Backend**:   
     Navegue até a pasta do backend:
*cd backend/PaschoalottoBackend*
Restaure as dependências do projeto:
*dotnet restore*
Execute o projeto:
*dotnet run*
**O backend estará disponível em** http://localhost:5000.
**Execute o Frontend**:
Navegue até a pasta do frontend:
cd ../../frontend
*Abra o arquivo index.html no navegador ou use um servidor local (como o Live Server do VS Code).*
**O frontend estará disponível em http://localhost:5500.** -- atenção ao final do port. Backend:**5000** | Frontend:**5500**

## 📸 Screenshots
https://github.com/jhuancamargo/DesafioPaschoalottoTech/tree/main/screenshots
