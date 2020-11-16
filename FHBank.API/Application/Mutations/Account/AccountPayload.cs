namespace FHBank.API.Application.Mutations.Account
{
    public class AccountPayload
    {
        public int Conta { get; set; }
        public decimal Saldo { get; set; }

        public AccountPayload(int conta, decimal saldo)
        {
            Conta = conta;
            Saldo = saldo;
        }
    }
}
