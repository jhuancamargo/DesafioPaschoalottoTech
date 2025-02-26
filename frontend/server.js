const http = require('http');
const fs = require('fs');
const path = require('path');

const PORT = 5500; 
const FILE_PATH = path.join(__dirname, 'index.html');

const server = http.createServer((req, res) => {


    fs.readFile(FILE_PATH, (err, data) => {
        if (err) {
            res.writeHead(500, { 'Content-Type': 'text/plain' });
            res.end('Erro ao carregar o arquivo index.html');
            return;
        }

        res.writeHead(200, { 'Content-Type': 'text/html' });
        res.end(data);
    }
);

}
);

server.listen(PORT, () => {
    console.log(`Servidor rodando em http://localhost:${PORT}`);
});

// acabou não sendo necessário, foi uma opção de implementaçãom -- acabei por utilizar o Live Server do VSCode.