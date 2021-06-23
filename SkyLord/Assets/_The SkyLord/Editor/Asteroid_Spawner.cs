using UnityEngine;
using UnityEditor;

public class Asteroid_Spawner : EditorWindow
{
    string m_asteroidBaseName = "Asteroid";
    int m_asteroidNumber = 1, m_asteroidCount = 100;
    GameObject m_asteroidPrefab;
    float m_asteroidScale, m_offsetDistance = 50f;
    Vector3 m_spawnRangeLower = new Vector3(-1000f, -500f, 100f), m_spawnRangeUpper = new Vector3(1000f, 500f, 1000f);

    Transform m_asteroidContainer;
    float m_minScaleValue = 3f, m_maxScaleValue = 10f;
    float m_minScaleLimit = 1f, m_maxScaleLimit = 15f;

    [MenuItem("Tools/SkyLord Tools/Asteroid Spawner")]
    static void OpenWindow()
    {
        GetWindow(typeof(Asteroid_Spawner), false, "Asteroid Spawner");
    }

    private void OnGUI()
    {
        GUILayout.Label("Asteroid Specifications", EditorStyles.boldLabel);
        EditorGUILayout.Space();

        GUILayout.Label("Asteroid GameObject", EditorStyles.boldLabel);
        m_asteroidPrefab = EditorGUILayout.ObjectField("Asteroid Prefab", m_asteroidPrefab, typeof(GameObject), false) as GameObject;
        m_asteroidContainer = EditorGUILayout.ObjectField("Asteroid Container", m_asteroidContainer, typeof(Transform), true) as Transform;
        EditorGUILayout.HelpBox("Assign a container object above for a cleaner hierarchy", MessageType.None, false);
        EditorGUILayout.Space();

        GUILayout.Label("Asteroid Basics", EditorStyles.boldLabel);
        m_asteroidBaseName = EditorGUILayout.TextField("Asteroid Base Name", m_asteroidBaseName);
        m_asteroidNumber = EditorGUILayout.IntField("Asteroid Number", m_asteroidNumber);
        EditorGUILayout.Space();
        m_spawnRangeLower = EditorGUILayout.Vector3Field("Spawn Range Lower Bound", m_spawnRangeLower);
        m_spawnRangeUpper = EditorGUILayout.Vector3Field("Spawn Range Upper Bound", m_spawnRangeUpper);
        EditorGUILayout.Space();
        m_asteroidCount = EditorGUILayout.IntField("Asteroid Count", m_asteroidCount);
        m_offsetDistance = EditorGUILayout.FloatField("Asteroid Offset", m_offsetDistance);
        EditorGUILayout.Space();

        GUILayout.Label("Asteroid Scaling", EditorStyles.boldLabel);
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PrefixLabel("Min Size: " + m_minScaleLimit);
        EditorGUILayout.MinMaxSlider(ref m_minScaleValue, ref m_maxScaleValue, m_minScaleLimit, m_maxScaleLimit);
        EditorGUILayout.PrefixLabel("Max Size: " + m_maxScaleLimit);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Min Value: " + m_minScaleValue.ToString());
        EditorGUILayout.LabelField("Max Value: " + m_maxScaleValue.ToString());
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space();

        EditorGUI.BeginDisabledGroup(m_asteroidPrefab == null || m_asteroidBaseName == string.Empty || (m_asteroidContainer != null && EditorUtility.IsPersistent(m_asteroidContainer)));
        if (GUILayout.Button("Spawn Asteroid"))
            SpawnAsteroid();
        EditorGUI.EndDisabledGroup();
        EditorGUILayout.Space();

        if (m_asteroidPrefab == null)
            EditorGUILayout.HelpBox("Asteroid Prefab Missing", MessageType.Warning);
        if (m_asteroidBaseName == string.Empty)
            EditorGUILayout.HelpBox("Give a base name to the asteroid", MessageType.Warning);
        if ((m_asteroidContainer != null && EditorUtility.IsPersistent(m_asteroidContainer)))
            EditorGUILayout.HelpBox("Container object must be from the hierarchy", MessageType.Warning);
    }

    void SpawnAsteroid()
    {
        for(int i = 0; i < m_asteroidCount; i++)
        {
            Vector3 position = new Vector3(Random.Range(m_spawnRangeLower.x, m_spawnRangeUpper.x) + m_offsetDistance,
                                           Random.Range(m_spawnRangeLower.y, m_spawnRangeUpper.y) + m_offsetDistance,
                                           Random.Range(m_spawnRangeLower.z, m_spawnRangeUpper.z));

            //var position = CalculateOrbitalAsteroidPosition();

            m_asteroidScale = Random.Range(m_minScaleValue, m_maxScaleValue);
            GameObject asteroid = Instantiate(m_asteroidPrefab, position, Random.rotation, m_asteroidContainer);
            asteroid.name = (m_asteroidBaseName + m_asteroidNumber.ToString());
            asteroid.transform.localScale = Vector3.one * m_asteroidScale;
            m_asteroidNumber++;
        }  
    }

    Vector3 CalculateOrbitalAsteroidPosition()
    {
        float x, z;
        do
        {
            var radius = Random.Range(m_spawnRangeLower.magnitude, m_spawnRangeUpper.magnitude);
            var radian = Random.Range(0, (2 * Mathf.PI));

            x = radius * Mathf.Cos(radian);
            z = radius * Mathf.Sin(radian);
        }
        while (float.IsNaN(z) && float.IsNaN(x));

        var position = m_asteroidContainer.position + (m_asteroidContainer.rotation * (new Vector3(x, Random.Range(m_spawnRangeLower.y, m_spawnRangeUpper.y), z - 100f)));

        return position;
    }
}
