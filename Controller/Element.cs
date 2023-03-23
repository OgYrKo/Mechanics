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
        Vector3 elementVector;

        private const float RADIUS = 0.05f;

        public Element(Device device, Vector3 startPoint,Vector3 endPoint)
        {
            SetElement(device, startPoint, endPoint);
            this.prevElement = null;
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
            SetDevice();
        }

        private void SetCylinder()
        {
            elementVector = endPoint - startPoint;
            cylinder = Mesh.Cylinder(device, RADIUS, RADIUS, elementVector.Length(), 16, 1);
        }

        private void SetCylinderMaterial()
        {
            cylinderMaterial = new Material();
            cylinderMaterial.Diffuse = Color.Yellow;
            cylinderMaterial.Specular = Color.White;
        }

        private void SetDevice()
        {
            device.Material = cylinderMaterial;
            //device.Transform.World *=Matrix.Translation(new Vector3(0,5f,0));

            //device.Transform.World = Matrix.RotationX((float)Math.Acos((double)elementVector.Y / (double)elementVector.Length()))
            //                          * Matrix.RotationY((float)Math.Acos((double)elementVector.Z / (double)elementVector.Length()))
            //                          * Matrix.RotationZ((float)Math.Acos((double)elementVector.X / (double)elementVector.Length()))
            //                          * Matrix.Translation(new Vector3(0,5f,0));
                                      //* Matrix.Translation(new Vector3(elementVector.X/2, elementVector.Y / 2, elementVector.Z / 2));
        }
         
        public void DrawElement()
        {
            SetDevice();
            cylinder.DrawSubset(0);
        }

    }
}
