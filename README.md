# API - Ticket Sales

Uma API para venda de ingressos de eventos.

---
## Descrição:
Essa API foi desenvolvida para cadastrar, gerenciar e efetuar vendas de ingressos de eventos. Através dessa API será possível realizar o fluxo completo de cadastrar, visualizar, alterar e excluir eventos e ingressos; além de cadastrar um pedido de venda dos ingressos. Ao cadastrar um pedido, será feito o cálculo do valor total do pedido e será descontado a quantidade de ingressos comprados do total de ingressos disponíveis.  
A aplicação foi desenvolvida em C#, utilizando Entity Framework Core e com banco de dados SQL Server.

---
## Testes de Endpoints
Foram implementados uma série de métodos que permitem o gerenciamento dos ingressos e eventos, que serão apresentados e descritos a seguir. Para efetuar os testes basta acessar o endereço da API e realizar os cadastros, consultas, atualização e exclusões de acordo com os exemplos e informações passadas sobre cada endpoint. Os testes podem ser feitos através da própria interface do Swagger disponível no endereço da API ou por aplicativos auxiliares, como o Postman.   
Esses são os endpoints criados para a aplicação:

### Eventos (Events):
[GET]/api/event - Retorna a lista completa com todos os Eventos cadastrados pelo usuário.  
[GET]/api/event/{id} - Retorna o Evento único com o Id equivalente ao passado pelo parâmetro {id}.
[POST]/api/event - Cria um novo Evento e os ingressos vinculados à ele, a partir do json passado pelo corpo da requisição. Exemplo json:
```
{
  "id": 0,
  "name": "Rock in Rio",
  "description": "Evento Rock in Rio",
  "adress": "Rua do Rock",
  "date": "2024-10-10T10:16:38.504Z",
  "maxCapacity": 100000,
  "tickets": [
    {
      "id": 0,
      "eventId": 0,
      "price": 150,
      "type": "Pista",
      "initialQuantity": 1200
    },
    {
      "id": 0,
      "eventId": 0,
      "price": 1500,
      "type": "VIP",
      "initialQuantity": 120
    }
  ]
}
```
[PUT]/api/event - Atualiza um Evento já cadastrado pelo usuário a partir do id e informações fornecidas pelo corpo da requisição: Exemplo json:
```
{
  "id": 1,
  "name": "Rock In Rio - 2024",
  "description": "Rock in Rio o maior evento de Rock do Brasil",
  "adress": "Rua do Rock",
  "date": "2024-10-10T10:16:38.504Z",
  "maxCapacity": 100000
}
```
[DELETE]/api/ticket - Deleta um Evento já cadastrado pelo usuário a partir do Id fornecido pelo parâmetro {id}.

### Ingressos (Tickets):
[GET]/api/ticket - Retorna a lista completa com todos os Ingressos cadastrados pelo usuário.  
[GET]/api/ticket/{id} - Retorna o Ingresso único com o Id equivalente ao passado pelo parâmetro {id}.
[GET]/api/ticket/list - Retorna os Ingressos com os Ids equivalentes aos passados pelo corpo da requisição. Exemplo json:
```
[
    1,
    2,
    5
]
```  
[POST]/api/ticket - Cria um novo Ingresso vinculado à um evento, a partir do json passado pelo corpo da requisição. Exemplo json:
```
{
  "id": 0,
  "eventId": 1,
  "price": 550,
  "type": "VIP",
  "initialQuantity": 1100
}
```
[PUT]/api/ticket - Atualiza um Ingresso já cadastrado pelo usuário a partir do id e informações fornecidas pelo corpo da requisição: Exemplo json:
```
{
  "id": 1,
  "price": 600,
  "type": "VIP"
}
```
[DELETE]/api/ticket - Deleta um Ingresso já cadastrado pelo usuário a partir do Id fornecido pelo parâmetro {id}.

### Pedidos (Orders):
[GET]/api/order - Retorna a lista completa com todos os Pedidos cadastrados pelo usuário.  
[GET]/api/order/{id} - Retorna o Pedido único com o Id equivalente ao passado pelo parâmetro {id}.
[POST]/api/order - Cria um novo Pedido com os ingressos e quantidades selecionadas pelo usuário fornecidas pelo corpo da requisição. Exemplo json:
```
{
  "id": 0,
  "orderItems": [
    {
      "ticketId": 1,
      "quantity": 2
    },
    {
      "ticketId": 2,
      "quantity": 5
    }
  ]
}
```