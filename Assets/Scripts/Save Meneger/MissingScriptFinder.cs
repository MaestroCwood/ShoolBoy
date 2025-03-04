using UnityEngine;

public class MissingScriptFinder : MonoBehaviour
{
    [ContextMenu("Find Missing Scripts")]
    void FindMissingScripts()
    {
        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();
        foreach (GameObject obj in allObjects)
        {
            Component[] components = obj.GetComponents<Component>();
            foreach (Component component in components)
            {
                if (component == null)
                {
                    Debug.LogWarning($"Missing script on GameObject: {obj.name}", obj);
                }
            }
        }
    }
}
