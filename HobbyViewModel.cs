using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace G23W14
{
    public class HobbyViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<HobbyModel> HobbyModels { get; set; } = new ObservableCollection<HobbyModel>();
        public string HobbyImage { get; set; }

        private HobbyModel _model;

        public HobbyModel Model
        {
            get
            {
                return _model;
            }
            set
            {
                if(value != _model)
                {
                    Select(value);
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void Add(HobbyModel model)
        {
            HobbyModels.Add(model);
        }

        public void Remove(HobbyModel model) 
        {
            HobbyImage = "";
            _model = null;
            HobbyModels.Remove(model);
            OnPropertyChanged(nameof(HobbyImage));
        }

        public void Select(HobbyModel model)
        {
            _model = model;
            HobbyImage = model.ImagePath;
            OnPropertyChanged(nameof(HobbyImage));
        }

        protected void OnPropertyChanged(string propName = "")
        {
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        public void Edit(HobbyModel model)
        {
            HobbyModels[HobbyModels.IndexOf(_model)] = model;
            HobbyImage = model.ImagePath;
            OnPropertyChanged(nameof(HobbyImage));
        }
    }
}
