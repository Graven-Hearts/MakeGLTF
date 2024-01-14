using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Threading;
using Graven.Hearts.MakeGLTF.Constants;
using Graven.Hearts.MakeGLTF.Extensions;
using Graven.Hearts.MakeGLTF.ViewModels;
using Graven.Hearts.MakeGLTF.Views.Layouts;
using Microsoft.VisualBasic;

namespace Graven.Hearts.MakeGLTF.Views
{
    public partial class MainWindow : Window
    {
        private static GridLayout MainGridLayoutHorizontal => new
            (new ("Auto, Auto, Auto, Auto, Auto, Auto, Auto, Auto, Auto"),
            new ("Auto"),
            new Dictionary<string, GridPosition>
            {
                { "BaseColourImagePanel", new (0, 0) },
                { "NormalImagePanel", new (1, 0) },
                { "AlphaImagePanel", new (2, 0) },
                { "OcclusionImagePanel", new (3, 0) },
                { "RoughnessImagePanel", new (4, 0) },
                { "MetallicImagePanel", new (5, 0) },
                { "EmissionImagePanel", new (6, 0) },
                { "SavePanel", new (7, 0) },
                { "AboutPanel", new (8, 0) }
            });

        private static GridLayout MainGridLayoutVertical => new
            (new ("Auto"),
            new ("Auto, Auto, Auto, Auto, Auto, Auto, Auto, Auto, Auto"),
            new Dictionary<string, GridPosition>
            {
                { "BaseColourImagePanel", new (0, 0) },
                { "NormalImagePanel", new (0, 1) },
                { "AlphaImagePanel", new (0, 2) },
                { "OcclusionImagePanel", new (0, 3) },
                { "RoughnessImagePanel", new (0, 4) },
                { "MetallicImagePanel", new (0, 5) },
                { "EmissionImagePanel", new (0, 6) },
                { "SavePanel", new (0, 7) },
                { "AboutPanel", new (0, 8) }
            });

        private static GridLayout MainGridLayoutGrid => new
            (new ("Auto, Auto, Auto"),
            new ("Auto, Auto, Auto"),
            new Dictionary<string, GridPosition>
            {
                { "BaseColourImagePanel", new (0, 0) },
                { "NormalImagePanel", new (0, 1) },
                { "AlphaImagePanel", new (0, 2) },
                { "OcclusionImagePanel", new (1, 0) },
                { "RoughnessImagePanel", new (1, 1) },
                { "MetallicImagePanel", new (1, 2) },
                { "EmissionImagePanel", new (2, 0) },
                { "SavePanel", new (2, 1) },
                { "AboutPanel", new (2, 2) }
            });

        private MainWindowViewModel ViewModel { get; set; } = null!;

        private GridLayout MainGridCurrentLayout { get; set; } = MainGridLayoutGrid;

        private bool StayOnTop { get; set; } = true;

        public MainWindow()
        {
            InitializeComponent();

            CanResize = false;

            DataContextChanged += OnLoaded;
            ScalingChanged += OnScalingChanged;
            Deactivated += OnDeactivated;
        }

        public void OnLoaded(object? sender, EventArgs args)
        {
            if (DataContext is not MainWindowViewModel mainWindowViewModel)
                return;

            UpdateLayout(mainWindowViewModel);

            ViewModel = mainWindowViewModel;
        }

        public void OnScalingChanged(object? sender, EventArgs args)
        {
            UpdateLayout(ViewModel);
        }

        public void OnDeactivated(object? sender, EventArgs args)
        {
            if (sender is Window window)
                window.Topmost = StayOnTop;
        }

        private async void OnBaseColourImageTapped(object sender, TappedEventArgs args)
        {
            if (ViewModel is null)
                return;

            if (ViewModel.BaseColourImage is not null)
            {
                ViewModel.OnBaseColourImageChanged(string.Empty, null);
                return;
            }

            var storageFile = await this.ShowOpenFileDialog();
            if (storageFile is null)
                return;

            var fullLocalPath = storageFile.Path.LocalPath;
            if (fullLocalPath is null)
                return;

            var localPath = Path.GetFileNameWithoutExtension(fullLocalPath);
            var materialName = localPath.Replace(FileExtConsts.Albedo, "").
                Replace(FileExtConsts.Base, "").
                Replace(FileExtConsts.BaseColor, "").
                Replace(FileExtConsts.Color, "").
                Replace(FileExtConsts.Col, "").
                Replace(FileExtConsts.Diffuse, "").
                Replace(FileExtConsts.Diff, "").
                Replace(FileExtConsts.Alb, "");

            var folderName = Path.GetDirectoryName(fullLocalPath);
            if (folderName.IsNullOrEmpty())
                return;

            ViewModel.AutoFillFromColor(materialName, folderName, Path.GetExtension(fullLocalPath));

            var image = await storageFile.ReadImage();
            if (image is not null)
                ViewModel.OnBaseColourImageChanged(fullLocalPath, image);
        }

