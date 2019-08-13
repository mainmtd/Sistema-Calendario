using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace NL.GI.ComponentesWPF.Cliente
{
    public class GIModuleItemsButton : Telerik.Windows.Controls.RadButton
    {
        public GIModuleItemsButton()
        {
            this.DefaultStyleKey = typeof(GIModuleItemsButton);
        }

        public static readonly DependencyProperty ImageProperty = DependencyProperty.Register(
                "Image",
                typeof(ImageSource),
                typeof(GIModuleItemsButton),
                new UIPropertyMetadata(null));

        public ImageSource Image
        {
            get { return (ImageSource)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }

        public static readonly DependencyProperty DescriptionProperty = DependencyProperty.Register(
                "Description",
                typeof(string),
                typeof(GIModuleItemsButton),
                new UIPropertyMetadata(null));

        public string Description
        {
            get { return (string)GetValue(DescriptionProperty); }
            set { SetValue(DescriptionProperty, value); }
        }

        public static readonly DependencyProperty IdProperty = DependencyProperty.Register(
                "Id",
                typeof(string),
                typeof(GIModuleItemsButton),
                new UIPropertyMetadata(null));

        public string Id
        {
            get { return (string)GetValue(IdProperty); }
            set { SetValue(IdProperty, value); }
        }

    }
}
