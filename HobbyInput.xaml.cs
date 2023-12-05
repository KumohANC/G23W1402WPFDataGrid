using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;

namespace G23W14
{
    /// <summary>
    /// HobbyInput.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class HobbyInput : Window
    {
        public new string Name { get; set; } = string.Empty;
        public string HobbyType { get; set; } = string.Empty;
        public string HobbyImagePath { get; set; } = string.Empty;
        
        private static string[] _hobbies = new string[] { "SPORTS", "IT*COMPUTER", "STOCK*INVESTMENT", "FOOD", "HUMANITIES", "MUSIC", "PICTURE", "FASHION", "MAKE*DIY" };

        public HobbyInput(string name, string path, string type)
        {
            InitializeComponent();
            foreach (var i in _hobbies)
            {
                HComboBox.Items.Add(i);
            }

            HobbyName.Text = name;
            if (File.Exists(path))
            {
                BitmapImage img = new BitmapImage(new Uri(path));
                PreviewImage.Source = img;
            }
            HComboBox.SelectedIndex = Array.IndexOf(_hobbies, type);
        }

        public HobbyInput()
        {
            InitializeComponent();
            foreach (var i in _hobbies)
            {
                HComboBox.Items.Add(i);
            }
            
            HComboBox.SelectedIndex = 0;
        }

        private void OnDone(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(HobbyName.Text))
            {
                ErrorMessage.Text = "Enter the Name";
                return;
            }

            Name = HobbyName.Text;

            if (string.IsNullOrEmpty(HComboBox.Text))
            {
                ErrorMessage.Text = "Select the hobby(type)";
                return;
            }

            HobbyType = HComboBox.SelectedItem.ToString();

            DialogResult = true;
        }

        private void OnCancel(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void OnSelectImage(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "이미지를 선택하세요.";
            ofd.Filter = "picture image(*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png";
            if ((bool)ofd.ShowDialog())
            {
                HobbyImagePath = ofd.FileName;
                BitmapImage img = new BitmapImage(new Uri(HobbyImagePath));

                PreviewImage.Source = img;
            }
        }

        private void HobbyName_GotFocus(object sender, RoutedEventArgs e)
        {
            ErrorMessage.Text = "";
        }

        private void HComboBox_GotFocus(object sender, RoutedEventArgs e)
        {
            ErrorMessage.Text = "";
        }
    }
}
