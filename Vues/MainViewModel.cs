using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Drawing;
using System.Drawing.Printing;

using System.Linq;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Modele;

/*RESTE A FAIRE : 
 * 1. Mettre à jour l'effet de la touche ENTER sur la textbox EnteredNumber
 * 1.1 Il convient lors de l'appuie de la touche ENTER d'appeler la fonction PLUButtonClicked
 *  (PS : J'ai tout essayé mais sans aucun résultat)
 * 2. Corriger la MAJ des articles (lignes +/- 470 du présent code)
 * 3. finir les boutons articles
 * 4. Boutons verts
 * 4.1 CLOTURE
 * 4.1.0 : Créer la structure {int Z, int noTicket; GrandTotal; GrandBons; GrandCash;GrandCartes}
 * 4.1.1 : MAJ zEnCours après chaque ticket. zEnCours = zEncours + totaux ticket
 * 4.1.2 : Créer SaveZ and LoadZ -> Met à jour les Totaux de la caisse
 * 4.1.3 : Créer SaveHistoZ and LoadHistoZ (historique des Z -> XML car la compta les récupere)
 * 4.1.4 : Au demarrage, on charge LoadZ vers zenCours pour faire le 4.1.1
 * 4.1.5 : Click bouton Z -> PRN + SaveZ
 * 4.2 DEMO
 * 5. Le slider ne fonctionne pas
 * 6. Faire le fichier .log qui simule une 2eme imprimante pour le jouranal
 * 7. Triller la place des variables dans le code car c'est moche
 * 8. Mettre du commentaire et rendre présentable
 * 
 * 999. Avant EXAM s'assurer de changer l'imprimante par défaut
 * 
 * */
namespace Vues
{
    public class MainViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private ObservableCollection<Article> _articles;
        public ObservableCollection<Article> Articles
        {
            get { return _articles; }
            set { _articles = value;
                OnPropertyChanged("Articles");
            }
        }

        private ObservableCollection<Client> _clients;
        public ObservableCollection<Client> Clients
        {
            get { return _clients; }
            set { _clients = value; }
        }

        private string _enteredNumber;
        public string EnteredNumber
        {
            get { return _enteredNumber; }
            set
            {
                _enteredNumber = value;
                OnPropertyChanged("EnteredNumber");
            }
        }

        private float _quEntree;
        public float QuEntree
        {
            get { return _quEntree; }
            set { _quEntree = value; }
        }

        private ulong _cb;
        public ulong CB
        {
            get { return _cb; }
            set { _cb = value; }
        }

        private ulong _codeBarre;
        public ulong CodeBarre
        {
            get { return _codeBarre; }
            set { _codeBarre = value; }
        }

        private string _denomination;
        public string Denomination
        {
            get { return _denomination; }
            set { _denomination = value; }
        }

        private int _quantite;
        public int Quantite
        {
            get { return _quantite; }
            set { _quantite = value; }
        }

        private float _prix;
        public float Prix
        {
            get { return _prix; }
            set { _prix = value; }
        }

        private int _points;
        public int Points
        {
            get { return _points; }
            set { _points = value; }
        }

        private int _taille;
        public int Taille
        {
            get { return _taille; }
            set {
                _taille = value;
            }
        }

        public double SliderValue { get; set; }
        public string CheminAcces { get; set; }

        private string _numeroCarteFidelite;
        public string NumeroCarteFidelite
        {
            get { return _numeroCarteFidelite; }
            set { _numeroCarteFidelite = value; }
        }

        private string _nom;
        public string Nom
        {
            get { return _nom; }
            set { _nom = value; }
        }

        private string _prenom;
        public string Prenom
        {
            get { return _prenom; }
            set { _prenom = value; }
        }

        private DateTime _dateNaissance;
        public DateTime DateNaissance
        {
            get { return _dateNaissance; }
            set { _dateNaissance = value; }
        }

        private int _numTicket;
        public int NumTicket
        {
            get { return _numTicket; }
            set { _numTicket = value; }
        }

        private Ticket _ticket;
        public Ticket Ticket
        {
            get { return _ticket; }
            set
            {
                _ticket = value;
                OnPropertyChanged("Ticket");
            }
        }
        private int _buttonTextSize;
        public int ButtonTextSize
        {
            get { return _buttonTextSize; }
            set
            {
                    _buttonTextSize = value * 2;
                   // OnPropertyChanged("ButtonTextSize");
                Console.WriteLine("bouton" + _buttonTextSize);

            }
        }

