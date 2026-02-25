using System;
using System.IO;

namespace WithPattern.Documents
{
    public abstract class BaseDocument
    {
        public string DocumentType { get; set; }
        public int DocNumber { get; set; }
        public string Title { get; set; }

        public string CompanyName { get; set; }
        public string INN { get; set; }
        public string BankAccount { get; set; }
        public string LegalAddress { get; set; }

        protected BaseDocument()
        {
            DocNumber = GenerateNumber();
        }

        protected BaseDocument(BaseDocument other)
        {
            DocumentType = other.DocumentType;
            DocNumber = GenerateNumber();
            Title = other.Title;
            CompanyName = other.CompanyName;
            INN = other.INN;
            BankAccount = other.BankAccount;
            LegalAddress = other.LegalAddress;
        }

        private int GenerateNumber()
        {
            return new Random().Next(1000, 9999);
        }

        public abstract BaseDocument Clone();
        public abstract void Save(string path);
        public abstract void Render();
        public abstract string GetText();
    }
}