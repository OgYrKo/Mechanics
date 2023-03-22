using Microsoft.DirectX;

namespace Controller
{
    public class Camera
    {
        public Vector3 Position { get; set; }
        public Vector3 Target { get; set; }
        public Vector3 UpVector { get; set; }

        public Camera(Vector3 position, Vector3 target, Vector3 upVector)
        {
            this.Position = position;
            this.Target = target;
            this.UpVector = upVector;
        }
    }
}
