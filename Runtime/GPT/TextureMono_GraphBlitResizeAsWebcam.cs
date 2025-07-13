using UnityEngine;
using UnityEngine.Events;

public class TextureMono_GraphBlitResizeAsWebcam : MonoBehaviour
{
    public WebCamTexture m_source;
    public RenderTexture m_result;

    public int m_wantedWidth = 1280;
    public int m_wantedHeight = 930;
    public UnityEvent<RenderTexture> m_onCreated;
    public UnityEvent<RenderTexture> m_onUpdated;
    public bool m_useUpdate;

    private void Awake()
    {
        SetRenderTexture(null);
    }

    public void SetRenderTexture(WebCamTexture source)
    {
        WebCamTexture previous = m_source;
        if (previous == null || previous != source)
        {
            if (m_result != null)
            {
                m_result.Release();
                Destroy(m_result);
            }

            int width = m_wantedWidth;
            int height = m_wantedHeight;


            m_result = new RenderTexture(m_wantedWidth, m_wantedHeight, 0);
            m_result.wrapMode = TextureWrapMode.Clamp;
            m_result.filterMode = FilterMode.Point;
            m_result.enableRandomWrite = true;
            m_result.useMipMap = false;
            m_result.Create();
            if (m_onCreated != null)
                m_onCreated.Invoke(m_result);
        }
        m_source = source;
    }

    private void Update()
    {
        if (m_useUpdate)
            ResizeUpdate();
    }

    public void ResizeUpdate()
    {
        if (m_source != null && m_result != null && m_source.width > 0 && m_source.height > 0)
        {
            Graphics.Blit(m_source, m_result);
            if (m_onUpdated != null)
                m_onUpdated.Invoke(m_result);
        }
    }
}


