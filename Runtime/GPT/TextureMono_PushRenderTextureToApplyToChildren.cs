using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Eloi.TextureUtility
{
    public class TextureMono_PushRenderTextureToApplyToChildren : MonoBehaviour, I_PushRenderTextureToApply
    {
        public Transform m_target;
        public List<I_PushRenderTextureToApply> m_childrenInterface = new();

        public int m_interfaceCountFound;
        public RenderTexture m_lastReceived;
        public bool m_refreshAtAwake=true;
        private void Reset()
        {
            m_target = transform;
            RefreshTheList();
        }

        private void RefreshTheList()
        {
            m_childrenInterface.Clear();
            if (m_target == null)
                return;

            I_PushRenderTextureToApply[] list =
                m_target.GetComponentsInChildren<I_PushRenderTextureToApply>(includeInactive: true);

            I_PushRenderTextureToApply thisScript = this;
            list = list.Where(x => x != null && x != thisScript).Distinct().ToArray();
            m_interfaceCountFound = list.Length;
            m_childrenInterface.AddRange(list);
        }
        private void Awake()
        {
            if (m_refreshAtAwake)
                RefreshTheList();
        }

        public void SetTextureToUse(RenderTexture texture)
        {
            m_lastReceived = texture;
            foreach (var child in m_childrenInterface)
            {
                if (child!=null)
                    child.SetTextureToUse(texture);
            }

        }

        public void SetTextureToUseAndCompute(RenderTexture texture)
        {
            m_lastReceived = texture;
            foreach (var child in m_childrenInterface)
            {
                if (child != null)
                    child.SetTextureToUseAndCompute(texture);
            }
        }
    }
}


