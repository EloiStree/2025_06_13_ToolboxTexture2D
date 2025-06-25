using System;
using UnityEngine;

namespace Eloi.TextureUtility {
    /// <summary>
    /// If you plan to use color to be exported by UDP, Websocket or memory you will need it to be Bufferable.
    /// I am a class that store the color as bytes in a arry starting by it width and height in ushort.
    /// To win 1/4 of bandwidth, I store the color as RGB instead of RGBA.
    /// </summary>
    [Serializable]
    public class Color32AsBytesArray2DRGB
    {
        public int m_width;
        public int m_height;
        public byte[] m_rgbBytes;

        public void SetWidth(int width) { 
        
            m_width = width;
            BitConverter.GetBytes(m_width).CopyTo(m_rgbBytes, 0);
        }
        public void SetHeight(int height)
        {
            m_height = height;
            BitConverter.GetBytes(m_height).CopyTo(m_rgbBytes, 4);
        }

        
        public Color32AsBytesArray2DRGB(ushort width, ushort height)
        {
            m_width = width;
            m_height = height;
            m_rgbBytes = new byte[ (width * height * 3) + 8 ];
            BitConverter.GetBytes(m_width).CopyTo(m_rgbBytes, 0);
            BitConverter.GetBytes(m_height).CopyTo(m_rgbBytes, 4);
        }
        public void FlushToZeroColor()
        {
            for (int i = 8; i < m_rgbBytes.Length; i++)
            {
                m_rgbBytes[i] = 0;
            }
        }

        public void SetColor32(Color32[] color32)
        {
            if (color32.Length != m_width * m_height)
            {
                throw new ArgumentException("Color32 array length does not match width and height.");
            }
            for (int i = 0; i < color32.Length; i++)
            {
                m_rgbBytes[i * 3 + 8] = color32[i].r;
                m_rgbBytes[i * 3 + 9] = color32[i].g;
                m_rgbBytes[i * 3 + 10] = color32[i].b;
            }
        }
    }

}