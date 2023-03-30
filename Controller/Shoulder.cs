﻿using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace Controller
{
    using Degree = Double;

    internal class Shoulder : Element
    {
        private Device device;
        private Mesh cylinder;
        private Material cylinderMaterial;
        private Element nextElement;
        private Vector3 startPoint;
        private Vector3 endPoint;
        private Vector3 elementVector;
        private const float RADIUS = 0.05f;

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

        public Degree GoToPoint(Vector3 point)
        {
            Degree angle = nextElement.GoToPoint(point, startPoint);
            return angle;
            //int rotationCount = Convert.ToInt32(angle);
            //for(int i = 0; i < rotationCount; i++)
            //{
            //    Rotate(1);
            //}
            //Rotate(angle - rotationCount);
            
        }

        public Degree GoToPoint(Vector3 point, Vector3 O)
        {
            Space s = new Space(O, startPoint, new List<Vector3>() { endPoint, point });
            return s.GetAngle();
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
            List<Vector3> points = new List<Vector3>();
            points.Add(startPoint);
            points.Add(endPoint);
            Space space = new Space(A, B, points);
            List<Vector3> newPoints = space.Rotate(alpha);
            startPoint = newPoints[0];
            endPoint = newPoints[1];

            if (nextElement != null) nextElement.Rotate(alpha, A, B);
            SetElementVector();

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
            device.Material = cylinderMaterial;
            SetPosition();
            cylinder.DrawSubset(0);
        }

    }
}