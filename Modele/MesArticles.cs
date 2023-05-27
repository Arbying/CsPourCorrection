namespace Modele
{
    public class MesArticles
    {
        #region Variables_membre
        private int _qu;
        private Article _monArticle;
        private float _montant;
        #endregion

        #region Constructeurs
        // Constructeur par défaut
        public MesArticles()
        {
            _qu = 0;
            _monArticle = new Article();
            _montant = _qu * _monArticle.Prix;
        }

        // Constructeur avec paramètres
        public MesArticles(int qu, Article monArticle)
        {
            _qu = qu;
            _monArticle = monArticle;
            _montant = _qu * _monArticle.Prix;
        }
        #endregion

        #region Accesseurs
        public int Qu
        {
            get { return _qu; }
            set { _qu = value; }
        }

        public Article MonArticle
        {
            get { return _monArticle; }
            set { _monArticle = value; }
        }

        public float Montant
        {
            get { return _montant; }
            set { _montant = value; }
        }
        #endregion

        #region Méthodes
        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is MesArticles))
            {
                return false;
            }

            MesArticles ma = (MesArticles)obj;
            return _qu == ma._qu && _monArticle.Equals(ma._monArticle) && _montant == ma._montant;
        }

        public override string ToString()
        {
            return _qu + " x " + _monArticle.ToString() + " = " + _montant + "€";
        }

        public string PrnVersTicket()
        {
            string retour;
            if (_qu == 1)
            {
                

                retour = _monArticle.Denomination + "   " + _montant + "€";
                if (_monArticle.Points> 0)
                {
                    retour += "\n Points plus : " + _monArticle.Points;
                }
                return retour;
            }
            else
            {
                retour = "  " + _qu + " * " + _monArticle.Prix + "\n" + _monArticle.Denomination + "   " + _montant + "€";
                if (_monArticle.Points > 0)
                {
                    retour += "\n Points plus : " + (_monArticle.Points * _qu);
                }
                return retour;
            }

        }
        #endregion
    }
}
