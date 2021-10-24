using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor (typeof (IslandManager))]
public class IslandManagerEditor: Editor
{
    void OnSceneGUI()
    {
        IslandManager fow = (IslandManager)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(fow.transform.position, Vector3.forward, Vector3.right, 360, fow.viewRadius);
        Vector3 viewAngleA = fow.DirFromAngle(-fow.viewAngle/2, false);
        Vector3 viewAngleB = fow.DirFromAngle(fow.viewAngle/2, false);

        Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleA * fow.viewRadius);
        Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleB * fow.viewRadius);

        Handles.color = Color.red;
        if (fow.getVisibleTarget() != null)
        {
            for (int i = 0; i < fow.getVisibleTarget().Length; i += 1)
                Handles.DrawLine(fow.transform.position, fow.getVisibleTarget()[i].transform.position);
        }
    }
}
