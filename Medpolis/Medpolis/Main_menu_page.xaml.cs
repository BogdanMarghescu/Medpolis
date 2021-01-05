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
    public partial class Main_menu_page : Page
    {
        private Clinica_MedpolisEntities medpolis_context = new Clinica_MedpolisEntities();
        private CollectionViewSource specComboBox;
        private CollectionViewSource pretSpecialitateDatagrid;
        private CollectionViewSource clientDetails;
        private CollectionViewSource doctorSpecialitateDatagrid;

        internal class Programare_Detalii
        {
            public int ID { get; set; }
            public string Denumire { get; set; }
            public DateTime Data { get; set; }
            public string Doctor { get; set; }
            public int Pret { get; set; }
            public string DataReadable { get; set; }

            public Programare_Detalii() { DataReadable = ""; }

            public Programare_Detalii(int iD, string denumire, DateTime data, string doctor, int pret)
            {
                ID = iD;
                Denumire = denumire;
                Data = data;
                Doctor = doctor;
                Pret = pret;
            }

            public void ConvertData() { DataReadable = Data.ToString("g"); }

            public override bool Equals(object obj)
            {
                return obj is Programare_Detalii other &&
                       ID == other.ID &&
                       Denumire == other.Denumire &&
                       Data == other.Data &&
                       Doctor == other.Doctor &&
                       Pret == other.Pret;
            }

            public override int GetHashCode()
            {
                int hashCode = -180803959;
                hashCode = hashCode * -1521134295 + ID.GetHashCode();
                hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Denumire);
                hashCode = hashCode * -1521134295 + Data.GetHashCode();
                hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Doctor);
                hashCode = hashCode * -1521134295 + Pret.GetHashCode();
                return hashCode;
            }
        }

        private void doctorSelectProgramari_changeVisibility(Visibility visibility)
        {
            doctorComboBox.Visibility = visibility;
            select_doctor_label.Visibility = visibility;
        }

        private void serviciuSelectProgramari_changeVisibility(Visibility visibility)
        {
            serviciuComboBox.Visibility = visibility;
            select_serviciu_label.Visibility = visibility;
        }

        private void dataSelectProgramari_changeVisibility(Visibility visibility)
        {
            select_data.Visibility = visibility;
            select_data_label.Visibility = visibility;
        }

        private void oraSelectProgramari_changeVisibility(Visibility visibility)
        {
            oraComboBox.Visibility = visibility;
            select_ora_label.Visibility = visibility;
        }

        private void detaliiLabels_changeVisibility(Visibility visibility)
        {
            detalii_doctor_label.Visibility = visibility;
            email_doctor_label.Visibility = visibility;
            telefon_doctor_label.Visibility = visibility;
            program_doctor_label.Visibility = visibility;
        }

        private void select_data_setup()
        {
            var start_date = DateTime.Today.AddDays(1);
            var end_date = start_date.AddMonths(2);
            select_data.DisplayDate = start_date;
            select_data.DisplayDateStart = start_date;
            select_data.DisplayDateEnd = end_date;
            for (var date = start_date; date < end_date; date = date.AddDays(1))
                if (date.DayOfWeek == DayOfWeek.Saturday)
                    select_data.BlackoutDates.Add(new CalendarDateRange(date, date.AddDays(1)));
        }

        private DateTime getFullData(DateTime date, string hourString)
        {
            var full_hour = hourString.Split(':');
            return date.AddHours(int.Parse(full_hour[0])).AddMinutes(int.Parse(full_hour[1]));
        }

        private Client get_user_account(Clinica_MedpolisEntities context)
        {
            return (from c in context.Client where c.Email.Equals(email_label.Content.ToString().Trim()) select c).Take(1).ToList()[0];
        }

        private void get_programari_table(Clinica_MedpolisEntities context, Client user)
        {
            var programari = (from p in context.Programare
                              join c in context.Client on p.ID_Client equals c.ID
                              join s in context.Serviciu on p.ID_Serviciu equals s.ID
                              join d in context.Doctor on p.ID_Doctor equals d.ID
                              where c.ID == user.ID
                              select new Programare_Detalii
                              {
                                  ID = p.ID,
                                  Denumire = s.Denumire,
                                  Data = p.Data,
                                  Doctor = d.Nume + " " + d.Prenume,
                                  Pret = s.Pret
                              }).ToList();
            foreach (var programare in programari)
                programare.ConvertData();
            programari_table.ItemsSource = programari;
        }

        public Main_menu_page()
        {
            InitializeComponent();
            specComboBox = ((CollectionViewSource)(FindResource("specialitateViewSource")));
            pretSpecialitateDatagrid = ((CollectionViewSource)(FindResource("specialitateServiciuViewSource")));
            clientDetails = ((CollectionViewSource)(FindResource("clientViewSource")));
            doctorSpecialitateDatagrid = ((CollectionViewSource)(FindResource("specialitateDoctorViewSource")));
            select_data_setup();
            using (var context = new Clinica_MedpolisEntities())
            {
                context.Programare.RemoveRange(from p in context.Programare where p.Data < DateTime.Now select p);
                context.Concediu.RemoveRange(from c in context.Concediu where c.Data_final < DateTime.Now select c);
                context.SaveChanges();
            }
            DataContext = this;
        }

        private void specialitati_tab_Loaded(object sender, RoutedEventArgs e)
        {
            medpolis_context.Specialitate.Load();
            specComboBox.Source = medpolis_context.Specialitate.Local;
            detaliiLabels_changeVisibility(Visibility.Hidden);
            doctoriDataGrid.SelectedIndex = -1;
            program_doctor_TextBox.Content = "";
            doctor_type_label.Content = "Doctori " + ((Specialitate)specialitateComboBox_specialitati.SelectedItem).Denumire;
        }

        private void specialitateComboBox_specialitati_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            doctor_type_label.Content = "Doctori " + ((Specialitate)specialitateComboBox_specialitati.SelectedItem).Denumire;
            detaliiLabels_changeVisibility(Visibility.Hidden);
            doctoriDataGrid.SelectedIndex = -1;
            program_doctor_TextBox.Content = "";
        }

        private void doctoriDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var tip_program = new List<string>() { "8:00 - 14:00", "14:00 - 20:00" };
            var doctor_selectat = (Doctor)doctoriDataGrid.SelectedItem;
            if (doctor_selectat != null)
            {
                doctorSelectProgramari_changeVisibility(Visibility.Visible);
                detaliiLabels_changeVisibility(Visibility.Visible);
                program_doctor_TextBox.Content = tip_program[doctor_selectat.Tura - 1];
            }
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
        
        private void programari_tab_Loaded(object sender, RoutedEventArgs e)
        {
            pret_programare_label.Content = "";
            doctorComboBox.SelectedIndex = -1;
            serviciuComboBox.SelectedIndex = -1;
            select_data.SelectedDate = null;
            doctorSelectProgramari_changeVisibility(Visibility.Hidden);
            serviciuSelectProgramari_changeVisibility(Visibility.Hidden);
            dataSelectProgramari_changeVisibility(Visibility.Hidden);
            oraSelectProgramari_changeVisibility(Visibility.Hidden);
            pret_programare_label.Visibility = Visibility.Hidden;
            notificare_label.Visibility = Visibility.Hidden;
            notificare_label.Content = "";
            stabilire_programare_button.Visibility = Visibility.Hidden;
        }

        private void specialitateComboBox_programare_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            pret_programare_label.Content = "";
            doctorComboBox.SelectedIndex = -1;
            serviciuComboBox.SelectedIndex = -1;
            oraComboBox.SelectedIndex = -1;
            oraComboBox.Items.Clear();
            select_data.SelectedDate = null;
            doctorSelectProgramari_changeVisibility(Visibility.Visible);
            serviciuSelectProgramari_changeVisibility(Visibility.Hidden);
            dataSelectProgramari_changeVisibility(Visibility.Hidden);
            oraSelectProgramari_changeVisibility(Visibility.Hidden);
            pret_programare_label.Visibility = Visibility.Hidden;
            notificare_label.Visibility = Visibility.Hidden;
            notificare_label.Content = "";
            stabilire_programare_button.Visibility = Visibility.Hidden;
        }

        private void doctorComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            pret_programare_label.Content = "";
            serviciuComboBox.SelectedIndex = -1;
            oraComboBox.SelectedIndex = -1;
            oraComboBox.Items.Clear();
            select_data.SelectedDate = null;
            serviciuSelectProgramari_changeVisibility(Visibility.Visible);
            dataSelectProgramari_changeVisibility(Visibility.Hidden);
            oraSelectProgramari_changeVisibility(Visibility.Hidden);
            pret_programare_label.Visibility = Visibility.Hidden;
            notificare_label.Visibility = Visibility.Hidden;
            notificare_label.Content = "";
            stabilire_programare_button.Visibility = Visibility.Hidden;
        }

        private void serviciuComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            oraComboBox.SelectedIndex = -1;
            oraComboBox.Items.Clear();
            select_data.SelectedDate = null;
            dataSelectProgramari_changeVisibility(Visibility.Visible);
            oraSelectProgramari_changeVisibility(Visibility.Hidden);
            pret_programare_label.Visibility = Visibility.Hidden;
            pret_programare_label.Content = "";
            notificare_label.Visibility = Visibility.Hidden;
            notificare_label.Content = "";
            stabilire_programare_button.Visibility = Visibility.Hidden;
        }

        private void select_data_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            pret_programare_label.Content = "";
            oraComboBox.SelectedIndex = -1;
            oraComboBox.Items.Clear();
            pret_programare_label.Visibility = Visibility.Hidden;
            notificare_label.Visibility = Visibility.Hidden;
            notificare_label.Content = "";
            stabilire_programare_button.Visibility = Visibility.Hidden;
            if (select_data.SelectedDate != null)
            {
                oraSelectProgramari_changeVisibility(Visibility.Visible);
                var doctor_selectat = (Doctor)doctorComboBox.SelectedItem;
                var tura1 = new List<string> { "08:00", "08:30", "09:00", "09:30", "10:00", "10:30", "11:00", "11:30", "12:00", "12:30", "13:00", "13:30" };
                var tura2 = new List<string> { "14:00", "14:30", "15:00", "15:30", "16:00", "16:30", "17:00", "17:30", "18:00", "18:30", "19:00", "19:30" };
                var tura = new List<string> { };
                if (doctor_selectat.Tura == 1)
                    tura = tura1;
                else if (doctor_selectat.Tura == 2)
                    tura = tura2;
                foreach (var ora_programare in tura)
                {
                    var new_date = getFullData(select_data.SelectedDate.Value, ora_programare);
                    using (var context = new Clinica_MedpolisEntities())
                    {
                        var user = get_user_account(context);
                        var programari_doctor_ziua_selectata = (from p in context.Programare
                                                                where p.ID_Doctor == doctor_selectat.ID
                                                                where p.Data == new_date
                                                                select p).Take(1).ToList();
                        var programari_acelasi_doctor_ziua_selectata = (from p in context.Programare
                                                                        where p.ID_Doctor == doctor_selectat.ID
                                                                        where DbFunctions.TruncateTime(p.Data) == DbFunctions.TruncateTime(new_date)
                                                                        where p.ID_Client == user.ID
                                                                        select p).Take(1).ToList();
                        var programari_client_ziua_selectata = (from p in context.Programare
                                                                where p.ID_Client == user.ID
                                                                where p.Data == new_date
                                                                select p).Take(1).ToList();
                        var doctor_in_concediu_ziua_selectata = (from c in context.Concediu
                                                                 where c.ID_Doctor == doctor_selectat.ID
                                                                 where (new_date >= c.Data_inceput && new_date <= c.Data_final)
                                                                 select c).Take(1).ToList();
                        if (programari_doctor_ziua_selectata.Count() == 0 &&
                            programari_client_ziua_selectata.Count() == 0 &&
                            programari_acelasi_doctor_ziua_selectata.Count() == 0 && 
                            doctor_in_concediu_ziua_selectata.Count() == 0)
                        {
                            oraComboBox.Items.Add(ora_programare);
                        }
                        if (doctor_in_concediu_ziua_selectata.Count() > 0)
                        {
                            notificare_label.Visibility = Visibility.Visible;
                            notificare_label.Content = "Doctorul se află în concediu in această zi!";
                        }
                        else if (programari_acelasi_doctor_ziua_selectata.Count() > 0)
                        {
                            notificare_label.Visibility = Visibility.Visible;
                            notificare_label.Content = "Aveți deja o programare in această zi la acest doctor!";
                        }
                        else
                        {
                            notificare_label.Visibility = Visibility.Hidden;
                            notificare_label.Content = "";
                        }
                    }
                }
            }
        }

        private void oraComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (((ComboBox)sender).SelectedIndex > -1)
            {
                notificare_label.Visibility = Visibility.Hidden;
                stabilire_programare_button.Visibility = Visibility.Visible;
                pret_programare_label.Visibility = Visibility.Visible;
                pret_programare_label.Content = string.Format("Preț consultație: {0} lei", ((Serviciu)serviciuComboBox.SelectedItem).Pret);
            }
        }

        private void stabilire_programare_button_Click(object sender, RoutedEventArgs e)
        {
            notificare_label.Visibility = Visibility.Hidden;
            if (specialitateComboBox_programare.SelectedIndex > -1)
            {
                if (doctorComboBox.SelectedIndex > -1)
                {
                    if (serviciuComboBox.SelectedIndex > -1)
                    {
                        if (select_data.SelectedDate != null)
                        {
                            if (oraComboBox.SelectedItem != null)
                            {
                                var result = MessageBox.Show(string.Format("Sigur doriți sa programați serviciul {0} la dr. {1} {2} pe data de {3} la ora {4} cu prețul de {5} de lei?", ((Serviciu)serviciuComboBox.SelectedItem).Denumire, ((Doctor)doctorComboBox.SelectedItem).Nume, ((Doctor)doctorComboBox.SelectedItem).Prenume, select_data.SelectedDate.Value.ToShortDateString(), ((string)oraComboBox.SelectedItem), ((Serviciu)serviciuComboBox.SelectedItem).Pret), "Finalizare formular programare", MessageBoxButton.OKCancel, MessageBoxImage.Question, MessageBoxResult.OK);
                                if (result == MessageBoxResult.OK)
                                {
                                    using (var context = new Clinica_MedpolisEntities())
                                    {
                                        var user = get_user_account(context);
                                        var newProgramare = new Programare()
                                        {
                                            ID_Client = user.ID,
                                            ID_Serviciu = ((Serviciu)serviciuComboBox.SelectedItem).ID,
                                            ID_Doctor = ((Doctor)doctorComboBox.SelectedItem).ID,
                                            Data = getFullData(select_data.SelectedDate.Value, ((string)oraComboBox.SelectedItem))
                                        };
                                        context.Programare.Add(newProgramare);
                                        context.SaveChanges();
                                        MessageBox.Show(string.Format("Programarea cu serviciul {0} la dr. {1} {2} pe data de {3} la ora {4} cu prețul de {5} de lei a fost stabilită.", ((Serviciu)serviciuComboBox.SelectedItem).Denumire, ((Doctor)doctorComboBox.SelectedItem).Nume, ((Doctor)doctorComboBox.SelectedItem).Prenume, select_data.SelectedDate.Value.ToShortDateString(), ((string)oraComboBox.SelectedItem), ((Serviciu)serviciuComboBox.SelectedItem).Pret), "Programare stabilită", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
                                        pret_programare_label.Content = "";
                                        doctorComboBox.SelectedIndex = -1;
                                        serviciuComboBox.SelectedIndex = -1;
                                        select_data.SelectedDate = null;
                                        doctorSelectProgramari_changeVisibility(Visibility.Hidden);
                                        serviciuSelectProgramari_changeVisibility(Visibility.Hidden);
                                        dataSelectProgramari_changeVisibility(Visibility.Hidden);
                                        oraSelectProgramari_changeVisibility(Visibility.Hidden);
                                        pret_programare_label.Visibility = Visibility.Hidden;
                                        stabilire_programare_button.Visibility = Visibility.Hidden;
                                        detaliiLabels_changeVisibility(Visibility.Hidden);
                                        program_doctor_TextBox.Content = "";
                                        doctoriDataGrid.SelectedIndex = -1;
                                        get_programari_table(context, user);
                                    }
                                }
                            }
                            else MessageBox.Show("Selectați o oră a programării!", "Formular invalid pentru programare", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
                        }
                        else MessageBox.Show("Selectați o dată a programării!", "Formular invalid pentru programare", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
                    }
                    else MessageBox.Show("Selectați un tip de serviciu!", "Formular invalid pentru programare", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
                }
                else MessageBox.Show("Selectați un doctor!", "Formular invalid pentru programare", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
            }
            else MessageBox.Show("Selectați o specialitate!", "Formular invalid pentru programare", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
        }

        private void cont_tab_Loaded(object sender, RoutedEventArgs e)
        {
            programari_table.SelectedIndex = -1;
            using (var context = new Clinica_MedpolisEntities())
            {
                var user = get_user_account(context);
                medpolis_context.Client.Load();
                clientDetails.Source = new List<Client> { user };
                profile_label.Content += (user.Prenume + " " + user.Nume);
                get_programari_table(context, user);
            }
        }

        private void leave_account_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Doriți să părăsiți contul dumneavoastră?", "Părăsire cont", MessageBoxButton.OKCancel, MessageBoxImage.Question, MessageBoxResult.Cancel);
            if (result == MessageBoxResult.OK)
                NavigationService.Navigate(new Login_page());
        }

        private void DeleteProgramareCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            var programare = e.Parameter as Programare_Detalii;
            using (var context = new Clinica_MedpolisEntities())
            {
                var prog = (from p in context.Programare
                            where p.ID == programare.ID
                            select p).FirstOrDefault();
                context.Programare.Remove(prog);
                context.SaveChanges();
                var user = get_user_account(context);
                get_programari_table(context, user);
            }
        }

        private void crystal_report_viewer_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new Clinica_MedpolisEntities())
            {
                var crystal_reports_page = new Crystal_reports_page();
                crystal_reports_page.get_user(get_user_account(context));
                NavigationService.Navigate(crystal_reports_page);
            }
        }

        private void report_viewer_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new Clinica_MedpolisEntities())
            {
                var reportViewer_Page = new ReportViewer_page();
                reportViewer_Page.get_user(get_user_account(context));
                NavigationService.Navigate(reportViewer_Page);
            }
        }
    }
}
