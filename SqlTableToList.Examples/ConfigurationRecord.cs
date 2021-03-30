using System.ComponentModel;
using System.Security;
using System.Xml.Serialization;
using GenericSqlProvider.Configuration;

namespace SqlTableToList.Examples
{

    public class ConfigurationRecord : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        // set default
        private DatabaseProviderInfo _databaseProvider;

        private string _databaseHostName = "";
        private string _databasePort = "";
        private string _databaseName = "";
        private string _databaseUserName = "";

        public DatabaseProviderInfo DatabaseProvider
        {
            set
            {
                _databaseProvider = value;
                OnPropertyChanged("DatabaseProvider");
            }
            get { return _databaseProvider; }
        }

        public string DatabaseHostName
        {
            set
            {
                _databaseHostName = value;
                OnPropertyChanged("DatabaseHostName");
            }
            get { return _databaseHostName; }
        }

        public string DatabasePort
        {
            set
            {
                _databasePort = value;
                OnPropertyChanged("DatabasePort");
            }
            get { return _databasePort; }
        }

        public string DatabaseName
        {
            set
            {
                _databaseName = value;
                OnPropertyChanged("DatabaseName");
            }
            get { return _databaseName; }
        }

        public string DatabaseUserName
        {
            set
            {
                _databaseUserName = value;
                OnPropertyChanged("DatabaseUserName");
            }
            get { return _databaseUserName; }
        }

        [XmlIgnore]
        public string DatabaseUserPassword;        
               
        protected virtual void OnPropertyChanged(string strPropertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(strPropertyName));
        }
    }
}
