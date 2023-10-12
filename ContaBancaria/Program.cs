using ContaBancaria.Controller;
using ContaBancaria.Model;

namespace ContaBancaria;

class Program
{
    private static ConsoleKeyInfo keyInfo;
    static void Main(string[] args)
    {
        int opcao, agencia, tipo, aniversario, numero, numeroDestino;
        string? titular;
        decimal saldo, limite, valor;

        ContaController contas = new();

        while (true)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("*****************************************************");
            Console.WriteLine("                                                     ");
            Console.WriteLine("                     BANCO LDM                       ");
            Console.WriteLine("                                                     ");
            Console.WriteLine("*****************************************************");
            Console.WriteLine("                                                     ");
            Console.WriteLine("            1 - Criar Conta                          ");
            Console.WriteLine("            2 - Listar todas as Contas               ");
            Console.WriteLine("            3 - Buscar Conta por Numero              ");
            Console.WriteLine("            4 - Atualizar Dados da Conta             ");
            Console.WriteLine("            5 - Apagar Conta                         ");
            Console.WriteLine("            6 - Sacar                                ");
            Console.WriteLine("            7 - Depositar                            ");
            Console.WriteLine("            8 - Transferir valores entre Contas      ");
            Console.WriteLine("            9 - Consulta por titular                 ");
            Console.WriteLine("            10 - Sair                                ");
            Console.WriteLine("                                                     ");
            Console.WriteLine("*****************************************************");
            Console.WriteLine("Entre com a opção desejada:                          ");
            Console.WriteLine("                                                     ");
            Console.ResetColor();

            // Tratamento de Exceção para impedir a digitação de strings
            try
            {
                opcao = Convert.ToInt32(Console.ReadLine());
            }

            catch (FormatException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Digite um valor inteiro entre 1 e 9");
                opcao = 0;
                Console.ResetColor();
            }

            switch (opcao)
            {
                case 1:
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine("\nCriar Conta\n\n");
                    Console.ResetColor();

                    Console.Write("Digite o número da agência: ");
                    agencia = Convert.ToInt32(Console.ReadLine());

                    Console.Write("Digite o nome do titular: ");
                    titular = Console.ReadLine();
                    titular ??= string.Empty;

                    do
                    {
                        Console.Write("""
                        1 - Conta Corrente
                        2 - Conta Poupança
                        Digite o tipo da conta: 
                        """);
                        tipo = Convert.ToInt32(Console.ReadLine());

                    } while (tipo != 1 && tipo != 2);

                    Console.Write("Digite o saldo da conta: ");
                    saldo = Convert.ToDecimal(Console.ReadLine());

                    switch (tipo)
                    {
                        case 1:
                            Console.Write("Digite o limite da conta: ");
                            limite = Convert.ToDecimal(Console.ReadLine());

                            contas.Cadastrar(new ContaCorrente(contas.GerarNumero(), agencia, tipo, titular, saldo, limite));
                            break;

                        case 2:
                            Console.Write("Digite o dia do aniversário da conta: ");
                            aniversario = Convert.ToInt32(Console.ReadLine());

                            contas.Cadastrar(new ContaPoupanca(contas.GerarNumero(), agencia, tipo, titular, saldo, aniversario));
                            break;
                    }
                    KeyPress();
                    break;
                case 2:
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine("\nListar todas as Contas\n\n");
                    Console.ResetColor();

                    contas.ListarTodas();

                    KeyPress();
                    break;
                case 3:
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine("\nConsultar dados da Conta - por número\n\n");
                    Console.ResetColor();

                    Console.WriteLine("Digite o número da Conta: ");
                    numero = Convert.ToInt32(Console.ReadLine());

                    contas.ProcurarPorNumero(numero);

                    KeyPress();
                    break;
                case 4:
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine("\nAtualizar dados da Conta\n\n");
                    Console.ResetColor();

                    Console.Write("Digite o número da conta: ");
                    numero = Convert.ToInt32(Console.ReadLine());

                    var conta = contas.BuscarNaCollection(numero);

                    if (conta is not null)
                    {

                        Console.Write("Digite o número da agência: ");
                        agencia = Convert.ToInt32(Console.ReadLine());

                        Console.Write("Digite o nome do titular: ");
                        titular = Console.ReadLine();
                        titular ??= string.Empty;

                        Console.Write("Digite o saldo da conta: ");
                        saldo = Convert.ToDecimal(Console.ReadLine());

                        tipo = conta.GetTipo();

                        switch (tipo)
                        {
                            case 1:
                                Console.Write("Digite o limite da conta: ");
                                limite = Convert.ToDecimal(Console.ReadLine());

                                contas.Atualizar(new ContaCorrente(numero, agencia, tipo, titular, saldo, limite));
                                break;

                            case 2:
                                Console.Write("Digite o dia do aniversário da conta: ");
                                aniversario = Convert.ToInt32(Console.ReadLine());

                                contas.Atualizar(new ContaPoupanca(numero, agencia, tipo, titular, saldo, aniversario));
                                break;
                        }
                    }

                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"A conta número {numero} não foi encontrada!");
                        Console.ResetColor();
                    }

