using System;
using Eloi.TextureUtility;
using UnityEngine;
using UnityEngine.Events;

public class TextureMono_ComputeFilterAndOrRGB : MonoBehaviour
{
    public ComputeShader m_computeShader;
    public RenderTexture m_source;
    public RenderTexture m_result;
    public UnityEvent<RenderTexture> m_onCreated;
    public UnityEvent<RenderTexture> m_onUpdated;
   
    public bool m_useRed;
    public bool m_inverseRed;
    public bool m_useGreen;
    public bool m_inverseGreen;
    public bool m_useBlue;
    public bool m_inverseBlue;

    public MultiColorAndOrType m_multiColorConditionType;
    public enum MultiColorAndOrType { And, Or}

    public Color32 m_colorMin;
    public Color32 m_colorMax;

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
        m_computeShader.SetFloat("m_colorMinRed", m_colorMin.r / 255f);
        m_computeShader.SetFloat("m_colorMinGreen", m_colorMin.g / 255f);
        m_computeShader.SetFloat("m_colorMinBlue", m_colorMin.b / 255f);
        m_computeShader.SetFloat("m_colorMaxRed", m_colorMax.r / 255f);
        m_computeShader.SetFloat("m_colorMaxGreen", m_colorMax.g / 255f);
        m_computeShader.SetFloat("m_colorMaxBlue", m_colorMax.b / 255f);

        int threadGroupsX = Mathf.CeilToInt(m_source.width / 8.0f);
        int threadGroupsY = Mathf.CeilToInt(m_source.height / 8.0f);

        m_computeShader.SetInt("m_checkForRed", m_useRed ? 1 : 0);
        m_computeShader.SetInt("m_checkForRedInverse", m_inverseRed ? 1 : 0);

        m_computeShader.SetInt("m_checkForGreen", m_useGreen ? 1 : 0);
        m_computeShader.SetInt("m_checkForGreenInverse", m_inverseGreen ? 1 : 0);

        m_computeShader.SetInt("m_checkForBlue", m_useBlue ? 1 : 0);
        m_computeShader.SetInt("m_checkForBlueInverse", m_inverseBlue ? 1 : 0);
        m_computeShader.SetInt("m_multiColor_or0_and1", m_multiColorConditionType==MultiColorAndOrType.Or ? 0 : 1);

        


        m_computeShader.Dispatch(kernelIndex, threadGroupsX, threadGroupsY, 1);

        m_result.Create();

        m_onUpdated.Invoke(m_result);
    }

}


