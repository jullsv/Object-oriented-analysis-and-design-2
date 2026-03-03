using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using WithPattern.Documents;

namespace WithPattern
{
    public class MainForm : Form
    {
        private BaseDocument invoicePrototype;
        private BaseDocument contractPrototype;
        private BaseDocument actPrototype;

        private List<BaseDocument> documents = new List<BaseDocument>();
        private BaseDocument currentDocument;

        private TabControl tabControl;
        private TextBox txtClient;
        private TextBox txtAmountInvoice;
        private TextBox txtDueDate;
        private TextBox txtParty2;
        private TextBox txtStartDate;
        private TextBox txtEndDate;
        private TextBox txtClauses;
        private TextBox txtCustomer;
        private TextBox txtActDate;
        private ListBox lstDocuments;
        private TextBox txtContent;

        public MainForm()
        {
            InitializeComponent();
            ShowStartupDialog();
            CreatePrototypes();
        }

        private void ShowStartupDialog()
        {
            var form = new Form();
            form.Text = "Данные организации";
            form.Size = new Size(450, 350);
            form.StartPosition = FormStartPosition.CenterScreen;

            var txtCompanyName = new TextBox { Location = new Point(20, 45), Size = new Size(390, 23) };
            var txtINN = new TextBox { Location = new Point(20, 110), Size = new Size(390, 23), Text = "7701234567" };
            var txtBankAccount = new TextBox { Location = new Point(20, 175), Size = new Size(390, 23), Text = "40702810100000001234" };
            var txtAddress = new TextBox { Location = new Point(20, 240), Size = new Size(390, 23), Text = "г. Томск" };

            form.Controls.Add(new Label { Text = "Название:", Location = new Point(20, 20), AutoSize = true });
            form.Controls.Add(txtCompanyName);
            form.Controls.Add(new Label { Text = "ИНН:", Location = new Point(20, 85), AutoSize = true });
            form.Controls.Add(txtINN);
            form.Controls.Add(new Label { Text = "Р/с:", Location = new Point(20, 150), AutoSize = true });
            form.Controls.Add(txtBankAccount);
            form.Controls.Add(new Label { Text = "Адрес:", Location = new Point(20, 215), AutoSize = true });
            form.Controls.Add(txtAddress);

            var btnOK = new Button { Text = "OK", DialogResult = DialogResult.OK, Location = new Point(150, 280), Size = new Size(120, 35) };
            form.Controls.Add(btnOK);
            form.AcceptButton = btnOK;

            if (form.ShowDialog() == DialogResult.OK)
            {
                var companyName = txtCompanyName.Text;
                var inn = txtINN.Text;
                var bankAccount = txtBankAccount.Text;
                var address = txtAddress.Text;

                invoicePrototype = new Invoice
                {
                    CompanyName = companyName,
                    INN = inn,
                    BankAccount = bankAccount
                };

                contractPrototype = new Contract
                {
                    CompanyName = companyName,
                    INN = inn,
                    LegalAddress = address,
                    Party1 = companyName
                };

                actPrototype = new Act
                {
                    CompanyName = companyName,
                    INN = inn,
                    LegalAddress = address,
                    Executor = companyName
                };
            }
        }

        private void CreatePrototypes()
        {
            if (invoicePrototype == null)
            {
                MessageBox.Show("Сначала введите данные организации");
                Close();
            }
        }

