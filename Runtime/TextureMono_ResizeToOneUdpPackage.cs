using System;
using UnityEngine;
using UnityEngine.UI;

namespace Eloi.TextureUtility {

public class TextureMono_ResizeToOneUdpPackage : MonoBehaviour
{

    public Color32[] m_color32ToInWebcam;
    public Color32[] m_color32ToFitInUdp;

    public Texture2D m_producedTexture2D;

    public bool m_mipmap = false;
    public bool m_linear = false;
    public bool m_useUdp = true;


    [Header("For info")]
    public int m_widthExpectedQuest3 = 1280;
    public int m_heightExpectedQuest3 = 930;
    public int m_widthToFitInUdp = 128;
    public int m_heightToFitInUdp = 93;


    [Header("For info, not used in this script")]

    public int m_sourcePixelCounts;
    public int m_fitBuildPixelCounts;
    public int m_rgbaSourceInBytes;
    public int m_rgbaToFitInUdpInBytes;
    public int m_rgbToFitInUdpInBytes;
    public bool m_toMuchForOneUdpPackage;


        private void OnValidate()
        {
            RefreshInfoFromSource();
        }

        private void RefreshInfoFromSource()
        {
            m_sourcePixelCounts = m_color32ToInWebcam.Length;
            m_fitBuildPixelCounts = m_color32ToFitInUdp.Length;
            m_rgbaSourceInBytes = m_sourcePixelCounts * 4;
            m_rgbaToFitInUdpInBytes = m_fitBuildPixelCounts * 4;
            m_rgbToFitInUdpInBytes = m_fitBuildPixelCounts *3;
            m_toMuchForOneUdpPackage = m_rgbToFitInUdpInBytes > (65507 - 8);

        }

        void Update()
    {
        if (m_useUdp) {
            ConvertWebcamToColor32();
        }
    }

    public void SetColor2D32(int width,int height  , Color32 webcamTexture)
    {

        m_heightExpectedQuest3 = height;
        m_widthExpectedQuest3 = width;

        if (m_color32ToInWebcam == null || m_color32ToInWebcam.Length != height * width)
        {
            m_color32ToInWebcam = new Color32[height * width];
        }
        for (int i = 0; i < m_color32ToInWebcam.Length; i++)
        {
            m_color32ToInWebcam[i] = webcamTexture;
        }
        m_rgbaSourceInBytes = m_color32ToInWebcam.Length + 4; // +4 for the header

            RefreshInfoFromSource();
    }

    private void ConvertWebcamToColor32()
    {
     
        if (m_color32ToFitInUdp == null || m_color32ToFitInUdp.Length != m_widthToFitInUdp * m_heightToFitInUdp)
        {
            m_color32ToFitInUdp = new Color32[m_widthToFitInUdp * m_heightToFitInUdp];
        }
        // Get the pixels from the webcam texture
        m_rgbaSourceInBytes = m_color32ToInWebcam.Length +4;
        // make average of color32ToInWebcam in fitinudp color array by merging pixels
        int widthWebcam = m_widthExpectedQuest3;
        int heightWebcam = m_heightExpectedQuest3;
        int widthToFit = m_widthToFitInUdp;
        int heightToFit = m_heightToFitInUdp;
        for (int y = 0; y < heightToFit; y++)
        {
            for (int x = 0; x < widthToFit; x++)
            {
                int srcX = x * widthWebcam / widthToFit;
                int srcY = y * heightWebcam / heightToFit;
                int indexSrc = srcY * widthWebcam + srcX;
                Color32 pixelColor = m_color32ToInWebcam[indexSrc];
                m_color32ToFitInUdp[y * widthToFit + x] = pixelColor;
            }
        }
        if (m_producedTexture2D == null || m_producedTexture2D.width != widthToFit || m_producedTexture2D.height != heightToFit)
        {
            m_producedTexture2D = new Texture2D(widthToFit, heightToFit, TextureFormat.RGBA32, m_mipmap,m_linear);
            m_producedTexture2D.filterMode = FilterMode.Point;
            m_producedTexture2D.wrapMode = TextureWrapMode.Clamp;
            m_producedTexture2D.Apply();
            m_rgbaToFitInUdpInBytes = m_producedTexture2D.width * m_producedTexture2D.height * 4; // RGBA32 has 4 bytes per pixel
            m_rgbToFitInUdpInBytes = m_producedTexture2D.width * m_producedTexture2D.height * 3; // RGB has 3 bytes per pixel

            m_fitBuildPixelCounts = m_producedTexture2D.width * m_producedTexture2D.height;
            m_sourcePixelCounts = m_color32ToInWebcam.Length;
        }
        m_producedTexture2D.SetPixels32(m_color32ToFitInUdp);
        m_producedTexture2D.Apply();
    }
}

}