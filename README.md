# API de Gerenciamento de Boletos Bancários

Este é um projeto de API desenvolvido em .NET 6 que gerencia boletos bancários e bancos. Utiliza Entity Framework como ORM de Banco de Dados, preferencialmente PostgreSQL, inclui documentação com Swagger e autorização com Identity.

## Funcionalidades

- Cadastro de Boletos
- Cadastro de Bancos
- Busca de todos os Bancos
- Busca de um Banco por Código
- Busca de um Boleto por Id
- Cálculo de valor de Boleto com juros após a data de vencimento
- Autorização com Identity e JWT Token

## Requisitos

- .NET 6 SDK
- PostgreSQL (ou outro banco de dados suportado pelo Entity Framework)

## Configuração do Projeto

1. Clone o repositório para sua máquina local.
2. Configure a string de conexão com o banco de dados no arquivo `appsettings.json`.
3. Abra um terminal e navegue até o diretório do projeto.
4. Execute o comando `dotnet ef database update` para criar o banco de dados.

## Endpoints

- `POST /Bancos`: Cadastra um novo banco. Requer as seguintes propriedades no corpo da requisição:
  - Nome do Banco (obrigatório)
  - Código do Banco (obrigatório)
  - Percentual de Juros (obrigatório)

- `GET /Bancos`: Retorna todos os bancos cadastrados.

- `GET /Bancos/{codigo}`: Retorna um banco específico com base no código fornecido.

- `POST /Boletos`: Cadastra um novo boleto. Requer as seguintes propriedades no corpo da requisição:
  - Nome do Pagador (obrigatório)
  - CPF/CNPJ do Pagador (obrigatório)
  - Nome do Beneficiário (obrigatório)
  - CPF/CNPJ do Beneficiário (obrigatório)
  - Valor (obrigatório)
  - Data de Vencimento (obrigatório)
  - Observação
  - BancoId (obrigatório)

- `GET /Boletos/{id}`: Retorna um boleto com base no Id fornecido. Se consultado após a data de vencimento, o valor do boleto será calculado com os juros aplicados.

- `POST /Usuario/Cadastro`: Cadastra um usuário, de acordo com as credenciais.
- `POST /Usuario/Login`: Retorna um token JWT para autenticação.

## Autorização

A API utiliza o Identity para autorização. É necessário cadastrar um usuário e fazer login, para ter acesso aos endpoins da API.

## DTOs

Para a comunicação entre a API e o cliente, são utilizados DTOs (Data Transfer Objects) para representar os dados de forma mais eficiente e segura.

## AutoMapper

O AutoMapper é utilizado para facilitar o mapeamento entre entidades e DTOs, reduzindo a necessidade de código manual para essa tarefa.

## Testes com TDD

Foram implementados testes automatizados utilizando TDD, para garantir a evolução e integridade da aplicação. Também foi utilizado o contexto `UseInMemoryDatabase`, para que os dados sejam armazenadas apenas em memória durante a execução testes.

## Documentação com Swagger

- **Acesso:** Disponível em `http://localhost:{PORTA}/swagger` após iniciar a aplicação.

- **Navegação:** Lista de endpoints com detalhes sobre parâmetros e exemplos de uso.

## Estrutura do Projeto

- `Controllers`: Contém os controladores da API.
- `Data`: Contém as configurações do banco de dados e os contextos.
- `DTOs`: Contém os DTOs utilizados para comunicação com o cliente.
- `Models`: Contém as entidades do banco de dados.
- `Services`: Contém a lógica de negócio.
- `Tests`: Contém os testes automatizados utilizando TDD.

## Executando os Testes

1. Navegue até o diretório `PagamentosTestes` no terminal.
2. Execute o comando `dotnet test`.


