# SAP HANA Client com ODBC em .NET Core - PROJETO DE EXEMPLO TROQUE PELA SUAS CREDENCIAIS 

Este projeto demonstra como configurar e usar o **SAP HANA Client** instalado no diretório **C:\Program Files\SAP\hdbclient** em uma aplicação .NET Core. O objetivo é conectar a um banco de dados SAP HANA usando o driver ODBC, permitindo que você execute comandos SQL e consulte dados.

## Pré-requisitos

1. **SAP HANA Client** instalado no seu sistema.
   - Certifique-se de que o **SAP HANA Client** esteja instalado corretamente no diretório `C:\Program Files\SAP\hdbclient`.
   - O SAP HANA Client inclui os drivers necessários, como o ODBC (HDBODBC) e ADO.NET, para se conectar ao banco de dados.

2. **Driver ODBC do SAP HANA**:
   - O driver ODBC do SAP HANA é instalado junto com o SAP HANA Client e é utilizado para estabelecer a conexão entre a aplicação .NET Core e o banco de dados SAP HANA.

3. **.NET Core SDK** instalado na sua máquina:
   - Se você ainda não tem o .NET Core SDK, instale-o a partir do [site oficial](https://dotnet.microsoft.com/download).

## Passos para Configuração

### 1. Verifique a instalação do SAP HANA Client

Certifique-se de que o **SAP HANA Client** esteja instalado no seguinte diretório:

C:\Program Files\SAP\hdbclient

O cliente SAP HANA inclui o driver **HDBODBC** que será utilizado para a conexão via ODBC.

### 2. Instale o Driver ODBC do SAP HANA

- Abra o **Painel de Controle** > **Ferramentas Administrativas** > **Fontes de Dados ODBC (32 bits ou 64 bits)**.
- No painel **Fontes de Dados ODBC**, clique em **Adicionar** e selecione o driver **HDBODBC** (se o SAP HANA Client estiver instalado corretamente, o driver aparecerá na lista).
- Crie uma nova **Fonte de Dados ODBC** configurando a conexão para o seu servidor SAP HANA:
  - **Server**: Endereço do servidor SAP HANA.
  - **Porta**: Porta do servidor.
  - **Usuário** e **Senha**: As credenciais de acesso ao SAP HANA.

### 3. Crie uma aplicação .NET Core

Crie uma nova aplicação console no .NET Core para usar a conexão ODBC:

```bash
dotnet new console -n HanaConnectionApp
cd HanaConnectionApp
dotnet add package System.Data.Odbc


using System;
using System.Data.Odbc;

class Program
{
    static void Main(string[] args)
    {
        // O nome da fonte de dados ODBC configurada no painel de controle
        var dsn = "DSN=HANA_ODBC_Connection;"; // Nome da fonte de dados ODBC
        var user = "YourUsername"; // Nome de usuário SAP HANA
        var password = "YourPassword"; // Senha SAP HANA
        
        // Conectando via ODBC
        var connectionString = $"{dsn}UID={user};PWD={password};";
        
        try
        {
            using (var connection = new OdbcConnection(connectionString))
            {
                connection.Open();
                Console.WriteLine("Conexão bem-sucedida!");
                
                // Aqui você pode executar comandos SQL, por exemplo:
                var command = new OdbcCommand("SELECT * FROM MY_TABLE", connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine(reader.GetString(0)); // Exemplo de leitura de coluna
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erro ao conectar: " + ex.Message);
        }
    }
}

Explicação do Código:
DSN (Data Source Name): Nome da fonte de dados ODBC configurada no painel de controle.
UID (User ID): Nome de usuário do SAP HANA.
PWD (Password): Senha correspondente ao usuário do SAP HANA.
OdbcConnection: Utiliza a string de conexão ODBC para se conectar ao banco de dados.
OdbcCommand: Comando SQL executado no banco de dados.
5. Executando o código
Agora, execute o projeto com o seguinte comando:

dotnet run


**Notas Importantes
Verifique o Driver ODBC: O driver ODBC correto precisa estar instalado no computador. O HDBODBC é o driver que acompanha o SAP HANA Client.
Caminho do SAP HANA Client: O driver ODBC (HDBODBC) geralmente está localizado em C:\Program Files\SAP\hdbclient\lib.
Permissões de Conexão: Certifique-se de que o usuário configurado tenha permissões adequadas para acessar o banco de dados SAP HANA.
Alternativa: Usando o SAP HANA ADO.NET Driver
Se preferir, você pode usar o SAP HANA ADO.NET Driver para conectar diretamente ao banco de dados sem passar pelo ODBC. No entanto, isso requer o pacote adequado para o .NET Core, que pode ser mais difícil de configurar dependendo da versão do SAP HANA Client.

Caso precise de mais ajuda para configurar ou depurar, fique à vontade para entrar em contato!



 
