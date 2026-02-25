using System;
using System.IO;

namespace WithPattern.Documents
{
    public class Invoice : BaseDocument
    {
        public int Amount { get; set; }
        public string ClientName { get; set; }
        public int DueDate { get; set; }

        public Invoice()
        {
            DocumentType = "Счёт на оплату";
            Title = "Счёт на оплату";
        }

        private Invoice(Invoice other) : base(other)
        {
            Amount = other.Amount;
            ClientName = other.ClientName;
            DueDate = other.DueDate;
        }

        public override BaseDocument Clone()
        {
            return new Invoice(this);
        }

        public override void Save(string path)
        {
            File.WriteAllText(path, GetText());
        }

        public override void Render()
        {
            Console.WriteLine(GetText());
        }

        public override string GetText()
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