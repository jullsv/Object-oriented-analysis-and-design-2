using System;
using System.IO;

namespace DocBuilderNoPattern.Documents
{
    public class Act
    {
        public string DocumentType { get; set; } = "Акт выполненных работ";
        public int DocNumber { get; set; }
        public string Title { get; set; } = "Акт выполненных работ";

        public string CompanyName { get; set; }
        public string INN { get; set; }
        public string LegalAddress { get; set; }

        public string Executor { get; set; }
        public string Customer { get; set; }
        public DateTime ActDate { get; set; }
        public double Amount { get; set; }

        public Act(string companyName, string inn, string legalAddress)
        {
            DocNumber = GenerateNumber();
            CompanyName = companyName;
            INN = inn;
            LegalAddress = legalAddress;
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
            Console.WriteLine($"Исполнитель: {Executor}");
            Console.WriteLine($"Заказчик: {Customer}");
            Console.WriteLine($"Дата: {ActDate:dd.MM.yyyy}");
            Console.WriteLine($"Сумма: {Amount:C}");
        }

        public string GetDocumentText()
        {
            return $"Акт №{DocNumber}\n" +
                   $"Организация: {CompanyName}\n" +
                   $"ИНН: {INN}\n" +
                   $"Юр. адрес: {LegalAddress}\n" +
                   $"Исполнитель: {Executor}\n" +
                   $"Заказчик: {Customer}\n" +
                   $"Дата акта: {ActDate:dd.MM.yyyy}\n" +
                   $"Сумма: {Amount:C}";
        }
    }
}