using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace Controller
{
    using Degree = Int32;

    public interface Element
    {
        void Rotate(Degree alpha);

        void Rotate(Degree alpha, Vector3 A, Vector3 B);

        void DrawElement();
    }
}
