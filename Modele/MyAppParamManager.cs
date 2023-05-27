using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modele
{
    public class MyAppParamManager
    {
        private const string RegistryKeyPath = "Software\\MaCaisse";
        private const string RegistryValueChemin = "CheminDossier";
        private const string RegistryValueTaille = "TaillePolice";

        public string CheminDossier { get; set; }
        public int TaillePolice { get; set; }

        public void LoadRegistryParameter()
        {
            try
            {
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(RegistryKeyPath))
                {
                    if (key != null)
                    {
                        CheminDossier = key.GetValue(RegistryValueChemin) as string;
                        TaillePolice = (int)(key.GetValue(RegistryValueTaille) ?? 0);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Une erreur s'est produite lors du chargement des paramètres : " + ex.Message);
            }
        }

        public void SaveRegistryParameter()
        {
            try
            {
                using (RegistryKey key = Registry.CurrentUser.CreateSubKey(RegistryKeyPath))
                {
                    if (key != null)
                    {
                        key.SetValue(RegistryValueChemin, CheminDossier);
                        key.SetValue(RegistryValueTaille, TaillePolice);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Une erreur s'est produite lors de la sauvegarde des paramètres : " + ex.Message);
            }
        }
    }
}
