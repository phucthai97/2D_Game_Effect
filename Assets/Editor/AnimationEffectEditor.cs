using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AnimationEffect))]
public class AnimationEffectEditor : Editor
{
    private bool _showHelp = false;
    
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        MonoScript scriptPath = MonoScript.FromMonoBehaviour((AnimationEffect)target);

        EditorGUI.BeginDisabledGroup(true);
        EditorGUILayout.ObjectField("Script", scriptPath, typeof(MonoScript), false);
        EditorGUI.EndDisabledGroup();

        // Bắt đầu một hàng ngang mới
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace(); // Tạo khoảng trống linh hoạt ở bên trái
        // Nút để toggle HelpBox
        if (GUILayout.Button("Toggle Help Guide", GUILayout.Width(200)))
            _showHelp = !_showHelp; // Đảo ngược trạng thái hiển thị của HelpBox

        GUILayout.FlexibleSpace(); // Tạo khoảng trống linh hoạt ở bên phải
        GUILayout.EndHorizontal(); // Kết thúc hàng ngang

        // Hiển thị HelpBox nếu showHelp là true
        if (_showHelp)
        {
            EditorGUILayout.HelpBox("Follow these steps to make sure everything runs fine.\n1. Download & import Dotween.\n2. Add this element to the object you want to animate. \n3. Choose Effect Type", MessageType.Info);
        }
        EditorGUILayout.Space(4);
        
        AnimationEffect script = (AnimationEffect)target;

        // Enum popup
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Animation Settings", EditorStyles.boldLabel);
        script.Type = (AnimationEffect.AnimaEffectType)EditorGUILayout.EnumPopup("Effect Type", script.Type);

        EditorGUILayout.Space(2);
        DrawSeparator(Color.gray);
        EditorGUILayout.Space(2);

        // Show fields based on selected EffectType
        switch (script.Type)
        {
            case AnimationEffect.AnimaEffectType.Rotationz:
                EditorGUILayout.PropertyField(serializedObject.FindProperty("_duration"), new GUIContent("Duration"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("_reverseRotation"), new GUIContent("Reverse"));
                break;
            case AnimationEffect.AnimaEffectType.ScaleInOut:
                EditorGUILayout.PropertyField(serializedObject.FindProperty("_duration"), new GUIContent("Duration"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("_scaleFactor"), new GUIContent("ScaleFactor"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("_aniCurveScaleOutIn"), new GUIContent("AnimationCurveScaleOutIn"));
                break;
            case AnimationEffect.AnimaEffectType.StetchScale:
                EditorGUILayout.PropertyField(serializedObject.FindProperty("_duration"), new GUIContent("Duration"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("_scaleFactorX"), new GUIContent("ScaleFactorX"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("_scaleFactorY"), new GUIContent("ScaleFactorY"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("_aniCurveStrectch"), new GUIContent("AniCurveStrectch"));
                break;
        }
        serializedObject.ApplyModifiedProperties();
    }

    // Hàm vẽ đường gạch ngang với màu tùy chỉnh
    private void DrawSeparator(Color color)
    {
        Rect rect = EditorGUILayout.GetControlRect(false, 1);
        rect.height = 1.2f;
        EditorGUI.DrawRect(rect, color);
    }
}
