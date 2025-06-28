# iTasks

Aplicação de gestão de tarefas em estilo Kanban, desenvolvida para a unidade curricular de **Desenvolvimento de Aplicações**.

## Funcionalidades

- Login de utilizadores
- Criação de tarefas com campos como descrição, estado, datas e prioridade
- Visualização em painel Kanban com três colunas: **To Do**, **Doing**, **Done**
- Mudança de estado das tarefas entre colunas
- Registo de utilizadores feito apenas por utilizadores com permissão ("GereUtilizadores")
- Armazenamento de dados via **Entity Framework** (LINQ to Entities)
- Base de dados local com SQL Server

## Tecnologias utilizadas

- C# (WinForms)
- Entity Framework 
- SQL Server LocalDB
- .NET Framework 4.x

## Estrutura das entidades principais

- "Utilizador": entidade base com "Nome", "Username", "Password"
- "Programador": com "Utilizador", tem "NivelExperiencia" e referência a "Gestor"
- "Gestor": com "Utilizador", tem "Departamento" e flag "GereUtilizadores"
- "Tarefa": tem estado atual (enum "todo", "doing", "done"), datas, relações com "Programador", "Gestor" e "TipoTarefa"

## Dar run ao projeto

1. Abre a solução ".sln" no **Visual Studio**
2. Certifica-te que a base de dados está corretamente ligada no "App.config" ou "Basededados.cs"
3. Garante que tens migrations aplicadas (ou cria a base de dados se necessário)
4. Corre o projeto ("F5")
5. Faz login com um utilizador já existente na BD

> O registo de novos utilizadores é feito apenas por um utilizador com "GereUtilizadores = true"

## Exemplo de utilizador válido (para testes)

Username: admin
Password: 1234
