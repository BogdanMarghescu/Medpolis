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
    public partial class Login_page : Page
    {
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

        public Login_page()
        {
            InitializeComponent();
        }

        private void login_Click(object sender, RoutedEventArgs e)
        {
            if (IsEmail(email_box.Text))
            {
                if (IsPassword(password_box.Password))
                {
                    using (var context = new Clinica_MedpolisEntities())
                    {
                        var user = (((from c in context.Client where c.Email.Equals(email_box.Text) select c).Take(1))).ToList();
                        if (user.Count() > 0)
                        {
                            var passwd_encryption = new PasswordEncryption();
                            if (password_box.Password.Equals(passwd_encryption.Decrypt(user[0].Parola)))
                            {
                                var main_menu = new Main_menu_page();
                                main_menu.email_label.Content = email_box.Text;
                                NavigationService.Navigate(main_menu);
                            }
                            else MessageBox.Show("Parolă greșită!", "Eroare de logare", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
                        }
                        else MessageBox.Show("Contul cu adresa de email \"" + email_box.Text + "\" nu există!", "Eroare de logare", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
                    }
                }
                else MessageBox.Show("Introduceți o parolă validă (între 8 si 32 de caractere)!", "Eroare de logare", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
            }
            else MessageBox.Show("Introduceți o adresă de email validă!", "Eroare de logare", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
        }

        private void new_user_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new New_user_page());
        }
    }
}
