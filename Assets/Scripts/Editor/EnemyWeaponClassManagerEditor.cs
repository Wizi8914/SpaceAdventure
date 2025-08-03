using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EnemyWeaponClassManager))]
public class EnemyWeaponClassManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        // gap
        GUILayout.Space(10);

        EnemyWeaponClassManager weaponClassManager = (EnemyWeaponClassManager)target;

        if (GUILayout.Button("Update Weapon"))
        {
            weaponClassManager.UpdateWeapon();
        }
    }
}