using System.Collections.Generic;
using UnityEngine;

public class StarField : MonoBehaviour {
    [Range(1, 100)] // Adjust the range if needed
    [SerializeField] private float starSizeMin = 1f; // Default minimum size
    [Range(1, 100)] // Adjust the range if needed
    [SerializeField] private float starSizeMax = 6f; // Default maximum size

    private List<StarDataLoader.Star> stars;
    private List<GameObject> starObjects;

    private readonly int starFieldScale = 400;

    void Start() {
        // Read in the star data.
        StarDataLoader sdl = new();
        stars = sdl.LoadData();
        starObjects = new();
        foreach (StarDataLoader.Star star in stars) {
            // Create star game objects.
            GameObject stargo = GameObject.CreatePrimitive(PrimitiveType.Quad);
            stargo.transform.parent = transform;
            stargo.name = $"HR {star.catalog_number}";
            stargo.transform.localPosition = star.position * starFieldScale;
            // Use default values for star sizes
            //stargo.transform.localScale = Vector3.one * Mathf.Lerp(starSizeMin, starSizeMax, star.size);
            stargo.transform.LookAt(transform.position);
            stargo.transform.Rotate(0, 180, 0);
            Material material = stargo.GetComponent<MeshRenderer>().material;
            material.shader = Shader.Find("Unlit/StarShader"); //StarShader
            material.SetFloat("_Size", Mathf.Lerp(starSizeMin, starSizeMax, star.size));
            material.color = star.colour;
            starObjects.Add(stargo);
        }
    }

    private void OnValidate() {
        if (starObjects != null) {
            for (int i = 0; i < starObjects.Count; i++) {
                // Update the size set in the shader.
                Material material = starObjects[i].GetComponent<MeshRenderer>().material;
                material.SetFloat("_Size", Mathf.Lerp(starSizeMin, starSizeMax, stars[i].size));
            }
        }
    }
}
