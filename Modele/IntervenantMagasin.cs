using System.Xml.Serialization;

namespace Modele
{
    [Serializable]
    public abstract class IntervenantMagasin : Personne
    {
        #region Variables_membre
        private int _numIntervenant;
        #endregion

        #region Constructeurs
        // Constructeur par défaut
        public IntervenantMagasin() : base()
        {
            _numIntervenant = 0;
        }

        // Constructeur avec paramètres
        public IntervenantMagasin(string nom, string prenom, DateTime dateNaissance, int numIntervenant) : base(nom, prenom, dateNaissance)
        {
            _numIntervenant = numIntervenant;
        }
        #endregion

        #region Accesseurs
        public int NumIntervenant
        {
            get { return _numIntervenant; }
            set { _numIntervenant = value; }
        }
        #endregion

        #region Méthodes
        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is IntervenantMagasin))
            {
                return false;
            }

            IntervenantMagasin i = (IntervenantMagasin)obj;
            return base.Equals(i) && _numIntervenant == i._numIntervenant;
        }

        public override string ToString()
        {
            return base.ToString() + " - Intervenant n° " + _numIntervenant;
        }

        public void SaveUnXML(string fichier)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(IntervenantMagasin));
            using (StreamWriter writer = new StreamWriter(fichier + ".xml"))
            {
                serializer.Serialize(writer, this);
            }
        }

        public static IntervenantMagasin LoadUnXML(string fichier)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(IntervenantMagasin));
            using (StreamReader reader = new StreamReader(fichier + ".xml"))
            {
                return (IntervenantMagasin)serializer.Deserialize(reader);
            }
        }

        public static void SaveVecXML(string fichier, List<IntervenantMagasin> intervenants)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<IntervenantMagasin>));
            using (StreamWriter writer = new StreamWriter(fichier + ".xml"))
            {
                serializer.Serialize(writer, intervenants);
            }
        }

        public static List<IntervenantMagasin> LoadVecXML(string fichier)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<IntervenantMagasin>));
            using (StreamReader reader = new StreamReader(fichier + ".xml"))
            {
                return (List<IntervenantMagasin>)serializer.Deserialize(reader);
            }
        }

        public static void AjoutVecXML(string fichier, IntervenantMagasin intervenant)
        {
            List<IntervenantMagasin> intervenants = LoadVecXML(fichier);
            intervenants.Add(intervenant);
            SaveVecXML(fichier, intervenants);
        }
        #endregion
    }
}
