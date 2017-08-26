using System.Windows.Media.Media3D;
using MoonSharp.Interpreter;

namespace LevelDesign
{
    [MoonSharpUserData]
    public class Shape
    {
        private GeometryModel3D geometry;
        private TranslateTransform3D translate;
        private ScaleTransform3D scale;
        private RotateTransform3D rotation;

        public Shape(GeometryModel3D geometry)
        {
            this.geometry = geometry;
            translate = new TranslateTransform3D(0, 0, 0);
            scale = new ScaleTransform3D(1, 1, 1);
            rotation = new RotateTransform3D();
            var group = new Transform3DGroup();
            group.Children.Add(translate);
            group.Children.Add(scale);
            group.Children.Add(rotation);
            geometry.Transform = group;
        }

        public void Translate(double x, double y, double z)
        {
            translate.OffsetX = x;
            translate.OffsetY = y;
            translate.OffsetZ = z;
        }

        public void Scale(double x, double y, double z)
        {
            scale.ScaleX = x;
            scale.ScaleY = y;
            scale.ScaleZ = z;
        }

        public void Rotate(double x, double y, double z)
        {
            rotation.Rotation = new QuaternionRotation3D(new Quaternion(x, y, z, 1));
        }
    }
}