        private ObservableCollection<MesArticles> _articlesTicket;
        public ObservableCollection<MesArticles> ArticlesTicket
        {
            get { return _articlesTicket; }
            set { _articlesTicket = value; }
        }

        private float _reductions;
        public float Reductions
        {
            get { return _reductions; }
            set
            {
                _reductions = value;
                OnPropertyChanged("Reductions");
            }
        }

        private ICommand _appendNumberCommand;
        public ICommand AppendNumberCommand
        {
            get { return _appendNumberCommand; }
            private set { _appendNumberCommand = value; }
        }

        private ICommand _openNewArticleWindowCommand;
        public ICommand OpenNewArticleWindowCommand
        {
            get { return _openNewArticleWindowCommand; }
            private set { _openNewArticleWindowCommand = value; }
        }

        private ICommand _openNewClientWindowCommand;
        public ICommand OpenNewClientWindowCommand
        {
            get { return _openNewClientWindowCommand; }
            private set { _openNewClientWindowCommand = value; }
        }

        private ICommand _xButtonCommand;
        public ICommand XButtonCommand
        {
            get { return _xButtonCommand; }
            private set { _xButtonCommand = value; }
        }

        private ICommand _validerCommand;
        public ICommand ValiderCommand
        {
            get { return _validerCommand; }
            private set { _validerCommand = value; }
        }

        private ICommand _pluButtonCommand;
        public ICommand PLUButtonCommand
        {
            get { return _pluButtonCommand; }
            private set { _pluButtonCommand = value; }
        }

        private ICommand _saveArticleCommand;
        public ICommand SaveArticleCommand
        {
            get { return _saveArticleCommand; }
            private set { _saveArticleCommand = value; }
        }

        private ICommand _cancelCommand;
        public ICommand CancelCommand
        {
            get { return _cancelCommand; }
            private set { _cancelCommand = value; }
        }
        private ICommand _cancelCommand1;
        public ICommand CancelCommand1
        {
            get { return _cancelCommand1; }
            private set { _cancelCommand1 = value; }
        }
        private ICommand _cancelCommand2;
        public ICommand CancelCommand2
        {
            get { return _cancelCommand2; }
            private set { _cancelCommand2 = value; }
        }



        private ICommand _saveClientCommand;
        public ICommand SaveClientCommand
        {
            get { return _saveClientCommand; }
            private set { _saveClientCommand = value; }
        }

        private ICommand _boulangerieButtonCommand;

        public ICommand OuvrirPoliceWindowCommand { get; private set; }
        public ICommand BoulangerieButtonCommand
        {
            get { return _boulangerieButtonCommand; }
            private set { _boulangerieButtonCommand = value; }
        }

        private ICommand _totalButtonCommand;
        public ICommand TotalButtonCommand
        {
            get { return _totalButtonCommand; }
            private set { _totalButtonCommand = value; }
        }

