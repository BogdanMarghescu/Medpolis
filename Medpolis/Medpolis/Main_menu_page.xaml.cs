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
        CollectionViewSource specComboBox;
        CollectionViewSource pretViewSource;

        public Main_menu_page()
        {
            InitializeComponent();
            specComboBox = ((CollectionViewSource)(FindResource("specialitateViewSource")));
            pretViewSource = ((CollectionViewSource)(FindResource("specialitateServiciuViewSource")));
            DataContext = this;
        }

        private void preturi_tab_menu_Loaded(object sender, RoutedEventArgs e)
        {
            serviciuDataGrid.SelectedIndex = -1;
            medpolis_context.Specialitate.Load();
            specComboBox.Source = medpolis_context.Specialitate.Local;
        }

        private void specialitateComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            serviciuDataGrid.SelectedIndex = -1;
        }
    }
}
