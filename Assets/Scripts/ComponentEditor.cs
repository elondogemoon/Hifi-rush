using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;


[CustomEditor(typeof(AnimationKeyframeExtractor))]
public class ComponentEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // 기본 인스펙터를 렌더링
        DrawDefaultInspector();

        // 버튼 추가
        AnimationKeyframeExtractor myComponent = (AnimationKeyframeExtractor)target;
        if (GUILayout.Button("Run My Function"))
        {
            // 버튼 클릭 시 함수 호출
            myComponent.ExtractKeyframes();
        }
    }
}