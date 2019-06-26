using UnityEngine;

namespace Awar.Utils
{
    public static class MouseControl
    {

        public static Vector3 MouseScreenPosition()
        {
            return Input.mousePosition;
        }

        public static Ray MouseRay()
        {
            Vector3 mousePos = MouseScreenPosition();
            mousePos.z = UnityEngine.Camera.main.nearClipPlane;
            Ray ray = UnityEngine.Camera.main.ScreenPointToRay(mousePos);
            return ray;
        }

    }
}
