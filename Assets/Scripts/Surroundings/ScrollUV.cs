using UnityEngine;

public class ScrollUV : MonoBehaviour
{   
    void Update()
    {
        // Get the MeshRenderer component attached to the object
        MeshRenderer mr = GetComponent<MeshRenderer>();

        // Get the material applied to the MeshRenderer
        Material mat = mr.material;

        // Get the current offset of the main texture in the material
        Vector2 offset = mat.mainTextureOffset;

        // Increment the y component of the offset to create a scrolling effect
        offset.y += Time.deltaTime / 10.0f;

        // Apply the updated offset back to the material's main texture
        mat.mainTextureOffset = offset;
    }
}
