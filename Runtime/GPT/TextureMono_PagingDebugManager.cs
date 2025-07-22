namespace Eloi.TextureUtility
{
    using System.Collections.Generic;
    using UnityEngine;
    public class TextureMono_PagingDebugManager : MonoBehaviour
    {
        public List<TextureMono_PagingDebugItem> m_pages = new List<TextureMono_PagingDebugItem>();
        public int m_pageIndex = 0;
        public bool m_loopArray = true;

        private void Start()
        {
            UpdatePageDisplay();
        }

        [ContextMenu("Next")]
        public void NextPage()
        {
            if (m_pages.Count == 0) return;

            if (!m_pages[m_pageIndex].IsHidden())
                m_pages[m_pageIndex].Hide();

            m_pageIndex++;
            if (m_pageIndex >= m_pages.Count)
            {
                m_pageIndex = m_loopArray ? 0 : m_pages.Count - 1;
            }

            UpdatePageDisplay();
        }

        [ContextMenu("Previous")]
        public void PreviousPage()
        {
            if (m_pages.Count == 0) return;

            if (!m_pages[m_pageIndex].IsHidden())
                m_pages[m_pageIndex].Hide();

            m_pageIndex--;
            if (m_pageIndex < 0)
            {
                m_pageIndex = m_loopArray ? m_pages.Count - 1 : 0;
            }

            UpdatePageDisplay();
        }


        [ContextMenu("Fetch Pages From Children")]
        public void FetchPagesFromChildren()
        {
            m_pages.Clear();
            m_pages.AddRange(GetComponentsInChildren<TextureMono_PagingDebugItem>(includeInactive: true));

            m_pageIndex = 0;
            UpdatePageDisplay();
        }

        private void UpdatePageDisplay()
        {
            for (int i = 0; i < m_pages.Count; i++)
            {
                if (i == m_pageIndex)
                {
                    if (m_pages[i].IsHidden())
                        m_pages[i].Display();
                }
                else
                {
                    if (!m_pages[i].IsHidden())
                        m_pages[i].Hide();
                }
            }
        }
    }
}


