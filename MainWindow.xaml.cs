using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using System.Windows;
using System.Windows.Input;
namespace G23W14
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public HobbyViewModel HobbyViewModel { get; set; } = new HobbyViewModel();

        public MainWindow()
        {
            if (File.Exists("data.json"))
            { //load
                HobbyViewModel.HobbyModels = JsonSerializer.Deserialize<ObservableCollection<HobbyModel>>(File.ReadAllText("data.json"));
            }
            InitializeComponent();
            DataContext = HobbyViewModel;
            DGrid.ItemsSource = HobbyViewModel.HobbyModels;
        }

        private void OnAdd(object sender, RoutedEventArgs e)
        {
            HobbyInput hobbyInput = new HobbyInput();
            hobbyInput.ShowDialog();
            if (!(bool)hobbyInput.DialogResult)
                return;

            HobbyViewModel.Add(new HobbyModel()
            {
                Name = hobbyInput.Name,
                ImagePath = hobbyInput.HobbyImagePath,
                Type = hobbyInput.HobbyType
            });
            RefreshData();
        }

        public void RefreshData()
        {
            File.WriteAllText("data.json", JsonSerializer.Serialize(HobbyViewModel.HobbyModels));
        }

        private void OnGridKeyUp(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Delete)
            {
                HobbyViewModel.Remove(HobbyViewModel.Model);
                RefreshData();
            }
        }

        private void OnGridDoubleClick(object sender, MouseButtonEventArgs e)
        {
            HobbyInput hobbyInput = new HobbyInput(HobbyViewModel.Model.Name, HobbyViewModel.Model.ImagePath, HobbyViewModel.Model.Type);
            hobbyInput.ShowDialog();

            if (!(bool)hobbyInput.DialogResult)
                return;

            HobbyViewModel.Edit(new HobbyModel()
            {
                Name = hobbyInput.Name,
                ImagePath = hobbyInput.HobbyImagePath,
                Type = hobbyInput.HobbyType
            });

            RefreshData();
        }
    }
}
