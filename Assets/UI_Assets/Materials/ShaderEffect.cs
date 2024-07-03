using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderEffect : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] Material shadowMaterial;
    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {

        Graphics.Blit(source, destination,shadowMaterial);
    }
}
