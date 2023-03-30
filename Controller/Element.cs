using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace Controller
{
    using Degree = Double;

    public interface Element
    {
        void Rotate(Degree alpha);

        void DrawElement();

        void Rotate(Degree alpha, Vector3 A, Vector3 B);

        Degree GoToPoint(Vector3 point);

        Degree GoToPoint(Vector3 point, Vector3 O);
    }

}
