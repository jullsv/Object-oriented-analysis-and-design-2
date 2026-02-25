using System;
using System.IO;

namespace DocBuilderNoPattern.Documents
{
    public class Invoice
    {
        public string DocumentType { get; set; } = "Счёт на оплату";
        public int DocNumber { get; set; }
        public string Title { get; set; } = "Счёт на оплату";

        public string CompanyName { get; set; }
        public string INN { get; set; }
        public string BankAccount { get; set; }

        public double Amount { get; set; }
        public string ClientName { get; set; }
        public DateTime DueDate { get; set; }

        public Invoice(string companyName, string inn, string bankAccount)
        {
            DocNumber = GenerateNumber();
            CompanyName = companyName;
            INN = inn;
            BankAccount = bankAccount;
        }

        private int GenerateNumber()
        {
            return new Random().Next(1000, 9999);
        }

        public void Save(string path)
        {
            File.WriteAllText(path, GetDocumentText());
        }

        public void Render()
        {
            Console.WriteLine($"=== {Title} №{DocNumber} ===");
            Console.WriteLine($"Компания: {CompanyName}");
            Console.WriteLine($"Клиент: {ClientName}");
            Console.WriteLine($"Сумма: {Amount:C}");
        }

        public string GetDocumentText()
        {
            return $"Счёт №{DocNumber}\n" +
                   $"Организация: {CompanyName}\n" +
                   $"ИНН: {INN}\n" +
                   $"Расчётный счёт: {BankAccount}\n" +
                   $"Клиент: {ClientName}\n" +
                   $"Сумма: {Amount:C}\n" +
                   $"Дата оплаты: {DueDate:dd.MM.yyyy}";
        }
    }
}