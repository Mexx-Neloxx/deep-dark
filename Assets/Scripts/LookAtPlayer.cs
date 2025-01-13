using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    public Transform camera;
    void LateUpdate()
    {
        transform.LookAt(camera);
        
    }
}
