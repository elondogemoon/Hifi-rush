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
        // �⺻ �ν����͸� ������
        DrawDefaultInspector();

        // ��ư �߰�
        AnimationKeyframeExtractor myComponent = (AnimationKeyframeExtractor)target;
        if (GUILayout.Button("Run My Function"))
        {
            // ��ư Ŭ�� �� �Լ� ȣ��
            myComponent.ExtractKeyframes();
        }
    }
}