using CheckListWPF.MVVM.Model;
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

namespace CheckListWPF.MVVM.View.UserControls
{
    /// <summary>
    /// Interaction logic for CanvasControl.xaml
    /// </summary>
    public partial class CanvasControl : UserControl
    {
        public CanvasDisplayModel Canvas
        {
            get { return (CanvasDisplayModel)GetValue(CanvasProperty); }
            set { SetValue(CanvasProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CanvasProperty =
            DependencyProperty.Register("Canvas", typeof(CanvasDisplayModel), typeof(CanvasControl), new PropertyMetadata(null, SetValues));

        private static void SetValues(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            CanvasControl canvasCtrl = d as CanvasControl;
            if (canvasCtrl != null)
            {
                canvasCtrl.DataContext = canvasCtrl.Canvas;
            }
        }

        public CanvasControl()
        {
            InitializeComponent();
        }
    }
}
