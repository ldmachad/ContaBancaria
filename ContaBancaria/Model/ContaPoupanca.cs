namespace ContaBancaria.Model
{
    public class ContaPoupanca : Conta
    {
        private int aniversario;


        public ContaPoupanca(int numero, int agencia, int tipo, string titular, decimal saldo, int aniversario) 
        : base(numero, agencia, tipo, titular, saldo)
        {
            this.aniversario = aniversario;
        }

        public int GetAniversario()
        {
            return this.aniversario;
        }

        public void SetAniversario(int aniversario)
        {
            this.aniversario = aniversario;
        }

        // Polimorfismo de Sobrescrita da classe Conta
        public override void Visualizar()
        {
            base.Visualizar();
            Console.WriteLine($"Aniversário da conta: Dia {this.aniversario}");
        }
    }
}