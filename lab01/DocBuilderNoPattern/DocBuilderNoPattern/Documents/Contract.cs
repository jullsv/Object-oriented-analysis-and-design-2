using System;
using System.IO;

namespace DocBuilderNoPattern.Documents
{
    public class Contract
    {
        public string DocumentType { get; set; } = "Договор оказания услуг";
        public int DocNumber { get; set; }
        public string Title { get; set; } = "Договор оказания услуг";

        public string CompanyName { get; set; }
        public string INN { get; set; }
        public string LegalAddress { get; set; }

        public string Party1 { get; set; }
        public string Party2 { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Clauses { get; set; }

        public Contract(string companyName, string inn, string legalAddress)
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
            Console.WriteLine($"Сторона 1: {Party1}");
            Console.WriteLine($"Сторона 2: {Party2}");
            Console.WriteLine($"Период: {StartDate:dd.MM.yyyy} - {EndDate:dd.MM.yyyy}");
        }

        public string GetDocumentText()
        {
            return $"Договор №{DocNumber}\n" +
                   $"Организация: {CompanyName}\n" +
                   $"ИНН: {INN}\n" +
                   $"Юр. адрес: {LegalAddress}\n" +
                   $"Сторона 1: {Party1}\n" +
                   $"Сторона 2: {Party2}\n" +
                   $"Период: с {StartDate:dd.MM.yyyy} по {EndDate:dd.MM.yyyy}\n" +
                   $"Условия: {Clauses}";
        }
    }
}