                    KeyPress();
                    break;
                case 5:
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("\nApagar a Conta\n\n");
                    Console.ResetColor();

                    Console.WriteLine("Digite o número da conta: ");
                    numero = Convert.ToInt32(Console.ReadLine());

                    contas.Deletar(numero);

                    KeyPress();
                    break;
                case 6:
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine("\nSaque\n\n");
                    Console.ResetColor();

                    Console.WriteLine("Digite o número da conta: ");
                    numero = Convert.ToInt32(Console.ReadLine());

                    Console.Write("Digite o valor do saque: ");
                    valor = Convert.ToDecimal(Console.ReadLine());

                    contas.Sacar(numero, valor);

                    KeyPress();
                    break;
                case 7:
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine("\nDepósito\n\n");
                    Console.ResetColor();

                    Console.WriteLine("Digite o número da conta: ");
                    numero = Convert.ToInt32(Console.ReadLine());

                    Console.Write("Digite o valor do depósito: ");
                    valor = Convert.ToDecimal(Console.ReadLine());

                    contas.Depositar(numero, valor);

                    KeyPress();
                    break;
                case 8:
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine("\nTransferência entre Contas\n\n");
                    Console.ResetColor();

                    Console.WriteLine("Digite o número da conta de origem: ");
                    numero = Convert.ToInt32(Console.ReadLine());

                    Console.WriteLine("Digite o número da conta de destino: ");
                    numeroDestino = Convert.ToInt32(Console.ReadLine());

                    Console.Write("Digite o valor da transferência: ");
                    valor = Convert.ToDecimal(Console.ReadLine());

                    contas.Transferir(numero, numeroDestino, valor);

                    KeyPress();
                    break;

                case 9:
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine("\nConsulta por titular\n\n");
                    Console.ResetColor();

                    Console.Write("Digite o nome do titular: ");
                        titular = Console.ReadLine();
                        titular ??= string.Empty;

                    contas.ListarTodasPorTitular(titular);

                    KeyPress();
                    break;

                case 10:
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("\nBanco LDM - Aqui você tem valor!");
                    About();
                    Console.ResetColor();
                    System.Environment.Exit(0);
                    break;

                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nOpção Inválida!\n");
                    Console.ResetColor();

                    KeyPress();
                    break;
            }
        }

        static void About()
        {
            Console.WriteLine("\n*********************************************************");
            Console.WriteLine("Projeto Desenvolvido por: Leonardo Machado ");
            Console.WriteLine("E-mail - ldmachado.dev@gmail.com");
            Console.WriteLine("github.com/ldmachad/ContaBancaria");
            Console.WriteLine("*********************************************************");
        }

        static void KeyPress()
        {
            do
            {
                Console.Write("\nPressione Enter para continuar...");
                keyInfo = Console.ReadKey();
            } while (keyInfo.Key != ConsoleKey.Enter);
        }
    }
}
