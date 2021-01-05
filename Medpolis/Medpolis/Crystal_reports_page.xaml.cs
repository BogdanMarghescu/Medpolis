using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace Medpolis
{
    /// <summary>
    /// Interaction logic for Crystal_reports_page.xaml
    /// </summary>
    public partial class Crystal_reports_page : Page
    {
        Client user { get; set; }

        public void get_user(Client client)
        {
            user = client;
        }

        public Crystal_reports_page()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ReportDocument report = new ReportDocument();
            report.Load("../../CrystalReport_Medpolis.rpt");
            using (var context = new Clinica_MedpolisEntities())
            {
                report.SetDataSource(from p in context.Programare
                                  join c in context.Client on p.ID_Client equals c.ID
                                  join s in context.Serviciu on p.ID_Serviciu equals s.ID
                                  join d in context.Doctor on p.ID_Doctor equals d.ID
                                  select new
                                  {
                                      ID = p.ID,
                                      Pacient = c.Nume + " " + c.Prenume,
                                      Doctor = d.Nume + " " + d.Prenume,
                                      Serviciu = s.Denumire,
                                      Data = p.Data,
                                      Pret = s.Pret
                                  });
            }
            crystalReportsViewer_Medpolis.ViewerCore.ReportSource = report;
        }

        private void back_main_menu_Click(object sender, RoutedEventArgs e)
        {
            var main_menu = new Main_menu_page();
            main_menu.email_label.Content = user.Email;
            main_menu.tab_menu.SelectedIndex = 5;
            NavigationService.Navigate(main_menu);
        }
    }
}
