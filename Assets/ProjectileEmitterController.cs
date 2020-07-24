using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileEmitterController : MonoBehaviour
{
    [SerializeField]
    public ProjectileEmitter projectileEmitter;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = Input.mousePosition;

        pos = Camera.main.ScreenToWorldPoint(pos);
        
        Vector3 origin = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
        transform.LookAt(pos);
        transform.RotateAround(transform.position, transform.right, 90);
    }
}
