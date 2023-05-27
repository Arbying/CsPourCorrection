using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Modele
{
    public class Client : IntervenantMagasin
    {
        private ulong _carteFidelite;
        private int _points;

        public Client() : base()
        {
            _carteFidelite = 0;
            _points = 0;
        }

        public Client(string nom, string prenom, DateTime dateNaissance, int numIntervenant, ulong carteFidelite, int points) : base(nom, prenom, dateNaissance, numIntervenant)
        {
            _carteFidelite = carteFidelite;
            _points = points;
        }

        public ulong CarteFidelite
        {
            get { return _carteFidelite; }
            set { _carteFidelite = value; }
        }

        public int Points
        {
            get { return _points; }
            set { _points = value; }
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Client))
            {
                return false;
            }

            Client c = (Client)obj;
            return base.Equals(c) && _carteFidelite == c._carteFidelite && _points == c._points;
        }

        public override string ToString()
        {
            return base.ToString() + " - Client (Carte de fidélité : " + _carteFidelite + ", Points de fidélité : " + _points + ")";
        }

        public static void Save(List<Client> clients)
        {
            string fichier = "Clients.json";
            try
            {
                string jsonData = JsonSerializer.Serialize(clients);
                File.WriteAllText(fichier, jsonData);
                Console.WriteLine("Les clients ont été sauvegardés avec succès dans le fichier {0}.", fichier);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Une erreur s'est produite lors de la sauvegarde des clients : {0}", ex.Message);
            }
        }

        public static void AddClient(Client client)
        {
            string fichier = "Clients.json";
            try
            {
                List<Client> clients = Load();
                clients.Add(client);
                Save(clients);
                Console.WriteLine("Le client a été ajouté avec succès au fichier {0}.", fichier);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Une erreur s'est produite lors de l'ajout du client : {0}", ex.Message);
            }
        }

        public static List<Client> Load()
        {
            string fichier = "Clients.json";
            try
            {
                if (File.Exists(fichier))
                {
                    string jsonData = File.ReadAllText(fichier);
                    List<Client> clients = JsonSerializer.Deserialize<List<Client>>(jsonData);
                    return clients;
                }
                else
                {
                    return new List<Client>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Une erreur s'est produite lors du chargement des clients : {0}", ex.Message);
                return new List<Client>();
            }
        }
    }
}
