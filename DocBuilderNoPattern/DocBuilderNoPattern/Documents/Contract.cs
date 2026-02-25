using System;
using System.IO;

namespace DocBuilderNoPattern.Documents
{
    public class Contract
    {
        public string DocumentType { get; set; }
        public int DocNumber { get; set; }
        public string Title { get; set; }

        public string CompanyName { get; set; }
        public string INN { get; set; }
        public string LegalAddress { get; set; }

        public string Party1 { get; set; }
        public string Party2 { get; set; }
        public int StartDate { get; set; }
        public int EndDate { get; set; }
        public string Clauses { get; set; }

        public Contract()
        {
            DocNumber = new Random().Next(1000, 9999);
            DocumentType = "Договор оказания услуг";
            Title = "Договор оказания услуг";
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
            return $"Договор №{DocNumber}\n" +
                   $"Тип: {DocumentType}\n" +
                   $"Организация: {CompanyName}\n" +
                   $"ИНН: {INN}\n" +
                   $"Адрес: {LegalAddress}\n" +
                   $"Сторона 1: {Party1}\n" +
                   $"Сторона 2: {Party2}\n" +
                   $"Дата начала: {StartDate}\n" +
                   $"Дата окончания: {EndDate}\n" +
                   $"Условия: {Clauses}";
        }
    }
}