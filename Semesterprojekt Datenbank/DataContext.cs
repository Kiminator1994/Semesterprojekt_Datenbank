﻿using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Semesterprojekt_Datenbank.EntityConfiguration;
using Semesterprojekt_Datenbank.Model;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace Semesterprojekt_Datenbank
{
    public class DataContext : DbContext
    {
        DbSet<Article> Articles;
        DbSet<ArticleGroup> ArticleGroups;
        DbSet<Customer> Customer;
        DbSet<Invoice> Invoices;
        DbSet<Town> Town;
        DbSet<Order> Orders;
        DbSet<Position> Positions;
        DbSet<MWST> MWSTs;

        const string OUSAMA_CONNECTION = "Server=DESKTOP-PMVN625; Database=DBAProject; Trusted_Connection=true; Encrypt=false;";
        const string Leandro_Connection = "Server=LEANDROPAJE1C16\\ZBWMSSQL; Database=SemesterarbeitDBS; Trusted_Connection=true; Encrypt=false;";
        const string BigBoss = "Server=DESKTOP-1470VE0\\ZBW; Database=SemesterarbeitDBS; Trusted_Connection=true; Encrypt=false;";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            

            optionsBuilder.UseSqlServer(OUSAMA_CONNECTION);
            optionsBuilder.LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Debug);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new ArticleConf().Visit(modelBuilder);
            new ArticleGroupConf().Visit(modelBuilder);
            new CustomerConf().Visit(modelBuilder);
            new InvoiceConf().Visit(modelBuilder);
            new MWSTConf().Visit(modelBuilder);
            new OrderConf().Visit(modelBuilder);
            new PositionConf().Visit(modelBuilder);
            new TownConf().Visit(modelBuilder);

            var lineNumber = 0;

            using (var connection = new SqlConnection())
            {
                connection.ConnectionString = OUSAMA_CONNECTION;
                SqlCommand cmd = new SqlCommand();
                connection.Open();

                using (StreamReader reader = new StreamReader(@"C:\ZbwTechniker\DatenbankenAdvanced\plz_verzeichnis_new.csv"))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        if (lineNumber != 0)
                        {

                            var values = line.Split(";");
                            var zipcode = values[0];
                            var city = values[1];

                            var sql = "INSERT INTO dbo.Town(Zipcode, City, Country)" +
                                      "VALUES" +
                                      $"({values[0]}, '{values[1].Replace("'", "´")}' , 'CH')";

                            cmd.CommandText = sql;
                            cmd.CommandType = CommandType.Text;

                            Console.WriteLine(sql);
                            cmd.Connection = connection;
                            cmd.ExecuteNonQuery();
                        }

                        lineNumber++;
                    }
                }
            }
        }

    }
}
