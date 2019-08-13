using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NL.GI.ComponentesWPF.Cliente
{
    /// <summary>
    /// Interaction logic for UCGIBalloonHint.xaml
    /// </summary>
    public partial class UCGIBalloonHint : Window
    {
        public enum Position
        {
            Left, Right
        }

        public UCGIBalloonHint(FrameworkElement control, UserControl caption, Position position)
        {
            InitializeComponent();

            this.mainContent.Content = caption;

            // Compensate for the bubble point
            double captionPointMargin = this.PathPointLeft.Margin.Left;

            Point location = GetControlPosition(control);

            if (position == Position.Left)
            {
                this.PathPointRight.Visibility = Visibility.Hidden;
                this.Left = location.X + (control.ActualWidth / 2) - captionPointMargin;
            }
            else
            {
                this.PathPointLeft.Visibility = Visibility.Hidden;
                this.Left = location.X - this.Width + control.ActualWidth + (captionPointMargin / 2);
            }

            this.Top = location.Y + (control.ActualHeight / 2);
        }

        
        private static Point GetControlPosition(FrameworkElement control)
        {
            Point locationToScreen = control.PointToScreen(new Point(0, 0));
            var source = PresentationSource.FromVisual(control);
            return source.CompositionTarget.TransformFromDevice.Transform(locationToScreen);
        }

        private void DoubleAnimationCompleted(object sender, EventArgs e)
        {
            if (!this.IsMouseOver)
            {
                this.Close();
            }
        }
    }
}
