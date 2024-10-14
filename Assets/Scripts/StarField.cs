using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;
using TMPro;

public class StarField : MonoBehaviour {
    [Range(0, 100)]
    [SerializeField] private float starSizeMin = 0f;
    [Range(0, 100)]
    [SerializeField] private float starSizeMax = 5f;
    [SerializeField]
    private Camera _camera;
    private List<StarDataLoader.Star> stars;
    private List<GameObject> starObjects;
    private Dictionary<int, GameObject> constellationVisible = new();
    private InputData _inputData;
    private bool wasAButtonPressed = false;
    public float rotationSpeed = 30f;
    public float smoothTime = 0.2f;  // Controls how quickly the easing happens

    private Vector2 currentJoystickValue = Vector2.zero;
    private Vector2 smoothVelocity = Vector2.zero;

    private readonly int starFieldScale = 400;

    void Start() {
        // This is to retrieve the InputData script in order to communicate with the quest controllers
        _inputData = GetComponent<InputData>();
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
            //stargo.transform.localScale = Vector3.one * Mathf.Lerp(starSizeMin, starSizeMax, star.size);
            stargo.transform.LookAt(transform.position);
            stargo.transform.Rotate(0, 180, 0);
            Material material = stargo.GetComponent<MeshRenderer>().material;
            material.shader = Shader.Find("Unlit/StarShader");
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

    // A constellation is a tuple of the stars and the lines that join them.
    private readonly List<(int[], int[])> constellations = new() {
        // Aquarius
        (new int[] { 7950, 8232, 8414, 8499, 8418, 8518, 8558, 8597, 8834, 8698, 8679, 8812 },
        new int[] { 7950, 8232, 8232, 8414, 8414, 8499, 8499, 8418, 8414, 8518, 8518, 8558, 8597, 8834, 8834, 8698, 8698, 8679, 8679, 8812 }),
        // Aries
        (new int[] { 838, 617, 553, 545 },
        new int[] { 838, 617, 617, 553, 553, 545 }),
        // Cancer
        (new int[] {3475, 3449, 3461, 3572, 3249},
        new int[] {3475, 3449, 3449, 3461, 3461, 3572, 3461, 3249}),
        // Capricorn 
        (new int[] { 7754, 7776, 7936, 7980, 8080, 8204, 8213, 8322, 8167, 8075 },
        new int[] { 7754, 7776, 7776, 7936, 7936, 7980, 7980, 8080, 8080, 8204, 8204, 8213, 8213, 8322, 8322, 8167, 8167, 8075, 8075, 7776 }),
        // Centaurus 
        (new int[] { 4467, 4390, 4638, 4621, 4743, 4819, 5132, 5267, 5459, 5231, 5193, 5190, 5440, 5576, 5028, 5288 },
         new int[] { 4467, 4390, 4390, 4638, 4638, 4621, 4621, 4743, 4743, 4819, 4819, 5132, 5132, 5267, 5132, 5459, 5231, 5231, 5231, 5193, 5193, 5190, 5190, 5028, 5190, 5288, 5190, 5440, 5440, 5576 }),
        // Cygnus 
        (new int[] { 8115, 7949, 7796, 7528, 7469, 7420, 7328, 7924, 7615, 7417 },
        new int[] { 8115, 7949, 7949, 7796, 7796, 7924, 7796, 7528, 7528, 7469, 7469, 7420, 7420, 7328, 7796, 7615, 7615, 7417 }),
        // Gemini
        (new int[] { 2484, 2421, 2650, 2777, 2985, 2990, 2891, 2852, 2697, 2473, 2286, 2216},
        new int[] { 2484, 2421, 2421, 2650, 2650, 2777, 2777, 2985, 2985, 2990, 2990, 2891, 2891, 2852, 2852, 2697, 2697, 2473, 2473, 2286, 2286, 2216}),
        // Hydra 
        (new int[] { 5287, 5020, 4552, 4450, 4232, 4094, 3994, 3903, 3748, 3845, 3665, 3547, 3482, 3410, 3418, 3454 },
        new int[] { 5287, 5020, 5020, 4552, 4552, 4450, 4450, 4232, 4232, 4094, 4094, 3994, 3994, 3903, 3903, 3748, 3748, 3845, 3845, 3665, 3665, 3547, 3547, 3454, 3454, 3418, 3418, 3410, 3410, 3482, 3482, 3547 }),
        // Leo
        (new int[] { 3982, 4534, 4057, 4357, 3873, 4031, 4359, 3975, 4399, 4386, 3905, 3773, 3731 },
        new int[] { 4534, 4357, 4534, 4359, 4357, 4359, 4357, 4057, 4057, 4031, 4057, 3975, 3975, 3982, 3975, 4359, 4359, 4399, 4399, 4386, 4031, 3905, 3905, 3873, 3873, 3975, 3873, 3773, 3773, 3731, 3731, 3905 }),
        // Leo Minor
        (new int[] { 3800, 3974, 4100, 4247, 4090 },
        new int[] { 3800, 3974, 3974, 4100, 4100, 4247, 4247, 4090, 4090, 3974 }),
        // Libra 
        (new int[] { 5908, 5787, 5685, 5531, 5603, 5794, 5812 },
        new int[] { 5908, 5787, 5787, 5685, 5685, 5531, 5531, 5603, 5603, 5794, 5794, 5812, 5603, 5685 }),
        // Lynx
        (new int[] { 3705, 3690, 3612, 3579, 3275, 2818, 2560, 2238 },
        new int[] { 3705, 3690, 3690, 3612, 3612, 3579, 3579, 3275, 3275, 2818, 2818, 2560, 2560, 2238 }),
        // Monceros
        (new int[] { 2970, 3188, 2714, 2356, 2227, 2506, 2298, 2385, 2456, 2479 },
        new int[] { 2970, 3188, 3188, 2714, 2714, 2356, 2356, 2227, 2714, 2506, 2506, 2298, 2298, 2385, 2385, 2456, 2479, 2506, 2479, 2385 }),
        // Orion
        (new int[] { 1948, 1903, 1852, 2004, 1713, 2061, 1790, 1907, 2124, 2199, 2135, 2047, 2159, 1543, 1544, 1570, 1552, 1567 },
        new int[] { 1713, 2004, 1713, 1852, 1852, 1790, 1852, 1903, 1903, 1948, 1948, 2061, 1948, 2004, 1790, 1907, 1907, 2061, 2061, 2124, 2124, 2199, 2199, 2135, 2199, 2159, 2159, 2047, 1790, 1543, 1543, 1544, 1544, 1570, 1543, 1552, 1552, 1567, 2135, 2047 }),
        // Pisces 
        (new int[] { 352, 383, 360, 437, 510, 595, 489, 434, 294, 224, 9072, 8969, 8916, 8878, 8852, 8911, 8984, 9004},
        new int[] { 352, 383, 383, 360, 360, 437, 437, 510, 510, 595, 595, 489, 489, 434, 434, 294, 294, 224, 224, 9072, 9072, 8969, 8969, 8916, 8916, 8878, 8878, 8852, 8852, 8911, 8911, 8984, 8984, 9004, 9004, 8969 }),
        // Sagittarius 
        (new int[] { 7348, 7337, 7581, 7623, 7650, 7604, 7440, 7234, 7194, 7039, 7121, 7217, 7150, 7264, 7304, 7340, 6913, 6812, 6859, 6746, 6616, 6879, 6832 },
         new int[] { 7348, 7581,7337, 7581, 7581, 7623, 7623, 7650, 7650, 7604, 7604, 7440, 7440, 7234, 7234, 7194, 7194, 7039, 7234, 7121, 7121, 7039, 7121, 7217, 7217, 7150, 7217, 7264, 7264, 7304, 7304, 7340, 7039, 6913, 6913, 6812, 6913, 6859, 6859, 6746, 6746, 6616, 6859, 6879, 6879, 6832 }),
        // Scorpius
        (new int[] { 6527, 6580, 6615, 6553, 6380, 6271, 6247, 6241, 6165, 6134, 6084, 5928, 5944, 5953, 5984, 6027},
        new int[] { 6527, 6580, 6580, 6615, 6615, 6553, 6553, 6380, 6380, 6271, 6271, 6247, 6247, 6241, 6241, 6165, 6165, 6134, 6134, 6084, 6084, 5953, 5928, 5944, 5944, 5953, 5953, 5984, 5984, 6027}),
        // Southern Crux
        (new int[] { 4730, 4763, 4853, 4656},
        new int[] { 4853, 4656, 4730, 4763}),
        // Taurus 
        (new int[] { 1791, 1497, 1409, 1373, 1346, 1910, 1457, 1412, 1239, 1038, 1030 },
        new int[] { 1791, 1497, 1910, 1457, 1497, 1409, 1457, 1412, 1409, 1373, 1412, 1346, 1373, 1346, 1346, 1239, 1239, 1038, 1038, 1030 }),
        // Ursa Major
        (new int[] { 3569, 3594, 3775, 3888, 3323, 3757, 4301, 4295, 4554, 4660, 4905, 5054, 5191, 4518, 4335, 4069, 4033, 4377, 4375 },
        new int[] { 3569, 3594, 3594, 3775, 3775, 3888, 3888, 3323, 3323, 3757, 3757, 3888, 3757, 4301, 4301, 4295, 4295, 3888, 4295, 4554, 4554, 4660, 4660, 4301, 4660, 4905, 4905, 5054, 5054, 5191, 4554, 4518, 4518, 4335, 4335, 4069, 4069, 4033, 4518, 4377, 4377, 4375 }),
        // Virgo 
        (new int[] { 4932, 4910, 4825, 4689, 4540, 4963, 5056, 5315, 5107, 5264, 5511 },
        new int[] { 4932, 4910, 4910, 4825, 4825, 4689, 4689, 4540, 4825, 4963, 4910, 5107, 4963, 5056, 5056, 5107, 5056, 5315, 5107, 5264, 5264, 5511 }),
    };

    private void Update() {
        this.transform.position = _camera.transform.position;
        if (_inputData._rightController.TryGetFeatureValue(CommonUsages.primaryButton, out bool Abutton)) {
            if (Abutton && !wasAButtonPressed) {
                for (int i = 0; i < constellations.Count; i++) {
                    ToggleConstellation(i);
                }
            }
            // Update the previous state to the current state
            wasAButtonPressed = Abutton;
        }

        if (_inputData._rightController.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 joystickValue)) {
            // Smoothly interpolate joystick input to avoid abrupt jolts
            currentJoystickValue = Vector2.SmoothDamp(currentJoystickValue, joystickValue, ref smoothVelocity, smoothTime);

            // Get camera's right and up directions for intuitive rotation
            Vector3 right = _camera.transform.right;
            Vector3 up = _camera.transform.up;

            // Rotate the starfield with smoothed joystick input
            this.transform.Rotate(up, currentJoystickValue.x * rotationSpeed * Time.deltaTime, Space.World);
            this.transform.Rotate(right, -currentJoystickValue.y * rotationSpeed * Time.deltaTime, Space.World);
        } else {
            // Gradually ease joystick values back to zero when not in use
            currentJoystickValue = Vector2.SmoothDamp(currentJoystickValue, Vector2.zero, ref smoothVelocity, smoothTime);
        }
    }

