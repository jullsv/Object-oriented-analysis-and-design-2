using System;
using System.IO;

namespace WithPattern.Documents
{
    public class Act : BaseDocument
    {
        public string Executor { get; set; }
        public string Customer { get; set; }
        public int ActDate { get; set; }

        public Act()
        {
            DocumentType = "Акт выполненных работ";
            Title = "Акт выполненных работ";
        }

        private Act(Act other) : base(other)
        {
            Executor = other.Executor;
            Customer = other.Customer;
            ActDate = other.ActDate;
        }

        public override BaseDocument Clone()
        {
            return new Act(this);
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