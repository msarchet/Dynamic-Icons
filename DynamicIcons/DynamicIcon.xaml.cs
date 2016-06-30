using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace DynamicIcons
{
    public sealed partial class DynamicIcon : UserControl
    {
        public UIElement DynamicContent { get { return (UIElement)GetValue(DynamicContentProperty);  } set { SetValue(DynamicContentProperty, value); } }

        public object Reactangle { get; private set; }

        public static readonly DependencyProperty DynamicContentProperty =
            DependencyProperty.Register(nameof(DynamicContent), typeof(UIElement), typeof(DynamicIcon), new PropertyMetadata(null));
        public DynamicIcon()
        {
            this.InitializeComponent();
            this.PointerPressed += DynamicIcon_PointerPressed;
        }

        private void DynamicIcon_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            var rectangle = (Rectangle)((FrameworkElement)DynamicContent).FindName("rect3344");
            rectangle.Fill = new SolidColorBrush(Colors.Red);
        }

        public async Task LoadContentFromXaml() {
            var dispatcher = Windows.UI.Core.CoreWindow.GetForCurrentThread().Dispatcher; 
            await Task.Run(async () =>
            {
                try
                {
                    var file = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///icon.xaml"));
                    var s = await Windows.Storage.FileIO.ReadTextAsync(file);
                    await dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                    {
                        DynamicContent = (UIElement)XamlReader.Load(s);
                    });
                } catch(Exception e)
                {
                    throw e;
                }
            });
        }
    }
}
