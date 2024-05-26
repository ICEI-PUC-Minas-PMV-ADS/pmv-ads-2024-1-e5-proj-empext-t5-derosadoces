# Planos de Testes de Software

Apresente os cenários de testes utilizados na realização dos testes da sua aplicação. Escolha cenários de testes que demonstrem os requisitos sendo satisfeitos.

Enumere quais cenários de testes foram selecionados para teste. Neste tópico o grupo deve detalhar quais funcionalidades avaliadas, o grupo de usuários que foi escolhido para participar do teste e as ferramentas utilizadas.

Os requisitos para realização dos testes de software são:
- Aplicação rodando no ambiente local. 


Os testes funcionais a serem realizados na aplicação são descritos a seguir.


Plano de Testes: Funcionalidade de Login

|Caso de teste   | CT-01 - Teste de Cadastro de Novo Usuário:
|------|-----------------------------------------|
|Requisitos associados | RF-01​​  A aplicação deve permitir o usuário se cadastrar.
|Objetivo do teste | O sistema deve permitir cadastro de novos usuários. 
|Passos | <ol><li> Acessar o painel de controle. </li> <li> Clicar no botão "Criar Nova Conta". </li> <li> Preencher os campos obrigatórios. </li> <li>Clicar no botão "Salvar". </li> </ol>
|Critérios de Êxito | <ul> <li>A nova conta deve ser criada com sucesso e aparecer na lista de contas. </li>  

<br>

<br>

|Caso de teste   | CT-02 - Teste de Login com Credenciais Válidas:
|------|-----------------------------------------|
|Requisitos associados | RF-02​​  A aplicação deve permitir o usuário realizar login.
|Objetivo do teste | O sistema deve permitir o acesso do usuário. 
|Passos | <ol><li> Acessar a página de login. </li> <li> Inserir um nome de usuário e senha válidos. </li> <li> Clicar no botão de login. </li></ol>
|Critérios de Êxito | <ul> <li> Aparecer mensagem de êxito ao realizar login </li> 

<br>

|Caso de teste   | CT-03 - Teste de Login com Credenciais Inválidas:
|------|-----------------------------------------|
|Requisitos associados | RF-0​2  A aplicação deve permitir o usuário realizar login.
|Objetivo do teste | O sistema não permitir o acesso com credenciais inválidas. 
|Passos | <ol><li> Acessar a página de login. </li> <li> Inserir um nome de usuário e senha inválidos. </li> <li> Clicar no botão de login. </li></ol>
|Critérios de Êxito | <ul> <li>O sistema deve exibir uma mensagem de erro informando que as credenciais são inválidas. </li> 

<br>

|Caso de teste   | CT-04 - Teste de Alteração de Dados do Usuário:
|------|-----------------------------------------|
|Requisitos associados | RF-03  A aplicação deve permitir ao usuário EDITAR dados pessoais.
|Objetivo do teste | O sistema deve permitir que os usuários alterem os dados cadastrados anteriormente. 
|Passos | <ol><li> Acessar a página de login. </li> <li> Relaizar login com credenciais válidas. </li> <li> Clicar em minha conta. </li> <li> Alterar os dados. </li> <li> Clicar em salvar. </li></ol>
|Critérios de Êxito | <ul> <li>O sistema deve alterar os dados antigos para os que o usuário acabou de inserir. </li> 

<br>

|Caso de teste   | CT-05 - Teste de Navegação pelas páginas.:
|------|-----------------------------------------|
|Requisitos associados | RF-04  A aplicação deve permitir ao usuário navegar pelas páginas.
|Objetivo do teste | O sistema deve permitir que os usuários naveguem pelas páginas do sistema. 
|Passos | <ol><li> Acessar a página de login. </li> <li> Relaizar login com credenciais válidas. </li> <li> Clicar em alguma página. </li> <li> Clicar em outra página. </li></ol>
|Critérios de Êxito | <ul> <li>O sistema deve alterar de página ao clicar. </li> 

<br>

|Caso de teste   | CT-06 - Teste de Seleção de Produtos.:
|------|------------------------|
|Requisitos associados | RF-06 A aplicação deve permitir o usuário selecionar produtos.
|Objetivo do teste | O sistema deve permitir que os usuários selecionem produtos. 
|Passos | <ol><li> Acessar a página de produtos. </li> <li> Clicar no botão detalhes de algum produto. </li></ol>
|Critérios de Êxito | <ul> <li>O sistema deve abrir uma página com o produto selecionado. </li> 

<br>

|Caso de teste   | CT-07 - Teste de Adição de Produtos no Carrinho.:
|------|------------------------|
|Requisitos associados | RF-07 A aplicação deve permitir o usuário adicionar produtos ao carrinho.
|Objetivo do teste | O sistema deve permitir que os usuários adicionem produtos ao carrinho. 
|Passos | <ol><li> Acessar a página de produtos. </li> <li> Clicar no botão detalhes de algum produto. </li> <li> Colocar a quantidade desejada do produto. </li> <li> Clicar no botão de adicionar o produto ao carrinho. </li></ol>
|Critérios de Êxito | <ul> <li>O sistema deve adicionar o produto selecionado ao carrinho. </li> 

<br>

Os testes unitários a serem realizados na aplicação são descritos a seguir.

<br>

|Caso de teste   | CT-0 - Teste de Cadastro de Novos Produtos:
|------|-----------------------------------------|
|Requisitos associados | RF-01​2  A aplicação deve permitir somente ao administrador do site adicionar produtos para venda
|Objetivo do teste | O sistema deve permitir cadastro de novos produtos. 
|Passos | <ol><li> Acessar a página de login. </li> <li> Inserir dados válidos dos produtos. </li> <li> executar o teste unitário. </li></ol>
|Critérios de Êxito | <ul> <li>O sistema deve rodar o teste com êxito. </li> 

<br>
 
# Evidências de Testes de Software

Apresente imagens e/ou vídeos que comprovam que um determinado teste foi executado, e o resultado esperado foi obtido. Normalmente são screenshots de telas, ou vídeos do software em funcionamento.

### CT-01 - Teste de Cadastro de Novo Usuário
![Figura 1](img/registrarUsuario.jpg)

No primeiro momento foi retornado um erro, mostrado a seguir, mas o erro foi corrigido e o usuário foi cadastrado com sucesso
![Figura 1](img/erroregistrarUsuario.jpg)

### CT-02 - Teste de Login com Credenciais Válidas
![Figura 2](img/loginValido.jpg)

![Figura 3](img/loginValido2.jpg)

### CT-03 - Teste de Login com Credenciais Inválidas
![Figura 4](img/loginInvalido.jpg)

![Figura 5](img/loginInvalido2.jpg) 

### CT-04 - Teste de Alteração de Dados do Usuário
![Figura 6](img/alterardados01.png)

![Figura 7](img/alterardados02.png)

### CT-05 - Teste de Alteração de Dados do Usuário
![Video 1](img/navegacao.mp4)

### CT-06 - Teste de Seleção de Produtos
![Figura 8](img/selecionarprodutos01.png)

![Figura 9](img/selecionarprodutos02.png)

### CT-07 - Teste de Adição de Produtos no Carrinho
![Figura 10](img/adicionaraocarrinho01.png)

![Figura 11](img/adicionaraocarrinho02.png)

### CT-0 - Teste Unitário de Adição de Novo Produto
![Figura 1](img/testeUnitario.jpg)
