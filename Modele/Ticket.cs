using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Modele
{
    public class Ticket
    {
        #region Variables_membre
        private DateTime _dateTicket;
        private int _numTicket;
        private Client _client;
        private Caissier _caissier;
        private List<MesArticles> _articlesEnCours;

        #endregion

        #region Constructeurs
        // Constructeur par défaut
        public Ticket()
        {
            _dateTicket = DateTime.Now;
            _numTicket = 0;
            _client = new Client();
            _caissier = new Caissier();
            _articlesEnCours = new List<MesArticles>();
        }

        // Constructeur avec paramètres
        public Ticket(int numTicket, Client client, Caissier caissier)
        {
            _dateTicket = DateTime.Now;
            _numTicket = numTicket;
            _client = client;
            _caissier = caissier;
            _articlesEnCours = new List<MesArticles>();
        }
        #endregion

        #region Accesseurs
        public DateTime DateTicket
        {
            get { return _dateTicket; }
            set { _dateTicket = value; }
        }

        public int NumTicket
        {
            get { return _numTicket; }
            set { _numTicket = value; }
        }

        public Client Client
        {
            get { return _client; }
            set { _client = value; }
        }

        public Caissier Caissier
        {
            get { return _caissier; }
            set { _caissier = value; }
        }

        public List<MesArticles> ArticlesEnCours
        {
            get { return _articlesEnCours; }
            set { _articlesEnCours = value; }
        }
        #endregion

        #region Méthodes
        public float CalculerTotal()
        {
            float total = 0.0f;
            foreach (MesArticles article in _articlesEnCours)
            {
                total += article.Montant;
            }
            return total;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Ticket))
            {
                return false;
            }

            Ticket t = (Ticket)obj;
            return _numTicket == t._numTicket && _client.Equals(t._client) && _caissier.Equals(t._caissier) && _articlesEnCours.Equals(t._articlesEnCours);
        }

        public override string ToString()
        {
            string ticketString = "Date : " + _dateTicket.ToString("dd/MM/yyyy HH:mm:ss") + "\n";
            ticketString += "Ticket n°" + _numTicket + "\n";
            ticketString += "Client : " + _client.ToString() + "\n";
            ticketString += "Caissier : " + _caissier.ToString() + "\n";
            ticketString += "Articles :\n";
            foreach (MesArticles article in _articlesEnCours)
            {
                ticketString += "   " + article.ToString() + "\n";
            }
            ticketString += "Total : " + CalculerTotal() + "€\n";
            return ticketString;
        }

        public string PrnVersTicket()
        {
            string ticketString = "Date : " + _dateTicket.ToString("dd/MM/yyyy HH:mm:ss") + "\n";
            ticketString += "Ticket n°" + _numTicket + "\n";
            ticketString += "Client : " + _client.ToString() + "\n";
            ticketString += "Caissier : " + _caissier.ToString() + "\n";
            ticketString += "Articles :\n";
            for (int i = 0; i < _articlesEnCours.Count; i++)
            {
                ticketString += "   " + _articlesEnCours[i].PrnVersTicket() + "\n";
            }
            ticketString += "Total : " + CalculerTotal() + "€\n";
            return ticketString;

        }

        public void Save()
        {
            string fileName = "T" + _numTicket + ".xml";

            XmlSerializer serializer = new XmlSerializer(typeof(Ticket));
            using (FileStream stream = new FileStream(fileName, FileMode.Create))
            {
                serializer.Serialize(stream, this);
            }
        }

        public static Ticket Load(int numTicket)
        {
            string fileName = "T" + numTicket + ".xml";

            if (File.Exists(fileName))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Ticket));
                using (FileStream stream = new FileStream(fileName, FileMode.Open))
                {
                    return (Ticket)serializer.Deserialize(stream);
                }
            }
            else
            {
                throw new FileNotFoundException($"Ticket file '{fileName}' does not exist.");
            }
        }
        #endregion
    }
}
