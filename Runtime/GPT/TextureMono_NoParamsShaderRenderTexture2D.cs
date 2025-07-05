using UnityEngine;
using UnityEngine.Events;

namespace Eloi.TextureUtility
{
    public class TextureMono_NoParamsShaderRenderTexture2D : MonoBehaviour
    {
        public ComputeShader m_shaderToApply;
        public RenderTexture m_sourceRenderTexture;
        public RenderTexture m_newRendererTexture;
        public UnityEvent<RenderTexture> m_onNewFlippedTextureCreated;

        public void SetRenderTexture(RenderTexture renderTexture)
        {
            m_sourceRenderTexture = renderTexture;
            if (m_sourceRenderTexture != null)
            {
                InitializeRenderTexture();
            }
            else
            {
                m_newRendererTexture = null;
            }
        }

        private void InitializeRenderTexture()
        {
            // Create new if size changed
            if (m_newRendererTexture == null ||
                m_newRendererTexture.width != m_sourceRenderTexture.width ||
                m_newRendererTexture.height != m_sourceRenderTexture.height)
            {
                if (m_newRendererTexture != null)
                {
                    m_newRendererTexture.Release();
                    Destroy(m_newRendererTexture);
                }
                
                m_newRendererTexture = new RenderTexture(m_sourceRenderTexture.descriptor);
                m_newRendererTexture.enableRandomWrite = true;
                m_newRendererTexture.Create();
                m_onNewFlippedTextureCreated?.Invoke(m_newRendererTexture);
            }
        }
        public bool m_useUpdateToRefresh = false;

        public void Update()
        {
            if (m_useUpdateToRefresh)   
            {
                Refresh();
            }
        }
        [ContextMenu("Refresh")]
        public void Refresh()
        {
            if (m_sourceRenderTexture == null || m_shaderToApply == null)
            {
                return;
            }
            InitializeRenderTexture();

            int width = m_sourceRenderTexture.width;
            int height = m_sourceRenderTexture.height;
            int pixelCount = width * height;

            int kernel = m_shaderToApply.FindKernel("CSMain");


            // Set compute shader parameters
            m_shaderToApply.SetTexture(kernel, "m_source", m_sourceRenderTexture);
            m_shaderToApply.SetTexture(kernel, "m_result", m_newRendererTexture);
            m_shaderToApply.SetInt("m_width", width);
            m_shaderToApply.SetInt("m_height", height);
            m_shaderToApply.SetInt("m_pixelCount", pixelCount);

            // Dispatch
            int threadGroupsX = Mathf.CeilToInt(width / 8f);
            int threadGroupsY = Mathf.CeilToInt(height / 8f);
            m_shaderToApply.Dispatch(kernel, threadGroupsX, threadGroupsY, 1);

            // Optional: assign result to a material to visualize
            if (TryGetComponent<Renderer>(out var renderer))
                renderer.material.mainTexture = m_newRendererTexture;
        }
    }
}
