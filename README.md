# 💈 BarberSystem

O **BarberSystem** é um sistema de gerenciamento para barbearias desenvolvido em **.NET 8**, focado no controle de agendamentos e organização da rotina dos barbeiros. Este projeto está sendo desenvolvido para fins de estudo, aplicando conceitos avançados de arquitetura de software e boas práticas de desenvolvimento.

---

## 🚀 Tecnologias Utilizadas

* **Linguagem:** C#
* **Framework:** ASP.NET Core Web API (.NET 8.0)
* **ORM:** Entity Framework Core
* **Banco de Dados:** SQL Server
* **Documentação:** Swagger (OpenAPI)

### Padrões e Princípios
* **Clean Architecture / DDD (Domain-Driven Design)**
* **Domain Entities e Value Objects** (Ex: CPF, Email, Password)
* **Result Pattern** para tratamento de fluxos e erros
* **Unit of Work** e **Repository Pattern**

---

## 🏗️ Estrutura do Projeto

O projeto segue uma arquitetura em camadas para garantir o desacoplamento e a testabilidade:


* **`BarberSystem.API`**: Camada de entrada, contendo os Controllers e configurações da aplicação.
* **`BarberSystem.Application`**: Contém os serviços de aplicação, DTOs e interfaces de comunicação.
* **`BarberSystem.Domain`**: O coração do sistema, contendo Entidades, Value Objects, Enums e as interfaces dos repositórios.
* **`BarberSystem.Infrastructure`**: Implementação dos repositórios, contexto do banco de dados (EF Core) e serviços externos (como criptografia).

---

## 📋 Funcionalidades Atuais

Atualmente, o sistema conta com o CRUD e a lógica principal para:

### 🪒 Gerenciamento de Usuários (Barbeiros)
* Cadastro de colaboradores com definição de jornada de trabalho.

### 👥 Gerenciamento de Clientes
* Cadastro completo de clientes integrados ao sistema de identidade.

### 🛠️ Serviços Prestados
* Cadastro de serviços com definição de categoria, valor e tempo estimado.

### 📅 Agendamento
* Criação de agendamentos vinculando barbeiro, cliente e múltiplos serviços.
* **Validação de conflitos:** Impede que um barbeiro tenha dois agendamentos ao mesmo tempo.

---
> 💡 *Este projeto está em constante evolução.*