        private void InitializeComponent()
        {
            this.Text = "DocBuilder";
            this.Size = new Size(1000, 700);

            tabControl = new TabControl();
            tabControl.Location = new Point(10, 10);
            tabControl.Size = new Size(600, 300);

            var tabInvoice = new TabPage("Счёт");
            tabInvoice.Controls.Add(new Label { Text = "Клиент:", Location = new Point(10, 10), AutoSize = true });
            txtClient = new TextBox { Location = new Point(100, 7), Size = new Size(300, 23) };
            tabInvoice.Controls.Add(txtClient);

            tabInvoice.Controls.Add(new Label { Text = "Сумма:", Location = new Point(10, 40), AutoSize = true });
            txtAmountInvoice = new TextBox { Location = new Point(100, 37), Size = new Size(150, 23) };
            tabInvoice.Controls.Add(txtAmountInvoice);

            tabInvoice.Controls.Add(new Label { Text = "Дата (YYYYMMDD):", Location = new Point(10, 70), AutoSize = true });
            txtDueDate = new TextBox { Location = new Point(130, 67), Size = new Size(150, 23), Text = DateTime.Now.AddDays(30).ToString("yyyyMMdd") };
            tabInvoice.Controls.Add(txtDueDate);

            var btnCreateInvoice = new Button { Text = "Создать счёт", Location = new Point(10, 100), Size = new Size(150, 35) };
            btnCreateInvoice.Click += (s, e) => CreateInvoice();
            tabInvoice.Controls.Add(btnCreateInvoice);

            tabControl.TabPages.Add(tabInvoice);

            var tabContract = new TabPage("Договор");
            tabContract.Controls.Add(new Label { Text = "Заказчик:", Location = new Point(10, 10), AutoSize = true });
            txtParty2 = new TextBox { Location = new Point(100, 7), Size = new Size(300, 23) };
            tabContract.Controls.Add(txtParty2);

            tabContract.Controls.Add(new Label { Text = "Дата начала:", Location = new Point(10, 40), AutoSize = true });
            txtStartDate = new TextBox { Location = new Point(120, 37), Size = new Size(150, 23), Text = DateTime.Now.ToString("yyyyMMdd") };
            tabContract.Controls.Add(txtStartDate);

            tabContract.Controls.Add(new Label { Text = "Дата окончания:", Location = new Point(10, 70), AutoSize = true });
            txtEndDate = new TextBox { Location = new Point(140, 67), Size = new Size(150, 23), Text = DateTime.Now.AddYears(1).ToString("yyyyMMdd") };
            tabContract.Controls.Add(txtEndDate);

            tabContract.Controls.Add(new Label { Text = "Условия:", Location = new Point(10, 100), AutoSize = true });
            txtClauses = new TextBox { Location = new Point(10, 120), Size = new Size(400, 80), Multiline = true, Text = "Стандартные условия" };
            tabContract.Controls.Add(txtClauses);

            var btnCreateContract = new Button { Text = "Создать договор", Location = new Point(10, 210), Size = new Size(150, 35) };
            btnCreateContract.Click += (s, e) => CreateContract();
            tabContract.Controls.Add(btnCreateContract);

            tabControl.TabPages.Add(tabContract);

            var tabAct = new TabPage("Акт");
            tabAct.Controls.Add(new Label { Text = "Заказчик:", Location = new Point(10, 10), AutoSize = true });
            txtCustomer = new TextBox { Location = new Point(100, 7), Size = new Size(300, 23) };
            tabAct.Controls.Add(txtCustomer);

            tabAct.Controls.Add(new Label { Text = "Дата акта:", Location = new Point(10, 40), AutoSize = true });
            txtActDate = new TextBox { Location = new Point(100, 37), Size = new Size(150, 23), Text = DateTime.Now.ToString("yyyyMMdd") };
            tabAct.Controls.Add(txtActDate);

            var btnCreateAct = new Button { Text = "Создать акт", Location = new Point(10, 80), Size = new Size(150, 35) };
            btnCreateAct.Click += (s, e) => CreateAct();
            tabAct.Controls.Add(btnCreateAct);

            tabControl.TabPages.Add(tabAct);

            this.Controls.Add(tabControl);

            lstDocuments = new ListBox { Location = new Point(10, 320), Size = new Size(300, 150) };
            lstDocuments.SelectedIndexChanged += (s, e) =>
            {
                if (lstDocuments.SelectedIndex >= 0 && lstDocuments.SelectedIndex < documents.Count)
                {
                    currentDocument = documents[lstDocuments.SelectedIndex];
                    ShowDocument();
                }
            };
            this.Controls.Add(lstDocuments);

            txtContent = new TextBox { Location = new Point(320, 320), Size = new Size(650, 300), Multiline = true, ReadOnly = true, ScrollBars = ScrollBars.Vertical };
            this.Controls.Add(txtContent);

            var btnSave = new Button { Text = "Сохранить", Location = new Point(450, 630), Size = new Size(120, 35) };
            btnSave.Click += (s, e) => SaveDocument();
            this.Controls.Add(btnSave);
        }

        private void CreateInvoice()
        {
            var invoice = (Invoice)invoicePrototype.Clone();
            invoice.ClientName = txtClient.Text;
            invoice.Amount = int.Parse(txtAmountInvoice.Text);
            invoice.DueDate = int.Parse(txtDueDate.Text);

            documents.Add(invoice);
            currentDocument = invoice;
            UpdateList();
            ShowDocument();
            MessageBox.Show($"Счёт №{invoice.DocNumber} создан");
        }

        private void CreateContract()
        {
            var contract = (Contract)contractPrototype.Clone();
            contract.Party2 = txtParty2.Text;
            contract.StartDate = int.Parse(txtStartDate.Text);
            contract.EndDate = int.Parse(txtEndDate.Text);
            contract.Clauses = txtClauses.Text;

            documents.Add(contract);
            currentDocument = contract;
            UpdateList();
            ShowDocument();
            MessageBox.Show($"Договор №{contract.DocNumber} создан");
        }

        private void CreateAct()
        {
            var act = (Act)actPrototype.Clone();
            act.Customer = txtCustomer.Text;
            act.ActDate = int.Parse(txtActDate.Text);

            documents.Add(act);
            currentDocument = act;
            UpdateList();
            ShowDocument();
            MessageBox.Show($"Акт №{act.DocNumber} создан");
        }

        private void SaveDocument()
        {
            if (currentDocument == null)
            {
                MessageBox.Show("Выберите документ");
                return;
            }

            using (var sfd = new SaveFileDialog())
            {
                sfd.Filter = "Text|*.txt";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    currentDocument.Save(sfd.FileName);
                    MessageBox.Show("Сохранено!");
                }
            }
        }

        private void UpdateList()
        {
            lstDocuments.Items.Clear();
            foreach (var doc in documents)
            {
                if (doc is Invoice inv)
                    lstDocuments.Items.Add($"Счёт №{inv.DocNumber} — {inv.ClientName}");
                else if (doc is Contract con)
                    lstDocuments.Items.Add($"Договор №{con.DocNumber} — {con.Party2}");
                else if (doc is Act a)
                    lstDocuments.Items.Add($"Акт №{a.DocNumber} — {a.Customer}");
            }
        }

        private void ShowDocument()
        {
            if (currentDocument != null)
                txtContent.Text = currentDocument.GetText();
        }
    }
}