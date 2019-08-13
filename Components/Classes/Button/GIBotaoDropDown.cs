using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Telerik.Windows.Controls;

namespace NL.GI.ComponentesWPF.Cliente
{
    [TemplatePart(Name = "ContentItemsSource", Type = typeof(ItemsControl))]
    public class GIBotaoDropDown : RadDropDownButton
    {
        private ItemsControl _content;

        public GIBotaoDropDown()
        {
            this.DefaultStyleKey = typeof(GIBotaoDropDown);

        }
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            GetComponents();
        }

        private void GetComponents()
        {
            _content = GetTemplateChild("ContentItemsSource") as ItemsControl;

            if (_content != null)
            {
                if (ContentDropDown != null)
                {
                    _content.MaxHeight = 1000;
                    _content.MaxWidth = 1000;

                    ContentDropDown.MaxHeight = 1000;
                    ContentDropDown.MaxHeight = 1000;

                    _content.ItemsSource = ContentDropDown.Children;
                }
                    
            }
                
            
        }

        public static readonly DependencyProperty ContentDropDownProperty = DependencyProperty.Register(
              "ContentDropDown",
              typeof(Panel),
              typeof(GIBotaoDropDown),
              new UIPropertyMetadata(null));

        [Bindable(true)]
        public Panel ContentDropDown
        {
            get { return (Panel)GetValue(ContentDropDownProperty); }
            set { SetValue(ContentDropDownProperty, value); }
        }

    }
}
