using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Modele
{
    [Serializable]
    public class Personne
    {
        #region Variables_membre
        private string _nom;
        private string _prenom;
        private DateTime _dateNaissance;
        #endregion

        #region Constructeurs
        public Personne()
        {
            _nom = "";
            _prenom = "";
            _dateNaissance = DateTime.Now;
        }
        public Personne(string nom, string prenom)
        {
            _nom = nom;
            _prenom = prenom;
        }
        public Personne(string nom, string prenom, DateTime dateNaissance)
        {
            _nom = nom;
            _prenom = prenom;
            _dateNaissance = dateNaissance;
        }
        #endregion

        #region Accesseurs
        public string Nom
        {
            get { return _nom; }
            set { _nom = value; }
        }
        public string Prenom
        {
            get { return _prenom; }
            set { _prenom = value; }
        }
        public DateTime DateNaissance
        {
            get { return _dateNaissance; }
            set { _dateNaissance = value; }
        }
        #endregion

        #region Méthodes
        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Personne))
            {
                return false;
            }
            Personne p = (Personne)obj;
            return _nom == p._nom && _prenom == p._prenom && _dateNaissance == p._dateNaissance;
        }

        public override string ToString()
        {
            return _nom + " " + _prenom + " (" + _dateNaissance.ToString("dd/MM/yyyy") + ")";
        }

        public void SaveUnXML(string fichier)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Personne));
            using (StreamWriter writer = new StreamWriter(fichier + ".xml"))
            {
                serializer.Serialize(writer, this);
            }
        }

        public static Personne LoadUnXML(string fichier)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Personne));
            using (StreamReader reader = new StreamReader(fichier + ".xml"))
            {
                return (Personne)serializer.Deserialize(reader);
            }
        }

        public static void SaveVecXML(string fichier, List<Personne> personnes)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Personne>));
            using (StreamWriter writer = new StreamWriter(fichier + ".xml"))
            {
                serializer.Serialize(writer, personnes);
            }
        }

        public static List<Personne> LoadVecXML(string fichier)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Personne>));
            using (StreamReader reader = new StreamReader(fichier + ".xml"))
            {
                return (List<Personne>)serializer.Deserialize(reader);
            }
        }

        public static void AjoutVecXML(string fichier, Personne personne)
        {
            List<Personne> personnes = LoadVecXML(fichier);
            personnes.Add(personne);
            SaveVecXML(fichier, personnes);
        }
        #endregion
    }
}