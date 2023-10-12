using ContaBancaria.Model;

namespace ContaBancaria.Repository
{
    public interface IContaRepository
    {
        // Métodos CRUD (Creat Read Update Delete)
        public void Cadastrar(Conta conta);
        public void ListarTodas();
        public void ProcurarPorNumero(int numero);
        public void Atualizar(Conta conta);
        public void Deletar(int numero);
        public void ListarTodasPorTitular(string titular);

        // Métodos Bancários
        public void Sacar(int numero, decimal valor);
        public void Depositar(int numero, decimal valor);
        public void Transferir(int numeroOrigem, int numeroDestino, decimal valor);
    }
}