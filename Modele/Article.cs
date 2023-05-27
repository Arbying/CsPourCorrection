using System.Xml;
using System.Text.Json;
using System.Collections.Generic;
using System.IO;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Net;
using System.Text;
using System.Threading.Channels;
using System;

namespace Modele
{
    public class Article
    {
        #region Variables_membre
        private ulong _codeBarre;
        private string _denomination;
        private float _prix;
        private int _points;    
        private int _quantite;
        #endregion

        #region Constructeurs
        // Constructeur par défaut
        public Article()
        {
            _codeBarre = 5400000000000;
            _denomination = "";
            _prix = 0.0f;
            _points = 0;
            _quantite = 0;
        }

        // Constructeur avec paramètres
        public Article(ulong codeBarre, string denomination, float prix, int points, int quantite)
        {
            _codeBarre = codeBarre;
            _denomination = denomination;
            _prix = prix;
            _points = points;
            _quantite = quantite;
        }
        #endregion

        #region Accesseurs
        public ulong CodeBarre
        {
            get { return _codeBarre; }
            set { _codeBarre = value; }
        }

        public string Denomination
        {
            get { return _denomination; }
            set { _denomination = value; }
        }

        public float Prix
        {
            get { return _prix; }
            set { _prix = value; }
        }

        public int Points
        {
            get { return _points; }
            set { _points = value; }
        }

        public int Quantite
        {
            get { return _quantite; }
            set { _quantite = value; }
        }
        #endregion

        #region Méthodes
        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Article))
            {
                return false;
            }

            Article a = (Article)obj;
            return _codeBarre == a._codeBarre && _denomination == a._denomination && _prix == a._prix && _points == a._points && _quantite == a._quantite;
        }

        public override string ToString()
        {
            return _denomination + " - Prix : " + _prix + "€ - Points : " + _points + " - Quantité en stock : " + _quantite;
        }
        #endregion

        public void SaveArticles(List<Article> articles, string filePath)
        {
            string json = JsonSerializer.Serialize(articles, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
        }
    


        public List<Article> LoadArticles(string filePath)
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                return JsonSerializer.Deserialize<List<Article>>(json);
            }
            else
            {
                return new List<Article>();
            }
        }


        public void AppendArticle(Article article, string filePath)
        {
            List<Article> articles = new List<Article>();

            // Vérifier si le fichier existe
            if (File.Exists(filePath))
            {
                // Charger les articles existants à partir du fichier
                string json = File.ReadAllText(filePath);
                articles = JsonSerializer.Deserialize<List<Article>>(json);
            }

            // Ajouter le nouvel article à la liste
            articles.Add(article);

            // Sérialiser la liste mise à jour en JSON
            string updatedJson = JsonSerializer.Serialize(articles, new JsonSerializerOptions { WriteIndented = true });

            // Écrire le JSON dans le fichier
            File.WriteAllText(filePath, updatedJson);
        }
    }

}

