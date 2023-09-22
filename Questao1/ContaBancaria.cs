using System.Globalization;

namespace Questao1
{
    class ContaBancaria {
        public int Numero { get; set; }
        public string Titular { get; set; }
        public double Saldo { get; set; } 

        public ContaBancaria(int numero, string titular, double saldo=0)
        {
            Numero = numero;
            Titular = titular;
            Saldo = saldo;
        }        

        public void Deposito(double valor)
        {
            Saldo += valor;
        }

        public void Saque(double valor)
        {
            Saldo -= (valor + 3.5);
        }

        public override string ToString()
        {
            return $"Conta: {Numero}, Titular: {Titular}, Saldo: $ {Saldo.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture)}";
        }
    }
}
