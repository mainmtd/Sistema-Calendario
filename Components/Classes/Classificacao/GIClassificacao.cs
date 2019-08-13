using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace NL.GI.ComponentesWPF.Cliente
{
    public class GIClassificacao : GIListBox
    {
        private bool tabSeted = false;
        //Thread thr = new Thread(new ThreadStart(this.AtualizaChecked));
        public GIClassificacao()
        {
            this.DefaultStyleKey = typeof(GIClassificacao);
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            PreencheList();

            
            //thr.Start();
        }

        private void PreencheList()
        {
            if (ItemsSource != null)
                ItemsSource = null;


            var itemsList = UltimoNivel.Retorna(this.TabG, CheckedItems);

            foreach (GIInputCheckbox check in itemsList)
            {
                check.Checked += check_Checked;
                check.Unchecked += check_Checked;
            }

            this.ItemsSource = itemsList;
        }   

        void check_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                var item = sender as GIInputCheckbox;
                AtualizaChecked(item);
            }
            catch
            {
                return;
            }       
            
        }       

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);

            //Verifica se a propriedade TabG foi setada
            if (e.Property == TabGProperty)
                tabSeted = true;

            if(e.Property == CheckedItemsProperty)
                if(tabSeted)
                    PreencheList();

        }

        private void AtualizaChecked(GIInputCheckbox item)
        {
                var codInt = item.Tag.ToString();
                var split = CheckedItems.Split(',');

                if (item.IsChecked == true)
                {
                    var teste = split.FirstOrDefault(p => p.Trim().Equals(codInt));
                    if (teste == null)
                        CheckedItems += ", " + codInt;

                }
                else
                {

                    var teste = split.FirstOrDefault(p => p.Trim().Equals(codInt));
                    if (teste != null)
                    {

                        CheckedItems = CheckedItems.Replace(teste.ToString(), "");
                        CheckedItems = CheckedItems.Replace(",,", ",");
                        CheckedItems = CheckedItems.Replace("  ", " ");
                        CheckedItems = CheckedItems.Replace(" ,", ",");
                        CheckedItems = CheckedItems.Trim();

                        if (CheckedItems.Trim().StartsWith(","))
                            CheckedItems = CheckedItems.Remove(CheckedItems.IndexOf(","), 1);


                        if (CheckedItems.Trim().EndsWith(","))
                            CheckedItems = CheckedItems.Remove(CheckedItems.LastIndexOf(","));
                    }
                        
                }
        }

        public static DependencyProperty CheckedItemsProperty =
                DependencyProperty.Register(
                "CheckedItems",
                typeof(string),
                typeof(GIClassificacao));


        public static DependencyProperty TabGProperty =
          DependencyProperty.Register(
              "TabG",
              typeof(Tab),
              typeof(GIClassificacao),
              new FrameworkPropertyMetadata(Tab.Servicos, FrameworkPropertyMetadataOptions.AffectsRender));

        [Bindable(true)]
        public string CheckedItems 
        {
            get { return (string)GetValue(CheckedItemsProperty); }
            set { SetValue(CheckedItemsProperty, value); }
        }

        public Tab TabG
        {
            get { return (Tab)GetValue(TabGProperty); }
            set { SetValue(TabGProperty, value); }
        }


    }
}
