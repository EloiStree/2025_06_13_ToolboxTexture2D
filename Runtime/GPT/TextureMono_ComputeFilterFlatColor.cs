using Eloi.TextureUtility;
using UnityEngine;
using UnityEngine.Events;

public class TextureMono_ComputeFilterFlatColor: MonoBehaviour
{
    public ComputeShader m_computeShader;
    public RenderTexture m_source;
    public RenderTexture m_result;
    public UnityEvent<RenderTexture> m_onCreated;
    public UnityEvent<RenderTexture> m_onUpdated;

    public bool m_inverseGlobal;
    public float m_flatPercentRed = 0.1f;
    public float m_flatPercentGreen = 0.1f;
    public float m_flatPercentBlue = 0.1f;
    public float m_blackPercent = 0.1f;
    public float m_whitePercent = 0.1f;
    public void SetTexture(RenderTexture source)
    {
        m_source = source;
        RenderTextureUtility.CheckThatTextureIsSameSize(ref source, out bool changed, ref m_result);
        if (changed)
        {
            m_onCreated.Invoke(m_result);
        }
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
        if (m_result == null || m_computeShader == null || m_source == null)
            return;

        int kernelIndex = m_computeShader.FindKernel("CSMain");

        m_computeShader.SetTexture(kernelIndex, "m_source", m_source);
        m_computeShader.SetTexture(kernelIndex, "m_result", m_result);
        m_computeShader.SetInt("m_width", m_source.width);
        m_computeShader.SetInt("m_height", m_source.height);
        m_computeShader.SetInt("m_inverseGlobal", m_inverseGlobal ? 1 : 0);
        m_computeShader.SetFloat("m_flatPercentRed", m_flatPercentRed);
        m_computeShader.SetFloat("m_flatPercentGreen", m_flatPercentGreen);
        m_computeShader.SetFloat("m_flatPercentBlue", m_flatPercentBlue);
        m_computeShader.SetFloat("m_blackPercent", m_blackPercent);
        m_computeShader.SetFloat("m_whitePercent", m_whitePercent);
      
        int threadGroupsX = Mathf.CeilToInt(m_source.width / 8.0f);
        int threadGroupsY = Mathf.CeilToInt(m_source.height / 8.0f);

    



        m_computeShader.Dispatch(kernelIndex, threadGroupsX, threadGroupsY, 1);

        m_result.Create();

        m_onUpdated.Invoke(m_result);
    }

}


