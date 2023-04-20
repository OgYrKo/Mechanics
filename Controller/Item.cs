using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using System.Drawing;

namespace Controller
{
    internal class Item
    {
        private Device device;
        private Mesh item;
        private Material itemMaterial;
        public Vector3 centerPoint { get; set; }
        public float radius { get; private set; }

        public Item(Device device, Vector3 centerPoint,float radius)
        {
            this.device = device;
            this.centerPoint = centerPoint;
            this.radius = radius;
            SetItem();
        }
        private void SetItem()
        {
            SetMesh();
            SetItemMaterial();
        }

        private void SetMesh()
        {
            item = Mesh.Sphere(device, radius, 20, 20);
        }

        private void SetItemMaterial() 
        {
            itemMaterial = new Material();
            itemMaterial.Diffuse = Color.Red;
            itemMaterial.Specular = Color.White;
        }

        private void SetPosition()
        {
            device.Transform.World = Matrix.Translation(centerPoint);
        }
        public void Draw()
        {
            device.Material = itemMaterial;
            SetPosition();
            item.DrawSubset(0);
        }
    }
}
