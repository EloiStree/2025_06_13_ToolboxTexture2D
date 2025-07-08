using System;
using Unity.Collections;
using UnityEngine;

[System.Serializable]
public class NativeArray2DColor32WH
{
    
    public NativeArray<Color32> m_nativeArray;
    public int m_width;
    public int m_height;

    public void ReCreateIfSizeChanged(int width, int height)
    {
        if (m_nativeArray.IsCreated && (m_width != width || m_height != height))
        {
            m_nativeArray.Dispose();
        }

        if (!m_nativeArray.IsCreated || m_width != width || m_height != height)
        {
            m_nativeArray = new NativeArray<Color32>(width * height, Allocator.Persistent);
            m_width = width;
            m_height = height;
        }
    }

    public void CopyNativeArray(NativeArray<Color32> source, int width, int height)
    {
        if (!m_nativeArray.IsCreated || m_nativeArray.Length != source.Length)
        {
            ReCreateIfSizeChanged(width, height);
        }

        NativeArray<Color32>.Copy(source, m_nativeArray, source.Length);
    }
    public void GetNativeArray(out NativeArray<Color32> nativeArray, out int width, out int height)
    {
        nativeArray = m_nativeArray;
        width = m_width;
        height = m_height;
    }
    public void GetNativeArray(out NativeArray<Color32> nativeArray)
    {
        nativeArray = m_nativeArray;
    }
    public void GetNativeArray(out int width, out int height)
    {
        width = m_width;
        height = m_height;
    }
    public void GetHeight( out int height)
    {
        height = m_height;
    }
    public void GetWidth(out int width)
    {
        width = m_width;
    }


    public void SetNativeArray(NativeArray<Color32> nativeArray, int width, int height)
    {
        m_nativeArray = nativeArray;
        m_width = width;
        m_height = height;
    }

    public void GetColorAtIndex1D(int index, out bool found, out Color32 foundColor)
    {
        found = false;
        foundColor = new Color32(0, 0, 0, 0);
        if (index < 0 || index >= m_nativeArray.Length) return;
        found = true;
        foundColor = m_nativeArray[index];
    }
    public void GetColorAtIndexLRTD2D(int leftRight, int topDown, out bool found, out Color32 foundColor)
    {
        found = false;
        foundColor = new Color32(0, 0, 0, 0);
        if (leftRight < 0 || leftRight >= m_width || topDown < 0 || topDown >= m_height) return;
        found = true;
        foundColor = m_nativeArray[leftRight + ((m_height - 1 - topDown )* m_width)];




    }
    public void GetColorAtIndexLRDT2D(int leftRight, int downTop, out bool found, out Color32 foundColor)
    {


        found = false;
        foundColor = new Color32(0, 0, 0, 0);
        if (leftRight < 0 || leftRight >= m_width || downTop < 0 || downTop >= m_height) return;
        found = true;
        foundColor = m_nativeArray[leftRight + (downTop * m_width)];

    }


    public void GetColorAtIndexRLTD2D(int rightLeft, int topDown, out bool found, out Color32 foundColor)
    {
        int leftRight = m_width - 1 - rightLeft;
        GetColorAtIndexLRTD2D(leftRight, topDown, out found, out foundColor);
    }
    public void GetColorAtIndexRLDT2D(int rightLeft, int downTop, out bool found, out Color32 foundColor)
    {
        int leftRight = m_width - 1 - rightLeft;
        GetColorAtIndexLRDT2D(leftRight, downTop, out found, out foundColor);
    }


    public void GetPixelAtPercentLRTD(float percentLeftRight, float percentTopDown, out bool found, out Color32 foundColor)
    {
        GetColorAtIndexLRTD2D(Mathf.RoundToInt(percentLeftRight * (m_width - 1)), Mathf.RoundToInt(percentTopDown * (m_height - 1)), out found, out foundColor);
    }

    public void GetPixelAtPercentRLTD(float percentRightLeft, float percentTopDown, out bool found, out Color32 foundColor)
    {
        GetColorAtIndexRLTD2D(Mathf.RoundToInt(percentRightLeft * (m_width - 1)), Mathf.RoundToInt(percentTopDown * (m_height - 1)), out found, out foundColor);
    }


    public void GetPixelAtPercentLRDT(float percentLeftRight, float percentDownTop, out bool found, out Color32 foundColor)
    {
        GetColorAtIndexLRDT2D(Mathf.RoundToInt(percentLeftRight * (m_width - 1)), Mathf.RoundToInt(percentDownTop * (m_height - 1)), out found, out foundColor);
    }

    public void GetPixelAtPercentRLDT(float percentRightLeft, float percentDownTop, out bool found, out Color32 foundColor)
    {
        GetColorAtIndexRLDT2D(Mathf.RoundToInt(percentRightLeft * (m_width - 1)), Mathf.RoundToInt(percentDownTop * (m_height - 1)), out found, out foundColor);
    }

}




