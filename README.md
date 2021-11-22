## Introdução

Este projeto reflete um dos desafios realizados como parte de um processo seletivo para uma empresa na cidade do Rio de Janeiro-RJ.

## Desafio: Cadastro de usuário

[![GitHub issues][ImagemProblema]][Problemas] [![GitHub license][ImagemLicenca]][Licenca]


## Cenário

Você trabalha em uma rede de distribuição de farmácia importância de sua cidade. O gestor do do negócio decidiu que chegou a hora de implementar uma nova funcionalidade que permita aos usuários se cadastrarem para obter enviem as notícias da região onde moram para contribuir com o site.

Sua função é programar um microserviço, onde o usuário irá preencher alguns campos seguindo etapas de validação de dados. Após o cadastramento correto, o usuário deverá receber um e-mail informando que o seu cadastro foi realizado com sucesso.


## Especificações

Imagine telas de aplicativos que vão consumir endpoints. O primeiro deve validar e verificar o telefone do usuário, que receberá um SMS (mockado) quando validado.

Ao receber o SMS o usuário deve informar o código recebido para então seguir com o cadastro informando:

- Nome;
- CPF;
- E-mail;
- CEP.

É importante validar todos os passos e dados informados.

## Observação

1. O problema foi resolvido utilizando uma Web API, observando as boas práticas de desenvolvimento orientadas a objetos;
2. O código “server-side” foi desenvolvido .NET 5.0 e seguindo convenções REST (o mínimo preferencialmente);
3. Para persistência dos dados foram utilizados Banco de Dados em Memória.


## Como testar?

1. Fazer um clone deste repositório em algum lugar de sua preferência:

```
git clone https://github.com/alexandredorea/Desafio.FarmApp.git
```

2. Rodas todas as APIs:
   - Ou usando o Visual Studio;
   - Ou linha de comando:

```PowerShell

dotnet build
dotnet run --project .\src\Desafio.WebApi\Desafio.WebApi.csproj

```

## Documentação dos Endpoint
![image](https://user-images.githubusercontent.com/11574354/142784479-5611ef3a-4e1c-4c67-b139-be12d1d72813.png)


## Ainda tem perguntas ou sugestão de melhoria?

Sinta-se à vontade em abrir um [issue][DefeitoFarmApp] ou [Pull Request][PullRequest].



[//]: # (Links de referências para os badges deste repositório)

[ImagemProblema]: <https://img.shields.io/github/issues/alexandredorea/Desafio.FarmApp.svg?style=flat-square>
[Problemas]: <https://github.com/alexandredorea/Desafio.FarmApp/issues>
[ImagemLicenca]: <https://img.shields.io/github/license/alexandredorea/Desafio.FarmApp.svg?style=flat-square>
[Licenca]: <https://github.com/alexandredorea/Desafio.FarmApp/blob/master/LICENSE>


[//]: # (Links de referências aos problemas neste projeto)

[DefeitoFarmApp]: <https://github.com/alexandredorea/Desafio.FarmApp/issues>
[PullRequest]: <https://github.com/alexandredorea/Desafio.FarmApp/pulls>
