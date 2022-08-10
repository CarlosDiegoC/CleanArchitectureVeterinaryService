# :dog: API CHALLENGE - BASIC VETERINARY CLINIC

## Sobre 
Este repositório se propôe a trazer uma API Rest utilizando os conceitos da Clean Architecture como solução para o desafio de projeto API do programa GFT Start 4.

Obs: Apesar de não haver necessidade de ter utilizado essa abordagem, aproveitei a oportunidade para me aventurar em tais conceitos seguindo a regra de dependência.

## Instruções básicas
* ### :hammer: Restaurando pacotes e compilando os projetos
    Após clonar o repositório, é preciso baixar os pacotes e frameworks utilizados, para isso, dentro do diretório raíz onde se encontra o arquivo de solução **PetDream.sln** basta executar o comando de build.

    <u>Restaurando pacotes e compilando projetos:</u> `dotnet build`

* ### :floppy_disk: Atualizando o banco de dados
    O banco de dados será criado com o nome **CarlosDiegoApiDb**, caso queira modificar esse nome, será preciso navegar até a classe **ContextFactory.cs** dentro do projeto **PetDream.Infra.Data** para ajustar a string de conexão, em seguida, deverá fazer a mesma modificação no arquivo **appsettings.json** dentro do projeto **PetDream.Api**.

    Nota: O projeto de testes **PetDream.Api.Tests** usa o banco de dados original do projeto, por isso, caso a string de conexão seja modificada, será preciso fazer a alteração na classe **BaseTest.cs** dentro desse projeto para continuar realizando estes testes.

    Para criar o banco de dados é preciso navegar até o projeto **PetDream.Infra.Data** e utilizar o comando básico do entity framework para aplicar as migrations.
    
    <u>Navegando até o projeto de infra-estrutura:</u> `cd .\src\PetDream.Infra.Data\`

    Uma vez dentro do projeto **PetDream.Infra.Data**, basta aplicar as migrations.
    
    <u>Aplicando migrations:</u> `dotnet ef database update`

* ### :arrow_forward: Rodando a aplicação
  Para rodar a aplicação basta navegar até o projeto **PetDream.Api** e executá-lo ou alternativamente, executar diretamente o comando dotnet run com a tag -p e o nome do projeto.

  <u>Rodando o projeto api:</u> `dotnet watch run -p .\src\PetDream.Api\`

  Alternativamente...
  
  <u>Navegando até a pasta do projeto api:</u> `cd .\src\PetDream.Api\`

  Uma vez dentro do projeto **PetDream.Api**, basta rodar o projeto.

  <u>Rodando o projeto api:</u> `dotnet watch run`

* ### :heavy_check_mark: Rodando os testes
  Para realizar os testes unitários basta executar o comando de teste na pasta raiz onde se encontra o arquivo de solução **PetDream.sln**.

  <u>Rodando testes unitários:</u> `dotnet test`

* ### :memo: Informações úteis
  * Apenas veterinários podem acessar livremente todos os endpoints da aplicação.
  * O login do usuário para acessar a aplicação é criado automaticamente quando o POST (veterinário ou tutor) é feito.
   
  **Abaixo estão alguns logins de veterinários já populados:**
  
  login: marciabezerra@gmail.com
  
  senha: Marcia@123456

  login: mayaracosta@gmail.com

  senha: Mayara@123456

  **Abaixo estão alguns logins de clientes já populados:**

  login: carlosdiego@gft.com

  senha: Gft@123456

  login: fernandapessoa@gft.com

  senha: Gft@123456

## Estrutura da solução
* ### PetDream.Domain
  Este é o projeto (classlib) que compôe a camada central da solução, a mais interna, totalmente independente das demais camadas ou de alguma tecnologia, contendo apenas a abstração do modelo de domínio (entidades, regras de negócio do domínio e suas validações).

  **Notas**: 
  * Nesta camada, as classes que representam as entidades são seladas e suas propriedades apresentam setters com acessibilidade privada.
  * Classes de validação de email e CPF foram implementadas para impedir a inconsistência dessas propriedades, onde o CPF apresenta apenas números, sem pontos ou hífens. Além disso um tratamento de exceções personalizado é utilizado para validar as entidades.
* ### PetDream.Application
  Este é o projeto (classlib) que traz as regras de negócio da aplicação e dita como as regras de negócio do domínio serão trabalhadas (mapeamentos, servicos e DTOs).
* ### PetDream.Api
  Camada conhecida como *adapter* (webapi), recebe da camada mais externa as requisições http e as envia as respostas, sendo essa sua única responsabilidade, não implementando regras de negócio ou acesso a dados.
  
  **Nota**: Devido a falta de tempo, implementei os métodos POST de PetOwner e Veterinarian de forma inadequada, violando a regra de responsabilidade. Além disso, os modelos de entidade para consumo da API externa também foram colocados nesse projeto. A API externa (TheDogAPI) não tem nenhuma participação do domínio do negócio, oferecendo apenas serviços GET.
* ### PetDream.Infra.Data
  É a camada mais externa da aplicação (classlib), realiza o acesso aos dados implementando as interfaces de repositório. Esta é a única camada que deve conhecer a lógica e a tecnologia de acesso a dados, além disso, implementa o esquema de autenticação Identity.

  Pacotes utilizados:
  
  * Microsoft.AspNetCore.Identity
  * Microsoft.AspNetCore.Identity.EntityFrameworkCore
  * Pomelo.EntityFrameworkCore.MySql
  * Microsoft.EntityFrameworkCore.Design
  * Microsoft.EntityFrameworkCore.Tools

* ### PetDream.Infra.IoC
  Atua como cross cutting, sendo responsável unicamente pelo registro dos serviços e injeção de dependências.

## Testes unitários
Utilizando o XUnit, foram implantados multiplos projetos de testes unitários para algumas camadas da solução, eventualmente foram utilizados pacotes extras como o FluentAssertions para otimizar o código.

Nota: Não foram realizados todos os testes necessários, mas apenas alguns a título de ilustração.

  






