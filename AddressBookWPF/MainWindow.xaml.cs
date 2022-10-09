using AddressBookWPF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
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

namespace AddressBookWPF
{

    public partial class MainWindow : Window
    {
        private ObservableCollection<ContactPerson> contacts;
        public MainWindow()
        {
            InitializeComponent();
            contacts = new ObservableCollection<ContactPerson>();

            if (contacts.Count > 0)
                lv_Contacts.ItemsSource = contacts.OrderByDescending(x => x.LastName).ToList();
            else
                lv_Contacts.ItemsSource = contacts;
            
            btn_Update.Visibility = Visibility.Hidden;
        }


        private void btn_Add_Click(object sender, RoutedEventArgs e)
        {
            AddToList();
        }
        private void btn_Add_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                if (!string.IsNullOrEmpty(tb_Email.Text))
                {
                    AddToList();
                }
                
            }
        }


        private void ClearFields()
        {
            tb_FirstName.Text = "";
            tb_LastName.Text = "";
            tb_Email.Text = "";
            tb_StreetName.Text = "";
            tb_PostalCode.Text = "";
            tb_City.Text = "";
        }



        private void btn_Remove_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var contact = (ContactPerson)button!.DataContext;

            contacts.Remove(contact);
            lv_Contacts.ItemsSource = contacts.OrderByDescending(x => x.LastName).ToList();
        }

        private void lv_Contacts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           var contact = (ContactPerson)lv_Contacts.SelectedItems[0]!;
            tb_FirstName.Text = contact.FirstName;
            tb_LastName.Text = contact.LastName;
            tb_Email.Text = contact.Email;
            tb_StreetName.Text = contact.StreetName;
            tb_PostalCode.Text = contact.PostalCode;
            tb_City.Text = contact.City;

            btn_Add.Visibility = Visibility.Hidden;
            btn_Update.Visibility = Visibility.Visible;
        }

        private void btn_Update_Click(object sender, RoutedEventArgs e)
        {
            var obj = (Button)sender;
            var contact = (ContactPerson)obj.DataContext;
            contacts.Remove(contact);

            contacts.Add(new ContactPerson
            {
                FirstName = "" + this.tb_FirstName.Text + "",
                LastName = "" + this.tb_LastName.Text + "",
                Email = "" + this.tb_Email.Text + "",
                StreetName = "" + this.tb_StreetName.Text + "",
                PostalCode = "" + this.tb_PostalCode.Text + "",
                City = "" + this.tb_City.Text + "",
            });





        }


        private void AddToList()
        {
                var contact = contacts.FirstOrDefault(x => x.Email == tb_Email.Text);
            if (contact == null)
            {
                contacts.Add(new ContactPerson
                {
                    FirstName = tb_FirstName.Text,
                    LastName = tb_LastName.Text,
                    Email = tb_Email.Text,
                    StreetName = tb_StreetName.Text,
                    PostalCode = tb_PostalCode.Text,
                    City = tb_City.Text,

                }); ;
            }
            else
            {
                MessageBox.Show("Det finns redan en kontakt med samma e-postadress.");
            }

            lv_Contacts.ItemsSource = contacts.OrderByDescending(x => x.LastName).ToList();

            ClearFields();
        }

    }
}
