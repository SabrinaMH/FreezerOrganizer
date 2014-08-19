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
        private string _fileName;
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
                    _openFileDialog = new RelayCommand(
                        param => ShowFileDialog(),
                        param => true // todo: necessary??
                        );
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
                FileName = fileDialog.FileName;
            }
        }

        public string FileName
        {
            get { return _fileName; }
            set 
            {
                if (_fileName != value)
                {
                    _fileName = value;
                    OnPropertyChanged("FileName");
                }
            }
        }

        internal void Save()
        {
            _settings.Save();
        }
    }
}
