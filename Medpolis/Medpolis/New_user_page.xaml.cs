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
using System.Text.RegularExpressions;

namespace Medpolis
{
    /// <summary>
    /// Interaction logic for New_user_page.xaml
    /// </summary>
    public partial class New_user_page : Page
    {
        private static bool IsName(string text)
        {
            var name_regex = new Regex("^[A-Z]([a-z])+$");
            return name_regex.IsMatch(text);
        }

        private static bool IsCNP(string text)
        {
            var cnp_regex = new Regex("^[1-8]\\d{2}(0[1-9]|1[0-2])(0[1-9]|[12]\\d|3[01])(0[1-9]|[1-4]\\d|5[0-2]|99)\\d{4}$");
            return cnp_regex.IsMatch(text);
        }

        private static bool IsPhone(string text)
        {
            var phone_regex = new Regex("^07([1-9]){2}([0-9]){6}$");
            return phone_regex.IsMatch(text);
        }

        private static bool IsEmail(string text)
        {
            var email_regex = new Regex("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$");
            return email_regex.IsMatch(text);
        }

        private static bool IsPassword(string text)
        {
            var email_regex = new Regex("^[\\w-]{8,32}$");
            return email_regex.IsMatch(text);
        }
        
        public New_user_page()
        {
            InitializeComponent();
        }

        private void new_user_Click(object sender, RoutedEventArgs e)
        {
            if (IsName(surname_box.Text))
            {
                if (IsName(name_box.Text))
                {
                    if (IsCNP(cnp_box.Text))
                    {
                        if (IsPhone(phone_box.Text))
                        {
                            if (IsEmail(email_box.Text))
                            {
                                if (IsPassword(password_box.Password))
                                {
                                    using (var context = new Clinica_MedpolisEntities())
                                    {
                                        var user = ((from c in context.Client where c.Email.Equals(email_box.Text) select c).Take(1)).ToList();
                                        if (user.Count() == 0)
                                        {
                                            var passwd_encryption = new PasswordEncryption();
                                            var newUser = new Client()
                                            {
                                                Nume = surname_box.Text,
                                                Prenume = name_box.Text,
                                                CNP = cnp_box.Text,
                                                Telefon = phone_box.Text,
                                                Email = email_box.Text,
                                                Parola = passwd_encryption.Encrypt(password_box.Password)
                                            };
                                            context.Client.Add(newUser);
                                            context.SaveChanges();
                                            MessageBox.Show("Contul cu adresa de email \"" + email_box.Text + "\" a fost creat!", "Cont nou creat", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
                                            var main_menu = new Main_menu_page();
                                            main_menu.email_label.Content = email_box.Text;
                                            NavigationService.Navigate(main_menu);
                                        }
                                        else MessageBox.Show("Contul cu adresa de email \"" + email_box.Text + "\" este deja creat!", "Eroare la creare utilizator nou", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
                                    }
                                }
                                else MessageBox.Show("Introduceți o parolă validă (între 8 si 32 de caractere)!", "Eroare la creare utilizator nou", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
                            }
                            else MessageBox.Show("Introduceți o adresă de email validă!", "Eroare la creare utilizator nou", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
                        }
                        else MessageBox.Show("Introduceți un număr de telefon valid!", "Eroare la creare utilizator nou", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
                    }
                    else MessageBox.Show("Introduceți un CNP valid!", "Eroare la creare utilizator nou", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
                }
                else MessageBox.Show("Introduceți un prenume valid!", "Eroare la creare utilizator nou", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
            }
            else MessageBox.Show("Introduceți un nume de familie valid!", "Eroare la creare utilizator nou", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Login_page());
        }
    }
}
