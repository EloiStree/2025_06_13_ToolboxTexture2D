using UnityEngine;


namespace Eloi.TextureUtility
{
    public class RenderTextureUtility {

        public static void CheckThatTextureIsSameSize(
            ref RenderTexture source, out bool changed ,ref RenderTexture result)
            {
                    changed=false;
                    if (source != null)
                    {
                        int width = source.width;
                        int height = source.height;
                        if (result == null ||width != result.width || height != result.height)
                        {
                            if (result != null)
                                result.Release();

                            result = new RenderTexture(
                                source.width,
                                source.height,
                                source.depth,
                                source.format // Use source.graphicsFormat if you're on newer Unity
                            );

                            result.dimension = source.dimension;
                            result.volumeDepth = source.volumeDepth;
                            result.antiAliasing = source.antiAliasing;
                            result.useMipMap = source.useMipMap;
                            result.autoGenerateMips = source.autoGenerateMips;
                            result.enableRandomWrite = source.enableRandomWrite;
                            result.wrapMode = source.wrapMode;
                            result.filterMode = source.filterMode;
                            result.anisoLevel = source.anisoLevel;
                            result.Create();
                            changed=true;
                        }
                    }
        }
            
    }
}
