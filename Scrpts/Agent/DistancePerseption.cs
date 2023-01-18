using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistancePerseption : Perception
{
    public override GameObject[] GetGameObjects() 
    {
        List<GameObject> result = new List<GameObject>();
        Collider[] colliders = Physics.OverlapSphere(transform.position, distance);

        foreach (Collider collider in colliders)
        {
            if (collider.gameObject == gameObject) continue;

            if (tagName == "" || collider.CompareTag(tagName))
            {
                result.Add(collider.gameObject);

                // calculate angle from transform forward vector to direction of game object 
                Vector3 direction = (collider.transform.position - transform.position).normalized;

                float cos = Vector3.Dot(transform.forward, direction);
                float angle = Mathf.Acos(cos) * Mathf.Rad2Deg;

                //float angle = Vector3.Angle(transform.forward, direction);

                if (angle <= maxAngle)
                {
                    result.Add(collider.gameObject);
                }
            }
        }

        result.Sort(CompareDistance);

        return result.ToArray();
    }
}
