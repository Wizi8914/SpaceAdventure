using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(WeaponBoxWeaponDisplay))]
public class WeaponBoxWeaponDisplayEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        WeaponBoxWeaponDisplay weaponBox = (WeaponBoxWeaponDisplay)target;

        if (GUILayout.Button("Update Weapon Display"))
        {
            weaponBox.RefreshDisplay();
        }
    }
}
