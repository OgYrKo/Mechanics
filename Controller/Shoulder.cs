using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Xml.Schema;

namespace Controller
{
    using Degree = Double;

    internal class Shoulder : Element
    {
        private Device device;
        private Mesh cylinder;
        private Material cylinderMaterial;
        private Element nextElement;
        public Vector3 startPoint { get; private set; }
        public Vector3 endPoint { get; private set;}
        private Vector3 elementVector;
        private const float RADIUS = 0.05f;
        private Mutex rotateMutex;
        private Mutex drawMutex;

        public Shoulder(Device device, Vector3 startPoint, Vector3 endPoint)
        {
            SetElement(device, startPoint, endPoint, null);
        }

        public Shoulder(Device device, Vector3 startPoint, Vector3 endPoint, Element nextElement)
        {
            SetElement(device, startPoint, endPoint, nextElement);
        }

        private void SetElement(Device device, Vector3 startPoint, Vector3 endPoint, Element nextElement)
        {
            this.device = device;
            this.startPoint = startPoint;
            this.endPoint = endPoint;
            this.nextElement = nextElement;
            rotateMutex = new Mutex();
            drawMutex = new Mutex();
            SetElementVector();
            SetCylinder();
            SetCylinderMaterial();
        }

        private void SetElementVector()
        {
            elementVector = endPoint - startPoint;
        }

        private void SetCylinder()
        {
            cylinder = Mesh.Cylinder(device, RADIUS, RADIUS, elementVector.Length(), 16, 1);
        }

        private void SetCylinderMaterial()
        {
            cylinderMaterial = new Material();
            cylinderMaterial.Diffuse = Color.Yellow;
            cylinderMaterial.Specular = Color.White;
        }

        public Degree GoToPoint(ref Vector3 point, Vector3 controllerEndPoint)
        {
            Space s = new Space(startPoint,endPoint);
            double angle= s.GetAngle(point,controllerEndPoint);
            if(!double.IsNaN(angle))
                point=s.RotatePoints(new List<Vector3>() { point, controllerEndPoint } ,- angle)[0];
            return angle;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vector"></param>
        /// <returns>-1 при ошибке или индекс координаты вектора (начинается с 0), которая должна быть 0</returns>
        private int GetPlane(in Vector3 vector)
        {
            Vector3 new_vector = vector;
            new_vector.X = 0;
            if (new_vector.Length() == vector.Length())
            {
                return 0;
            }
            new_vector.X=vector.X;
            new_vector.Y = 0;
            if (new_vector.Length() == vector.Length())
            {
                return 1;
            }
            new_vector.Y = vector.Y;
            new_vector.Z = 0;
            if (new_vector.Length() == vector.Length())
            {
                return 2;
            }
            return -1;
        }

        public void Rotate(Degree alpha)
        {
            nextElement.Rotate(alpha, startPoint, endPoint);
        }

        public void Rotate(Degree alpha, Vector3 A, Vector3 B)
        {
            rotateMutex.WaitOne();
            List<Vector3> points = new List<Vector3>();
            points.Add(startPoint);
            points.Add(endPoint);
            Space space = new Space(A, B);
            List<Vector3> newPoints = space.RotatePoints(points,alpha);
            startPoint = newPoints[0];
            endPoint = newPoints[1];

            if (nextElement != null) nextElement.Rotate(alpha, A, B);
            SetElementVector();
            rotateMutex.ReleaseMutex();
        }

        private void SetPosition()
        {
            Vector3 center = new Vector3(startPoint.X + elementVector.X / 2,
                                         startPoint.Y + elementVector.Y / 2,
                                         startPoint.Z + elementVector.Z / 2);

            Vector3 axisRotation = new Vector3(elementVector.X / 2, elementVector.Y / 2, (elementVector.Z + elementVector.Length()) / 2);


            device.Transform.World = Matrix.RotationAxis(axisRotation, (float)Math.PI)
                                   * Matrix.Translation(center);
        }

        public void DrawElement()
        {
            drawMutex.WaitOne();
            device.Material = cylinderMaterial;
            SetPosition();
            cylinder.DrawSubset(0);
            drawMutex.ReleaseMutex();
        }

    }
}
