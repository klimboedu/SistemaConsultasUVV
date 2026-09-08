# Sistema de Consultas UVV

Sistema web desenvolvido em C# com ASP.NET Core MVC para gerenciamento de consultas.

O sistema permite que usuários realizem seu cadastro, façam login e gerenciem suas próprias consultas, podendo cadastrar, visualizar, editar e excluir consultas.

## Tecnologias que utilizei

- C#
- ASP.NET Core
- ASP.NET Core MVC
- Entity Framework Core
- SQL Server
- HTML
- CSS
- Bootstrap

## Arquitetura

O projeto utiliza o padrão **MVC (Model-View-Controller)**.

### Model

Responsável pela representação dos dados e regras de validação.

Principais modelos:

- `Usuario`
- `Consulta`

### View

Responsável pela interface apresentada ao usuário.

As páginas estão localizadas na pasta:

Views/

Principais controllers:

- `AccountController`
- `ConsultaController`
- `Funcionalidades`

O sistema possui as seguintes funções:

Cadastro de usuário;
Login de usuário;
Logout;
Cadastro de consultas;
Visualização das consultas do usuário;
Edição de consultas;
Exclusão de consultas;
Proteção das rotas de consultas através de autenticação;
Validação dos dados através de Data Annotations.

Cada usuário possui acesso somente às próprias consultas, volta a página inicial se tentar ir aonde não tem acesso pela URL

## Banco de dados

O projeto utiliza SQL Server e Entity Framework Core com a abordagem Code First.

As configurações de acesso ao banco de dados estão no arquivo:

appsettings.json

A estrutura do banco é criada e atualizada através das migrations do Entity Framework Core que estão na pasta:

Migrations/

# Configuração do banco de dados

Antes de executar o sistema, configure a string de conexão com o SQL Server no arquivo:

appsettings.json

Por exemplo: 


`"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=SistemasConsultas;Trusted_Connection=True;MultipleActiveResultSets=true"`
}

A string de conexão deve ser ajustada de acordo com a configuração do SQL Server utilizado.

# Aplicação das migrations

Após configurar a conexão com o banco de dados, abra o Package Manager Console no Visual Studio e execute:

Update-Database

## Execução do projeto

1. Clone ou baixe o repositório.
1. Abra a solução preferencialmente no Visual Studio.
1. Verifique a string de conexão no arquivo appsettings.json.
1. Execute as migrations utilizando:
   
                            `Update-Database`
1. Execute o projeto.

## Validações

Os dados inseridos pelo usuário possuem validações utilizando Data Annotations, incluindo:

Campos obrigatórios;
Validação de endereço de e-mail;
Limite de caracteres;
Validação de senha.

As validações são realizadas antes dos dados serem registrados no banco de dados.

## Autenticação e segurança

O sistema utiliza autenticação baseada em cookies, as áreas relacionadas às consultas são protegidas por autenticação, sendo necessário estar logado para acessá-las.

As consultas são associadas ao usuário autenticado, garantindo que cada usuário visualize e altere somente suas próprias consultas, e as senhas dos usuários são armazenadas utilizando hash através 
do PasswordHasher, não sendo armazenadas em texto puro no banco de dados.

## Demonstração

Vídeo de demonstração do sistema:

(link)

O vídeo apresenta as principais funcionalidades do sistema, incluindo:

Cadastro de usuário;
Login;
Cadastro de consulta;
Visualização de consultas;
Edição de consulta;
Exclusão de consulta;
Logout;
Proteção das páginas de consultas.
Repositório

Projeto disponível no GitHub:

https://github.com/klimboedu/SistemaConsultasUVV