        private async void OnNormalImageTapped(object sender, TappedEventArgs args)
        {
            if (ViewModel is null)
                return;

            if (ViewModel.NormalImage is not null)
            {
                ViewModel.OnNormalImageChanged(string.Empty, null);
                return;
            }

            var image = await this.GetImage();
            if (image.Bitmap is not null)
                ViewModel.OnNormalImageChanged(image.File.Path.LocalPath, image.Bitmap);
        }

        private async void OnAlphaImageTapped(object sender, TappedEventArgs args)
        {
            if (ViewModel is null)
                return;

            if (ViewModel.AlphaImage is not null)
            {
                ViewModel.OnAlphaImageChanged(string.Empty, null);
                return;
            }

            var image = await this.GetImage();
            if (image.Bitmap is not null)
                ViewModel.OnAlphaImageChanged(image.File.Path.LocalPath, image.Bitmap);
        }

        private async void OnOcclusionImageTapped(object sender, TappedEventArgs args)
        {
            if (ViewModel is null)
                return;

            if (ViewModel.OcclusionImage is not null)
            {
                ViewModel.OnOcclusionImageChanged(string.Empty, null);
                return;
            }

            var image = await this.GetImage();
            if (image.Bitmap is not null)
                ViewModel.OnOcclusionImageChanged(image.File.Path.LocalPath, image.Bitmap);
        }

        private async void OnRoughnessImageTapped(object sender, TappedEventArgs args)
        {
            if (ViewModel is null)
                return;

            if (ViewModel.RoughnessImage is not null)
            {
                ViewModel.OnRoughnessImageChanged(string.Empty, null);
                return;
            }

            var image = await this.GetImage();
            if (image.Bitmap is not null)
                ViewModel.OnRoughnessImageChanged(image.File.Path.LocalPath, image.Bitmap);
        }

        private async void OnMetallicImageTapped(object sender, TappedEventArgs args)
        {
            if (ViewModel is null)
                return;

            if (ViewModel.MetallicImage is not null)
            {
                ViewModel.OnMetallicImageChanged(string.Empty, null);
                return;
            }

            var image = await this.GetImage();
            if (image.Bitmap is not null)
                ViewModel.OnMetallicImageChanged(image.File.Path.LocalPath, image.Bitmap);
        }

        private async void OnEmissionImageTapped(object sender, TappedEventArgs args)
        {
            if (ViewModel is null)
                return;

            if (ViewModel.EmissionImage is not null)
            {
                ViewModel.OnEmissionImageChanged(string.Empty, null);
                return;
            }

            var image = await this.GetImage();
            if (image.Bitmap is not null)
                ViewModel.OnEmissionImageChanged(image.File.Path.LocalPath, image.Bitmap);
        }

        private void OnSaveTapped(object sender, TappedEventArgs args)
        {
            var viewModel = ViewModel;
            if (viewModel is null)
                return;

            var originalCursor = Cursor;
            Cursor = new Cursor(StandardCursorType.Wait);

            var elementStates = new List<(InputElement Element, bool IsEnabled)>();

            foreach (var element in VisualChildren.
                Where(child => child is InputElement).
                Select(element => (InputElement)element))
            {
                elementStates.Add((element, element.IsEnabled));
                element.IsEnabled = false;
            }

            var originalValues = viewModel.PrepareUIForSave();

            Task.Run(() =>
            {
                viewModel.OnSave();

                Dispatcher.UIThread.Post(() =>
                {
                    elementStates.ForEach((elementState) => elementState.Element.IsEnabled = elementState.IsEnabled);

                    viewModel.RestoreUIAfterSave(originalValues);

                    Cursor = originalCursor;
                });
            });
        }

        private void OnClearTapped(object sender, TappedEventArgs args)
        {
            ViewModel?.OnClear();
        }

        private void OnInfoTapped(object sender, TappedEventArgs args)
        {
            System.Diagnostics.Process.Start("xdg-open", App.AppURL);
        }

        private void OnSetGridLayout(object sender, TappedEventArgs args)
        {
            SetLayout(MainGridLayoutGrid);
        }

        private void OnSetHorizontalLayout(object sender, TappedEventArgs args)
        {
            SetLayout(MainGridLayoutHorizontal);
        }

        private void OnSetVerticalLayout(object sender, TappedEventArgs args)
        {
            SetLayout(MainGridLayoutVertical);
        }

