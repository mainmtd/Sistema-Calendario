using br.com.nucleosplog.GI_Uteis.Classes;
using Microsoft.Practices.Prism.PubSubEvents;
using NL.GI.ComponentesWPF.Cliente;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace NL.GI.ComponentesWPF.Cliente
{
    public enum SearchFor
    {
        Relacionamento,
        Materiais
    }
    
    public class GISearchTextBox : TextBox
    {
        private Window window = null;

        private IEventAggregator _eventAggregator = Factory.CriaEventAggregator();

        public static DependencyProperty LabelTextProperty =
            DependencyProperty.Register(
                "LabelText",
                typeof(string),
                typeof(GISearchTextBox));

        public static DependencyProperty LabelTextColorProperty =
            DependencyProperty.Register(
                "LabelTextColor",
                typeof(Brush),
                typeof(GISearchTextBox));

        public static DependencyProperty SearchForProperty =
            DependencyProperty.Register(
                "SearchFor",
                typeof(SearchFor),
                typeof(GISearchTextBox),
                new PropertyMetadata(SearchFor.Relacionamento));

        public static DependencyProperty CodMatPropery =
            DependencyProperty.Register(
                "CodMat",
                typeof(string),
                typeof(GISearchTextBox),
                new PropertyMetadata("0"));

        public static DependencyProperty CodFavorecProperty =
            DependencyProperty.Register(
                "CodFavorec",
                typeof(string),
                typeof(GISearchTextBox));

        private static DependencyPropertyKey IsMouseLeftButtonDownPropertyKey =
            DependencyProperty.RegisterReadOnly(
                "IsMouseLeftButtonDown",
                typeof(bool),
                typeof(GISearchTextBox),
                new PropertyMetadata());
        public static DependencyProperty IsMouseLeftButtonDownProperty = IsMouseLeftButtonDownPropertyKey.DependencyProperty;

        //public static DependencyProperty SearchEventTimeDelayProperty =
        //    DependencyProperty.Register(
        //        "SearchEventTimeDelay",
        //        typeof(Duration),
        //        typeof(GISearchTextBox),
        //        new FrameworkPropertyMetadata(
        //            new Duration(new TimeSpan(0, 0, 0, 0, 500)),
        //            new PropertyChangedCallback(OnSearchEventTimeDelayChanged)));

        public static DependencyProperty CommandProperty =
            DependencyProperty.Register(
                "Command",
                typeof(ICommand),
                typeof(GISearchTextBox),
                new UIPropertyMetadata(null));

        public static readonly RoutedEvent SearchEvent =
            EventManager.RegisterRoutedEvent(
                "Search",
                RoutingStrategy.Bubble,
                typeof(RoutedEventHandler),
                typeof(GISearchTextBox));



        //static GISearchTextBox()
        //{
        //    DefaultStyleKeyProperty.OverrideMetadata(
        //        typeof(GISearchTextBox),
        //        new FrameworkPropertyMetadata(typeof(GISearchTextBox)));
        //}

        //private DispatcherTimer searchEventDelayTimer;

        //public GISearchTextBox()
        //    : base()
        //{
        //    searchEventDelayTimer = new DispatcherTimer();
        //    searchEventDelayTimer.Interval = SearchEventTimeDelay.TimeSpan;
        //    searchEventDelayTimer.Tick += new EventHandler(OnSeachEventDelayTimerTick);
        //}

        //void OnSeachEventDelayTimerTick(object o, EventArgs e)
        //{
        //    searchEventDelayTimer.Stop();
        //    RaiseSearchEvent();
        //}

        //static void OnSearchEventTimeDelayChanged(
        //    DependencyObject o, DependencyPropertyChangedEventArgs e)
        //{
        //    GISearchTextBox stb = o as GISearchTextBox;
        //    if (stb != null)
        //    {
        //        stb.searchEventDelayTimer.Interval = ((Duration)e.NewValue).TimeSpan;
        //        stb.searchEventDelayTimer.Stop();
        //    }
        //}

        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            base.OnTextChanged(e);
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

            if (SearchFor == SearchFor.Materiais)
            {
                    if (Command != null)
                        this.Command.Execute(obj);
                    
                    window = CreateNewWindow(SearchFor.Materiais);
                    window.Show();
            }

            if (SearchFor == SearchFor.Relacionamento)
            {
                    if(Command != null)
                        this.Command.Execute(obj);

                    window = CreateNewWindow(SearchFor.Relacionamento);
                    window.Show();
            }

            IsMouseLeftButtonDown = false;
        }

        private void IconBorder_MouseLeave(object obj, MouseEventArgs e)
        {
            IsMouseLeftButtonDown = false;
        }

        //VM _vm;

        protected override void OnKeyDown(KeyEventArgs e)
        {
            char key = GI_Conversores.ObjectToChar(e.Key);            

            if (char.IsLetterOrDigit(key))
            {
                if (SearchFor == SearchFor.Relacionamento)
                {
                    //_vm = new VM();
                    window = CreateNewWindow(SearchFor.Relacionamento);
                    this.Text = e.Key.ToString().ToLower();
                    _eventAggregator.GetEvent<KeyDownEvento>().Publish(e.Key.ToString());
                    window.Show();
                    _eventAggregator.GetEvent<KeyDownEvento>().Publish(e.Key.ToString());
                }
                else if (SearchFor == SearchFor.Materiais)
                {
                    window = CreateNewWindow(SearchFor.Materiais);
                    this.Text = e.Key.ToString().ToLower();
                    _eventAggregator.GetEvent<KeyDownEvento>().Publish(e.Key.ToString());
                    window.Show();
                    _eventAggregator.GetEvent<KeyDownEvento>().Publish(e.Key.ToString());
                }

               
            }

        }

        private void RaiseSearchEvent()
        {
            RoutedEventArgs args = new RoutedEventArgs(SearchEvent);
            RaiseEvent(args);

        }

        public string LabelText
        {
            get { return (string)GetValue(LabelTextProperty); }
            set { SetValue(LabelTextProperty, value); }
        }

        public Brush LabelTextColor
        {
            get { return (Brush)GetValue(LabelTextColorProperty); }
            set { SetValue(LabelTextColorProperty, value); }
        }

        [Bindable(true)]
        public SearchFor SearchFor
        {
            get { return (SearchFor)GetValue(SearchForProperty); }
            set { SetValue(SearchForProperty, value); }
        }
        public string CodMat 
        {
            get { return (string)GetValue(CodMatPropery); }
            set { SetValue(CodMatPropery, value); }
        }

        public string CodFavorec
        {
            get { return (string)GetValue(CodFavorecProperty); }
            set { SetValue(CodFavorecProperty, value); }
        }

        //public Duration SearchEventTimeDelay
        //{
        //    get { return (Duration)GetValue(SearchEventTimeDelayProperty); }
        //    set { SetValue(SearchEventTimeDelayProperty, value); }
        //}

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

            if (searchFor == SearchFor.Relacionamento)
            {
                _eventAggregator.GetEvent<DadosItemSelecionadoFav>().Subscribe(PreencheDadosItemSelecionadoFav);

                window = new Window();
                window.Height = 650;
                window.Width = 750;
                window.Content = new BuscaDeFavorecidos(SearchFor, window);
                //window.DataContext = _vm;

            }
            else if (searchFor == SearchFor.Materiais)
            {
                _eventAggregator.GetEvent<DadosItemSelecionadoMat>().Subscribe(PreencheDadosItemSelecionadoMat);

                window = new Window();
                window.Height = 650;
                window.Width = 750;
                window.Content = new BuscaDeMateriais(SearchFor, window);
            }


            return window;
        }

        private void PreencheDadosItemSelecionadoFav(ItemSelecionadoFav obj)
        {
            this.Text = obj.Nome;
            this.CodFavorec = obj.CodFavorec;
        }

        private void PreencheDadosItemSelecionadoMat(Material obj)
        {
            this.Text = obj.CodExt + " - " + obj.Descr;
            this.CodMat = obj.CodMat;
        }



    }
}
