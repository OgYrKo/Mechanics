using Microsoft.DirectX;

namespace Controller
{
    public static class Vector3Extencion
    {
        public static bool Compare(Vector3 lhs, Vector3 rhs)
        {
            return lhs.Equals(rhs);

            //double eps = 0.0000001;
            //if (Math.Abs(lhs.X - rhs.X) > eps) return false;
            //else if (Math.Abs(lhs.Y - rhs.Y) > eps) return false;
            //else if (Math.Abs(lhs.Z - rhs.Z) > eps) return false;
            //else return true;
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
