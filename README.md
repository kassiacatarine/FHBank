# FHBank

Api desenvolvida com o intuito de possibilitar a movimentação de uma conta bancária.

## Modo de uso

Execute o seguinte comando para gerar as imagens docker necessárias para o funcionamento

    docker-compose build

Em seguida execute o comando para iniciar os containers

    docker-compose up

## Sobre o projeto

- Tecnologia: Dotnet Core 3.1

- Banco de dados: MongoDb

- Design de arquitetura: CQRS, DDD e outros

- Framework de teste: XUnit

- Endereço de acesso:

        https://localhost:32778/playground/

## Ações disponiveis

- Criar conta

        mutation CreateAccount {
            createAccount(valor: 600) {
                conta
                saldo
            }
        }

- Depositar

        mutation Deposit {
            depositar(conta: 12345, valor: 400) {
                conta
                saldo
            }
        }

- Sacar

        mutation Withdraw {
            sacar(conta: 12345, valor: 400) {
                conta
                saldo
            }
        }

- Saldo

        query Saldo {
            saldo(conta: 12345)
        }
