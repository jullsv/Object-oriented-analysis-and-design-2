using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using DocBuilderNoPattern.Documents;
using Microsoft.VisualBasic;

namespace DocBuilderNoPattern
{
    public partial class MainForm : Form
    {
        private class CompanyData
        {
            public string Name { get; set; }
            public string INN { get; set; }
            public string BankAccount { get; set; }
            public string LegalAddress { get; set; }
        }

        private Dictionary<string, CompanyData> companies = new Dictionary<string, CompanyData>
        {
            ["Строймастер"] = new CompanyData
            {
                Name = "ООО \"Строймастер\"",
                INN = "7701234567",
                BankAccount = "40702810100000001234",
                LegalAddress = "г. Москва, ул. Ленина, д. 1"
            },
            ["Аэророт"] = new CompanyData
            {
                Name = "ООО \"Аэророт\"",
                INN = "7709876543",
                BankAccount = "40702810200000005678",
                LegalAddress = "г. СПб, ул. Мира, д. 10"
            },
            ["Чипсик"] = new CompanyData
            {
                Name = "ООО \"Чипсик\"",
                INN = "7705554433",
                BankAccount = "40702810300000009999",
                LegalAddress = "г. Казань, пр. Победы, д. 5"
            }
        };

        private List<object> documents = new List<object>();
        private object currentDocument;
        private ComboBox cmbCompany;

        public MainForm()
        {
            InitializeComponent();
            InitializeGUI();
        }

        private void InitializeGUI()
        {
            this.Text = "DocBuilder";
            this.Size = new Size(1200, 700);

            var companyPanel = new Panel();
            companyPanel.Dock = DockStyle.Top;
            companyPanel.Height = 50;

            var lblCompany = new Label();
            lblCompany.Text = "Компания:";
            lblCompany.Location = new Point(20, 15);
            lblCompany.Size = new Size(150, 23);
            lblCompany.AutoSize = true;

            cmbCompany = new ComboBox();
            cmbCompany.Location = new Point(180, 12);
            cmbCompany.Size = new Size(250, 23);
            cmbCompany.DropDownStyle = ComboBoxStyle.DropDownList;

            foreach (var company in companies.Keys)
            {
                cmbCompany.Items.Add(company);
            }
            cmbCompany.SelectedIndex = 0;

            companyPanel.Controls.Add(lblCompany);
            companyPanel.Controls.Add(cmbCompany);
            this.Controls.Add(companyPanel);

            var splitContainer = new SplitContainer();
            splitContainer.Dock = DockStyle.Fill;
            splitContainer.SplitterDistance = 300;
            splitContainer.SplitterWidth = 5;

            var listBox = new ListBox();
            listBox.Name = "lstDocuments";
            listBox.Dock = DockStyle.Fill;
            listBox.SelectedIndexChanged += ListBox_SelectedIndexChanged;
            splitContainer.Panel1.Controls.Add(listBox);

            var txtContent = new RichTextBox();
            txtContent.Name = "txtDocumentContent";
            txtContent.Multiline = true;
            txtContent.Dock = DockStyle.Fill;
            txtContent.ReadOnly = true;
            txtContent.ScrollBars = RichTextBoxScrollBars.Vertical;
            txtContent.Font = new Font("Consolas", 11, FontStyle.Regular);
            txtContent.BackColor = Color.FromArgb(250, 250, 250);
            txtContent.BorderStyle = BorderStyle.FixedSingle;
            txtContent.Padding = new Padding(10);
            splitContainer.Panel2.Controls.Add(txtContent);

            this.Controls.Add(splitContainer);

            var panel = new Panel();
            panel.Dock = DockStyle.Top;
            panel.Height = 70;
            panel.BringToFront();

            var btnInvoice = CreateButton("Счёт", 20, 15, BtnInvoice_Click);
            var btnContract = CreateButton("Договор", 150, 15, BtnContract_Click);
            var btnAct = CreateButton("Акт", 280, 15, BtnAct_Click);
            var btnClone = CreateButton("Клонировать", 430, 15, BtnClone_Click);
            var btnSave = CreateButton("Сохранить", 580, 15, BtnSave_Click);
            var btnEdit = CreateButton("Редактировать", 710, 15, BtnEdit_Click);

            panel.Controls.AddRange(new Control[] { btnInvoice, btnContract, btnAct, btnClone, btnSave, btnEdit});
            this.Controls.Add(panel);
            panel.BringToFront();
        }

