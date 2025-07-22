namespace Eloi.TextureUtility
{
    using UnityEngine;

    public class TextureMono_PagingDebugItem : MonoBehaviour
    {
        public GameObject m_target;

        private void Reset()
        {
            m_target = gameObject;
        }

        public bool IsHidden()
        {
            return m_target == null || !m_target.activeSelf;
        }

        [ContextMenu("Display")]
        public void Display()
        {
            if (IsHidden())
            {
                m_target.SetActive(true);
            }
        }

        [ContextMenu("Hide")]
        public void Hide()
        {
            if (!IsHidden())
            {
                m_target.SetActive(false);
            }
        }
    }
}


