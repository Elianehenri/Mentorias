# Agenda de Mentorias

Este é um projeto de agenda de mentorias desenvolvido em C# .NET. 
Ele permite que você agende mentorias, visualize, atualize e exclua agendamentos. 
Além disso, inclui regras para evitar conflitos de horarios e define automaticamente 
a duraçao da mentoria como 1 hora se o horario de termino nao for especificado.

## Funcionalidades Principais
O projeto inclui as seguintes funcionalidades:

- POST /agenda - Crie um novo agendamento com Data, Hora de Início, Hora de Término e Assunto.
- GET /agenda - Recupere todos os agendamentos disponíveis.
- GET /agenda?data=yyyy-MM-dd - Recupere agendamentos com filtro por data.
- PUT /agenda/{id} - Atualize um agendamento existente (requer autenticação).
- DELETE /agenda/{id} - Exclua um agendamento existente (requer autenticação).

## Requisitos

- Visual Studio (ou IDE de sua escolha) com suporte ao .NET 7.0
- Banco de dados SQL Server
- Pacotes NuGet:
  - Microsoft.EntityFrameworkCore (7.0.12)
  - Microsoft.EntityFrameworkCore.Design (7.0.12)
  - Microsoft.EntityFrameworkCore.SqlServer (7.0.12)
  - Microsoft.AspNetCore.Authentication.JwtBearer (7.0.12)
  - Microsoft.EntityFrameworkCore.Tools (7.0.12)

 ## Configuração
1. Clone este repositório para o seu ambiente local.
   ``` git clone https://github.com/Elianehenri/Mentorias.git ```
2. Certifique-se de ter o .NET SDK instalado (versão compatível com as dependências acima).
3. No terminal, navegue até o diretório raiz do projeto.
4. Execute `dotnet restore` para restaurar as dependências.
5. Configure sua conexão com o banco de dados no arquivo `appsettings.json`.
6. Execute as migrações do Entity Framework Core com `dotnet ef database update`.
7. Execute o projeto com `dotnet run`.





  #
### Autor
* **Eliane Henriqueta**

