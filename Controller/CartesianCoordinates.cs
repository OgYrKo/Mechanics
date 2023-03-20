using System;
using System.Drawing;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

namespace Controller
{
    internal class CartesianCoordinates
    {
        private Device device;
        private VertexBuffer cylinderVertexBuffer;
        private VertexBuffer axisVertexBuffer;
        private IndexBuffer cylinderIndexBuffer;
        private IndexBuffer axisIndexBuffer;
        private readonly float cylinderRadius = 0.05f;
        private readonly float cylinderHeight = 1.0f;
        private readonly int cylinderSides = 32;
        private readonly float axisHeight = 3.0f;
        private readonly float axisWidth = 0.1f;

        public CartesianCoordinates(Device device)
        {
            this.device = device;

            // Create the vertex and index buffers for the cylinder
            CreateCylinderVertexBuffer();
            CreateCylinderIndexBuffer();

            // Create the vertex and index buffers for the axis lines
            CreateAxisVertexBuffer();
            CreateAxisIndexBuffer();
        }

        private void CreateCylinderVertexBuffer()
        {
            CustomVertex.PositionNormal[] vertices = new CustomVertex.PositionNormal[cylinderSides * 2];
            float angleIncrement = (float)(2 * Math.PI / cylinderSides);

            for (int i = 0; i < cylinderSides; i++)
            {
                float angle = i * angleIncrement;
                float x = (float)Math.Cos(angle) * cylinderRadius;
                float y = (float)Math.Sin(angle) * cylinderRadius;

                vertices[i * 2] = new CustomVertex.PositionNormal(x, 0, y, 0, 1, 0);
                vertices[i * 2 + 1] = new CustomVertex.PositionNormal(x, cylinderHeight, y, 0, 1, 0);
            }

            cylinderVertexBuffer = new VertexBuffer(typeof(CustomVertex.PositionNormal), vertices.Length, device, Usage.WriteOnly, CustomVertex.PositionNormal.Format, Pool.Default);
            cylinderVertexBuffer.SetData(vertices, 0, LockFlags.None);
        }

        private void CreateCylinderIndexBuffer()
        {
            short[] indices = new short[cylinderSides * 6];

            for (int i = 0; i < cylinderSides; i++)
            {
                indices[i * 6] = (short)(i * 2);
                indices[i * 6 + 1] = (short)(i * 2 + 1);
                indices[i * 6 + 2] = (short)(((i + 1) % cylinderSides) * 2 + 1);

                indices[i * 6 + 3] = (short)(i * 2);
                indices[i * 6 + 4] = (short)(((i + 1) % cylinderSides) * 2 + 1);
                indices[i * 6 + 5] = (short)(((i + 1) % cylinderSides) * 2);
            }

            cylinderIndexBuffer = new IndexBuffer(typeof(short), indices.Length, device, Usage.WriteOnly, Pool.Default);
            cylinderIndexBuffer.SetData(indices, 0, LockFlags.None);
        }

        private void CreateAxisVertexBuffer()
        {
            CustomVertex.PositionNormal[] vertices = new CustomVertex.PositionNormal[6];

            // x-axis
            vertices[0] = new CustomVertex.PositionNormal(0, 0, 0, 1, 0, 0);
            vertices[1] = new CustomVertex.PositionNormal(axisHeight, 0, 0, 1, 0, 0);

            // y-axis
            vertices[2] = new CustomVertex.PositionNormal(0, 0, 0, 0, 1, 0);
            vertices[3] = new CustomVertex.PositionNormal(0, axisHeight, 0, 0, 1, 0);

            // z-axis
            vertices[4] = new CustomVertex.PositionNormal(0, 0, 0, 0, 0, 1);
            vertices[5] = new CustomVertex.PositionNormal(0, 0, axisHeight, 0, 0, 1);

            axisVertexBuffer = new VertexBuffer(typeof(CustomVertex.PositionNormal), vertices.Length, device, Usage.WriteOnly, CustomVertex.PositionNormal.Format, Pool.Default);
            axisVertexBuffer.SetData(vertices, 0, LockFlags.None);
        }

        private void CreateAxisIndexBuffer()
        {
            short[] indices = new short[] { 0, 1, 2, 3, 4, 5 };

            axisIndexBuffer = new IndexBuffer(typeof(short), indices.Length, device, Usage.WriteOnly, Pool.Default);
            axisIndexBuffer.SetData(indices, 0, LockFlags.None);
        }

        public void Draw(Matrix worldMatrix, Matrix viewMatrix, Matrix projectionMatrix)
        {
            // Set the vertex and index buffers for the cylinder
            device.SetStreamSource(0, cylinderVertexBuffer, 0);
            device.Indices = cylinderIndexBuffer;

            // Draw the x-axis cylinder
            Matrix xCylinderWorldMatrix = Matrix.RotationZ((float)(-Math.PI / 2)) * Matrix.Translation(new Vector3(cylinderHeight / 2, 0, 0));
            device.Transform.World = xCylinderWorldMatrix * worldMatrix;
            device.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, cylinderSides * 2, 0, cylinderSides * 2 - 2);

            // Draw the y-axis cylinder
            Matrix yCylinderWorldMatrix = Matrix.Translation(new Vector3(0, cylinderHeight / 2, 0));
            device.Transform.World = yCylinderWorldMatrix * worldMatrix;
            device.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, cylinderSides * 2, 0, cylinderSides * 2 - 2);

            // Draw the z-axis cylinder
            Matrix zCylinderWorldMatrix = Matrix.RotationX((float)(Math.PI / 2)) * Matrix.Translation(new Vector3(0, 0, cylinderHeight / 2));
            device.Transform.World = zCylinderWorldMatrix * worldMatrix;
            device.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, cylinderSides * 2, 0, cylinderSides * 2 - 2);

            // Set the vertex and index buffers for the axis lines
            device.SetStreamSource(0, axisVertexBuffer, 0);
            device.Indices = axisIndexBuffer;

            // Draw the x-axis line
            device.Transform.World = worldMatrix;
            device.DrawIndexedPrimitives(PrimitiveType.LineList, 0, 0, 2, 0, 1);

            // Draw the y-axis line
            device.Transform.World = worldMatrix;
            device.DrawIndexedPrimitives(PrimitiveType.LineList, 0, 2, 2, 0, 1);

            // Draw the z-axis line
            device.Transform.World = worldMatrix;
            device.DrawIndexedPrimitives(PrimitiveType.LineList, 0, 4, 2, 0, 1);
        }

        public void Dispose()
        {
            cylinderVertexBuffer.Dispose();
            cylinderIndexBuffer.Dispose();
            axisVertexBuffer.Dispose();
            axisIndexBuffer.Dispose();
        }


    }
}