using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using HelixToolkit.Wpf;
using Microsoft.Win32;
using MoonSharp.Interpreter;

namespace LevelDesign
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Script script;

        public MainWindow()
        {
            InitializeComponent();

            Model = new Model3DGroup();

            DataContext = this;

            UserData.RegisterAssembly();
            UserData.RegisterType<Material>();

            script = new Script();
            script.Globals["SceneFactory"] = new SceneFactory(Model);
            script.Globals["MaterialFactory"] = new MaterialFactory(script);

            var text = @"red = MaterialFactory.CreateSimple('red');
                         pink = MaterialFactory.CreateSimple('pink');
                         SceneFactory.CreatePlane('ground', 10, 10, pink);
                         SceneFactory.CreateSphere('ball', 0, 0, 1, red);";

            script.DoString(text);
        }

        public Model3DGroup Model { get; set; }

        #region Commands

        private void OnOpen(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                DefaultExt = ".lua",
                Filter = "LUA files (*.lua)|*.lua"
            };

            if (dialog.ShowDialog() == true)
            {
                
            }
        }

        private void OnSave(object sender, RoutedEventArgs e)
        {
            var dialog = new SaveFileDialog
            {
                DefaultExt = ".lua",
                Filter = "LUA files (*.lua)|*.lua"
            };

            if (dialog.ShowDialog() == true)
            {

            }
        }

        private void OnExport(object sender, RoutedEventArgs e)
        {
            var dialog = new SaveFileDialog
            {
                DefaultExt = ".dae",
                Filter = "Collada DAE files (*.dae)|*.dae"
            };

            if (dialog.ShowDialog() == true)
            {
                var exporter = new ColladaExporter
                {
                    Author = "Level Designer"
                };

                using (var stream = File.Create(dialog.FileName))
                {
                    exporter.Export(Viewport3D.Viewport, stream);
                }
            }
        }

        private void OnExit(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown(0);
        }

        #endregion
    }
}
