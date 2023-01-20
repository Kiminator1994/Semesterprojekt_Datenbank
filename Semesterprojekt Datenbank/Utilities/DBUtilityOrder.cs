﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using Semesterprojekt_Datenbank.Interfaces;
using Semesterprojekt_Datenbank.Viewmodel;

namespace Semesterprojekt_Datenbank.Utilities
{
    public class DBUtilityOrder : IDBUtility<OrderVM>
    {
        ModelBuilder modelBuilder = new ModelBuilder();

        public void Create(OrderVM item)
        {
            throw new NotImplementedException();
        }

        public bool Delete(OrderVM item)
        {
            throw new NotImplementedException();
        }

        public List<OrderVM> Read()
        {
            throw new NotImplementedException();
        }

        public OrderVM ReadSingle(OrderVM customerVm)
        {
            throw new NotImplementedException();
        }

        public void Update(OrderVM item)
        {
            throw new NotImplementedException();
        }

        public List<string> ReadCustomerNames()
        {
            try
            {
                using (var context = new DataContext())
                {
                    return (from customer in context.Customer
                        select customer.Name).ToList();
                }
            }
            catch (Microsoft.Data.SqlClient.SqlException e)
            {
                MessageBox.Show("Fehler beim auslesen der Daten von der Datenbank. Keine Verbindung zur Datenbank!\r\n \r\n" +
                                "Error Message: \r\n" + e.Message);
                return null;
            }
            catch (Exception e)
            {
                MessageBox.Show("Fehler beim auslesen der Daten von der Datenbank. \r\n \r\n" +
                                "Error Message: \r\n" + e.Message);
                return null;
            }
        }
        public List<string> ReadArticles()
        {
            try
            {
                using (var context = new DataContext())
                {
                    return (from article in context.Article
                        select article.Name).ToList();
                }
            }
            catch (Microsoft.Data.SqlClient.SqlException e)
            {
                MessageBox.Show("Fehler beim auslesen der Daten von der Datenbank. Keine Verbindung zur Datenbank!\r\n \r\n" +
                                "Error Message: \r\n" + e.Message);
                return null;
            }
            catch (Exception e)
            {
                MessageBox.Show("Fehler beim auslesen der Daten von der Datenbank. \r\n \r\n" +
                                "Error Message: \r\n" + e.Message);
                return null;
            }
        }
    }
}