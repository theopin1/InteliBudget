# InteliBudget

Sistema de educação e organização financeira desenvolvido para auxiliar usuários no controle de gastos, planejamento financeiro e aprendizado sobre finanças pessoais de forma acessível e interativa.

## 📌 Sobre o Projeto

O **InteliBudget** é uma aplicação focada em educação financeira que combina funcionalidades de gerenciamento financeiro com um chatbot inteligente para auxiliar usuários na tomada de decisões relacionadas a finanças pessoais.

O projeto tem como objetivo ajudar pessoas que possuem dificuldade em organizar suas finanças, oferecendo ferramentas para controle financeiro e apoio educativo através de interações automatizadas.

---

## 🚀 Funcionalidades

* Cadastro e autenticação de usuários
* Controle de receitas e despesas
* Organização de gastos por categorias
* Dashboard financeiro
* Relatórios e análises financeiras
* Chatbot para aconselhamento financeiro
* API REST para integração com frontend
* Persistência de dados em banco relacional

---

## 🛠️ Tecnologias Utilizadas

### Back-end

* C#
* .NET
* ASP.NET Core
* Entity Framework Core

### Front-end

* React
* JavaScript
* HTML5
* CSS3

### Banco de Dados

* MySQL / PostgreSQL

### Ferramentas

* Git e GitHub
* Swagger
* Docker
* Docker Compose

---

## 📂 Estrutura do Projeto

```bash
InteliBudget/
│
├── backend/
│   ├── Application/
│   ├── Domain/
│   ├── Infra/
│   ├── API/
│
├── frontend/
│   ├── src/
│   ├── public/
│   ├── components/
│   ├── pages/
│
├── tests/
│
├── docs/
│
└── README.md
```

---

## ⚙️ Como Executar o Projeto

### Executando o Back-end

### Pré-requisitos

* Docker
* Docker Compose
* Git

### Clone o repositório

```bash
git clone https://github.com/seuusuario/InteliBudget.git
```

### Acesse a pasta do projeto

```bash
cd InteliBudget
```

### Configure as variáveis de ambiente

Edite os arquivos de configuração e variáveis de ambiente conforme necessário.

### Execute os containers

```bash
docker-compose up --build
```

A aplicação e os serviços necessários serão inicializados automaticamente através do Docker.

---

## 💻 Executando o Front-end

Caso o frontend não esteja containerizado, ele também pode ser executado manualmente.

### Acesse a pasta do frontend

```bash
cd frontend
```

### Instale as dependências

```bash
npm install
```

### Execute o projeto React

```bash
npm start
```

---

## 👨‍💻 Autor

Desenvolvido por Theo Pinheiro.
