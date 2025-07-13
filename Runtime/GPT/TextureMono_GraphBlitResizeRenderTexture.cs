using UnityEngine;
using UnityEngine.Events;


public class TextureMono_GraphBlitResizeRenderTexture : MonoBehaviour
{
    public RenderTexture m_source;
    public RenderTexture m_result;

    public int m_wantedWidth = 1280;
    public int m_wantedHeight = 930;
    public UnityEvent<RenderTexture> m_onCreated;
    public UnityEvent<RenderTexture> m_onUpdated;
    public bool m_useUpdate;

    private void Awake()
    {
        SetRenderTexture(m_source);
    }

    public void SetRenderTexture(RenderTexture source)
    {
        if (source == null)
        {
            m_result = null;
            m_source = null;
            return;
        }

        if (m_result != null)
        {
            if (m_result.width != m_wantedWidth || m_result.height != m_wantedHeight ||
                m_result.format != source.format)
            {
                m_result.Release();
                DestroyImmediate(m_result);
                m_result = null;
            }
        }

        if (m_result == null)
        {
            m_result = new RenderTexture(source.descriptor);
            m_result.width = m_wantedWidth;
            m_result.height = m_wantedHeight;
            m_result.Create();
            m_onCreated?.Invoke(m_result);
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
        if (m_source != null && m_result != null)
        {
            Graphics.Blit(m_source, m_result);
            m_onUpdated?.Invoke(m_result);
        }
    }
}


