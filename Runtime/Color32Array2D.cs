using System;
using UnityEngine;

namespace Eloi.TextureUtility {
    /// <summary>
    /// I am a class that store the width and height of a Color32 as to make computation I dont really need Texture2D information.
    /// </summary>
    [Serializable]
    public class Color32Array2D
    {
        public int m_width;
        public int m_height;
        public Color32[] m_color32;
    }

}