using ContaBancaria.Model;
using ContaBancaria.Repository;

namespace ContaBancaria.Controller
{
    public class ContaController : IContaRepository
    {
        private readonly List<Conta> listaContas = new();

        int numero = 0;

        // Métodos CRUD
        public void Cadastrar(Conta conta)
        {
            listaContas.Add(conta);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"A conta número {conta.GetNumero()} foi criada com sucesso!");
            Console.ResetColor();
        }

        public void ListarTodas()
        {
            if (listaContas.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Não existe conta cadastrada!");
                Console.ResetColor();
            }
            else
            {
                foreach (var conta in listaContas)
                    conta.Visualizar();
            }
        }

        public void ListarTodasPorTitular(string titular)
        {
            var contasPorTitular = from conta in listaContas
                                   where conta.GetTitular().Contains(titular)
                                   select (conta);

            contasPorTitular.ToList().ForEach(c => c.Visualizar());
        }

        public void ProcurarPorNumero(int numero)
        {
            var conta = BuscarNaCollection(numero);

            if (conta is not null)
                conta.Visualizar();
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"A conta número {numero} não foi encontrada!");
                Console.ResetColor();
            }
        }

        public void Atualizar(Conta conta)
        {
            var buscaConta = BuscarNaCollection(conta.GetNumero());

            if (buscaConta is not null)
            {
                var index = listaContas.IndexOf(buscaConta);

                listaContas[index] = conta;

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"A conta número {conta.GetNumero()} foi atualizada com sucesso!");
                Console.ResetColor();
            }

            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"A conta número {numero} não foi encontrada!");
                Console.ResetColor();
            }
        }

        public void Deletar(int numero)
        {
            var conta = BuscarNaCollection(numero);

            if (conta is not null)
            {

                if (listaContas.Remove(conta) == true)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"A conta número {numero} foi apagada com sucesso!");
                    Console.ResetColor();
                }
            }

            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"A conta número {numero} não foi encontrada!");
                Console.ResetColor();
            }
        }


        // Métodos Bancários
        public void Sacar(int numero, decimal valor)
        {
            var conta = BuscarNaCollection(numero);

            if (conta is not null)
            {
                if (conta.Sacar(valor) == true)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"O saque na conta número {numero} foi efetuado com sucesso!");
                    Console.ResetColor();
                }
            }

            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"A conta número {numero} não foi encontrada!");
                Console.ResetColor();
            }
        }

        public void Depositar(int numero, decimal valor)
        {
            var conta = BuscarNaCollection(numero);

            if (conta is not null)
            {
                conta.Depositar(valor);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"O depósito na conta número {numero} foi efetuado com sucesso!");
                Console.ResetColor();
            }

            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"A conta número {numero} não foi encontrada!");
                Console.ResetColor();
            }
        }

        public void Transferir(int numeroOrigem, int numeroDestino, decimal valor)
        {
            var contaOrigem = BuscarNaCollection(numeroOrigem);
            var contaDestino = BuscarNaCollection(numeroDestino);

            if (contaOrigem is not null && contaDestino is not null)
            {
                if (contaOrigem.Sacar(valor) == true)
                {
                    contaDestino.Depositar(valor);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"A transferência foi efetuada com sucesso!");
                    Console.ResetColor();
                }
            }

            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Conta de origem e/ou conta de destino não foram encontradas!");
                Console.ResetColor();
            }
        }

        // Métodos Auxiliares
        public int GerarNumero()
        {
            return ++numero;
        }

        // Método para Buscar Objeto Conta específico
        public Conta? BuscarNaCollection(int numero)
        {
            foreach (var conta in listaContas)
            {
                if (conta.GetNumero() == numero)
                    return conta;
            }

            return null;
        }
    }
}