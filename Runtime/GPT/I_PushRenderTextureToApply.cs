namespace Eloi.TextureUtility
{
    using UnityEngine;

    public interface I_PushRenderTextureToApply {

        public void SetTextureToUse(RenderTexture texture);
        public void SetTextureToUseAndCompute(RenderTexture texture);


    }
}