        private Button CreateButton(string text, int x, int y, EventHandler click)
        {
            var btn = new Button();
            btn.Text = text;
            btn.Location = new Point(x, y);
            btn.Size = new Size(120, 35);
            btn.Click += click;
            return btn;
        }

        private void BtnInvoice_Click(object sender, EventArgs e)
        {
            var selectedCompany = cmbCompany.SelectedItem.ToString();
            var company = companies[selectedCompany];

            var invoice = new Invoice(company.Name, company.INN, company.BankAccount)
            {
                ClientName = "Новый клиент",
                Amount = 10000,
                DueDate = DateTime.Now.AddDays(30)
            };

            documents.Add(invoice);
            currentDocument = invoice;
            UpdateList();
            DisplayDocumentContent(currentDocument);
            MessageBox.Show($"Счёт №{invoice.DocNumber} создан!\nКомпания: {company.Name}", "Успех");
        }

        private void BtnContract_Click(object sender, EventArgs e)
        {
            var selectedCompany = cmbCompany.SelectedItem.ToString();
            var company = companies[selectedCompany];

            var contract = new Contract(company.Name, company.INN, company.LegalAddress)
            {
                Party1 = company.Name,
                Party2 = "Заказчик",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddYears(1),
                Clauses = "Стандартные условия"
            };

            documents.Add(contract);
            currentDocument = contract;
            UpdateList();
            DisplayDocumentContent(currentDocument);
            MessageBox.Show($"Договор №{contract.DocNumber} создан!\nКомпания: {company.Name}", "Успех");
        }

        private void BtnAct_Click(object sender, EventArgs e)
        {
            var selectedCompany = cmbCompany.SelectedItem.ToString();
            var company = companies[selectedCompany];

            var act = new Act(company.Name, company.INN, company.LegalAddress)
            {
                Executor = company.Name,
                Customer = "Заказчик",
                ActDate = DateTime.Now,
                Amount = 50000
            };

            documents.Add(act);
            currentDocument = act;
            UpdateList();
            DisplayDocumentContent(currentDocument);
            MessageBox.Show($"Акт №{act.DocNumber} создан!\nКомпания: {company.Name}", "Успех");
        }
        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (currentDocument == null)
            {
                MessageBox.Show("Выберите документ для редактирования");
                return;
            }

            bool hasChanges = false;

            if (currentDocument is Invoice invoice)
            {
                var clientName = Microsoft.VisualBasic.Interaction.InputBox(
                    "Введите имя клиента:", "Редактирование счёта", invoice.ClientName);
                if (!string.IsNullOrEmpty(clientName))
                {
                    invoice.ClientName = clientName;
                    hasChanges = true;
                }

                var amountStr = Microsoft.VisualBasic.Interaction.InputBox(
                    "Введите сумму:", "Редактирование счёта", invoice.Amount.ToString("F2"));
                if (double.TryParse(amountStr, out double amount))
                {
                    invoice.Amount = amount;
                    hasChanges = true;
                }
            }
            else if (currentDocument is Contract contract)
            {
                var party2 = Microsoft.VisualBasic.Interaction.InputBox(
                    "Введите заказчика:", "Редактирование договора", contract.Party2);
                if (!string.IsNullOrEmpty(party2))
                {
                    contract.Party2 = party2;
                    hasChanges = true;
                }
            }
            else if (currentDocument is Act act)
            {
                var customer = Microsoft.VisualBasic.Interaction.InputBox(
                    "Введите заказчика:", "Редактирование акта", act.Customer);
                if (!string.IsNullOrEmpty(customer))
                {
                    act.Customer = customer;
                    hasChanges = true;
                }

                var amountStr = Microsoft.VisualBasic.Interaction.InputBox(
                    "Введите сумму:", "Редактирование акта", act.Amount.ToString("F2"));
                if (double.TryParse(amountStr, out double amount))
                {
                    act.Amount = amount;
                    hasChanges = true;
                }
            }

