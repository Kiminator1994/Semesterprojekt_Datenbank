﻿using Semesterprojekt_Datenbank.Model;
using Semesterprojekt_Datenbank.Viewmodel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DBS_View.View
{
    public partial class ArticleForm : Form
    {
        public ArticleForm()
        {
            InitializeComponent();
        }

        private void CmdAddArticle_Click(object sender, EventArgs e)
        {
            AddArticleForm addArticleForm = new AddArticleForm();
            //addArticleForm.FormClosed += new FormClosedEventHandler(this.AddCustomerForm_FormClosed); // Findet addArticleForm nicht....-------------
            addArticleForm.ShowDialog();
        }
        private void CmdDelete_Click(object sender, EventArgs e)
        {
            //--------------------------------------------------------------------------
            //Muss noch aus der DB gelöscht werden!!!!!!!!!!!!!
            //--------------------------------------------------------------------------

            if (DgvArticle.CurrentRow != null)
            {
                // Create row and column variable for better readability
                DataGridViewColumnCollection column = DgvArticle.Columns;
                // Getting column number to prevent errors when changing the order of the columns
                string articleName = Convert.ToString(DgvArticle.CurrentRow.Cells[column["colName"].Index].Value);

                if (MessageBox.Show("Möchten Sie den Kunden" + " \"" + articleName + "\" " +
                                    "wirklich löschen?", "Eintrag löschen?", MessageBoxButtons.YesNo,
                                    MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    //Article article = ArticleVm.ArticleList.Find(x => x.Name == articleName);

                    // Search Data.CustomerList for customers with the same ContactPerson
                    // This prevents an error when deleting a customer with the same ContactPerson as another customer
                    //List<Customer> customerList = CustomerVm.CustomerList.FindAll(x => x.ContactPerson == cu.ContactPerson);
                    ////List<Customer> customerList = Data.CustomerList.FindAll(x => x.Equals(cu.ContactPerson));
                    //if (customerList.Count <= 1)
                    //{
                    //    CustomerVm.PersonList.Remove(cu.ContactPerson);
                    //}
                    //CustomerVm.CustomerList.Remove(cu);
                    DgvArticle.Rows.RemoveAt(DgvArticle.CurrentRow.Index);

                    //CsvHandler.WriteToCsv(CustomerVm.PersonList, CustomerVm.EmployeeList, CustomerVm.ApprenticeList, CustomerVm.CadreList, CustomerVm.CustomerList,
                    //                @"saveDataPerson.csv", @"saveDataEmployee.csv", @"saveDataApprentice.csv", @"saveDataCadre.csv", @"saveDataCustomer.csv");
                }
            }
        }

        private void CmdSearch_Click(object sender, EventArgs e)
        {
            SearchCustomer();
        }
        private void AddCustomerForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            FillDataGrid();
        }

        private void DataResultList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView gridSender = (DataGridView)sender;

            if (gridSender.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.ColumnIndex >= 0)
            {
                int customerNameColumnIndex = DgvArticle.Columns["colName"].Index;
                DataGridViewRow row = DgvArticle.Rows[e.RowIndex];
                DataGridViewCell cell = row.Cells[customerNameColumnIndex];
                string customerName = Convert.ToString(cell.Value);

                CustomerVm cu = CustomerVm.CustomerList.Find(x => x.Name == customerName);

                AddCustomerForm addCustomerForm = new AddCustomerForm(cu);
                addCustomerForm.FormClosed += new FormClosedEventHandler(this.AddCustomerForm_FormClosed);
                addCustomerForm.ShowDialog();
            }
        }

        private void FillDataGrid()
        {
            DgvArticle.Rows.Clear();

            // Create row and column variable for better readability
            DataGridViewRowCollection row = DgvArticle.Rows;

            DataGridViewColumnCollection column = DgvArticle.Columns;

            foreach (Article article in ArticleVm.ArticleList)
            {
                // Add new row to DataGridView
                int rowIndex = row.Add();

                // Create cell variable for better readability
                DataGridViewCellCollection cell = row[rowIndex].Cells;

                // Add data to new row
                cell[column["colNr"].Index].Value = article.Nr;
                cell[column["colName"].Index].Value = article.Name;
                cell[column["colPrice"].Index].Value = article.Price;

                //------------------------------------------------------------------------
                cell[column["colArticleGroup"].Index].Value = "";  // implement  ArticleGroup!!!!!!!
                //------------------------------------------------------------------------


            }
            //DgvCustomer.Sort(column["colName"], ListSortDirection.Ascending);
            DgvArticle.ClearSelection();
        }

        private void SearchCustomer()
        {
            // Create row and column variable for better readability
            DataGridViewColumnCollection column = DgvArticle.Columns;

            int count = 0;
            // Check every row for cells that contain the search string
            if (TxtSearch.Text.Length >= 3)
            {
                foreach (DataGridViewRow row in DgvArticle.Rows)
                {
                    row.Visible = false;
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        if (cell.Value != null)
                        {
                            // search for cells containing string but not cells that are in a button column
                            if (cell.Value.ToString().Contains(TxtSearch.Text, StringComparison.OrdinalIgnoreCase) &&
                                !(cell.OwningColumn is DataGridViewButtonColumn))
                            {
                                row.Visible = true;
                                cell.Selected = true;
                                count++;
                                break;
                            };
                        }

                    }
                }
                if (count == DgvArticle.RowCount)
                {
                    MessageBox.Show("Das Suchergebniss entspricht allen Feldern." + "\r\n" + "Bitte die Suche weiter Eingrenzen",
                                    "Hinweis", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    MessageBox.Show("Die Suche ergab " + count + " Treffer", "Hinweis", MessageBoxButtons.OK,
                                    MessageBoxIcon.Asterisk);
                }
                DgvArticle.Sort(column["colName"], ListSortDirection.Ascending);
                DgvArticle.ClearSelection();
            }
            else
            {
                MessageBox.Show("Bitte mindestens drei Zeichen für die Suche eingeben");
            }
        }

        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            if (TxtSearch.Text.Length < 1)
            {
                foreach (DataGridViewRow row in DgvArticle.Rows)
                {
                    row.Visible = true;
                }
                DgvArticle.ClearSelection();
            }
        }

        private void TxtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SearchCustomer();

                // Suppresses the Windows sound if "enter" is pressed in a single line textbox
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }


    }


}
