using System;
using UnityEngine;
using UnityEngine.Events;

namespace Eloi.TextureUtility {
    public class TextureMono_GenreateBetweenColorSample : MonoBehaviour
    {

        public Color32 m_minColorRange;
        public Color32 m_maxColorRange;
        public int m_widthToGenerate;
        public int m_heightToGenerate;
        Color32[] m_colorsSample ;
        public Texture2D m_textureSample;
        public void SetMinColorRange(Color32 minColorRange) {

            m_minColorRange = minColorRange;
            RefreshSample();
        }
        public void SetMaxColorRange(Color32 maxColorRange) {

            m_maxColorRange = maxColorRange;
            RefreshSample();
        }
        public TextureFormat m_format = TextureFormat.RGBA32;
        public bool m_useMipmap = true;
        public bool m_useLinear = true;


        public UnityEvent<Texture2D> m_onTextureUpdated;


        [ContextMenu("Refresh")]
        public void RefreshSample()
        {
            if (m_textureSample == null || m_colorsSample==null
                || m_textureSample.width != m_widthToGenerate
                || m_textureSample.height != m_heightToGenerate
                 ) { 
            
                m_colorsSample = new Color32[m_widthToGenerate*m_heightToGenerate];
                m_textureSample = new Texture2D(m_widthToGenerate, m_heightToGenerate, m_format, m_useMipmap, m_useLinear);
                m_textureSample.filterMode = FilterMode.Point;
                m_textureSample.wrapMode = TextureWrapMode.Clamp;
            }

            for (int i = 0; i < m_colorsSample.Length; i++) {
                Color32 c = new Color32();
                c.r = (byte)UnityEngine.Random.Range(m_minColorRange.r, m_maxColorRange.r);
                c.g = (byte)UnityEngine.Random.Range(m_minColorRange.g, m_maxColorRange.g);
                c.b = (byte)UnityEngine.Random.Range(m_minColorRange.b, m_maxColorRange.b);
                c.a = 255;
                m_colorsSample[i] = c;
            }
            m_textureSample.SetPixels32(m_colorsSample);
            m_textureSample.Apply();
            m_onTextureUpdated?.Invoke(m_textureSample);

        }
    }

}