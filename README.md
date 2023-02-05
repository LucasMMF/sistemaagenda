# Sistema Agenda
> Aplicação para controle de uma agenda de contatos, desenvolvido ao longo do curso C# WebDeveloper - Formação Fullstack da Coti Informática.
## Projeto desenvolvido em C# com AspNet MVC e utilizando
1. C#
2. AspNet MVC
3. Razor
4. NuGet
5. Bootstrap CSS
6. JQuery + Plugins (DataTable, MaskedInput etc)
7. SQLServer
8. iText (Relatórios PDF)
## Link de live demo do projeto:
link: (Link fora do ar, originalmente publicado no host MyAspNet, porém conta free expirou).
## Quickstart:
* Baixe o projeto na sua máquina.  
![alt text](/ImagensReadme/AgendaPull.png "Projeto baixado utilizando git pull")  
* Abra o projeto no seu Visual Studio (Recomendado utilizar a versão mais atual do Visual Studio).  
![alt text](/ImagensReadme/ProjetoNoVisualStudio.png "Projeto aberto no Visual Studio")  
> Siga os passos descritos nas imagens abaixo para criar o banco de dados SQLServer localmente.
* Clique em Exibir e selecione a opção "SQL Server Object Explorer" para abrir o explorador de objetos do SQL Server.  
![alt text](/ImagensReadme/ExibirSqlServerObject.png "Exiba o explorador de objetos do SQL Server")  
![alt text](/ImagensReadme/SqlServerObject.png "Exibição do SQL Server Object")  
* Abra o "SQL Server", depois, abra MSSQLLocalDB. Clique com a direita em Databases e selecione "Adicionar novo banco de dados"  
![alt text](/ImagensReadme/AdicionandoDB.png "Adicionando Banco de Dados")  
* Informe o nome do banco de dados, para este exemplo será usado o nome BDAgendaQS  
![alt text](/ImagensReadme/CreateDatabase.png "Criando banco de dados")  
* Com o banco de dados criado, clique no banco e depois que ele exibir as suas pastas internas (Tables, Views, etc), clique com a direita sobre o banco e selecione "Nova Query"  
![alt text](/ImagensReadme/NewQuery.png "Nova query")  
* Agora volte a pasta do projeto, abra o arquivo script.sql, que está localizado na raiz do projeto, em qualquer editor de texto. Copie o conteúdo do arquivo para a janela de nova query aberta no Visual Studio.  
![alt text](/ImagensReadme/script.png "Script")  
![alt text](/ImagensReadme/ScriptConteudo.png "Conteúdo do script")  
![alt text](/ImagensReadme/ExecutandoScript.png "Executando script")  
> Após a execução do script, o banco de dados estará criado.
> Por fim, precisamos alterar a ConnectionString no arquivo SqlServerConfiguration.cs, para que seja possível acessar o banco criado. Para isso...
* Retorne ao SQL Server Object Explorer, vá até o banco de dados criado e clique no banco novamente para exibir as pastas internas. Clique com a direita no banco e selecione "Propriedades"  
![alt text](/ImagensReadme/Propriedades.png "Propriedades do banco")  
* Isso fará com que uma janela de exibição, contendo as propriedades do banco, seja aberta. Busque pela propriedade "Connection string" e clique duas vezes no seu conteudo para copiar a connection string do banco.  
![alt text](/ImagensReadme/ConnectionString.png "ConnectionString na propriedade")  
![alt text](/ImagensReadme/CopiandoACS.png "Copiando a ConnectionString")  
* Guarde a ConnectionString em um arquivo de texto temporário. Depois, vá para a camada "2 - Camada de Acesso a dados". Entre no projeto Agenda.Data, abra a pasta "Configurations" e abra SqlServerConfiguration.cs  
![alt text](/ImagensReadme/SqlServerConfiguration.png "SqlServerConfiguration.cs")  
* Agora coloque a ConnectionString obtida dentro das aspas aonde está escrito => @""; O resultado final deve ficar da seguinte forma:  
![alt text](/ImagensReadme/ConnectionStringAlterada.png "ConnectionString alterada")  
> Com isso, o sistema já deve estar operacional. Execute o sistema no Visual Studio, crie um usuário e realize o seu login para utilizar o sistema.  
![alt text](/ImagensReadme/login.png "Tela de login")  
## COTI Informática
> Turma de C# WebDeveloper FullStack - 2022