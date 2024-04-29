# Thunders.Assignments
Aplicação Asp.net Core desenvolvida como proposta para o desafio técnico da Thunders. Trata-se de um CRUD simples de tarefas.

Tecnologias Utilizadas:

 - Asp.net  Core 8 Rest Api
 - C#
 - Entity Framework
 - Sqlite

A aplicação é separada em camadas, seguindo a proposta da arquitetura limpa. São elas (da mais interna para a mais externa):

 - **Thunders.Assignments.Domain** - Camada mais interna contendo as entidades do domínio e a definição de elementos pertencentes ao domínio;
 - **Thunders.Assignments.Application** - Camada onde é definida os casos de uso ou regras de negócio. A lógica da aplicação se encontra nessa camada;
 - **Thunders.Assignments.Infrastructure** - Camada onde é definido elementos específicos de infraestrutura como a tecnologia de acesso à dados. Aqui são implementadas algumas definições estabelecidas no domínio como o repositório;
 - **Thunders.Assignments.Api** - Camada mais externa contento a apresentação. Aqui também são registrados no container de injeção de dependências as implementações das interfaces definidas no domínio e também os elementos de infraestrutura.

Para rodar a aplicação basta fazer o download do código fonte e abrir na IDE de sua escolha (Visual Studio ou Rider) e executar. Não é necessário rodar nenhum comando ou fazer nenhum ajuste em cadeia de conexão (connection string) pois a aplicação utiliza um banco Sqlite criado automaticamente no ato de inicialização da aplicação.
