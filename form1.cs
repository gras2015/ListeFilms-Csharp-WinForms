using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ListeFilms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: cette ligne de code charge les données dans la table 'mONCINEMA6DataSet.VUE_TITRE_RESERVATION_NOM_PRENOM_NB_PLACE'. Vous pouvez la déplacer ou la supprimer selon les besoins.
           // this.vUE_TITRE_RESERVATION_NOM_PRENOM_NB_PLACETableAdapter.Fill(this.mONCINEMA6DataSet.VUE_TITRE_RESERVATION_NOM_PRENOM_NB_PLACE);
            // TODO: cette ligne de code charge les données dans la table 'mONCINEMA6DataSet.VUE_NBPLACE_PAR_FILM'. Vous pouvez la déplacer ou la supprimer selon les besoins.
            this.vUE_NBPLACE_PAR_FILMTableAdapter.Fill(this.mONCINEMA6DataSet.VUE_NBPLACE_PAR_FILM);
            // TODO: cette ligne de code charge les données dans la table 'mONCINEMA6DataSet.VUE_TITRE_FILM_ORDRE_ALPHABETIQUE'. Vous pouvez la déplacer ou la supprimer selon les besoins.
            this.vUE_TITRE_FILM_ORDRE_ALPHABETIQUETableAdapter.Fill(this.mONCINEMA6DataSet.VUE_TITRE_FILM_ORDRE_ALPHABETIQUE);

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string value_selection = comboBox1.SelectedValue.ToString();
           
            int value_int = (int)comboBox1.SelectedValue;
            var connexion = new DONNESDataContext();

            var requetelinq = from table in connexion.VUE_NBPLACE_PAR_FILM where table.SOMME_PLACE == value_int select table;
            label3.Text = requetelinq.Count().ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StreamWriter flux_ecriture_fichier = new StreamWriter("reservation.csv");
            string value_selection = comboBox1.SelectedValue.ToString();
            
            int value = (int)comboBox1.SelectedValue;
            var connexion = new DONNESDataContext();
            var requete_linq = from table in connexion.VUE_TITRE_RESERVATION_NOM_PRENOM_NB_PLACE where table.nbPlaces == value select table;
           
            //Création et ouverture du fichier


            foreach (VUE_TITRE_RESERVATION_NOM_PRENOM_NB_PLACE text in requete_linq)
            {
                string requeteText = text.dateSeance + "; " + text.heureSeance + "; " + text.nom + "; " + text.prenom + "; " + text.nbPlaces;
                
                // Ecriture dans le fichier
                flux_ecriture_fichier.WriteLine(requeteText);
            }
            // Fermeture et sauvegarde du fichier
            flux_ecriture_fichier.Close();
        }
    }
}
