using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Telerik.Windows.Controls.Input;

namespace NL.GI.ComponentesWPF.Cliente
{
    public class GIComboBox : Telerik.Windows.Controls.RadComboBox
    {
        public GIComboBox()
        {
            this.DefaultStyleKey = typeof(GIComboBox);
        }

        public static readonly DependencyProperty OpenDropDownOnKeyDownProperty = DependencyProperty.Register(
               "OpenDropDownOnKeyDown",
               typeof(bool),
               typeof(GIComboBox),
               new UIPropertyMetadata(true));

        public bool OpenDropDownOnKeyDown
        {
            get { return (bool)GetValue(OpenDropDownOnKeyDownProperty); }
            set { SetValue(OpenDropDownOnKeyDownProperty, value); }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (OpenDropDownOnKeyDown)
            {
                OpenDropDownOnFocus = false;

                if (!this.IsDropDownOpen)
                    this.IsDropDownOpen = true;
            }

        }
    }

}
