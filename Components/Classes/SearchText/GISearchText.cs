using br.com.nucleosplog.GI_Uteis.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace NL.GI.ComponentesWPF.Cliente
{

    public enum SearchFor
    {
        Relacionamento,
        Materiais
    }

    public class GISearchText : TextBox
    {
        private Window window = null;

        private SearchFavored _telaFavored;

        public static DependencyProperty SearchForProperty =
            DependencyProperty.Register(
                "SearchFor",
                typeof(SearchFor),
                typeof(GISearchText),
                new PropertyMetadata(SearchFor.Relacionamento));

        public static DependencyProperty CodMatProperty =
            DependencyProperty.Register(
                "CodMat",
                typeof(string),
                typeof(GISearchText),
                new PropertyMetadata("0"));

        public static DependencyProperty CodFavorecProperty =
            DependencyProperty.Register(
                "CodFavorec",
                typeof(string),
                typeof(GISearchText));

        public static DependencyProperty TypeSearchProperty =
            DependencyProperty.Register(
            "TypeSearch",
            typeof(object),
            typeof(GISearchText));

        private static DependencyPropertyKey IsMouseLeftButtonDownPropertyKey =
            DependencyProperty.RegisterReadOnly(
                "IsMouseLeftButtonDown",
                typeof(bool),
                typeof(GISearchText),
                new PropertyMetadata());
        public static DependencyProperty IsMouseLeftButtonDownProperty = IsMouseLeftButtonDownPropertyKey.DependencyProperty;

        public static DependencyProperty CommandProperty =
            DependencyProperty.Register(
                "Command",
                typeof(ICommand),
                typeof(GISearchText),
                new UIPropertyMetadata(null));

        public static readonly RoutedEvent SearchEvent =
            EventManager.RegisterRoutedEvent(
                "Search",
                RoutingStrategy.Bubble,
                typeof(RoutedEventHandler),
                typeof(GISearchText));

        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            string x = this.Text;
            base.OnTextChanged(e);
            this.Text = x;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            Border iconBorder = GetTemplateChild("PART_SearchIconBorder") as Border;
            if (iconBorder != null)
            {
                iconBorder.MouseLeftButtonDown += new MouseButtonEventHandler(IconBorder_MouseLeftButtonDown);
                iconBorder.MouseLeftButtonUp += new MouseButtonEventHandler(IconBorder_MouseLeftButtonUp);
                iconBorder.MouseLeave += new MouseEventHandler(IconBorder_MouseLeave);
            }
        }

        private void IconBorder_MouseLeftButtonDown(object obj, MouseButtonEventArgs e)
        {
            IsMouseLeftButtonDown = true;
        }

        private void IconBorder_MouseLeftButtonUp(object obj, MouseButtonEventArgs e)
        {
            if (!IsMouseLeftButtonDown) return;

            window = null;

            if (Command != null)
                this.Command.Execute(obj);

            window = CreateNewWindow(SearchFor);
            window.ShowDialog();

            if (ChecksRelationship() && _telaFavored.GetVM().fav != null)
                SetTypeFavored();

            else if (ChecksMaterials() && _telaFavored.GetVM().mat != null)
                SetTypeMaterial();

            IsMouseLeftButtonDown = false;

        }

        private void IconBorder_MouseLeave(object obj, MouseEventArgs e)
        {
            IsMouseLeftButtonDown = false;
        }


        protected override void OnKeyDown(KeyEventArgs e)
        {
            var strKey = new KeyConverter().ConvertToString(e.Key);

            char key = GI_Conversores.ObjectToChar(strKey);

            if (char.IsLetterOrDigit(key))
            {
                this.Text = strKey.ToString().ToLower();

                if (ChecksRelationship())
                    window = CreateNewWindow(SearchFor.Relacionamento);

                else if (ChecksMaterials())
                    window = CreateNewWindow(SearchFor.Materiais);

                _telaFavored.GetVM().RecebePressedKey(Text);
                
                window.ShowDialog();

                if (ChecksRelationship() && _telaFavored.GetVM().fav != null)
                    SetTypeFavored();

                else if (ChecksMaterials() && _telaFavored.GetVM().mat != null)
                    SetTypeMaterial();


                e.Handled = true;
            }  
          
        }

        [Bindable(true)]
        public SearchFor SearchFor
        {
            get { return (SearchFor)GetValue(SearchForProperty); }
            set { SetValue(SearchForProperty, value); }
        }
        public string CodMat
        {
            get { return (string)GetValue(CodMatProperty); }
            set { SetValue(CodMatProperty, value); }
        }

        public string CodFavorec
        {
            get { return (string)GetValue(CodFavorecProperty); }
            set { SetValue(CodFavorecProperty, value); }
        }

        [Bindable(true)]
        public object TypeSearch
        {
            get { return (object)GetValue(TypeSearchProperty); }
            set { SetValue(TypeSearchProperty, value); }
        }

        public bool IsMouseLeftButtonDown
        {
            get { return (bool)GetValue(IsMouseLeftButtonDownProperty); }
            private set { SetValue(IsMouseLeftButtonDownPropertyKey, value); }
        }

        public event RoutedEventHandler Search
        {
            add { AddHandler(SearchEvent, value); }
            remove { RemoveHandler(SearchEvent, value); }
        }

        [Bindable(true)]
        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        private Window CreateNewWindow(SearchFor searchFor)
        {
            window = null;
            window = new Window();
            window.Height = 650;
            window.Width = 750;
            
            _telaFavored = new SearchFavored(SearchFor);
            window.Content = _telaFavored;
           
           _telaFavored.GetVM().RecebeWindow(window);

            return window;
        }

        private bool ChecksRelationship()
        {
            bool _flag = false;

            if (SearchFor == SearchFor.Relacionamento)
            {
                _flag = true;
            }
            return _flag;
        }

        private bool ChecksMaterials()
        {
            bool _flag = false;

            if (SearchFor == SearchFor.Materiais)
            {
                _flag = true;
            }
            return _flag;
        }

        private void SetTypeFavored()
        {
            this.Text = _telaFavored.GetVM().fav.Nome;
            TypeSearch = _telaFavored.GetVM().fav;
        }

        private void SetTypeMaterial()
        {
            this.Text = _telaFavored.GetVM().mat.Descr;
            TypeSearch = _telaFavored.GetVM().mat;
        }

    }
}
