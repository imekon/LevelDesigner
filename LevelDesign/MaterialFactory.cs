using System.Windows.Media;
using HelixToolkit.Wpf;
using MoonSharp.Interpreter;

namespace LevelDesign
{
    [MoonSharpUserData]
    public class MaterialFactory
    {
        private Script script;

        public MaterialFactory(Script script)
        {
            this.script = script;
        }

        public DynValue CreateSimple(string name)
        {
            var brush = (SolidColorBrush)new BrushConverter().ConvertFromString(name);
            var material = MaterialHelper.CreateMaterial(brush);
            return DynValue.FromObject(script, material);
        }
    }
}