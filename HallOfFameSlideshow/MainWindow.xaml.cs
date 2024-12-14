using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;
using HallOfFameSlideshow.Properties;
using System.ComponentModel;

namespace HallOfFameSlideshow
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {

        // Event for PropertyChanged notification
        public event PropertyChangedEventHandler PropertyChanged;

        private static readonly HttpClient HttpClient = new HttpClient();
        public List<ApiImageResponse> ImageHistory = new List<ApiImageResponse>();
        public int CurrentImageHistoryIndex = 0;
        private CancellationTokenSource _cancellationTokenSource;
        private bool _isPlaying = false;
        private bool _isMaximized = false;

        // Property to track the current state (Play or Pause)
        public bool IsPlaying
        {
            get { return _isPlaying; }
            set
            {
                if (_isPlaying != value)
                {
                    _isPlaying = value;
                    OnPropertyChanged(nameof(IsPlaying));  // Notify when property changes
                    OnPropertyChanged(nameof(PlayPauseIcon)); // Notify that PlayPauseIcon property should be updated
                }
            }
        }

        public string PlayPauseIcon
        {
            get
            {
                return _isPlaying ? "PauseCircle" : "PlayCircle"; // Return "Pause" if playing, otherwise "Play"
            }
        }

        public bool IsMaximized
        {
            get { return _isMaximized; }
            set
            {
                if (_isMaximized != value)
                {
                    _isMaximized = value;
                    OnPropertyChanged(nameof(IsPlaying));  // Notify when property changes
                    OnPropertyChanged(nameof(MaxMinIcon)); // Notify that PlayPauseIcon property should be updated
                }
            }
        }

        public string MaxMinIcon
        {
            get
            {
                return _isMaximized ? "WindowRestore" : "WindowMaximize"; // Return "Pause" if playing, otherwise "Play"
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            if (ImageHistory.Count == 0)
            {
                btnPrevious.IsEnabled = false;
            }
            else
            {
                btnPrevious.IsEnabled = true;            
            }

            _ = LoadImageAsync();
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private async Task LoadImageAsync()
        {

            // Show the loader
            Dispatcher.Invoke(() => Loader.Visibility = Visibility.Visible);

            try
            {
                // Replace with your API endpoint URL
                string apiUrl = Properties.Settings.Default.GetRandomWeightedImageApiEndpoint;

                // Add any properties required for the request
                var requestData = new
                {
                    random = Properties.Settings.Default.RandomValue,
                    trending = Properties.Settings.Default.TrendingValue,
                    recent = Properties.Settings.Default.RecentValue,
                    archeologist = Properties.Settings.Default.ArcheologistValue,
                    supporter = Properties.Settings.Default.SupporterValue,
                    viewMaxAge = Properties.Settings.Default.ViewMaxAgeValue
                };


                // Fetch the image URL from the API
                ApiImageResponse image = await GetImageFromApi(apiUrl, requestData);

                ImageHistory.Add(image);

                CurrentImageHistoryIndex = ImageHistory.Count - 1;

                // Load and display the image
                if (!string.IsNullOrEmpty(image.ImageUrlFHD))
                {
                    BitmapImage bitmap = await LoadImageFromUrl(image.ImageUrlFHD);

                    // Subscribe to the DownloadCompleted event
                    bitmap.DownloadCompleted += Bitmap_DownloadCompleted;

                    SlideshowImage.Source = bitmap;

                    CityName.Text = image.CityName;
                    CityCreator.Text = image.Creator.CreatorName;
                    ImageViews.Text = image.ViewsCount.ToString();
                    ImageLikes.Text = image.FavoritesCount.ToString();
                    ImageCreatedOn.Text = image.CreatedAt.ToString("yyyy-MM-dd");

                    if (CurrentImageHistoryIndex == 0)
                    {
                        btnPrevious.IsEnabled = false;
                    }
                    else
                    {
                        btnPrevious.IsEnabled = true;
                    }

                }
                else
                {
                    MessageBox.Show("No image info was received from the API.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // This is the event handler that will be called when the image has fully loaded
        private void Bitmap_DownloadCompleted(object sender, EventArgs e)
        {
            // Hide the loader once the image is loaded
            Dispatcher.Invoke(() => Loader.Visibility = Visibility.Collapsed);
        }

        private async Task<ApiImageResponse> GetImageFromApi(string apiUrl, object requestData)
        {
            try
            {
                // Create a GET request
                using (var request = new HttpRequestMessage(HttpMethod.Get, apiUrl))
                {
                    // Add Authorization header
                    request.Headers.Add("Authorization", "Creator name=" + Properties.Settings.Default.CreatorName + "&id=" + Properties.Settings.Default.CreatorId + "&provider=" + Properties.Settings.Default.Provider + "&hwid=" + Properties.Settings.Default.HWID);

                    // Send the request using the static HttpClient
                    HttpResponseMessage response = await HttpClient.SendAsync(request);
                    response.EnsureSuccessStatusCode();

                    // Parse the response JSON into ApiResponse object
                    string jsonResponse = await response.Content.ReadAsStringAsync();

                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };

                    ApiImageResponse result = JsonSerializer.Deserialize<ApiImageResponse>(jsonResponse, options);

                    // Return the image URL (e.g., FHD resolution)
                    return result;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }

        private async Task<BitmapImage> LoadImageFromUrl(string imageUrl)
        {
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(imageUrl);
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.EndInit();
            return bitmap;
        }

        private async void btnNext_ClickAsync(object sender, RoutedEventArgs e)
        {
            if(CurrentImageHistoryIndex == ImageHistory.Count - 1)
            {
                await LoadImageAsync();
            }
            else
            {
                // Show the loader
                Dispatcher.Invoke(() => Loader.Visibility = Visibility.Visible);

                ApiImageResponse image = ImageHistory[CurrentImageHistoryIndex + 1];
                BitmapImage bitmap = await LoadImageFromUrl(image.ImageUrlFHD);

                // Subscribe to the DownloadCompleted event
                bitmap.DownloadCompleted += Bitmap_DownloadCompleted;

                SlideshowImage.Source = bitmap;
                CityName.Text = image.CityName;
                CityCreator.Text = image.Creator.CreatorName;
                ImageViews.Text = image.ViewsCount.ToString();
                ImageLikes.Text = image.FavoritesCount.ToString();

                CurrentImageHistoryIndex = CurrentImageHistoryIndex + 1;
            }

            
        }
        private async void btnPrevious_ClickAsync(object sender, RoutedEventArgs e)
        {
            // Show the loader
            Dispatcher.Invoke(() => Loader.Visibility = Visibility.Visible);

            try
            {
                ApiImageResponse image = ImageHistory[CurrentImageHistoryIndex - 1];

                if (!string.IsNullOrEmpty(image.ImageUrlFHD))
                {
                    BitmapImage bitmap = await LoadImageFromUrl(image.ImageUrlFHD);

                    // Subscribe to the DownloadCompleted event
                    bitmap.DownloadCompleted += Bitmap_DownloadCompleted;

                    SlideshowImage.Source = bitmap;
                    CityName.Text = image.CityName;
                    CityCreator.Text = image.Creator.CreatorName;
                    ImageViews.Text = image.ViewsCount.ToString();
                    ImageLikes.Text = image.FavoritesCount.ToString();

                    CurrentImageHistoryIndex = CurrentImageHistoryIndex - 1;

                    if (CurrentImageHistoryIndex == 0)
                    {
                        btnPrevious.IsEnabled = false;
                    }
                    else
                    {
                        btnPrevious.IsEnabled = true;
                    }

                }
                else
                {
                    MessageBox.Show("No image info was received from the API.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }            

        }

        private async void BtnPlay_Click(object sender, RoutedEventArgs e)
        {
            if (_isPlaying)
            {
                // Pause the image loading by cancelling the task
                _cancellationTokenSource.Cancel();
                //BtnPlay.Content = "Play"; // Change button text to Play
                IsPlaying = false;
                this.InvalidateVisual();
            }
            else
            {
                // Start the image loading task
                _cancellationTokenSource = new CancellationTokenSource();
                //BtnPlay.Content = "Pause"; // Change button text to Pause
                IsPlaying = true;
                this.InvalidateVisual();
                await LoadImagesPeriodically(_cancellationTokenSource.Token);
            }

        }

        private async Task LoadImagesPeriodically(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    if (CurrentImageHistoryIndex == ImageHistory.Count - 1)
                    {
                        await LoadImageAsync();
                    }
                    else
                    {
                        // Show the loader
                        Dispatcher.Invoke(() => Loader.Visibility = Visibility.Visible);

                        ApiImageResponse image = ImageHistory[CurrentImageHistoryIndex + 1];
                        BitmapImage bitmap = await LoadImageFromUrl(image.ImageUrlFHD);

                        // Subscribe to the DownloadCompleted event
                        bitmap.DownloadCompleted += Bitmap_DownloadCompleted;

                        SlideshowImage.Source = bitmap;
                        CityName.Text = image.CityName;
                        CityCreator.Text = image.Creator.CreatorName;
                        ImageViews.Text = image.ViewsCount.ToString();
                        ImageLikes.Text = image.FavoritesCount.ToString();

                        CurrentImageHistoryIndex = CurrentImageHistoryIndex + 1;
                    }

                    // Wait for 10 seconds before loading the next image
                    await Task.Delay(Properties.Settings.Default.SlideShowInterval * 1000, cancellationToken); // delay based on settings property
                }
                catch (OperationCanceledException)
                {
                    // This exception is expected when cancellation is requested
                    break;
                }
            }
        }

        private void btnToggleMaximize_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
                IsMaximized = false;
                //btnToggleMaximize.Content = "⛶";  // Optional: Change button content for 'restore' state
            }
            else
            {
                this.WindowState = WindowState.Maximized;
                IsMaximized = true;
                //btnToggleMaximize.Content = "↕";  // Optional: Change button content for 'maximize' state
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void TopBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // Start dragging when the left mouse button is pressed
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

    }
}