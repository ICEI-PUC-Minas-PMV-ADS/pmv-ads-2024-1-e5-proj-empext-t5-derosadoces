# Planos de Testes de Software

Apresente os cenários de testes utilizados na realização dos testes da sua aplicação. Escolha cenários de testes que demonstrem os requisitos sendo satisfeitos.

Enumere quais cenários de testes foram selecionados para teste. Neste tópico o grupo deve detalhar quais funcionalidades avaliadas, o grupo de usuários que foi escolhido para participar do teste e as ferramentas utilizadas.

Os requisitos para realização dos testes de software são:
- Aplicativo rodando no ambiente local. 


Os testes funcionais a serem realizados na aplicação são descritos a seguir.


Plano de Testes: Funcionalidade de Login
<br>

|Caso de teste   | CT-01 - Teste de Login com Credenciais Válidas:
|------|-----------------------------------------|
|Requisitos associados | RF-01​​  Usuário fazer login
|Objetivo do teste | O sistema deve permitir o acesso do usuário. 
|Passos | <ol><li> Acessar a página de login. </li> <li> Inserir um nome de usuário e senha válidos. </li> <li> Clicar no botão de login. </li></ol>
|Critérios de Êxito | <ul> <li> Aparecer mensagem de êxito ao realizar login </li> 

<br>

|Caso de teste   | CT-02 - Teste de Login com Credenciais Inválidas:
|------|-----------------------------------------|
|Requisitos associados | RF-01​​  Usuário fazer login
|Objetivo do teste | O sistema não permitir o acesso com credenciais inválidas. 
|Passos | <ol><li> Acessar a página de login. </li> <li> Inserir um nome de usuário e senha inválidos. </li> <li> Clicar no botão de login. </li></ol>
|Critérios de Êxito | <ul> <li>O sistema deve exibir uma mensagem de erro informando que as credenciais são inválidas. </li> 

<br>

|Caso de teste   | CT-03 - Teste de recuperação de senha. 
|------|-----------------------------------------|
|Requisitos associados | RF-01​​  Usuário fazer login
|Objetivo do teste | Permitir que o usuário recupere a senha. 
|Passos | <ol><li> Acessar a página de login. </li> <li> Clicar no link "Esqueci minha senha". </li> <li> Inserir o endereço de e-mail associado à conta. </li> <li>Clicar no botão de recuperação de senha. </li></ol>
|Critérios de Êxito | <ul> <li> O sistema deve enviar um e-mail de recuperação de senha para o endereço fornecido e exibir uma mensagem de confirmação. </li> 

<br>
 
# Evidências de Testes de Software

Apresente imagens e/ou vídeos que comprovam que um determinado teste foi executado, e o resultado esperado foi obtido. Normalmente são screenshots de telas, ou vídeos do software em funcionamento.
