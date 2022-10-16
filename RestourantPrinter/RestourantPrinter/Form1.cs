using Microsoft.EntityFrameworkCore;
using RestourantPrinter.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TableDependency.SqlClient;
using TableDependency.SqlClient.Base.Enums;
using TableDependency.SqlClient.Base.EventArgs;

namespace RestourantPrinter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public SqlTableDependency<OrderHeader> OrderHeaderTableDependency;
        string connectionString = "Server=LAPTOP-RD7402J8;Database=RestaurantDb;Trusted_Connection=True;MultipleActiveResultSets=true";
        RestaurantDbContext _context = new RestaurantDbContext();

        PrintDocument printDocument = new PrintDocument();

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Text = "STARTED";
            startOrderHeaderTableDependency();

            Control.CheckForIllegalCrossThreadCalls = false;
        }


        public void Textboxtext(string mesaj)
        {
            this.Invoke(new MethodInvoker(() =>
            {
                textBox1.Text = textBox1.Text + System.Environment.NewLine + mesaj;
            }));
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                stopOrderHeaderTableDependency();
            }
            catch (Exception ex)
            {
                Textboxtext(ex.ToString());
            }
        }
        private bool startOrderHeaderTableDependency()
        {
            try
            {
                OrderHeaderTableDependency = new SqlTableDependency<OrderHeader>(connectionString);
                OrderHeaderTableDependency.OnChanged += OrderHeaderTableDependency_Changed;
                OrderHeaderTableDependency.OnError += OrderHeaderTableDependency_OnError;
                OrderHeaderTableDependency.Start();
                return true;
            }
            catch (Exception ex)
            {

                Textboxtext(ex.ToString());
            }
            return false;

        }
        private bool stopOrderHeaderTableDependency()
        {
            try
            {
                if (OrderHeaderTableDependency != null)
                {
                    OrderHeaderTableDependency.Stop();

                    return true;
                }
            }
            catch (Exception ex) { Textboxtext(ex.ToString()); }

            return false;

        }
        private void OrderHeaderTableDependency_OnError(object sender, ErrorEventArgs e)
        {
            Textboxtext(e.Error.Message);
        }
        private void PrintDocumentOnPrintPage(object sender, PrintPageEventArgs e)
        {
            e.Graphics.DrawString(this.textBox1.Text, this.textBox1.Font, Brushes.Black, 10, 25);
        }
        private void OrderHeaderTableDependency_Changed(object sender, RecordChangedEventArgs<OrderHeader> e)
        {
            try
            {
                var changedEntity = e.Entity;
                var orderDetailData = _context.OrderDetails.Include(x => x.OrderDetailProps).Where(x => x.OrderId == changedEntity.id).ToList();
                double total = 0;
                if (e.ChangeType == ChangeType.Insert)
                {
                    Textboxtext("BESTELLEN\n" +
                            "\n Bestell Datum  : " + changedEntity.OrderDate.ToString() +
                            "\n Name           : " + changedEntity.Name +
                            "\n Nachname       : " + changedEntity.Surname +
                            "\n Telefonnummer  : " + changedEntity.PhoneNumber +
                            "\n Adresse        : " + changedEntity.Address +
                            "\n Bezirk         : " + changedEntity.District +
                            "\n Bestellhinweis : " + changedEntity.SummaryNote +
                            "\n Zahlungsmethode: " + changedEntity.PaymentMethod);

                    if (orderDetailData != null)
                    {
                        for (int i = 0; i < orderDetailData.Count(); i++)
                        {
                            Textboxtext("\n\n Product        :" + orderDetailData[i].OrderMenuTitle + " " + orderDetailData[i].OrderMenuPrice.ToString("0.00") + " CHF" + "\tx" + orderDetailData[i].Count.ToString());
                            if (orderDetailData[i].OrderDetailProps != null)
                            {
                                for (int x = 0; x < orderDetailData[i].OrderDetailProps.Count(); x++)
                                {
                                    Textboxtext("\n\t\t + " + orderDetailData[i].OrderDetailProps[x].OrderProductPropertiesTitle + " " +
                                        orderDetailData[i].OrderDetailProps[x].OrderProductPropertiesPrice.ToString("0.00") + " CHF");
                                }
                            }
                            var productTotal = (orderDetailData[i].Price) * (orderDetailData[i].Count);
                            Textboxtext("\n\t Gesamt Produkt Preis : " + productTotal.ToString("0.00" + " CHF"));
                            total += productTotal;
                        }

                        Textboxtext("\n\n\n\n GESAMTSUMME    : " + total.ToString("0.00") + " CHF");
                    }

                    printDocument.PrintPage += PrintDocumentOnPrintPage;
                    printDocument.Print();
                    textBox1.Text = null;
                }

            }
            catch (Exception ex)
            {
                Textboxtext(ex.Message);
            }

        } 
    }
}
