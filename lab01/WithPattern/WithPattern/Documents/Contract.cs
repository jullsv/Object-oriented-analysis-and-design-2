using System;
using System.IO;

namespace WithPattern.Documents
{
    public class Contract : BaseDocument
    {
        public string Party1 { get; set; }
        public string Party2 { get; set; }
        public int StartDate { get; set; }
        public int EndDate { get; set; }
        public string Clauses { get; set; }

        public Contract()
        {
            DocumentType = "Договор оказания услуг";
            Title = "Договор оказания услуг";
        }

        private Contract(Contract other) : base(other)
        {
            Party1 = other.Party1;
            Party2 = other.Party2;
            StartDate = other.StartDate;
            EndDate = other.EndDate;
            Clauses = other.Clauses;
        }

        public override BaseDocument Clone()
        {
            return new Contract(this);
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