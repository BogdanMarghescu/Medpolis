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

namespace Medpolis
{
    /// <summary>
    /// Interaction logic for ReportViewer_page.xaml
    /// </summary>
    public partial class ReportViewer_page : Page
    {
        Client user { get; set; }

        public void get_user(Client client)
        {
            user = client;
        }

        public ReportViewer_page()
        {
            InitializeComponent();
        }

        private void report_viewer_page_Loaded(object sender, RoutedEventArgs e)
        {
            var context = new Clinica_MedpolisEntities();
            var reportDataSource = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet_Medpolis");
            _reportViewer.LocalReport.DataSources.Add(reportDataSource);
            reportDataSource.Value = (from d in context.Doctor join s in context.Specialitate on d.ID_Specialitate equals s.ID
                                        select new
                                        {
                                            Doctor = d.Nume + " " + d.Prenume,
                                            Specialitate = s.Denumire,
                                            Email = d.Email,
                                            Telefon = d.Telefon,
                                            Tura = d.Tura
                                        });
            _reportViewer.LocalReport.ReportPath = "../../Report_Medpolis.rdlc";
            _reportViewer.RefreshReport();
        }

        private void back_main_menu_rv_Click(object sender, RoutedEventArgs e)
        {
            var main_menu = new Main_menu_page();
            main_menu.email_label.Content = user.Email;
            main_menu.tab_menu.SelectedIndex = 5;
            NavigationService.Navigate(main_menu);
        }
    }
}