        private void OnStayOnTopChecked(object sender, RoutedEventArgs args)
        {
            StayOnTop = true;
        }

        private void OnStayOnTopUnchecked(object sender, RoutedEventArgs args)
        {
            StayOnTop = false;
        }

        private void UpdateLayout(MainWindowViewModel viewModel)
        {
            if (viewModel is null)
                return;

            var screenBounds = Screens.ScreenFromPoint(Position)?.Bounds;
            var screenWidth = screenBounds?.Width;
            var screenHeight = screenBounds?.Height;

            if (screenWidth > 1920 && screenHeight > 1080)
            {
                if (viewModel.CurrentMaxWidth != MainWindowViewModel.MaxWidth4K &&
                    viewModel.CurrentMaxHeight != MainWindowViewModel.MaxWidth4K)
                    viewModel.InitialiseFor4K();
            }
            else
            {
                if (viewModel.CurrentMaxWidth != MainWindowViewModel.MaxWidthHD &&
                    viewModel.CurrentMaxHeight != MainWindowViewModel.MaxWidthHD)
                    viewModel.InitialiseForHD();
            }

            SetLayout(MainGridCurrentLayout);
        }

        private void SetLayout(GridLayout layout)
        {
            MainGrid.ColumnDefinitions = layout.ColumnDefinitions;
            MainGrid.RowDefinitions = layout.RowDefinitions;

            if (BaseColourImagePanel.Name is not null &&
                layout.Positions.TryGetValue(BaseColourImagePanel.Name, out var baseColourImagePanelPosition))
            {
                Grid.SetColumn(BaseColourImagePanel, baseColourImagePanelPosition.Column);
                Grid.SetRow(BaseColourImagePanel, baseColourImagePanelPosition.Row);
            }

            if (NormalImagePanel.Name is not null &&
                layout.Positions.TryGetValue(NormalImagePanel.Name, out var normalImagePanelPosition))
            {
                Grid.SetColumn(NormalImagePanel, normalImagePanelPosition.Column);
                Grid.SetRow(NormalImagePanel, normalImagePanelPosition.Row);
            }

            if (AlphaImagePanel.Name is not null &&
                layout.Positions.TryGetValue(AlphaImagePanel.Name, out var alphaImagePanelPosition))
            {
                Grid.SetColumn(AlphaImagePanel, alphaImagePanelPosition.Column);
                Grid.SetRow(AlphaImagePanel, alphaImagePanelPosition.Row);
            }

            if (OcclusionImagePanel.Name is not null &&
                layout.Positions.TryGetValue(OcclusionImagePanel.Name, out var occlusionImagePanelPosition))
            {
                Grid.SetColumn(OcclusionImagePanel, occlusionImagePanelPosition.Column);
                Grid.SetRow(OcclusionImagePanel, occlusionImagePanelPosition.Row);
            }

            if (RoughnessImagePanel.Name is not null &&
                layout.Positions.TryGetValue(RoughnessImagePanel.Name, out var roughnessImagePanelPosition))
            {
                Grid.SetColumn(RoughnessImagePanel, roughnessImagePanelPosition.Column);
                Grid.SetRow(RoughnessImagePanel, roughnessImagePanelPosition.Row);
            }

            if (MetallicImagePanel.Name is not null &&
                layout.Positions.TryGetValue(MetallicImagePanel.Name, out var metallicImagePanelPosition))
            {
                Grid.SetColumn(MetallicImagePanel, metallicImagePanelPosition.Column);
                Grid.SetRow(MetallicImagePanel, metallicImagePanelPosition.Row);
            }

            if (EmissionImagePanel.Name is not null &&
                layout.Positions.TryGetValue(EmissionImagePanel.Name, out var emissionImagePanelPosition))
            {
                Grid.SetColumn(EmissionImagePanel, emissionImagePanelPosition.Column);
                Grid.SetRow(EmissionImagePanel, emissionImagePanelPosition.Row);
            }

            if (SavePanel.Name is not null &&
                layout.Positions.TryGetValue(SavePanel.Name, out var buttonPanelPosition))
            {
                Grid.SetColumn(SavePanel, buttonPanelPosition.Column);
                Grid.SetRow(SavePanel, buttonPanelPosition.Row);
            }

            if (AboutPanel.Name is not null &&
                layout.Positions.TryGetValue(AboutPanel.Name, out var aboutPanelPosition))
            {
                Grid.SetColumn(AboutPanel, aboutPanelPosition.Column);
                Grid.SetRow(AboutPanel, aboutPanelPosition.Row);
            }

            MainGridCurrentLayout = layout;

            SizeToContent = SizeToContent.WidthAndHeight;
        }
    }
}
