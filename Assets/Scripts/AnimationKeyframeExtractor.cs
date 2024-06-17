using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AnimationKeyframeExtractor : MonoBehaviour
{
    //[MenuItem("Tools/Extract Keyframes")]

    [SerializeField]
    AnimationClip sourceClip;
    public void ExtractKeyframes()
    {
        if (sourceClip == null)
        {
            Debug.LogError("Animation clip not found.");
            return;
        }

        // 추출할 경로 설정 (여기서는 "YourObjectPath"로 가정)
        string path = "Assets/Clip";  // 대상 오브젝트의 경로

        

        AnimationClip rootMotionClip = new AnimationClip();
        EditorCurveBinding[] curveBindings = AnimationUtility.GetCurveBindings(sourceClip);

        List<AnimationCurve> rootPositionCurves = new List<AnimationCurve> {
            new AnimationCurve(), // x-axis
            new AnimationCurve(), // y-axis
            new AnimationCurve()  // z-axis
        };

        foreach (var binding in curveBindings)
        {
            if (binding.path == "YourObjectPath" && binding.propertyName.StartsWith("m_LocalPosition"))
            {
                AnimationCurve sourceCurve = AnimationUtility.GetEditorCurve(sourceClip, binding);
                int index = binding.propertyName == "m_LocalPosition.x" ? 0 :
                            binding.propertyName == "m_LocalPosition.y" ? 1 : 2;

                rootPositionCurves[index] = sourceCurve;
            }
        }
        EditorCurveBinding rootMotionBindingX = new EditorCurveBinding
        {
            path = "",
            type = typeof(Animator),
            propertyName = "RootT.x"
        };

        EditorCurveBinding rootMotionBindingY = new EditorCurveBinding
        {
            path = "",
            type = typeof(Animator),
            propertyName = "RootT.y"
        };

        EditorCurveBinding rootMotionBindingZ = new EditorCurveBinding
        {
            path = "",
            type = typeof(Animator),
            propertyName = "RootT.z"
        };

        AnimationUtility.SetEditorCurve(rootMotionClip, rootMotionBindingX, rootPositionCurves[0]);
        AnimationUtility.SetEditorCurve(rootMotionClip, rootMotionBindingY, rootPositionCurves[1]);
        AnimationUtility.SetEditorCurve(rootMotionClip, rootMotionBindingZ, rootPositionCurves[2]);

        AssetDatabase.CreateAsset(rootMotionClip, "Assets/RootMotionClip.anim");
        AssetDatabase.SaveAssets();
    }
}

