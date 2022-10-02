using DataAccessLayer.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace ClientGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string searchText;
        public RestClient client;
        private RestResponse numOfThings;
        private List<BankData> list;
        public string URL = null;
        public MainWindow()
        {
            InitializeComponent();

            // Default URL
            if (URL == null)
            {
                URL = "http://localhost:52604/";
                client = new RestClient(URL);
            }
            RestRequest request = new RestRequest("api/getalldata");
            numOfThings = client.Get(request);
            list = JsonConvert.DeserializeObject<List<BankData>>(numOfThings.Content);
           
            TotalNum.Text = "Total Records : " + list.Count.ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int index = 0;
            //On click, Get the index....
            bool isNumber = int.TryParse(IndexNum.Text, out int n);
            if (isNumber)
            {
                index = Int32.Parse(IndexNum.Text);
                int length = list.Count;
                if (index > 0 && index <= length)
                {
                    try
                    {
                        RestRequest request = new RestRequest("api/getbankdata/" + index.ToString());
                        RestResponse resp = client.Get(request);
                        //dynamic obj = JObject.Parse(resp.Content);
                        // string fname = JObject.Parse(resp.Content)["FirstName"].ToString();
                        /*string lname = (string)obj.SelectToken("LastName");
                        int accNo = (int)obj.SelectToken("AccNum");
                        uint pin = (uint)obj.SelectToken("Pin");
                        uint balance = (uint)obj.SelectToken("Balance");*/
                        //And now, set the values in the GUI!
                        //image.Source = new BitmapImage(new Uri(dataIntermed.image));
                        //FNameBox.Text = fname;
                        /*LNameBox.Text = lname;
                        BalanceBox.Text = balance.ToString("C");
                        AcctNoBox.Text = accNo.ToString();
                        PinBox.Text = pin.ToString("D4");*/
                        foreach (BankData item in list)
                        {
                            if (item.Id.Equals(index))
                            {
                                uint pin = (uint)item.Pin;
                                uint balance = (uint)item.Balance;
                                FNameBox.Text = item.FirstName;
                                LNameBox.Text = item.LastName;
                                BalanceBox.Text = balance.ToString("C");
                                AcctNoBox.Text = item.AccNum.ToString();
                                PinBox.Text = pin.ToString("D4");
                                break;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message + " Invalid URL!");
                    }
                }
            }

        }

        private async void findBtn_Click(object sender, RoutedEventArgs e)
        {
            bool isText = Regex.IsMatch(searchBox.Text, "[a-zA-Z]");
            if (isText)
            {
                searchText = searchBox.Text;
                //On click, Get the data....
                Task<BankData> task = new Task<BankData>(SearchName);
                // Display LoadBar and lock textboxes
                findBtn.IsEnabled = false;
                goBtn.IsEnabled = false;
                FNameBox.IsReadOnly = true;
                LNameBox.IsReadOnly = true;
                BalanceBox.IsReadOnly = true;
                AcctNoBox.IsReadOnly = true;
                PinBox.IsReadOnly = true;
                searchBox.IsReadOnly = true;
                IndexNum.IsReadOnly = true;
                loadBar.Visibility = Visibility.Visible;
                loadBar.IsIndeterminate = true;
                task.Start();
                BankData db = await task;
                loadBar.IsIndeterminate = false;
                loadBar.Visibility = Visibility.Hidden;
                DisplaySearchData(db);
                findBtn.IsEnabled = true;
                goBtn.IsEnabled = true;
                FNameBox.IsReadOnly = false;
                LNameBox.IsReadOnly = false;
                BalanceBox.IsReadOnly = false;
                AcctNoBox.IsReadOnly = false;
                PinBox.IsReadOnly = false;
                searchBox.IsReadOnly = false;
                IndexNum.IsReadOnly = false;
            }
            else
            {
                searchBox.Text = "Enter a Name!";
            }
        }

        private void DisplaySearchData(BankData data)
        {
            bool isText = Regex.IsMatch(searchBox.Text, "[a-zA-Z]");
            if (isText)
            {
                if (data != null)
                {
                    if (data.LastName != null)
                    {
                        //Set the values in the GUI!
                        //image.Source = new BitmapImage(new Uri("./test.jpg"));
                        uint pin = (uint)data.Pin;
                        uint balance = (uint)data.Balance;
                        FNameBox.Text = data.FirstName;
                        LNameBox.Text = data.LastName;
                        BalanceBox.Text = balance.ToString("C");
                        AcctNoBox.Text = data.AccNum.ToString();
                        PinBox.Text = pin.ToString("D4");
                    }
                    else
                    {
                        searchBox.Text = "Not Found!";
                    }
                }
                else
                {
                    MessageBox.Show("Something Went Wrong!");
                }

            }
        }

        private BankData SearchName()
        {
            try
            {
                //On click, Get the data....
                RestRequest request = new RestRequest("api/searchdata/?name=" + searchText);
                RestResponse resp = client.Post(request);
                BankData result = JsonConvert.DeserializeObject<BankData>(resp.Content);
                if (result != null)
                {
                    return result;
                }
                else
                {
                    searchBox.Text = "Not Found!";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " Invalid URL!");
            }
            return null;
        }

        private void insertBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        public string FactorString(string val)
        {
            string pattern = @"^(\[){1}(.*?)(\]){1}$";
            if (val != null)
            {
                return Regex.Replace(val, pattern, "$2");
            }
            else
            {
                return null;
            }

        }
    }
}
