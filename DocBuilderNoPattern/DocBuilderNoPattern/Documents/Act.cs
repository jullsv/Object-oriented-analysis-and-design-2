using System;
using System.IO;

namespace DocBuilderNoPattern.Documents
{
    public class Act
    {
        public string DocumentType { get; set; }
        public int DocNumber { get; set; }
        public string Title { get; set; }

        public string CompanyName { get; set; }
        public string INN { get; set; }
        public string LegalAddress { get; set; }

        public string Executor { get; set; }
        public string Customer { get; set; }
        public int ActDate { get; set; }

        public Act()
        {
            DocNumber = new Random().Next(1000, 9999);
            DocumentType = "Акт выполненных работ";
            Title = "Акт выполненных работ";
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
            return $"Акт №{DocNumber}\n" +
                   $"Тип: {DocumentType}\n" +
                   $"Организация: {CompanyName}\n" +
                   $"ИНН: {INN}\n" +
                   $"Адрес: {LegalAddress}\n" +
                   $"Исполнитель: {Executor}\n" +
                   $"Заказчик: {Customer}\n" +
                   $"Дата акта: {ActDate}";
        }
    }
}