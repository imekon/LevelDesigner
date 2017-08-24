﻿using System;
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

            script = new Script();
            script.Globals["CreatePlane"] = (Action<double, double, string>) CreatePlaneScript;
            script.Globals["CreateBox"] = (Action<double, double, double, string>) CreateBoxScript;
            script.Globals["CreateSphere"] = (Action<double, double, double, string>) CreateSphereScript;

            var text = @"CreatePlane(10, 10, 'Pink');
                         CreateSphere(0, 0, 1, 'Red');";

            script.DoString(text);
          
            //CreatePlane(10, 10, MaterialHelper.CreateMaterial(Brushes.Green));
            //CreateBox(0, 0, 0.5, MaterialHelper.CreateMaterial(Brushes.Red));
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

        #region Creators

        private void CreatePlane(double width, double depth, Material material)
        {
            var meshBuilder = new MeshBuilder(false, false);

            var polygon = new List<Point>
            {
                new Point(-1, -1),
                new Point(1, -1),
                new Point(1, 1),
                new Point(-1, 1)
            };

            meshBuilder.AddPolygon(polygon, new Vector3D(1, 0, 0), new Vector3D(0, 1, 0), new Point3D(0, 0, 0));

            var mesh = meshBuilder.ToMesh(true);

            Model.Children.Add(new GeometryModel3D
            {
                Geometry = mesh,
                Transform = new ScaleTransform3D(width, depth, 1),
                Material = material
            });
        }

        private void CreateBox(double x, double y, double z, Material material)
        {
            var meshBuilder = new MeshBuilder(false, false);

            meshBuilder.AddBox(new Point3D(0, 0, 0), 1, 1, 1);

            var mesh = meshBuilder.ToMesh(true);

            Model.Children.Add(new GeometryModel3D
            {
                Geometry = mesh,
                Transform = new TranslateTransform3D(x, y, z),
                Material = material
            });
        }

        private void CreateSphere(double x, double y, double z, Material material)
        {
            var meshBuilder = new MeshBuilder(false, false);

            meshBuilder.AddSphere(new Point3D(), 1);

            var mesh = meshBuilder.ToMesh(true);

            Model.Children.Add(new GeometryModel3D
            {
                Geometry = mesh,
                Transform = new TranslateTransform3D(x, y, z),
                Material = material
            });

        }

        #endregion

        #region Scripting

        private void CreatePlaneScript(double width, double depth, string materialName)
        {
            var brush = (SolidColorBrush)new BrushConverter().ConvertFromString(materialName);
            CreatePlane(width, depth, MaterialHelper.CreateMaterial(brush));
        }

        private void CreateBoxScript(double x, double y, double z, string materialName)
        {
            var brush = (SolidColorBrush)new BrushConverter().ConvertFromString(materialName);
            CreateBox(x, y, z, MaterialHelper.CreateMaterial(brush));
        }

        private void CreateSphereScript(double x, double y, double z, string materialName)
        {
            var brush = (SolidColorBrush)new BrushConverter().ConvertFromString(materialName);
            CreateSphere(x, y, z, MaterialHelper.CreateMaterial(brush));
        }

        #endregion
    }
}
