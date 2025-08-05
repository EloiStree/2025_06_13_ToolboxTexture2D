using UnityEngine;

public class TextureMono_NativeColor32ToTexture2D : MonoBehaviour {


    public NativeArray2DColor32WH m_source;
    public Texture2D m_result;
    public void SetFromNativeColor32(NativeArray2DColor32WH given) {

        if (m_source != given) {

            m_source = given;

            bool sizeChange = m_result==null|| given.m_width != m_result.width || given.m_height != m_result.height;
            if (sizeChange) {

                if (m_result != null)
                    DestroyImmediate(m_result);

                m_result = new Texture2D(given.m_width, given.m_height, TextureFormat.ARGB32, false, false);
                m_result.filterMode = FilterMode.Point;
                m_result.wrapMode = TextureWrapMode.Clamp;
            }

            Refresh();
        }
    }

    [ContextMenu("Refresh for debug")]
    public void Refresh() {

        // Protect against null or uninitialized m_source or m_source.m_nativeArray
        if (m_source == null || !m_source.m_nativeArray.IsCreated)
            return;

        m_result.SetPixels32(m_source.m_nativeArray.ToArray());
        m_result.Apply();

    }
}
