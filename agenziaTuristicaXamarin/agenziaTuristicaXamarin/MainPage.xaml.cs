using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using AS2021_TPSIT_4H_BartoliniLiam_AgenziaTuristica.Models;

namespace agenziaTuristicaXamarin
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();   
        }

        private void Picker_Focused(object sender, FocusEventArgs e)
        {
            if (!pckAzioni.Items.Contains("Aggiungi escursione"))
            {
                pckAzioni.Items.Add("Aggiungi escursione");
                pckAzioni.Items.Add("Modifica escursione");
                pckAzioni.Items.Add("Elimina escursione");
                pckAzioni.Items.Add("Aggiungi partecipante");
                pckAzioni.Items.Add("Elimina partecipante");
                pckAzioni.SelectedIndex = 0;
            }

            if (pckAzioni.SelectedIndex != -1)
                Debug.WriteLine($"Scelta {pckAzioni.Items[pckAzioni.SelectedIndex]}");
        }
    }
}