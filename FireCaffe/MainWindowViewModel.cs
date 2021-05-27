using FireCaffeDAL.Models;
using FireCaffeDAL.Services;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Windows.Input;

namespace FireCaffe
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {

        private ObservableCollection<Client> clients;
        private DelegateCommand<Client> saveCommand;
        private Client client;

        public Client Client
        {
            get { return this.client; }
            set
            {
                if (value != this.client)
                {
                    this.client = value;
                    OnPropertyChanged("Client");
                }
            }
        }

        public ObservableCollection<Client> Clients
        {
            get { return this.clients; }
            set
            {
                if (value != this.clients)
                {
                    this.clients = value;
                    OnPropertyChanged("Clients");
                }
            }
        }

        public ICommand SaveCommand
        {
            get { return this.saveCommand; }
        }

        public MainWindowViewModel()
        {
            ClientServices clientServices = new ClientServices();
            clients = new ObservableCollection<Client>(clientServices.GetClients());
            this.saveCommand = new DelegateCommand<Client>(this.SaveClient);
            Client = new Client();
        }

        public void SaveClient(Client client)
        {
            ClientServices clientServices = new ClientServices();
            clientServices.AddClient(client);
            clients.Add(client);
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
