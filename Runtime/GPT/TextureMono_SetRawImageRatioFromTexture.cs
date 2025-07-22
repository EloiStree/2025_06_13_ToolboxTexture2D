namespace Eloi.TextureUtility
{
    using UnityEngine;
    using UnityEngine.UI;

    public class TextureMono_SetRawImageRatioFromTexture : MonoBehaviour {

        public RawImage m_rawImage;
        public AspectRatioFitter m_aspectRatioFitter;


        private void Reset()
        {
            m_rawImage = GetComponent<RawImage>();  
            m_aspectRatioFitter = GetComponent<AspectRatioFitter>();
        }
        public void SetTexture(Texture texture) { 

            float ratio = texture.width/(float)texture.height;
            if (m_aspectRatioFitter != null ) 
                m_aspectRatioFitter.aspectRatio = ratio;

            if (m_rawImage != null )
                m_rawImage.texture = texture;
        }
    }
}


