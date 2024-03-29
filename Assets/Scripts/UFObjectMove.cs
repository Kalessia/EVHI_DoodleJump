using UnityEngine;

public class UFObjectMove : MonoBehaviour
{
    float x;
    float y;
    float z;

    void Update()
    {
        x = Mathf.Abs(Mathf.Cos(Time.time) * 1.5f) + 0.7f;
        y = - Mathf.Abs(Mathf.Sin(Time.time) * 1.5f) + 3;
        z = transform.position.z;
        transform.position = new Vector3(x, y, z);
    }
}
