using System;
using System.Collections.Generic;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

namespace Controller
{
    public static class Vector3Extencion
    {
        public static Vector3 CrossProduct(this Vector3 u, Vector3 v)
        {
            Vector3 perp = new Vector3();

            perp.X = u.Y * v.Z - u.Z * v.Y;
            perp.Y = u.Z * v.X - u.X * v.Z;
            perp.Z = u.X * v.Y - u.Y * v.X;

            return perp;
        }

        public static Vector3 GetPerpendicular(this Vector3 v)
        {
            Vector3 perp = new Vector3();

            // Compute a perpendicular vector
            if (v.X != 0 || v.Y != 0)
            {
                perp.X = -v.Y;
                perp.Y = v.X;
                perp.Z = 0;
            }
            else
            {
                perp.X = 0;
                perp.Y = -v.Z;
                perp.Z = v.Y;
            }
            return perp;
        }
    }
}
