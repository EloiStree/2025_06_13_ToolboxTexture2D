using UnityEngine;
using UnityEngine.Events;

namespace Eloi.TextureUtility 

{

    public class TextureMono_RelayWebcamByIndex : MonoBehaviour
    {
        public int m_index;

        public WebCamTexture m_texture;
        public UnityEvent<WebCamTexture> m_onWebcamTextureFound;
        public bool m_autoStartTheWebcam=true;
        public void OnEnable()
        {
            ResetTheWebcam();
        }

        private void ResetTheWebcam()
        {
            if (m_texture == null)
            {
                TryToConnect();
            }
            else
            {
                m_texture.Stop();
                m_texture = null;
                TryToConnect();
            }
        }

        private void TryToConnect()
        {
            if (m_texture != null)
                return;
            if (m_index<WebCamTexture.devices.Length ) { 
                WebCamDevice cam = WebCamTexture.devices[m_index];
                if (m_autoStartTheWebcam) {
                    m_texture = new WebCamTexture(cam.name);
                    m_texture.Play();
                }

                m_onWebcamTextureFound?.Invoke(m_texture);
            }
        }
    }

}