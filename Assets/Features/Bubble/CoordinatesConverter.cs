using UnityEngine;

namespace Features.Pieces
{
    public class CoordinatesConverter
    {
        public static Vector3 Convert(Vector2Int boardCoordinates, float size)
        {
            return new Vector3((float)boardCoordinates.x/2 * size, -boardCoordinates.y*0.86f * size);
        }
    }
}