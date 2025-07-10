using UnityEngine;
using UnityEngine.Events;


namespace Eloi.TextureUtility
{
    public class TextureMono_KeepCrowdedPixelWithCompute : MonoBehaviour
    {
        public ComputeShader m_computeShader;
        public RenderTexture m_source;
        public RenderTexture m_result;
        public UnityEvent<RenderTexture> m_onCreated;
        public UnityEvent<RenderTexture> m_onUpdated;
       
        public int m_squareRadius = 5;
        public int m_minToExist = 12;

        public bool m_removeSinglePixel = true;


        public int m_totalPixels;
        public int m_halfPixels;

        private void OnValidate()
        {
            m_totalPixels = (m_squareRadius * 2 + 1) *(m_squareRadius * 2 + 1);
            m_halfPixels = m_totalPixels /2;
        }

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
            m_computeShader.SetInt("m_removeSinglePixel", m_removeSinglePixel ? 1 : 0);

            m_computeShader.SetInt("m_squareRadius", m_squareRadius);
            m_computeShader.SetInt("m_minToExist", m_minToExist);


            int threadGroupsX = Mathf.CeilToInt(m_source.width / 8.0f);
            int threadGroupsY = Mathf.CeilToInt(m_source.height / 8.0f);

            m_computeShader.Dispatch(kernelIndex, threadGroupsX, threadGroupsY, 1);

            m_result.Create();

            m_onUpdated.Invoke(m_result);
        }
    }
}
