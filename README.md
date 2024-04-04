# Desafio para Vaga de Desenvolvedor de Software - Avaliação de Candidato (.NET)

Este projeto consiste na implementação de uma aplicação em .NET Core com uma WebAPI REST, que contempla as operações básicas de CRUD (Create, Read, Update, Delete) para duas entidades fornecidas como exemplo. A aplicação também inclui uma interface web para visualização e interação com os dados.

## Tecnologias Utilizadas
- .NET Core 6+
- ASP.NET Core WebAPI
- Entity Framework Core (para persistência de dados)
- HTML, CSS e JavaScript, Jquery, Bootstrap (para a interface web)

## Funcionalidades
- **Operações de CRUD**: A aplicação permite criar, visualizar, atualizar e excluir registros das entidades fornecidas.
- **Persistência de Dados**: Implementa uma forma de persistência dos dados, permitindo que sejam armazenados em memória durante a execução da aplicação ou em um banco de dados.
- **Interface Web**: Inclui uma interface web para visualização e interação com os dados, proporcionando uma experiência amigável ao usuário.

## Como Executar o Projeto
1. Certifique-se de ter o .NET Core 6+ instalado em sua máquina.
2. Clone este repositório em seu ambiente local.
3. Navegue até o diretório do projeto.
4. Abra o terminal e execute os seguintes comandos:
   ```
   dotnet restore
   dotnet build
   dotnet run
   ```
5. Acesse a interface web através do navegador, utilizando o endereço fornecido pelo terminal.

## Estrutura do Projeto
O projeto está estruturado da seguinte forma:
- `src/`: Contém o código-fonte da aplicação.
- `API/`: Contém a implementação da WebAPI REST.
- `WebApp/`: Contém a interface web para visualização e interação com os dados.

## Contribuição
Contribuições são bem-vindas! Sinta-se à vontade para abrir uma issue ou enviar um pull request com melhorias, correções de bugs ou novas funcionalidades.

## Autores
Este projeto foi desenvolvido por [Seu Nome] como parte do desafio para a vaga de desenvolvedor de software.

## Licença
Este projeto está licenciado sob a [Licença MIT](LICENSE).
