﻿using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;

namespace Controller
{
    using Degree = Double;

    internal class Brush : Element
    {
        private Device device;
        private Mesh[] cylinders;
        private Vector3[] cylindersEndPoints;//текущее положение пальцев
        private Vector3[] cylindersEndPointsDirection;//направление пальцев
        private Material cylinderMaterial;
        private Vector3 startPoint;//точка соприкосновения с плечом
        public Vector3 endPoint { get; private set; }//точка схвата
        private Vector3 elementVector;//координаты текущего вектора
        private const float RADIUS = 0.05f;
        private const int cylindersCount = 3;
        private Degree sumAlpha = 0;
        private Mutex rotateMutex;
        private readonly object paralelLock;
        private Mutex drawMutex;
        private Item item;

        public Brush(Device device, Vector3 startPoint, Vector3 endPoint)
        {
            this.device = device;
            this.startPoint = startPoint;
            this.endPoint = endPoint;
            rotateMutex = new Mutex();
            drawMutex = new Mutex();
            item = null;
            SetElementVector();
            SetCylinders();
            SetCylinderMaterial();
            SetCylindersEndPoints();
        }

        private void SetElementVector()
        {
            elementVector = endPoint - startPoint;
        }

        private void SetCylinders()
        {

            cylinders = new Mesh[cylindersCount];
            for (int i = 0; i < cylindersCount; i++)
            {
                cylinders[i] = Mesh.Cylinder(device, RADIUS, 0, elementVector.Length(), 16, 1);
            }

        }

        private void SetCylinderMaterial()
        {
            cylinderMaterial = new Material();
            cylinderMaterial.Diffuse = Color.Green;
            cylinderMaterial.Specular = Color.White;
        }

        private void SetCylindersEndPoints()
        {
            cylindersEndPoints = new Vector3[cylindersCount];
            cylindersEndPointsDirection = new Vector3[cylindersCount];

            Vector3 newVector = elementVector.GetPerpendicular(); // произвольный вектор, перпендикулярный u

            Space s = new Space(new Vector3(), elementVector, new List<Vector3>() { newVector });
            for (int i = 0; i < cylindersCount; i++)
            {
                cylindersEndPoints[i] = startPoint + s.Rotate(i * 360 / cylindersCount)[0];
                cylindersEndPointsDirection[i]= new Vector3(cylindersEndPoints[i].X, cylindersEndPoints[i].Y, cylindersEndPoints[i].Z);
            }
        }

        public bool IsTouch(Item itemIn)
        {
            // Координаты проверяемой точки
            Vector3 checkPoint = cylindersEndPoints[0];
            // Вычисляем расстояние от точки до центра шара
            double d= (checkPoint - itemIn.centerPoint).Length();
            if(d <= itemIn.radius)
            {
                AttachItem(itemIn);
                return true;
            }
            else
            {
                if (item != null) 
                    DettachItem();
                return false;
            }
            
        }

        private bool AttachItem(Item itemIn)
        {
            if (this.item != null) return false;
            this.item = itemIn;
            return true;
        }
        private bool DettachItem()
        {
            this.item = null;
            return true;
        }

        public Degree GoToPoint(Vector3 point, Vector3 O)
        {
            return 0;
        }
        
        //поворачивает сами пальцы
        public void Rotate(Degree alpha)
        {
            rotateMutex.WaitOne();
            sumAlpha += alpha;
            if (sumAlpha>=0&& sumAlpha <= 90)
            {
                    
                Vector3 A = startPoint;
                    
                for (int i = 0; i < cylindersEndPoints.Length; i++)
                
                {
                    Vector3 B = startPoint + Vector3.Cross(cylindersEndPointsDirection[i] - startPoint, elementVector);
                    
                    Space s = new Space(A, B, new List<Vector3>() { cylindersEndPoints[i] });
                
                    cylindersEndPoints[i] = s.Rotate(-alpha)[0];
                    
                }
                
            }
            else
            {
                sumAlpha -= alpha;
            }
            if (item != null) IsTouch(item);
            rotateMutex.ReleaseMutex();
        }

        //поворачивает захват
        public void Rotate(Degree alpha, Vector3 A, Vector3 B)
        {
            rotateMutex.WaitOne();
            List<Vector3> points = new List<Vector3>();
            points.Add(startPoint);
            points.Add(endPoint);
            
            for(int i = 0; i < cylindersEndPoints.Length; i++)
                points.Add(cylindersEndPoints[i]);

            for (int i = 0; i < cylindersEndPointsDirection.Length; i++)
                points.Add(cylindersEndPointsDirection[i]);

            Space space = new Space(A, B, points);
            List<Vector3> newPoints = space.Rotate(alpha);
            startPoint = newPoints[0];
            endPoint = newPoints[1];

            for (int i = 2; i < cylindersEndPoints.Length+2; i++)
                cylindersEndPoints[i-2] = newPoints[i];

            for (int i = 0; i < cylindersEndPointsDirection.Length; i++)
                cylindersEndPointsDirection[i] = newPoints[i + cylindersEndPoints.Length + 2];

            SetElementVector();
            if(item!=null)
                item.centerPoint=new Vector3(endPoint.X, endPoint.Y, endPoint.Z);
            rotateMutex.ReleaseMutex();
        }

        private void SetPosition(int index)
        {
            Vector3 center = new Vector3((startPoint.X + cylindersEndPoints[index].X) / 2,
                                         (startPoint.Y + cylindersEndPoints[index].Y) / 2,
                                         (startPoint.Z + cylindersEndPoints[index].Z) / 2);


            Vector3 elementVector = cylindersEndPoints[index] - startPoint;

            Vector3 axisRotation = new Vector3(elementVector.X / 2, elementVector.Y / 2, (elementVector.Z + elementVector.Length()) / 2);

            device.Transform.World = Matrix.RotationAxis(axisRotation, (float)Math.PI)
                                   * Matrix.Translation(center);
        }

        public void DrawElement()
        {
            drawMutex.WaitOne();
            for (int i = 0; i < cylindersCount; i++)
            {
                device.Material = cylinderMaterial;
                SetPosition(i);
                cylinders[i].DrawSubset(0);
            }
            drawMutex.ReleaseMutex();
        }

    }
}
