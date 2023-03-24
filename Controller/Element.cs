using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller
{
    internal class Element
    {
        private Device device;
        private Mesh cylinder;
        private Material cylinderMaterial;
        private Element prevElement;
        private Vector3 startPoint;
        private Vector3 endPoint;
        private Vector3 elementVector;
        private Color color;
        private const float RADIUS = 0.05f;

        public Element(Device device, Vector3 startPoint,Vector3 endPoint, Color color)
        {

            this.color = color;
            this.prevElement = null;
            SetElement(device, startPoint, endPoint);
        }
        public Element(Device device, Vector3 startPoint, Vector3 endPoint,Element prevElement)
        {
            SetElement(device, startPoint, endPoint);
            this.prevElement = prevElement;
        }

        private void SetElement(Device device, Vector3 startPoint, Vector3 endPoint)
        {
            this.device = device;
            this.startPoint = startPoint;
            this.endPoint = endPoint;
            SetCylinder();
            SetCylinderMaterial();
        }

        private void SetCylinder()
        {
            elementVector = endPoint - startPoint;
            cylinder = Mesh.Cylinder(device, RADIUS, RADIUS, elementVector.Length(), 16, 1);
        }

        private void SetCylinderMaterial()
        {
            cylinderMaterial = new Material();
            cylinderMaterial.Diffuse = color;//Color.Yellow;
            cylinderMaterial.Specular = Color.White;
        }

        private void SetPosition()
        {
            Vector3 center = new Vector3(startPoint.X + elementVector.X / 2,
                                         startPoint.Y + elementVector.Y / 2,
                                         startPoint.Z + elementVector.Z / 2);

            float theta_x = (float)Math.Atan((double)elementVector.X / (double)elementVector.Z);
            float theta_y = (float)Math.Atan((double)elementVector.Y / (double)elementVector.Z);
            float theta_z = (float)Math.Atan((double)elementVector.X / (double)elementVector.Y);

            if (float.IsNaN(theta_x)) theta_x = (float)Math.PI / 2;
            if (float.IsNaN(theta_y)) theta_y = (float)Math.PI / 2;
            if (float.IsNaN(theta_z)) theta_z = (float)Math.PI / 2;

            device.Transform.World = Matrix.RotationX(theta_x)
                                   * Matrix.RotationY(theta_y)
                                   * Matrix.RotationZ(theta_z)
                                   * Matrix.Translation(center);
        }
         
        public void DrawElement()
        {
            device.Material = cylinderMaterial;
            SetPosition();
            cylinder.DrawSubset(0);
        }
    }
}
