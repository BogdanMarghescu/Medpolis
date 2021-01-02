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
using System.Data.Entity;

namespace Medpolis
{
    /// <summary>
    /// Interaction logic for Main_menu_page.xaml
    /// </summary>
    public partial class Main_menu_page : Page
    {
        private Clinica_MedpolisEntities medpolis_context = new Clinica_MedpolisEntities();
        private CollectionViewSource specComboBox;
        private CollectionViewSource pretSpecialitateDatagrid;
        private CollectionViewSource clientDetails;

        public Main_menu_page()
        {
            InitializeComponent();
            specComboBox = ((CollectionViewSource)(FindResource("specialitateViewSource")));
            pretSpecialitateDatagrid = ((CollectionViewSource)(FindResource("specialitateServiciuViewSource")));
            clientDetails = ((CollectionViewSource)(FindResource("clientViewSource")));
            DataContext = this;
        }

        private void preturi_tab_menu_Loaded(object sender, RoutedEventArgs e)
        {
            medpolis_context.Specialitate.Load();
            specComboBox.Source = medpolis_context.Specialitate.Local;
            serviciuDataGrid.SelectedIndex = -1;
        }

        private void specialitateComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            serviciuDataGrid.SelectedIndex = -1;
        }

        private void cont_tab_Loaded(object sender, RoutedEventArgs e)
        {
            programari_table.SelectedIndex = -1;
            using (var context = new Clinica_MedpolisEntities())
            {
                var user_email = cont_label.Content.ToString().Substring(("Contul meu:  ").Length).Trim();
                var user = ((((from c in context.Client where c.Email.Equals(user_email) select c).Take(1))).ToList());
                medpolis_context.Client.Load();
                clientDetails.Source = user;
                profile_label.Content += (user[0].Prenume + " " + user[0].Nume);
                var consultatii = (from p in context.Programare
                                   join c in context.Client on p.ID_Client equals c.ID
                                   join s in context.Serviciu on p.ID_Serviciu equals s.ID
                                   join d in context.Doctor on p.ID_Doctor equals d.ID
                                   where c.Email.Equals(user_email)
                                   select new
                                   {
                                       Denumire = s.Denumire,
                                       Data = p.Data,
                                       Doctor = d.Nume + " " + d.Prenume,
                                       Pret = s.Pret
                                   }).ToList();
                //foreach (var consultatie in consultatii)
                //{
                //    consultatie.Data.ToShortDateString();
                //}
                programari_table.ItemsSource = consultatii;
            }
        }

        private void leave_account_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
