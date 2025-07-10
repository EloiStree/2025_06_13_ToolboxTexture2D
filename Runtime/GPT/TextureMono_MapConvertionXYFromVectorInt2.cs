using UnityEngine;
using UnityEngine.Events;

namespace Eloi.TextureUtility
{

    public class TextureMono_MapConvertionXYFromVectorInt2:MonoBehaviour
    {



        public void SetWidthAndHeight(int width, int height)
        {
            m_pixelHeight = height;
            m_pixelWidth = width;
        }

        public void SetWithRectangle(RectInt rectInt)
        {
            m_pixelHeight = rectInt.height;
            m_pixelWidth = rectInt.width;
        }

        public void SetPixelsUsedInMap(Vector2Int[] sourceCoordinate)
        {
            m_sourceCoordinate = sourceCoordinate;
          
            m_middleX = m_pixelWidth / 2;
            m_middleY = m_pixelHeight / 2;
            m_borderRadiusX = m_pixelWidth / 2;
            m_borderRadiusY = m_pixelHeight / 2;
            int lenght = m_sourceCoordinate.Length;
            if (m_percentCoordinateCoordinate.Length != lenght)
                m_percentCoordinateCoordinate = new Vector2[lenght];

            for (int i = 0; i < lenght; i++)
            {
                m_percentCoordinateCoordinate[i] = new Vector2(
                    ((float)m_sourceCoordinate[i].x - m_middleX) / (float)m_borderRadiusX,
                    ((float)m_sourceCoordinate[i].y - m_middleY) / (float)m_borderRadiusY);
            }
            m_onCoordinateAsPercentChanged.Invoke(m_percentCoordinateCoordinate);
        }
        public int m_pixelWidth;
        public int m_pixelHeight;
        public int m_middleX;
        public int m_middleY;
        public int m_borderRadiusX = 0;
        public int m_borderRadiusY = 0;

        public Vector2Int[] m_sourceCoordinate;
        public Vector2[] m_percentCoordinateCoordinate;

        public UnityEvent<Vector2[]> m_onCoordinateAsPercentChanged;
    }
}
