using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Planet))]
public class PlanetEditor : Editor
{
    private Planet planet;

    private Editor shapeEditor;
    private Editor climateEditor;

    void OnEnable()
    {
        planet = target as Planet;
    }
    
    public override void OnInspectorGUI()
    {
        using (var check = new EditorGUI.ChangeCheckScope())
        {
            base.OnInspectorGUI();
        }

        DrawSettingsEditor(planet.shapeSettings, ref planet.shapeSettingsFoldout, ref shapeEditor);
        DrawSettingsEditor(planet.climateSettings, ref planet.climateSettingsFoldout, ref climateEditor);
    }

    void DrawSettingsEditor(Object _settings, ref bool _foldout, ref Editor _editor)
    {
        if (_settings != null)
        {
            _foldout = EditorGUILayout.InspectorTitlebar(_foldout, _settings);
            
            using (var check = new EditorGUI.ChangeCheckScope())
            {
                if (_foldout)
                {
                    CreateCachedEditor(_settings, null, ref _editor);
                    
                    _editor.OnInspectorGUI();
                }
            }
        }
    }
}