using UnityEngine;

namespace Eloi.TextureUtility 

{
    public class TextureMono_SetMaterialTexture : MonoBehaviour
    {
        public Material m_material;
        public string m_textureName = "_MainTex";

        [ContextMenu("Set as _MainTex")]
        public void SetNameAsMainTex() => m_textureName = "_MainTex";

        [ContextMenu("Set as _BaseTex")]
        public void SetNameAsBaseTex() => m_textureName = "_BaseTex";


        [ContextMenu("Set as Default")]
        public void SetNameAsDefault() => m_textureName = "";

        public void SetTexture(Texture texture)
        {
            if (m_material == null)
            {
                return;
            }
            SetTextureWithNameInInspector(texture);
        }

        private void SetTextureWithNameInInspector(Texture texture)
        {
            if (string.IsNullOrEmpty(m_textureName))
                m_material.mainTexture = texture;
            else 
                m_material.SetTexture(m_textureName, texture);
        }

        public void SetTexture(Texture2D texture)
        {
            if (m_material == null)
            {
                return;
            }
            SetTextureWithNameInInspector(texture);
        }
        public void SetTexture(WebCamTexture texture)
        {
            if (m_material == null)
            {
                return;
            }
            SetTextureWithNameInInspector(texture);
        }


    }

}