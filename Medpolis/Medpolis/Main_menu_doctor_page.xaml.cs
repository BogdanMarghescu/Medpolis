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
    /// Interaction logic for Main_menu_doctor_page.xaml
    /// </summary>
    public partial class Main_menu_doctor_page : Page
    {
        private Clinica_MedpolisEntities medpolis_context = new Clinica_MedpolisEntities();
        private CollectionViewSource doctorDetails;
        private CollectionViewSource concediiDatagrid;

        internal class Programare_Detalii_Doctor
        {
            public int ID { get; set; }
            public string Denumire { get; set; }
            public DateTime Data { get; set; }
            public string Pacient { get; set; }
            public int Pret { get; set; }
            public string DataReadable { get; set; }

            public Programare_Detalii_Doctor() { DataReadable = ""; }

            public Programare_Detalii_Doctor(int iD, string denumire, DateTime data, string pacient, int pret)
            {
                ID = iD;
                Denumire = denumire;
                Data = data;
                Pacient = pacient;
                Pret = pret;
            }

            public void ConvertData() { DataReadable = Data.ToString("g"); }

            public override bool Equals(object obj)
            {
                return obj is Programare_Detalii_Doctor other &&
                       ID == other.ID &&
                       Denumire == other.Denumire &&
                       Data == other.Data &&
                       Pacient == other.Pacient &&
                       Pret == other.Pret;
            }

            public override int GetHashCode()
            {
                int hashCode = -180803959;
                hashCode = hashCode * -1521134295 + ID.GetHashCode();
                hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Denumire);
                hashCode = hashCode * -1521134295 + Data.GetHashCode();
                hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Pacient);
                hashCode = hashCode * -1521134295 + Pret.GetHashCode();
                return hashCode;
            }
        }

        internal class Perioada_Concediu_Doctor
        {
            public DateTime Data_inceput { get; set; }
            public DateTime Data_final { get; set; }
            public string DataReadable_inceput { get; set; }
            public string DataReadable_final { get; set; }

            public void ConvertData() 
            {
                DataReadable_inceput = Data_inceput.ToString("D");
                DataReadable_final = Data_final.ToString("D");
            }

            public Perioada_Concediu_Doctor()
            {
                DataReadable_inceput = "";
                DataReadable_final = "";
            }

            public Perioada_Concediu_Doctor(DateTime data_inceput, DateTime data_final)
            {
                Data_inceput = data_inceput;
                Data_final = data_final;
            }

            public override bool Equals(object obj)
            {
                return obj is Perioada_Concediu_Doctor other &&
                       Data_inceput == other.Data_inceput &&
                       Data_final == other.Data_final;
            }

            public override int GetHashCode()
            {
                int hashCode = 507541748;
                hashCode = hashCode * -1521134295 + Data_inceput.GetHashCode();
                hashCode = hashCode * -1521134295 + Data_final.GetHashCode();
                return hashCode;
            }
        }

        private void select_data_start_setup(DateTime date)
        {
            var start_date = date;
            var end_date = start_date.AddYears(1);
            select_data_start.SelectedDate = null;
            select_data_start.DisplayDate = start_date;
            select_data_start.DisplayDateStart = start_date;
            select_data_start.DisplayDateEnd = end_date;
            using (var context = new Clinica_MedpolisEntities())
            {
                var doctor = get_doctor_account(context);
                var concedii = (from c in context.Concediu 
                                where ((c.Data_inceput >= start_date && c.Data_final <= end_date) || 
                                (c.Data_inceput <= start_date && c.Data_final >= start_date && c.Data_final <= end_date) || 
                                (c.Data_inceput >= start_date && c.Data_inceput <= end_date && c.Data_final >= end_date))
                                where c.ID_Doctor == doctor.ID
                                select c).ToList();
                foreach (var concediu in concedii)
                    select_data_start.BlackoutDates.Add(new CalendarDateRange(concediu.Data_inceput, concediu.Data_final));
            }
        }

        private void select_data_final_setup(DateTime date)
        {
            var start_date = date;
            var end_date = start_date.AddYears(1);
            select_data_final.SelectedDate = null;
            select_data_final.DisplayDate = start_date;
            select_data_final.DisplayDateStart = start_date;
            select_data_final.DisplayDateEnd = end_date;
            using (var context = new Clinica_MedpolisEntities())
            {
                var doctor = get_doctor_account(context);
                var concedii = (from c in context.Concediu
                                where ((c.Data_inceput >= start_date && c.Data_final <= end_date) ||
                                (c.Data_inceput <= start_date && c.Data_final >= start_date && c.Data_final <= end_date) ||
                                (c.Data_inceput >= start_date && c.Data_inceput <= end_date && c.Data_final >= end_date))
                                where c.ID_Doctor == doctor.ID
                                select c).ToList();
                foreach (var concediu in concedii)
                    select_data_final.BlackoutDates.Add(new CalendarDateRange(concediu.Data_inceput, concediu.Data_final));
            }
        }

        private void dataSelectFinalConcediu_changeVisibility(Visibility visibility)
        {
            select_data_final.Visibility = visibility;
            select_data_final_label.Visibility = visibility;
        }

        private Doctor get_doctor_account(Clinica_MedpolisEntities context)
        {
            return (from d in context.Doctor where d.Email.Equals(email_label.Content.ToString().Trim()) select d).Take(1).ToList()[0];
        }

        private void get_programari_table_doctor(Clinica_MedpolisEntities context, Doctor doctor)
        {
            var programari = (from p in context.Programare
                              join c in context.Client on p.ID_Client equals c.ID
                              join s in context.Serviciu on p.ID_Serviciu equals s.ID
                              join d in context.Doctor on p.ID_Doctor equals d.ID
                              where d.ID == doctor.ID
                              select new Programare_Detalii_Doctor
                              {
                                  ID = p.ID,
                                  Denumire = s.Denumire,
                                  Data = p.Data,
                                  Pacient = c.Nume + " " + c.Prenume,
                                  Pret = s.Pret
                              }).ToList();
            foreach (var programare in programari)
                programare.ConvertData();
            programari_table.ItemsSource = programari;
        }

        private void get_concedii_table_doctor(Clinica_MedpolisEntities context, Doctor doctor)
        {
            var concedii = (from c in context.Concediu
                            where c.ID_Doctor == doctor.ID
                            select new Perioada_Concediu_Doctor { Data_inceput = c.Data_inceput, Data_final = c.Data_final }).ToList();
            foreach (var concediu in concedii)
                concediu.ConvertData();
            concediiDatagrid.Source = concedii;
        }

        public Main_menu_doctor_page()
        {
            InitializeComponent();
            doctorDetails = ((CollectionViewSource)(FindResource("doctorViewSource")));
            concediiDatagrid = ((CollectionViewSource)(FindResource("doctorConcediuViewSource")));
            using (var context = new Clinica_MedpolisEntities())
            {
                context.Programare.RemoveRange(from p in context.Programare where p.Data < DateTime.Now select p);
                context.Concediu.RemoveRange(from c in context.Concediu where c.Data_final < DateTime.Now select c);
                context.SaveChanges();
            }
            DataContext = this;
        }

        private void profil_tab_Loaded(object sender, RoutedEventArgs e)
        {
            programari_table.SelectedIndex = -1;
            using (var context = new Clinica_MedpolisEntities())
            {
                var doctor = get_doctor_account(context);
                medpolis_context.Doctor.Load();
                doctorDetails.Source = new List<Doctor> { doctor };
                profile_label.Content += (doctor.Prenume + " " + doctor.Nume);
                get_programari_table_doctor(context, doctor);
            }
        }

        private void DeleteProgramareCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            var programare = e.Parameter as Programare_Detalii_Doctor;
            using (var context = new Clinica_MedpolisEntities())
            {
                var prog = (from p in context.Programare
                            where p.ID == programare.ID
                            select p).FirstOrDefault();
                context.Programare.Remove(prog);
                context.SaveChanges();
                var doctor = get_doctor_account(context);
                get_programari_table_doctor(context, doctor);
            }
        }

        private void leave_account_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Doriți să părăsiți contul dumneavoastră?", "Părăsire cont", MessageBoxButton.OKCancel, MessageBoxImage.Question, MessageBoxResult.Cancel);
            if (result == MessageBoxResult.OK)
                NavigationService.Navigate(new Login_page());
        }

        private void concedii_tab_Loaded(object sender, RoutedEventArgs e)
        {
            select_data_start_setup(DateTime.Today.AddDays(1));
            select_data_final_setup(select_data_start.DisplayDate.AddDays(1));
            dataSelectFinalConcediu_changeVisibility(Visibility.Hidden);
            adaugare_concediu_button.Visibility = Visibility.Hidden;
            using (var context = new Clinica_MedpolisEntities())
            {
                var doctor = get_doctor_account(context);
                get_concedii_table_doctor(context, doctor);
            }
        }

        private void select_data_start_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (select_data_start.SelectedDate != null)
                select_data_final_setup(select_data_start.SelectedDate.Value.AddDays(1));
            else
                select_data_final_setup(select_data_start.DisplayDate.AddDays(1));
            dataSelectFinalConcediu_changeVisibility(Visibility.Visible);
            adaugare_concediu_button.Visibility = Visibility.Hidden;
        }

        private void select_data_final_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            adaugare_concediu_button.Visibility = Visibility.Visible;
        }

        private void adaugare_concediu_button_Click(object sender, RoutedEventArgs e)
        {
            if (select_data_start.SelectedDate != null)
            {
                if (select_data_final.SelectedDate != null)
                {
                    var result = MessageBox.Show(string.Format("Sigur doriți sa programați un concediu de pe {0} până pe {1}?", select_data_start.SelectedDate.Value.ToShortDateString(), select_data_final.SelectedDate.Value.ToShortDateString()), "Finalizare formular concediu", MessageBoxButton.OKCancel, MessageBoxImage.Question, MessageBoxResult.OK);
                    if (result == MessageBoxResult.OK)
                    {
                        using (var context = new Clinica_MedpolisEntities())
                        {
                            var doctor = get_doctor_account(context);
                            var data_inceput = select_data_start.SelectedDate.Value;
                            var data_final = select_data_final.SelectedDate.Value.AddHours(23).AddMinutes(59).AddSeconds(59);
                            var programari_in_concediu = (from p in context.Programare
                                                          where p.ID_Doctor == doctor.ID
                                                          where (p.Data >= data_inceput && p.Data <= data_final)
                                                          select p).Count();
                            var concedii_in_concediu = (from c in context.Concediu
                                                        where c.ID_Doctor == doctor.ID
                                                        where (c.Data_inceput >= data_inceput && c.Data_inceput <= data_final && c.Data_final >= data_inceput && c.Data_final <= data_final)
                                                        select c).Count();
                            if (concedii_in_concediu == 0)
                            {
                                if (programari_in_concediu == 0)
                                {
                                    var newConcediu = new Concediu()
                                    {
                                        ID_Doctor = doctor.ID,
                                        Data_inceput = data_inceput,
                                        Data_final = data_final
                                    };
                                    context.Concediu.Add(newConcediu);
                                    context.SaveChanges();
                                    MessageBox.Show(string.Format("Ați programat un concediu de pe {0} până pe {1}!", select_data_start.SelectedDate.Value.ToShortDateString(), select_data_final.SelectedDate.Value.ToShortDateString(), "Concediu stabilit", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK));
                                    select_data_start_setup(DateTime.Today.AddDays(1));
                                    select_data_final_setup(select_data_start.DisplayDate.AddDays(1));
                                    dataSelectFinalConcediu_changeVisibility(Visibility.Hidden);
                                    adaugare_concediu_button.Visibility = Visibility.Hidden;
                                    get_concedii_table_doctor(context, doctor);
                                }
                                else MessageBox.Show("Aveti deja consultații in această perioadă!", "Consultații în perioada concediului", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
                            }
                            else MessageBox.Show("Aveti deja cel puțin un concediu in această perioadă!", "Consultații în perioada concediului", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
                        }
                    }
                }
                else MessageBox.Show("Selectați o dată de final a concediului!", "Formular invalid pentru concediu", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
            }
            else MessageBox.Show("Selectați o dată de început a concediului!", "Formular invalid pentru concediu", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
        }
    }
}