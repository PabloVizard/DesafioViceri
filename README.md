# DesafioViceri

Para realização do desafio, foi utilizado um projeto de Web API AspNetCore com a versão do .Net6 e para o banco de dados, foi utilizado o banco local do SQLServer. O projeto foi estruturado com as principais pastas sendo Controllers, Models, Repositories e Services. Onde a Models contém a model de usuário onde estão os atributos solicitados no desafio, a pasta Repositories contém o nosso arquivo repository, que é responsável por administrar os objetos do banco de dados, a pasta Controllers contém o controlador dos usuários, onde estão as funções de obter, listar, atualizar e deletar um usuário e por ultimo a pasta Services que contém serviços auxiliares que são necessários para execução correta do código, sendo dessa vez a função de criptografar a senha em forma de Hash. Além disso, é disponibilizado ao executar o código, uma página do swagger contento todas as APIs e instruções para execução.

# Como Executar

Para construir o bancco de dados, primeiro crie um banco local com o SQL Server e adicione a string de conexão no arquivo "Program.cs". Após isso, abra o visual studio e vá em Ferramentas > Gerenciador de Pacotes do NuGet > Console do Gerenciador de Pacotes.
E então execute o seguinte comando:

Update-database -Context Repository

Após isso, apenas executar e chamar as respectivas APIs.
