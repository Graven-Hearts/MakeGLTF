using System;
using System.Reflection;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Graven.Hearts.MakeGLTF.ViewModels;
using Graven.Hearts.MakeGLTF.Views;

namespace Graven.Hearts.MakeGLTF
{
    public partial class App : Application
    {
        public static string AppName => "MakeGLTF";
        public static Version AppVersion => new ("1.0.0.0");
        public static string AppBranding => "Graven Hearts";
        public static string AppAuthor => "By Gabriele Graves";
        public static string AppOriginalCredits => "Based on Extrude Ragu's GLTF Packer";
        public static string AppURL => "https://github.com/Graven-Hearts/MakeGLTF";
        public static string AppOriginalURL => "https://aiaicapta.in/gltf-packer/";

        public App() : base()
        {
            Name = AppName;
        }

        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow
                {
                    DataContext = new MainWindowViewModel(),
                };
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}