            if (hasChanges)
            {
                UpdateList();

                DisplayDocumentContent(currentDocument);

                MessageBox.Show("Документ обновлён\nНе забудьте нажать 'Сохранить'", "Успех");
            }
            else
            {
                MessageBox.Show("Изменения не внесены", "Информация");
            }
        }
        private void BtnClone_Click(object sender, EventArgs e)
        {
            if (currentDocument == null)
            {
                MessageBox.Show("Выберите документ для клонирования");
                return;
            }

            if (currentDocument is Invoice invoice)
            {
                var newInvoice = new Invoice(invoice.CompanyName, invoice.INN, invoice.BankAccount)
                {
                    ClientName = invoice.ClientName,
                    Amount = invoice.Amount,
                    DueDate = invoice.DueDate,
                    CompanyName = invoice.CompanyName,
                    INN = invoice.INN,
                    BankAccount = invoice.BankAccount
                };
                documents.Add(newInvoice);
                currentDocument = newInvoice;
            }
            else if (currentDocument is Contract contract)
            {
                var newContract = new Contract(contract.CompanyName, contract.INN, contract.LegalAddress)
                {
                    Party1 = contract.Party1,
                    Party2 = contract.Party2,
                    StartDate = contract.StartDate,
                    EndDate = contract.EndDate,
                    Clauses = contract.Clauses,
                    CompanyName = contract.CompanyName,
                    INN = contract.INN,
                    LegalAddress = contract.LegalAddress
                };
                documents.Add(newContract);
                currentDocument = newContract;
            }
            else if (currentDocument is Act act)
            {
                var newAct = new Act(act.CompanyName, act.INN, act.LegalAddress)
                {
                    Executor = act.Executor,
                    Customer = act.Customer,
                    ActDate = act.ActDate,
                    Amount = act.Amount,
                    CompanyName = act.CompanyName,
                    INN = act.INN,
                    LegalAddress = act.LegalAddress
                };
                documents.Add(newAct);
                currentDocument = newAct;
            }

            UpdateList();
            DisplayDocumentContent(currentDocument);
            MessageBox.Show("Документ клонирован!", "Успех");
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (currentDocument == null)
            {
                MessageBox.Show("Нет документа для сохранения");
                return;
            }

            using (var sfd = new SaveFileDialog())
            {
                sfd.Filter = "Text Files|*.txt";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    if (currentDocument is Invoice inv) inv.Save(sfd.FileName);
                    else if (currentDocument is Contract con) con.Save(sfd.FileName);
                    else if (currentDocument is Act a) a.Save(sfd.FileName);

                    MessageBox.Show("Документ сохранён!", "Успех");
                }
            }
        }

        private void ListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var list = sender as ListBox;
            if (list?.SelectedIndex >= 0 && list.SelectedIndex < documents.Count)
            {
                currentDocument = documents[list.SelectedIndex];
                DisplayDocumentContent(currentDocument);
            }
        }

        private void UpdateList()
        {
            var listBox = this.Controls.Find("lstDocuments", true)[0] as ListBox;
            if (listBox == null)
            {
                MessageBox.Show("Ошибка: не найден список документов!");
                return;
            }

            listBox.Items.Clear();

            int index = 0;
            foreach (var doc in documents)
            {
                if (doc is Invoice inv)
                    listBox.Items.Add($"Счёт №{inv.DocNumber} — {inv.ClientName}");
                else if (doc is Contract con)
                    listBox.Items.Add($"Договор №{con.DocNumber} — {con.Party2}");
                else if (doc is Act a)
                    listBox.Items.Add($"Акт №{a.DocNumber} — {a.Customer}");

                index++;
            }
        }

        private void DisplayDocumentContent(object doc)
        {
            var txtContent = this.Controls.Find("txtDocumentContent", true)[0] as RichTextBox;

            if (txtContent == null) return;

            txtContent.Clear();
            txtContent.Font = new Font("Consolas", 10, FontStyle.Regular);
            txtContent.WordWrap = true;

            if (doc is Invoice invoice)
            {
                txtContent.AppendText($"═══════════════════════════════════\n");
                txtContent.AppendText($"       СЧЁТ №{invoice.DocNumber}\n");
                txtContent.AppendText($"═══════════════════════════════════\n\n");
                txtContent.AppendText($"ОРГАНИЗАЦИЯ:\n");
                txtContent.AppendText($"  {invoice.CompanyName}\n");
                txtContent.AppendText($"  ИНН: {invoice.INN}\n");
                txtContent.AppendText($"  Р/с: {invoice.BankAccount}\n\n");
                txtContent.AppendText($"КЛИЕНТ:\n");
                txtContent.AppendText($"  {invoice.ClientName}\n\n");
                txtContent.AppendText($"СУММА: {invoice.Amount:C}\n");
                txtContent.AppendText($"ДАТА ОПЛАТЫ: {invoice.DueDate:dd.MM.yyyy}");
            }
            else if (doc is Contract contract)
            {
                txtContent.AppendText($"═══════════════════════════════════\n");
                txtContent.AppendText($"    ДОГОВОР №{contract.DocNumber}\n");
                txtContent.AppendText($"═══════════════════════════════════\n\n");
                txtContent.AppendText($"ОРГАНИЗАЦИЯ:\n");
                txtContent.AppendText($"  {contract.CompanyName}\n");
                txtContent.AppendText($"  ИНН: {contract.INN}\n");
                txtContent.AppendText($"  АДРЕС:\n  {contract.LegalAddress}\n\n");
                txtContent.AppendText($"СТОРОНЫ:\n");
                txtContent.AppendText($"  1. {contract.Party1}\n");
                txtContent.AppendText($"  2. {contract.Party2}\n\n");
                txtContent.AppendText($"ПЕРИОД:\n");
                txtContent.AppendText($"  с {contract.StartDate:dd.MM.yyyy}\n");
                txtContent.AppendText($"  по {contract.EndDate:dd.MM.yyyy}\n\n");
                txtContent.AppendText($"УСЛОВИЯ:\n  {contract.Clauses}");
            }
            else if (doc is Act act)
            {
                txtContent.AppendText($"═══════════════════════════════════\n");
                txtContent.AppendText($"       АКТ №{act.DocNumber}\n");
                txtContent.AppendText($"═══════════════════════════════════\n\n");
                txtContent.AppendText($"ОРГАНИЗАЦИЯ:\n");
                txtContent.AppendText($"  {act.CompanyName}\n");
                txtContent.AppendText($"  ИНН: {act.INN}\n");
                txtContent.AppendText($"  АДРЕС:\n  {act.LegalAddress}\n\n");
                txtContent.AppendText($"ИСПОЛНИТЕЛЬ: {act.Executor}\n");
                txtContent.AppendText($"ЗАКАЗЧИК: {act.Customer}\n");
                txtContent.AppendText($"ДАТА: {act.ActDate:dd.MM.yyyy}\n");
                txtContent.AppendText($"СУММА: {act.Amount:C}");
            }
            else
            {
                txtContent.AppendText("Выберите документ для просмотра");
            }
        }
    }
}