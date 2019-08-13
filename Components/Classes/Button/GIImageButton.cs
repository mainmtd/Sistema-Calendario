using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace GI_ComponentesWPF
{
    [TemplatePart(Name = "image", Type = typeof(Image))]
    public class GIImageButton : Button
    {
        #region Propriedades de Dependencia

        public static readonly DependencyProperty ImageProperty = DependencyProperty.Register(
                "Image",
                typeof(ImageSource),
                typeof(GIImageButton),
                null);

        public static readonly DependencyProperty ImageOverProperty = DependencyProperty.Register(
                "ImageOver",
                typeof(ImageSource),
                typeof(GIImageButton),
                null);

        public static readonly DependencyProperty ImagePressedProperty = DependencyProperty.Register(
                "ImagePressed",
                typeof(ImageSource),
                typeof(GIImageButton),
                null);

        public static readonly DependencyProperty TextoProperty = DependencyProperty.Register(
                "TextoProperty",
                typeof(String),
                typeof(GIImageButton),
                null);

        public static readonly DependencyProperty OpacityOverProperty = DependencyProperty.Register(
                "OpacityOver",
                typeof(Double),
                typeof(GIImageButton),
                null);

        public static readonly DependencyProperty OpacityPressedProperty = DependencyProperty.Register(
                "OpacityPressed",
                typeof(Double),
                typeof(GIImageButton),
                null);


        public static readonly DependencyProperty ZoomOverProperty = DependencyProperty.Register(
                "ZoomOverProperty",
                typeof(Double),
                typeof(GIImageButton),
                null);

        public static readonly DependencyProperty ZoomPressedProperty = DependencyProperty.Register(
                "ZoomPressedProperty",
                typeof(Double),
                typeof(GIImageButton),
                null);

        public static readonly DependencyProperty ImageWidthProperty = DependencyProperty.Register(
                "ImageWidthProperty",
                typeof(Double),
                typeof(GIImageButton),
                null);

        public static readonly DependencyProperty ImageHeightProperty = DependencyProperty.Register(
                "ImageHeightProperty",
                typeof(Double),
                typeof(GIImageButton),
                null);

        public static readonly DependencyProperty MarginTextoProperty = DependencyProperty.Register(
                "MarginTextoProperty",
                typeof(Thickness),
                typeof(GIImageButton),
                null);

        #endregion

        #region TemplateParts
        private Image Part_Imagem;

        #endregion

        public GIImageButton()
        {
            this.DefaultStyleKey = typeof(GIImageButton);
            this.OpacityOver = this.Opacity;
            this.OpacityPressed = this.Opacity;
            this.ZoomOver = 1;
            this.ZoomPressed = 1;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            Part_Imagem = GetTemplateChild("image") as Image;
        }

        public ImageSource Image
        {
            get { return (ImageSource)base.GetValue(ImageProperty); }
            set { base.SetValue(ImageProperty, value); }
        }

        public ImageSource ImageOver
        {
            get { return (ImageSource)base.GetValue(ImageOverProperty); }
            set { base.SetValue(ImageOverProperty, value); }
        }

        public ImageSource ImagePressed
        {
            get { return (ImageSource)base.GetValue(ImagePressedProperty); }
            set { base.SetValue(ImagePressedProperty, value); }
        }

        public Double OpacityOver
        {
            get { return (Double)base.GetValue(OpacityOverProperty); }
            set { base.SetValue(OpacityOverProperty, value); }

        }

        public Double OpacityPressed
        {
            get { return (Double)base.GetValue(OpacityPressedProperty); }
            set { base.SetValue(OpacityPressedProperty, value); }

        }

        public double ZoomOver
        {
            get { return (Double)base.GetValue(ZoomOverProperty); }
            set { base.SetValue(ZoomOverProperty, value); }

        }

        public double ZoomPressed
        {
            get { return (Double)base.GetValue(ZoomPressedProperty); }
            set { base.SetValue(ZoomPressedProperty, value); }

        }

        public double ImageWidth
        {
            get { return (Double)base.GetValue(ImageWidthProperty); }
            set { base.SetValue(ImageWidthProperty, value); }

        }

        public double ImageHeight
        {
            get { return (Double)base.GetValue(ImageHeightProperty); }
            set { base.SetValue(ImageHeightProperty, value); }

        }

        public String Texto
        {
            get { return (String)base.GetValue(TextoProperty); }
            set { base.SetValue(TextoProperty, value); }
        }

        public Thickness MarginTexto
        {
            get { return (Thickness)base.GetValue(MarginTextoProperty); }
            set { base.SetValue(MarginTextoProperty, value); }
        }

    }
}
