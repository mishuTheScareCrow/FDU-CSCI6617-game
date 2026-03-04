#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(WanderRegion))]
public class WanderRegionInspector : Editor
{
    void OnSceneGUI()
    {
        WanderRegion wr = (WanderRegion)target;
        Handles.color = Color.green;
        Handles.DrawWireCube(wr.transform.position, wr.size);
    }
}
#endif
