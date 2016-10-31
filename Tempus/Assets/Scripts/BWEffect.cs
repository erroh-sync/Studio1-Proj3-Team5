using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class BWEffect : MonoBehaviour
{

    #region Singleton Declaration

    public static BWEffect bwEffect;

    #endregion

    #region Serialized Variables

    [SerializeField]
    private float lerpSpeed = 1.0f;

    #endregion

    #region Private Variables

    private float intensity = 0.0f;
    private float targetIntensity = 1.0f;
    private Material material;

    #endregion


    // Creates a private material used to the effect
    void Awake()
    {
        material = new Material(Shader.Find("Hidden/BWDiffuse"));
        bwEffect = this;
    }

    void Update()
    {
        intensity = Mathf.Lerp(intensity, targetIntensity, lerpSpeed * Time.deltaTime);
    }

    // Postprocess the image
    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (intensity == 0)
        {
            Graphics.Blit(source, destination);
            return;
        }

        material.SetFloat("_bwBlend", intensity);
        Graphics.Blit(source, destination, material);
    }

    public void setIntensity(float i)
    {
        targetIntensity = i;
    }
}