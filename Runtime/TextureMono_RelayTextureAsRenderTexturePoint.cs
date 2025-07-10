using UnityEngine;
using UnityEngine.Events;

namespace Eloi.TextureUtility
{
    public class TextureMono_RelayTextureAsRenderTexturePoint : MonoBehaviour
    {
        public Texture m_textureRelayed;
        public RenderTexture m_renderTextureCreated;
        public UnityEvent<RenderTexture> m_onTextureRelayed;
        public bool m_pushAtStart = true;

        private void Start()
        {
            if (m_pushAtStart)
            {
                RelayCurrentTextureInInspector();
            }
        }

        public void PushIn(Texture texture)
        {
            PushThroughRenderTexture(texture);
        }

        private void PushThroughRenderTexture(Texture texture)
        {
            m_textureRelayed = texture;

            if (texture != null)
            {
                if (m_renderTextureCreated == null ||
                    m_renderTextureCreated.width != texture.width ||
                    m_renderTextureCreated.height != texture.height)
                {
                    if (m_renderTextureCreated != null)
                    {
                        m_renderTextureCreated.Release();
                    }

                    m_renderTextureCreated = new RenderTexture(texture.width, texture.height, 0, RenderTextureFormat.ARGB32);
                    m_renderTextureCreated.enableRandomWrite = true;
                    m_renderTextureCreated.wrapMode = TextureWrapMode.Clamp;
                    m_renderTextureCreated.filterMode = FilterMode.Point;
                    m_renderTextureCreated.Create();
                }

                Graphics.Blit(texture, m_renderTextureCreated);
                m_onTextureRelayed?.Invoke(m_renderTextureCreated);
            }
        }



        public void PushIn(Texture2D texture)
        {
            PushThroughRenderTexture(texture);
        }

        public void PushIn(WebCamTexture texture)
        {
            if (texture != null && texture.isPlaying)
            {
                PushThroughRenderTexture(texture);
            }
            else
            {
                Debug.LogError("WebCamTexture is not initialized or not playing.");
            }
        }

        [ContextMenu("Replay Texture in Inspector")]
        public void RelayCurrentTextureInInspector()
        {
            PushIn(m_textureRelayed);
        }
    }
}
