using System.Collections.Generic;
using UnityEngine;

namespace Platformer2D.Assets.Extention
{
    public static class Vector3Extension
    {
        public static float SignAngle(Vector3 vectorZero, Vector3 direction, Vector3 normalAxis)
        {
            float angle = Vector3.Angle(vectorZero, direction);

            Vector3 normal = Vector3.Cross(direction, vectorZero);

            float sign = Mathf.Sign(Vector3.Dot(normal, normalAxis));

            return sign * angle;
        }
    }
}
