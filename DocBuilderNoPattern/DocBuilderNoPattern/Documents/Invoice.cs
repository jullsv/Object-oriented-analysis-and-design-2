using System;
using System.IO;

namespace DocBuilderNoPattern.Documents
{
    public class Invoice
    {
        public string DocumentType { get; set; }
        public int DocNumber { get; set; }
        public string Title { get; set; }

        public string CompanyName { get; set; }
        public string INN { get; set; }
        public string BankAccount { get; set; }

        public int Amount { get; set; }
        public string ClientName { get; set; }
        public int DueDate { get; set; }

        public Invoice()
        {
            DocNumber = new Random().Next(1000, 9999);
            DocumentType = "Счёт на оплату";
            Title = "Счёт на оплату";
        }

        public void Save(string path)
        {
            File.WriteAllText(path, GetText());
        }

        public void Render()
        {
            Console.WriteLine(GetText());
        }

        public string GetText()
        {
            return $"Счёт №{DocNumber}\n" +
                   $"Тип: {DocumentType}\n" +
                   $"Организация: {CompanyName}\n" +
                   $"ИНН: {INN}\n" +
                   $"Р/с: {BankAccount}\n" +
                   $"Клиент: {ClientName}\n" +
                   $"Сумма: {Amount}\n" +
                   $"Дата оплаты: {DueDate}";
        }
    }
}