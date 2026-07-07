using System.IO;
using System.Windows;

namespace FigmaJsonRenderer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += OnLoaded;
        }

        private async void OnLoaded(object sender, RoutedEventArgs e)
        {
            try
            {
                LayoutAppSettings settings = await LayoutAppSettings.LoadAsync(
                    Path.Combine(AppContext.BaseDirectory, "appsettings.json"));
                FigmaLayout layout = await LayoutDocumentLoader.LoadAsync(settings);

                FigmaLayoutRenderer renderer = new(RenderCanvas, new LayoutBindingResolver());
                renderer.Render(layout);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Figma JSON Renderer");
            }
        }
    }
}