    void ToggleConstellation(int index) {
        // Safety check the index is valid.
        if ((index < 0) || (index >= constellations.Count)) {
            return;
        }

        // Toggle on or off.
        if (constellationVisible.ContainsKey(index)) {
            RemoveConstellation(index);
        } else {
            CreateConstellation(index);
        }
    }

    void CreateConstellation(int index) {
        int[] constellation = constellations[index].Item1;
        int[] lines = constellations[index].Item2;

        // Change the colours of the stars
        foreach (int catalogNumber in constellation) {
            // Remember list is 0-up catalog numbers are 1-up.
            starObjects[catalogNumber - 1].GetComponent<MeshRenderer>().material.color = Color.white;
        }

        GameObject constellationHolder = new($"Constellation {index}");
        constellationHolder.transform.parent = transform;
        constellationVisible[index] = constellationHolder;

        // Draw the constellation lines.
        for (int i = 0; i < lines.Length; i += 2) {
            // Parent it to our constellation object so we can delete them all later.
            GameObject line = new("Line");
            line.transform.parent = constellationHolder.transform;
            // Defaults to white and width 1 which works for us.
            LineRenderer lineRenderer = line.AddComponent<LineRenderer>();
            // Doesn't get assigned a material so just dig out one that works.
            lineRenderer.material = new Material(Shader.Find("Legacy Shaders/Particles/Alpha Blended Premultiply"));
            // Disable useWorldSpace so it will track the parent game object.
            lineRenderer.useWorldSpace = false;
            Vector3 pos1 = starObjects[lines[i] - 1].transform.position;
            Vector3 pos2 = starObjects[lines[i + 1] - 1].transform.position;
            // Offset them so they don't occlude the stars, 3 chosen by trial and error.
            Vector3 dir = (pos2 - pos1).normalized * 3;
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, pos1 + dir);
            lineRenderer.SetPosition(1, pos2 - dir);
        }
    }

    void RemoveConstellation(int index) {
        int[] constallation = constellations[index].Item1;

        // Toggling off set the stars back to the original colour.
        foreach (int catalogNumber in constallation) {
            // Remember list is 0-up catalog numbers are 1-up.
            starObjects[catalogNumber - 1].GetComponent<MeshRenderer>().material.color = stars[catalogNumber - 1].colour;
        }
        // Remove the constellation lines.
        Destroy(constellationVisible[index]);
        // Remove from our dictionary as it's now off.
        constellationVisible.Remove(index);
    }

}