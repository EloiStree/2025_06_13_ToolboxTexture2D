using UnityEngine;

namespace Eloi.TextureUtility
{
    public class TextureMono_MapPercentXyToPlayerPosition : MonoBehaviour {


        public Vector2[] m_sourcePercentCoordinate;
        public Vector2[] m_worldCoordinateFromPlayerPosition;
        public Vector2 m_playerPosition;
        public Vector2 m_centerCornerCoordinateAtCalibration = new Vector2(-2355.1f, -9692.4f);
        public Vector2 m_topRightCornerCoordinateAtCalibration = new Vector2(-2680.9f, -9370.9f);

        public float m_squareWidthAtCalibration = 325.8f;
        public float m_squareHeightAtCalibration = 321.5f;

        public int m_pixelWidth = 0;
        public int m_pixelHeight = 0;
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

        public void SetPlayerPosition(Vector3 playerPosition)
        {
            m_playerPosition = new Vector2(playerPosition.x, playerPosition.z);

        }
        public void SetPlayerPosition(Vector2 playerPosition)
        {
            m_playerPosition = playerPosition;

        }


        public void SetWith(Vector2[] sourceCoordinate)
        {
            m_sourcePercentCoordinate = sourceCoordinate;
            m_squareHeightAtCalibration = (m_topRightCornerCoordinateAtCalibration.y - m_centerCornerCoordinateAtCalibration.y) ;
            m_squareWidthAtCalibration = (m_topRightCornerCoordinateAtCalibration.x - m_centerCornerCoordinateAtCalibration.x) ;

            int lenght = m_sourcePercentCoordinate.Length;
            if (m_worldCoordinateFromPlayerPosition.Length != lenght)
                m_worldCoordinateFromPlayerPosition = new Vector2[lenght];
            for (int i = 0; i < lenght; i++)
            {
                m_worldCoordinateFromPlayerPosition[i] = new Vector2(
                    (m_playerPosition.x + m_sourcePercentCoordinate[i].x * m_squareWidthAtCalibration ),
                    (m_playerPosition.y + m_sourcePercentCoordinate[i].y * m_squareHeightAtCalibration ));
            }
        }
    }
}
