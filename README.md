# 💈 BarberSystem

O **BarberSystem** é um sistema de gerenciamento para barbearias desenvolvido em **.NET 8**, focado no controle de agendamentos e organização da rotina dos barbeiros. Este projeto está sendo desenvolvido para fins de estudo, aplicando conceitos avançados de arquitetura de software e boas práticas de desenvolvimento.

---

## 🚀 Tecnologias Utilizadas

* **Linguagem:** C#
* **Framework:** ASP.NET Core Web API (.NET 8.0)
* **ORM:** Entity Framework Core
* **Banco de Dados:** SQL Server
* **Documentação:** Swagger (OpenAPI)
* **Segurança:** Autenticação e Autorização via JWT (JSON Web Tokens) e Cookies seguros
* **Background Jobs:** Quartz.NET para automação de tarefas
* **Testes:** xUnit e Moq para testes de unidade

### Padrões e Princípios
* **Clean Architecture / DDD (Domain-Driven Design)**
* **Domain Entities e Value Objects** (Ex: CPF, Email, Password)
* **CQRS (Command Query Responsibility Segregation):** Separação de responsabilidades de leitura e escrita através de Interfaces de Queries.
* **Result Pattern:** Padronização do retorno de operações para controle de fluxo e erros.
* **Unit of Work** e **Repository Pattern:** Abstração da persistência de dados.

---

## 🏗️ Estrutura do Projeto

O projeto segue uma arquitetura em camadas para garantir o desacoplamento e a testabilidade:


* **`BarberSystem`**: Camada de entrada, contendo os Controllers e configurações da aplicação.
* **`BarberSystem.Application`**: Contém os serviços de aplicação, DTOs e interfaces de comunicação.
* **`BarberSystem.Domain`**: O coração do sistema, contendo Entidades, Value Objects, Enums e as interfaces dos repositórios.
* **`BarberSystem.Infrastructure`**: Implementação dos repositórios, contexto do banco de dados (EF Core) e serviços externos (como criptografia).
* **`BarberSystem.Tests`**: Contém os testes de unidade das entidades de domínio para garantir a integridade das regras de negócio.

---

## 📋 Funcionalidades Atuais

O **BarberSystem** oferece um ecossistema completo para a gestão de barbearias, priorizando a segurança, a integridade dos dados e a automação de processos:

### 🔐 Segurança e Gestão de Identidade
* **Autenticação Robusta:** Login seguro utilizando **JWT (JSON Web Tokens)**.
* **Gestão de Sessão:** Implementação de tokens via **Cookies (HttpOnly e Secure)** para proteção adicional.
* **Criptografia de Dados:** Proteção de senhas com algoritmos de **Salt e Hash (SHA256)**.
* **Controle de Acesso (RBAC):** Autorização baseada em funções (*Roles*), diferenciando permissões para *Administrator*, *User* e *Client*.

### 🪒 Gerenciamento de Usuários (Barbeiros)
* **Gestão de Colaboradores:** Cadastro completo com vínculo ao sistema de identidade.
* **Controle de Jornada:** Definição rigorosa de horários de início e término de trabalho.
* **Validação de Expediente:** O sistema impede agendamentos fora do horário de expediente do barbeiro (ex: antes das 08h ou após as 18h).

### 👥 Gerenciamento de Clientes
* **Cadastro Integrado:** Perfil de cliente com validação de documentos únicos (**CPF**) e contato.
* **Consistência de Dados:** Verificação de e-mails e CPFs já existentes para evitar duplicidade.

### 🛠️ Serviços Prestados
* **Catálogo de Serviços:** Gestão de serviços por categorias (Cabelo, Barba, Sobrancelha) com valores e tempos estimados.
* **Regras de Negócio:** Validação de tempo mínimo (10 minutos) e nomes válidos para serviços.

### 📅 Agendamento Inteligente
* **Fluxo Multi-Serviço:** Possibilidade de selecionar múltiplos serviços em um único atendimento.
* **Cálculo Automático:** O sistema soma o valor total e o tempo de duração acumulado para definir o término exato do atendimento.
* **Validação de Conflitos:** Lógica avançada que impede que um barbeiro receba dois agendamentos sobrepostos.
* **Gestão de Status:** Controle de ciclos do agendamento (Pendente, Concluído, Cancelado).

### ⚙️ Automação e Qualidade
* **Processamento em Background:** Uso de **Quartz.NET** para monitorar e marcar agendamentos como concluídos automaticamente após o horário.
* **Padrões de Projeto:** Aplicação de **CQRS** (leitura e escrita separadas), **Unit of Work** e **Result Pattern** para um código resiliente.
* **Garantia de Qualidade:** Suite de testes de unidade com **xUnit** e **Moq** cobrindo as principais regras de domínio.

---

## 🎨 Design e Protótipo (Figma)

O Design da Interface está em constante evolução

[![Figma]](https://www.figma.com/site/hTtpLTsvRtb5ITQ4912Z33/BarberSystem_FrontEnd_Project?node-id=0-1&t=ZIAdBgoe6NxVadhN-1)

---
> 💡 *Este projeto está em constante evolução.*
