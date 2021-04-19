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
        List<string> azioniPicker = null;
        protected List<(PersonalEntry, Entry)> entryInPage = null;

        public MainPage()
        {
            InitializeComponent();
            azioniPicker = new List<string>{ "Seleziona azione", "Aggiungi escursione", "Modifica escursione", "Aggiungi partecipante", "Elimina partecipante" };
            entryInPage = new List<(PersonalEntry, Entry)>();
            pckAzioni.ItemsSource = azioniPicker;
            pckAzioni.SelectedIndex = 0;
        }

        private void Picker_Focused(object sender, FocusEventArgs e)
        {
            ControlloIndice(this, EventArgs.Empty);
        }

        private void RipristinaHome() 
        {
            StackLayout page = new StackLayout();
            Label titolo = new Label
            {
                Text = "Agenzia Turistica",
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                FontSize = 25,
                FontAttributes = FontAttributes.Bold
            };

            Picker picker = new Picker
            {
                Title = "Azioni disponibili",
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                ItemsSource = azioniPicker,
            };
            picker.Focused += Picker_Focused;
            picker.SelectedIndexChanged += myPickerSelectedIndexChanged;

            Label footer = new Label
            {
                Text = "Liam Bartolini, Lorenzo Curzi",
                FontSize = 10,
                VerticalOptions = LayoutOptions.EndAndExpand,
                HorizontalOptions = LayoutOptions.End,
                FontAttributes = FontAttributes.Italic
            };

            page.Children.Add(titolo);
            page.Children.Add(picker);
            page.Children.Add(footer);
            Content = page;
        }

        private void myPickerSelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedValue = pckAzioni.Items[pckAzioni.SelectedIndex];
            Debug.WriteLine($"VALORE SELEZIONATO: {selectedValue}");
        }

        private void ControlloIndice(object sender, EventArgs e)
        {
            // Cerco l'azione selezionata
            switch (pckAzioni.SelectedIndex)
            {
                case 0:
                    Debug.WriteLine($"INDICE {pckAzioni.SelectedIndex} - {pckAzioni.SelectedItem}");
                    break;
                case 1:
                    Debug.WriteLine($"INDICE {pckAzioni.SelectedIndex} - {pckAzioni.SelectedItem}");
                    Content = CreaContentPageNuovaEscursione();
                    pckAzioni.SelectedIndex = 0;
                    break;
                case 2:
                    Debug.WriteLine($"INDICE {pckAzioni.SelectedIndex} - {pckAzioni.SelectedItem}");
                    pckAzioni.SelectedIndex = 0;
                    break;
                case 3:
                    Debug.WriteLine($"INDICE {pckAzioni.SelectedIndex} - {pckAzioni.SelectedItem}");
                    pckAzioni.SelectedIndex = 0;
                    break;
                case 4:
                    Debug.WriteLine($"INDICE {pckAzioni.SelectedIndex} - {pckAzioni.SelectedItem}");
                    pckAzioni.SelectedIndex = 0;
                    break;
            }
        }

        private StackLayout CreaContentPageNuovaEscursione()
        {
            StackLayout stack = new StackLayout();
            
            Entry numeroEscursione = new Entry { Placeholder = "Inserisci il numero dell'escursione..." };
            PersonalEntry numEsc = new PersonalEntry { Name = "txtNumeroEscursione" };
            entryInPage.Add((numEsc, numeroEscursione));
            stack.Children.Add(numeroEscursione);

            Entry prezzoBase = new Entry { Placeholder = "Inserisci il prezzoBase dell'escursione..." };
            PersonalEntry prezzo = new PersonalEntry { Name = "txtPrezzo" };
            entryInPage.Add((prezzo, prezzoBase));
            stack.Children.Add(prezzoBase);

            Entry dataEscursione = new Entry { Placeholder = "Inserisci la data (mese/giorno/anno) dell'escursione..." };
            PersonalEntry data = new PersonalEntry { Name = "txtDataEscursione" };
            entryInPage.Add((data, dataEscursione));
            stack.Children.Add(dataEscursione);

            Entry tipoEscursione = new Entry { Placeholder = "Inserisci il tipo di escursione..." };
            PersonalEntry tipo = new PersonalEntry { Name = "txtTipo" };
            entryInPage.Add((tipo, tipoEscursione));
            stack.Children.Add(tipoEscursione);

            Entry descrizioneEscursione = new Entry { Placeholder = "Inserisci la descrizione dell'escursione..." };
            PersonalEntry descrizione = new PersonalEntry { Name = "txtDescrizione" };
            entryInPage.Add((descrizione, descrizioneEscursione));
            stack.Children.Add(descrizioneEscursione);

            Entry optionalEscursione = new Entry { Placeholder = "Inserisci gli optional dell'escursione..." };
            PersonalEntry optional = new PersonalEntry { Name = "txtOptional" };
            entryInPage.Add((optional, optionalEscursione)); stack.Children.Add(optionalEscursione);
            stack.Children.Add(optionalEscursione);

            // Creo uno stack layout orizzontale per metterci i due bottoni in fila
            StackLayout stack1 = new StackLayout();
            stack1.Orientation = StackOrientation.Horizontal;
            Button btn = new Button { Text = "Invia dati", HorizontalOptions = LayoutOptions.CenterAndExpand};
            btn.Clicked += Btn_Clicked;
            stack1.Children.Add(btn);

            Button btnRet = new Button { Text = "Torna alla pagina iniziale", HorizontalOptions = LayoutOptions.CenterAndExpand };
            btnRet.Clicked += Btn_Clicked;
            stack1.Children.Add(btnRet);

            stack.Children.Add(stack1);
            return stack;
        }

        private void Btn_Clicked(object sender, EventArgs e)
        {
            if ((sender as Button).Text != "Torna alla pagina iniziale")
            {
                bool flag = true;
                foreach (var ntr in entryInPage)
                {
                    if (string.IsNullOrEmpty(ntr.Item2.Text))
                    {
                        flag = false;
                        DisplayAlert("Errore Input", "Compilare tutti i campi!", "Chiudi");
                        ResetPage();
                        break;
                    }
                }

                if (flag)
                {
                    Agenzia.NuovaEscursione(
                        Convert.ToInt32(entryInPage[0].Item2.Text),
                        Convert.ToDouble(entryInPage[1].Item2.Text),
                        Convert.ToDateTime(entryInPage[2].Item2.Text),
                        entryInPage[3].Item2.Text,
                        entryInPage[4].Item2.Text,
                        entryInPage[5].Item2.Text );

                    Debug.WriteLine(Agenzia.VisualizzaEscursioni());
                    RipristinaHome();
                }
            }
            else
                RipristinaHome();
        }

        private void ResetPage()
        {
            foreach (var pe in entryInPage)
                pe.Item2.Text = string.Empty;
        }
    }

    public class PersonalEntry : MainPage
    {
        public string Name { get; set; }
    }
}