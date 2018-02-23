using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PhysGunTarget), true)]
public class PhysGunTargetEditor : Editor {

    PhysGunTarget targ;

    public void OnEnable()
    {
        targ = (PhysGunTarget)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUILayout.BeginVertical("Box");

        GUILayout.Label("Extents", EditorStyles.boldLabel);

        EditorGUILayout.BeginHorizontal();

        targ.useDefaultExtents = EditorGUILayout.Toggle(targ.useDefaultExtents, GUILayout.MaxWidth(16));
        GUILayout.Label("Use Default Extents");

        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();

        targ.useTargetedExtents = EditorGUILayout.Toggle(targ.useTargetedExtents, GUILayout.MaxWidth(16));
        GUILayout.Label("Use Targeted Extents");

        EditorGUILayout.EndHorizontal();

        if (targ.useDefaultExtents)
        {
            GUILayout.Label("Default");
            EditorGUILayout.BeginHorizontal();

            targ.extentsDefaultX = EditorGUILayout.FloatField(targ.extentsDefaultX, GUILayout.MaxWidth(48));
            GUILayout.Label("X", GUILayout.MaxWidth(32));

            targ.extentsDefaultXOffset = EditorGUILayout.FloatField(targ.extentsDefaultXOffset, GUILayout.MaxWidth(48));
            GUILayout.Label("Offset");

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();

            targ.extentsDefaultY = EditorGUILayout.FloatField(targ.extentsDefaultY, GUILayout.MaxWidth(48));
            GUILayout.Label("Y", GUILayout.MaxWidth(32));

            targ.extentsDefaultYOffset = EditorGUILayout.FloatField(targ.extentsDefaultYOffset, GUILayout.MaxWidth(48));
            GUILayout.Label("Offset");

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();

            targ.extentsDefaultZ = EditorGUILayout.FloatField(targ.extentsDefaultZ, GUILayout.MaxWidth(48));
            GUILayout.Label("Z", GUILayout.MaxWidth(32));

            targ.extentsZDefaultOffset = EditorGUILayout.FloatField(targ.extentsZDefaultOffset, GUILayout.MaxWidth(48));
            GUILayout.Label("Offset");

            EditorGUILayout.EndHorizontal();
        }

        if (targ.useTargetedExtents)
        {
            GUILayout.Label("Targeted");
            EditorGUILayout.BeginHorizontal();

            targ.extentsTargetedX = EditorGUILayout.FloatField(targ.extentsTargetedX, GUILayout.MaxWidth(48));
            GUILayout.Label("X", GUILayout.MaxWidth(32));

            targ.extentsTargetedXOffset = EditorGUILayout.FloatField(targ.extentsTargetedXOffset, GUILayout.MaxWidth(48));
            GUILayout.Label("Offset");

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();

            targ.extentsTargetedY = EditorGUILayout.FloatField(targ.extentsTargetedY, GUILayout.MaxWidth(48));
            GUILayout.Label("Y", GUILayout.MaxWidth(32));

            targ.extentsTargetedYOffset = EditorGUILayout.FloatField(targ.extentsTargetedYOffset, GUILayout.MaxWidth(48));
            GUILayout.Label("Offset");

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();

            targ.extentsTargetedZ = EditorGUILayout.FloatField(targ.extentsTargetedZ, GUILayout.MaxWidth(48));
            GUILayout.Label("Z", GUILayout.MaxWidth(32));

            targ.extentsZTargetedOffset = EditorGUILayout.FloatField(targ.extentsZTargetedOffset, GUILayout.MaxWidth(48));
            GUILayout.Label("Offset");

            EditorGUILayout.EndHorizontal();
        }

        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginVertical("Box");

        GUILayout.Label("On Start Constraints", EditorStyles.boldLabel);

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Gravity", GUILayout.MinWidth(16), GUILayout.MaxWidth(64));
        targ.useGravityStart = EditorGUILayout.Toggle(targ.useGravityStart, GUILayout.MaxWidth(16));
        EditorUtility.SetDirty(targ);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();

        GUILayout.Label("Position", GUILayout.MinWidth(16), GUILayout.MaxWidth(64));
        targ.movementStartX = EditorGUILayout.Toggle(targ.movementStartX, GUILayout.MaxWidth(16));
        GUILayout.Label("X", GUILayout.MinWidth(32), GUILayout.MaxWidth(32));

        targ.movementStartY = EditorGUILayout.Toggle(targ.movementStartY, GUILayout.MaxWidth(16));
        GUILayout.Label("Y", GUILayout.MinWidth(32), GUILayout.MaxWidth(32));

        targ.movementStartZ = EditorGUILayout.Toggle(targ.movementStartZ, GUILayout.MaxWidth(16));
        GUILayout.Label("Z", GUILayout.MinWidth(32), GUILayout.MaxWidth(32));

        if(GUILayout.Button("All", GUILayout.MaxWidth(40)))
        {
            targ.movementStartX= true;
            targ.movementStartY = true;
            targ.movementStartZ = true;
        }

        if (GUILayout.Button("None", GUILayout.MaxWidth(40)))
        {
            targ.movementStartX = false;
            targ.movementStartY = false;
            targ.movementStartZ = false;
        }

        if (GUILayout.Button("Invert", GUILayout.MaxWidth(48)))
        {
            targ.movementStartX = !targ.movementStartX;
            targ.movementStartY = !targ.movementStartY;
            targ.movementStartZ = !targ.movementStartZ;
        }

        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();

        GUILayout.Label("Rotation", GUILayout.MinWidth(16), GUILayout.MaxWidth(64));
        targ.rotationStartX = EditorGUILayout.Toggle(targ.rotationStartX, GUILayout.MaxWidth(16));
        GUILayout.Label("X", GUILayout.MinWidth(32), GUILayout.MaxWidth(32));

        targ.rotationStartY = EditorGUILayout.Toggle(targ.rotationStartY, GUILayout.MaxWidth(16));
        GUILayout.Label("Y", GUILayout.MinWidth(32), GUILayout.MaxWidth(32));

        targ.rotationStartZ = EditorGUILayout.Toggle(targ.rotationStartZ, GUILayout.MaxWidth(16));
        GUILayout.Label("Z", GUILayout.MinWidth(32), GUILayout.MaxWidth(32));

        if (GUILayout.Button("All", GUILayout.MaxWidth(40)))
        {
            targ.rotationStartX = true;
            targ.rotationStartY = true;
            targ.rotationStartZ = true;
        }

        if (GUILayout.Button("None", GUILayout.MaxWidth(40)))
        {
            targ.rotationStartX = false;
            targ.rotationStartY = false;
            targ.rotationStartZ = false;
        }

        if (GUILayout.Button("Invert", GUILayout.MaxWidth(48)))
        {
            targ.rotationStartX = !targ.rotationStartX;
            targ.rotationStartY = !targ.rotationStartY;
            targ.rotationStartZ = !targ.rotationStartZ;
        }

        EditorGUILayout.EndHorizontal();

        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginVertical("Box");

        GUILayout.Label("On Finish Constraints", EditorStyles.boldLabel);

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Gravity", GUILayout.MinWidth(16), GUILayout.MaxWidth(64));
        targ.useGravityFinish = EditorGUILayout.Toggle(targ.useGravityFinish, GUILayout.MaxWidth(16));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();

        GUILayout.Label("Position", GUILayout.MinWidth(16), GUILayout.MaxWidth(64));
        targ.movementFinishX = EditorGUILayout.Toggle(targ.movementFinishX, GUILayout.MaxWidth(16));
        GUILayout.Label("X", GUILayout.MinWidth(32), GUILayout.MaxWidth(32));

        targ.movementFinishY = EditorGUILayout.Toggle(targ.movementFinishY, GUILayout.MaxWidth(16));
        GUILayout.Label("Y", GUILayout.MinWidth(32), GUILayout.MaxWidth(32));

        targ.movementFinishZ = EditorGUILayout.Toggle(targ.movementFinishZ, GUILayout.MaxWidth(16));
        GUILayout.Label("Z", GUILayout.MinWidth(32), GUILayout.MaxWidth(32));

        if (GUILayout.Button("All", GUILayout.MaxWidth(40)))
        {
            targ.movementFinishX = true;
            targ.movementFinishY = true;
            targ.movementFinishZ = true;
        }

        if (GUILayout.Button("None", GUILayout.MaxWidth(40)))
        {
            targ.movementFinishX = false;
            targ.movementFinishY = false;
            targ.movementFinishZ = false;
        }

        if (GUILayout.Button("Invert", GUILayout.MaxWidth(48)))
        {
            targ.movementFinishX = !targ.movementFinishX;
            targ.movementFinishY = !targ.movementFinishY;
            targ.movementFinishZ = !targ.movementFinishZ;
        }

        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();

        GUILayout.Label("Rotation", GUILayout.MinWidth(16), GUILayout.MaxWidth(64));
        targ.rotationFinishX = EditorGUILayout.Toggle(targ.rotationFinishX, GUILayout.MaxWidth(16));
        GUILayout.Label("X", GUILayout.MinWidth(32), GUILayout.MaxWidth(32));

        targ.rotationFinishY = EditorGUILayout.Toggle(targ.rotationFinishY, GUILayout.MaxWidth(16));
        GUILayout.Label("Y", GUILayout.MinWidth(32), GUILayout.MaxWidth(32));

        targ.rotationFinishZ = EditorGUILayout.Toggle(targ.rotationFinishZ, GUILayout.MaxWidth(16));
        GUILayout.Label("Z", GUILayout.MinWidth(32), GUILayout.MaxWidth(32));

        if (GUILayout.Button("All", GUILayout.MaxWidth(40)))
        {
            targ.rotationFinishX = true;
            targ.rotationFinishY = true;
            targ.rotationFinishZ = true;
        }

        if (GUILayout.Button("None", GUILayout.MaxWidth(40)))
        {
            targ.rotationFinishX = false;
            targ.rotationFinishY = false;
            targ.rotationFinishZ = false;
        }

        if (GUILayout.Button("Invert", GUILayout.MaxWidth(48)))
        {
            targ.rotationFinishX = !targ.rotationFinishX;
            targ.rotationFinishY = !targ.rotationFinishY;
            targ.rotationFinishZ = !targ.rotationFinishZ;
        }

        EditorGUILayout.EndHorizontal();

        EditorGUILayout.EndVertical();
    }
}