        private ICommand _bonsButtonCommand;
        public ICommand BonsButtonCommand
        {
            get { return _bonsButtonCommand; }
            private set { _bonsButtonCommand = value; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public MainViewModel()
        {
            Taille = 10;
            OpenNewArticleWindowCommand = new RelayCommand(() => OpenNewArticleWindow());
            OpenNewClientWindowCommand = new RelayCommand(() => OpenNewClientWindow());
            AppendNumberCommand = new RelayCommand<string>(AppendNumber);
            XButtonCommand = new RelayCommand(XButtonClicked);
            PLUButtonCommand = new RelayCommand(PLUButtonClicked);
            SaveArticleCommand = new RelayCommand(SaveArticle);
            CancelCommand = new RelayCommand(Cancel);
            CancelCommand1 = new RelayCommand(Cancel1);
            CancelCommand2 = new RelayCommand(Cancel2);
            ValiderCommand = new RelayCommand(Valider);


            SaveClientCommand = new RelayCommand(SaveClient);
            BoulangerieButtonCommand = new RelayCommand(BoulangerieButtonClicked);
            TotalButtonCommand = new RelayCommand(TotalButtonClicked);
            BonsButtonCommand = new RelayCommand(BonsButtonClicked);
            OuvrirPoliceWindowCommand = new RelayCommand(OpenPoliceWindow);

            Articles = new ObservableCollection<Article>();
            Clients = new ObservableCollection<Client>();
            ArticlesTicket = new ObservableCollection<MesArticles>();

            LoadNumTicket();
            LoadArticles();

            Ticket = new Ticket();
            Ticket.NumTicket = _numTicket;
        }

        private void OpenNewArticleWindow()
        {
            MainWindowNewArticle newArticleWindow = new MainWindowNewArticle();
            newArticleWindow.Show();
        }

        private void OpenNewClientWindow()
        {
            MainWindowNewClient newClientWindow = new MainWindowNewClient();
            newClientWindow.Show();
        }

        private void Valider()
        {
            Taille = (int)SliderValue;
            ButtonTextSize = Taille;
            Console.WriteLine(Taille);
            OnPropertyChanged("ButtonTextSize");
            RaisePropertyChanged("ButtonTextSize");

            Console.WriteLine(CheminAcces);

            // Mettre à jour le registre
            MyAppParamManager paramManager = new MyAppParamManager();
            //paramManager.LoadRegistryParameter();
            paramManager.CheminDossier = CheminAcces;
            paramManager.TaillePolice = Taille;
            paramManager.SaveRegistryParameter();

            Cancel2();
        }



        private void AppendNumber(string number)
        {
            EnteredNumber += number;
            OnPropertyChanged("EnteredNumber");
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void XButtonClicked()
        {
            if (float.TryParse(EnteredNumber, out float result))
            {
                _quEntree = result;
            }
            else
            {
                _quEntree = 1;
            }
            Console.WriteLine(_quEntree);
            ClearEnteredNumber();
        }

        private void ClearEnteredNumber()
        {
            EnteredNumber = string.Empty;
        }

        private void PLUButtonClicked()
        {
            Article _articleEnCours = new Article();
            if (ulong.TryParse(EnteredNumber, out ulong result))
            {
                CB = result;
                Console.WriteLine(_cb);
                bool articleFound = false;
                foreach (Article article in Articles)
                {
                    if (article.CodeBarre == CB)
                    {
                        _articleEnCours = article;
                        articleFound = true;
                        break;
                    }
                }

                if (articleFound)
                {
                    MesArticles venteArticle = new MesArticles((int)QuEntree, _articleEnCours);
                    Ticket.ArticlesEnCours.Add(venteArticle);

                    Console.WriteLine(Ticket.PrnVersTicket());

                    string printerOutput = Ticket.PrnVersTicket();
                    PrintToPrinter(printerOutput);
                    Console.WriteLine("Imprimé");
                    UpdateArticlesTicket();
                }
                else
                {
                    MessageBox.Show("Article inexistant, mettre à jour les articles.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("ERREUR CB", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            ClearEnteredNumber();
            _quEntree = 1;
        }

        private void UpdateArticlesTicket()
        {
            ArticlesTicket.Clear();
            foreach (MesArticles article in Ticket.ArticlesEnCours)
            {
                ArticlesTicket.Add(article);
            }
        }

        private void SaveArticle()
        {
            Article newArticle = new Article()
            {
                CodeBarre = CodeBarre,
                Denomination = Denomination,
                Quantite = Quantite,
                Prix = Prix,
                Points = Points
            };

            //Vu que la ligne ci dessous n'a pas l'air de faire son effet 
            Articles.Add(newArticle);
            // J'ai testé ceci mais même résultat, les articles ne se mettent pas à jour sans avoir redémarré
            LoadArticles();

            string filePath = "Articles.dat";
            newArticle.AppendArticle(newArticle, filePath);



            Application.Current.Windows.OfType<MainWindowNewArticle>().FirstOrDefault()?.Close();
        }

        private void Cancel()
        {
            Application.Current.Windows.OfType<MainWindowNewArticle>().FirstOrDefault()?.Close();
        }
        private void Cancel1()
        {
            Application.Current.Windows.OfType<MainWindowNewClient>().FirstOrDefault()?.Close();
        }
        private void Cancel2()
        {
            Application.Current.Windows.OfType<PoliceWindow>().FirstOrDefault()?.Close();
        }

        private void SaveClient()
        {
            // Vérifier si le numéro de carte est un nombre valide pouvant être stocké dans un ulong
            if (ulong.TryParse(NumeroCarteFidelite, out ulong carteFidelite))
            {
                // Créer un nouveau client avec les données du formulaire
                Client newClient = new Client
                {
                    Nom = Nom,
                    Prenom = Prenom,
                    DateNaissance = DateNaissance,
                    NumIntervenant = 0, // Mettez ici la valeur appropriée pour le numéro d'intervenant
                    CarteFidelite = carteFidelite,
                    Points = 0 // Mettez ici la valeur appropriée pour les points de fidélité
                };

                // Ajouter le nouveau client au vecteur de clients
                Clients.Add(newClient);

                // Ajouter le client dans le fichier des clients en utilisant la méthode AddClient de la classe Client du modèle
                Client.AddClient(newClient);

                // Fermer la fenêtre de création de nouveau client
                Application.Current.Windows.OfType<MainWindowNewClient>().FirstOrDefault()?.Close();
            }
            else
            {
                // Afficher un message d'erreur si le numéro de carte n'est pas valide
                MessageBox.Show("Numéro de carte invalide.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadNumTicket()
        {
            string filePath = "Numtick.dat";

            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                _numTicket = JsonSerializer.Deserialize<int>(json);
            }
            else
            {
                _numTicket = 1;
            }
        }

        private void SaveNumTicket()
        {
            string filePath = "Numtick.dat";
            string json = JsonSerializer.Serialize(_numTicket);
            File.WriteAllText(filePath, json);
        }

        //J'appelle cette fonction pour mettre à jour mon vecteur d'articles :
        //  1. Au demarrage du programme
        //  2. Après chaque ajout d'article 
        private void LoadArticles()
        {
            string filePath = "Articles.dat";

            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                var loadedArticles = JsonSerializer.Deserialize<ObservableCollection<Article>>(json);

                Articles.Clear(); // Effacer les anciens articles de la collection

                foreach (Article article in loadedArticles)
                {
                    Articles.Add(article); // Ajouter les articles chargés à la collection
                }
            }
        }


        private void BoulangerieButtonClicked()
        {
            if (float.TryParse(EnteredNumber, out float result))
            {
                _quEntree = result;
            }
            else
            {
                _quEntree = 1;
            }

            CB = 10101010;
            Article _articleEnCours = new Article();
            bool articleFound = false;
            foreach (Article article in Articles)
            {
                if (article.CodeBarre == CB)
                {
                    _articleEnCours = article;
                    articleFound = true;
                    break;
                }
            }

            if (articleFound)
            {
                MesArticles venteArticle = new MesArticles((int)QuEntree, _articleEnCours);
                Ticket.ArticlesEnCours.Add(venteArticle);

                Console.WriteLine(Ticket.ToString());

                UpdateArticlesTicket();
            }

            _quEntree = 1;
            Console.WriteLine("JE SUIS LA");

            ClearEnteredNumber();
        }

        private void TotalButtonClicked()
        {
            float total = Ticket.CalculerTotal();
            SubTotal = total.ToString();
        }

        private void BonsButtonClicked()
        {
            if (float.TryParse(EnteredNumber, out float bons))
            {
                TotalButtonClicked();
                Reductions += bons;
                EnteredNumber = string.Empty;
                UpdateReductions();
            }
            else
            {
                MessageBox.Show("BON INVALIDE", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UpdateReductions()
        {
            ReductionsText = Reductions.ToString();
            UpdateTotal();
        }

        private void UpdateTotal()
        {
            float subTotal = 0;
            if (float.TryParse(SubTotal, out subTotal))
            {
                Total = (subTotal - Reductions).ToString();
            }
        }

        #region Properties

        private string _subTotal;
        public string SubTotal
        {
            get { return _subTotal; }
            set
            {
                _subTotal = value;
                OnPropertyChanged("SubTotal");
                UpdateTotal();
            }
        }

        private string _reductionsText;
        public string ReductionsText
        {
            get { return _reductionsText; }
            set
            {
                _reductionsText = value;
                OnPropertyChanged("ReductionsText");
            }
        }

        private string _total;
        public string Total
        {
            get { return _total; }
            set
            {
                _total = value;
                OnPropertyChanged("Total");
            }
        }
        private void OpenPoliceWindow()
        {
            PoliceWindow policeWindow = new PoliceWindow();
            policeWindow.ShowDialog();
        }
        private void PrintToPrinter(string output)
        {
            using (PrintDocument printDocument = new PrintDocument())
            {
                printDocument.PrintPage += (sender, e) =>
                {
                    e.Graphics.DrawString(output, new Font("Arial", 10), Brushes.Black, PointF.Empty);
                };
                printDocument.Print();
            }
        }

        #endregion
    }
}
