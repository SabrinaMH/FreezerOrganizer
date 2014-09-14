using FreezerOrganizer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FreezerOrganizer.ViewModel
{
    public class SettingsViewModel : ViewModelBase
    {
        private ICommand _openFileDialog;
        private Settings _settings;

        public SettingsViewModel() 
        {
            _settings = new Settings();
        }

        public ICommand OpenFileDialog
        {
            get 
            {
                if (_openFileDialog == null)
                {
                    _openFileDialog = new RelayCommand(param => ShowFileDialog());
                }
                return _openFileDialog;
            }
        }

        private void ShowFileDialog()
        {
            Microsoft.Win32.OpenFileDialog fileDialog = new Microsoft.Win32.OpenFileDialog();
            fileDialog.Filter = "XML files (*.xml)|*.xml";
            bool? isFileSelected = fileDialog.ShowDialog();

            if (isFileSelected == true)
            {
                Path = fileDialog.FileName;
            }
        }

        public string Path
        {
            get { return _settings["Path"] as string; }
            set
            {
                if ((_settings["Path"] as string) != value)
                {
                    _settings["Path"] = value;
                    OnPropertyChanged("Path");
                }
            }
        }

        internal void Save()
        {
            _settings.Save();
        }
    }
}
