namespace ContaBancaria.Model
{
    // Herança da classe Conta
    public class ContaCorrente : Conta
    {
        private decimal limite;

        public ContaCorrente(int numero, int agencia, int tipo, string titular, decimal saldo, decimal limite)
        : base(numero, agencia, tipo, titular, saldo)
        {
            this.limite = limite;
        }
        
        public decimal GetLimite()
        {
            return this.limite;
        }

        public void SetLimite(decimal limite)
        {
            this.limite = limite;
        }

        // Polimorfismo de Sobrescrita da classe Conta
        public override bool Sacar(decimal valor)
        {
            if (this.GetSaldo() + this.limite < valor)
            {
                Console.WriteLine("Saldo Insuficiente!");
                return false;
            }

            this.SetSaldo(this.GetSaldo() - valor);
            return true;
        }

        // Polimorfismo de Sobrescrita da classe Conta
        public override void Visualizar()
        {
            base.Visualizar();
            Console.WriteLine($"Limite da conta: R${this.limite}");
        }
    }
}