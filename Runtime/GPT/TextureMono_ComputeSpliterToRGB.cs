using UnityEngine;
using UnityEngine.Events;

public class TextureMono_ComputeSpliterToRGB : MonoBehaviour
{
    public ComputeShader m_computeShader;
    public RenderTexture m_source;
    public RenderTexture m_resultRed;
    public RenderTexture m_resultGreen;
    public RenderTexture m_resultBlue;
    public UnityEvent<RenderTexture> m_onCreatedRed;
    public UnityEvent<RenderTexture> m_onCreatedGreen;
    public UnityEvent<RenderTexture> m_onCreatedBlue;


    public void SetTexture(RenderTexture source)
    {
        m_source = source;

        if (m_resultRed == null  ||
            m_resultRed.width != source.width||
            m_resultRed.height != source.height)
        {
            if (m_resultRed)
                m_resultRed.Release();
            if (m_resultGreen)
                m_resultGreen.Release();
            if (m_resultBlue)
                m_resultBlue.Release();

            SetTextureDescriptor(source.descriptor, out m_resultRed);
            SetTextureDescriptor(source.descriptor, out m_resultGreen);
            SetTextureDescriptor(source.descriptor, out m_resultBlue);
            m_onCreatedRed.Invoke(m_resultRed);
            m_onCreatedGreen.Invoke(m_resultGreen);
            m_onCreatedBlue.Invoke(m_resultBlue);

        }
    }

    private void SetTextureDescriptor(RenderTextureDescriptor descriptor, out RenderTexture result)
    {
        RenderTexture rt = new RenderTexture(descriptor);
        rt.filterMode = FilterMode.Point;
        rt.wrapMode = TextureWrapMode.Clamp;
        rt.width = descriptor.width;
        rt.height = descriptor.height;
        rt.Create();
        result = rt;
    }

    [ContextMenu("Recompute Current")]
    public void RecomputeCurrentTexture()
    {
        SetTextureAndCompute(m_source);
    }

    public void SetTextureAndCompute(RenderTexture source)
    {
        SetTexture(source);
        ComputeTheTexture();
    }

    public bool m_useUpdate = true;

    private void Update()
    {
        if (m_useUpdate)
            ComputeTheTexture();
    }

    public void ComputeTheTexture()
    {
        if ( m_computeShader == null || m_source == null)
            return;

        int kernelIndex = m_computeShader.FindKernel("CSMain");

        m_computeShader.SetTexture(kernelIndex, "m_source", m_source);
        m_computeShader.SetTexture(kernelIndex, "m_resultRed", m_resultRed);
        m_computeShader.SetTexture(kernelIndex, "m_resultGreen", m_resultGreen);
        m_computeShader.SetTexture(kernelIndex, "m_resultBlue", m_resultBlue);
        m_computeShader.SetInt("m_width", m_source.width);
        m_computeShader.SetInt("m_height", m_source.height);
      

        int threadGroupsX = Mathf.CeilToInt(m_source.width / 8.0f);
        int threadGroupsY = Mathf.CeilToInt(m_source.height / 8.0f);

        m_computeShader.Dispatch(kernelIndex, threadGroupsX, threadGroupsY, 1);

    }

}


