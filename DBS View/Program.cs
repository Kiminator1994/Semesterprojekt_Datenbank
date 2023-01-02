using DBS_View.View;
using Semesterprojekt_Datenbank.Utilities;
using Semesterprojekt_Datenbank.Viewmodel;

namespace DBS_View
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            DBUtilityCustomer db = new DBUtilityCustomer();
            CustomerVm.CustomerList = db.Read();
            Application.Run(new MainForm());
        }
    }
}