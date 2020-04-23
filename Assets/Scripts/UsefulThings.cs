using UnityEngine;
using UnityEngine.InputSystem;
class UsefulThings
{
    public static Keyboard kb;
    public static Mouse mouse;
    public static InputManager inputManager;
    public static Transform TransformSearch(Transform transform, string name)
    {
        Transform go;
        if (transform.Find(name) == null)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                go = TransformSearch(transform.GetChild(i), name);
                if (go != null)
                    return go;
            }
            if (transform.childCount == 0)
                return null;
        }
        return transform.Find(name);
    }
}
