using UnityEngine;
using UnityEngine.Events;

namespace Eloi.TextureUtility 

{
    public class TextureMono_RelayTexture : MonoBehaviour {

        public Texture m_textureRelayed;
        public UnityEvent<Texture> m_onTextureRelayed;

        public void PushIn(Texture texture) { 
        
            m_textureRelayed = texture;
            m_onTextureRelayed?.Invoke(texture);
        }
        public void PushIn(Texture2D texture)
        {

            m_textureRelayed = texture;
            m_onTextureRelayed?.Invoke(texture);
        }
        public void PushIn(WebCamTexture texture)
        {

            m_textureRelayed = texture;
            m_onTextureRelayed?.Invoke(texture);
        }

        [ContextMenu("Replay Texture in Inspector")]
        public void RelayCurrentTextureInInspector() { 
            PushIn(m_textureRelayed);
        }
    }

}

namespace Eloi.TextureUtility

{

}