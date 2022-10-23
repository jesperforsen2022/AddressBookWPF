using AddressBookWPF.Models;
using AddressBookWPF.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
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
        //creates a lust
        private ObservableCollection<ContactPerson> contacts = new ObservableCollection<ContactPerson>();
        //saves the list to the desktop
        private IFileManager _fileManager = new FileManager();
        private List<ContactPerson> _contacts = new();
        private string _filePath = $@"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\ContactApp.json";

        public MainWindow()
        {
            InitializeComponent();
            lv_Contacts.ItemsSource = contacts;
            _fileManager.Save(_filePath, JsonConvert.SerializeObject(_contacts));

            if (contacts.Count > 0)
                lv_Contacts.ItemsSource = contacts.OrderByDescending(x => x.LastName).ToList();
            else
                lv_Contacts.ItemsSource = contacts;
            _contacts = JsonConvert.DeserializeObject<List<ContactPerson>>(_fileManager.Read(_filePath));
            btn_Clear.Visibility = Visibility.Hidden;


        }
        

        private void btn_Add_Click(object sender, RoutedEventArgs e)
        {
            //adds contact to list when clicking button
            AddToList();


        }

        private void ClearFields()
        {
            //method to clear the list
            tb_FirstName.Text = "";
            tb_LastName.Text = "";
            tb_Email.Text = "";
            tb_StreetName.Text = "";
            tb_PostalCode.Text = "";
            tb_City.Text = "";
            btn_Clear.Visibility = Visibility.Hidden;
        }



        private void btn_Remove_Click(object sender, RoutedEventArgs e)
        {
            //deletes contact when clicking the "Trash can" Button
            try
            {
                var button = sender as Button;
                var contact = (ContactPerson)button!.DataContext;

                contacts.Remove(contact);
                lv_Contacts.ItemsSource = contacts.OrderByDescending(x => x.LastName).ToList();
                _fileManager.Save(_filePath, JsonConvert.SerializeObject(_contacts));
            }
            catch { }
        }

        private void lv_Contacts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //shows details of a contact when marked
            try
            {
                var obj = sender as ListView;
                var contact = (ContactPerson)obj!.SelectedItem;
                if (contact != null)
                {
                    btn_Clear.Visibility = Visibility.Visible;
                    tb_FirstName.Text = contact.FirstName;
                    tb_LastName.Text = contact.LastName;
                    tb_Email.Text = contact.Email;
                    tb_StreetName.Text = contact.StreetName;
                    tb_PostalCode.Text = contact.PostalCode;
                    tb_City.Text = contact.City;
                }
               
            }
            catch { }
         
        }

        private void btn_Clear_Click(object sender, RoutedEventArgs e)
        {
            //clears the textboxes for the user to add a new contact
            var obj = (Button)sender;
            var contact = (ContactPerson)obj!.DataContext;

            ClearFields();

          

        }


        private void AddToList()
        {
            //the method to add contact to the list
            try
            {
                var contact = contacts.FirstOrDefault(x => x.Email == tb_Email.Text);
                btn_Clear.Visibility = Visibility.Visible;
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
            catch { }

        }
        private void btn_SecondUpdate_Click(object sender, RoutedEventArgs e) 
        {
            //updates the contact when pushing the "update marker"
            
            try
            {
                btn_Clear.Visibility = Visibility.Visible;
                var button = sender as Button;
                var contact = (ContactPerson)button!.DataContext;

                if (contact != null)
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
                    contacts.Remove(contact);
                    lv_Contacts.ItemsSource = contacts.OrderByDescending(x => x.LastName).ToList();
                    _fileManager.Save(_filePath, JsonConvert.SerializeObject(_contacts));
                }
                
            }
            catch { }
        }


    }
}
