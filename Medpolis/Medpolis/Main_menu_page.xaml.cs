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
        private CollectionViewSource doctorSpecialitateDatagrid;
        private readonly List<string> tip_program = new List<string>() { "", "8:00 - 14:00", "14:00 - 20:00" };

        public Main_menu_page()
        {
            InitializeComponent();
            specComboBox = ((CollectionViewSource)(FindResource("specialitateViewSource")));
            pretSpecialitateDatagrid = ((CollectionViewSource)(FindResource("specialitateServiciuViewSource")));
            clientDetails = ((CollectionViewSource)(FindResource("clientViewSource")));
            doctorSpecialitateDatagrid = ((CollectionViewSource)(FindResource("specialitateDoctorViewSource")));
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
                programari_table.ItemsSource = consultatii;
            }
        }

        private void leave_account_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Doriți să părăsiți contul dumneavoastră?", "Părăsire cont", MessageBoxButton.OKCancel, MessageBoxImage.Question, MessageBoxResult.Cancel);
            if (result == MessageBoxResult.OK)
                NavigationService.Navigate(new Login_page());
        }

        private void specialitati_tab_Loaded(object sender, RoutedEventArgs e)
        {
            medpolis_context.Specialitate.Load();
            specComboBox.Source = medpolis_context.Specialitate.Local;
            doctoriDataGrid.SelectedIndex = -1;
            program_doctor_label.Content = "";
            doctor_type_label.Content = "Doctori " + ((Specialitate)specialitateComboBox_specialitati.SelectedItem).Denumire;
        }

        private void specialitateComboBox_specialitati_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            doctor_type_label.Content = "Doctori " + ((Specialitate)specialitateComboBox_specialitati.SelectedItem).Denumire;
            doctoriDataGrid.SelectedIndex = -1;
            program_doctor_label.Content = "";
        }

        private void doctoriDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (turaTextBox_doctor.Content != null)
            {
                program_doctor_label.Content = tip_program[(short)turaTextBox_doctor.Content];
            }
        }

        private void specialitateComboBox_programare_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            doctorComboBox.SelectedIndex = -1;
            serviciuComboBox.SelectedIndex = -1;
        }
    }
}
