using System.Collections.Generic;
using System.Windows;
using System.Windows.Media.Media3D;
using HelixToolkit.Wpf;
using MoonSharp.Interpreter;

namespace LevelDesign
{
    [MoonSharpUserData]
    public class SceneFactory
    {
        private Model3DGroup model;

        public SceneFactory(Model3DGroup model)
        {
            this.model = model;
        }

        private void CreatePlaneMesh(double width, double depth, Material material)
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

            model.Children.Add(new GeometryModel3D
            {
                Geometry = mesh,
                Transform = new ScaleTransform3D(width, depth, 1),
                Material = material
            });
        }

        private void CreateBoxMesh(double x, double y, double z, Material material)
        {
            var meshBuilder = new MeshBuilder(false, false);

            meshBuilder.AddBox(new Point3D(0, 0, 0), 1, 1, 1);

            var mesh = meshBuilder.ToMesh(true);

            model.Children.Add(new GeometryModel3D
            {
                Geometry = mesh,
                Transform = new TranslateTransform3D(x, y, z),
                Material = material
            });
        }

        private void CreateSphereMesh(double x, double y, double z, Material material)
        {
            var meshBuilder = new MeshBuilder(false, false);

            meshBuilder.AddSphere(new Point3D(), 1);

            var mesh = meshBuilder.ToMesh(true);

            model.Children.Add(new GeometryModel3D
            {
                Geometry = mesh,
                Transform = new TranslateTransform3D(x, y, z),
                Material = material
            });
        }

        public void CreatePlane(string name, double width, double depth, Material material)
        {
            CreatePlaneMesh(width, depth, material);
        }

        public void CreateBox(string name, double x, double y, double z, Material material)
        {
            CreateBoxMesh(x, y, z, material);
        }

        public void CreateSphere(string name, double x, double y, double z, Material material)
        {
            CreateSphereMesh(x, y, z, material);
        }
    }
}