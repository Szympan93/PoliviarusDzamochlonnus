using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    public static Transform ClosestTo(this IEnumerable<Transform> items, Vector3 position)
    {
        if (items == null || !items.Any()) return null;

        var closest = items.First();
        var closestSqrDistance = (closest.position - position).sqrMagnitude;

        foreach (var item in items)
        {
            var sqrDistance = (item.position - position).sqrMagnitude;
            if (sqrDistance < closestSqrDistance)
            {
                closestSqrDistance = sqrDistance;
                closest = item;
            }
        }
        return closest;
    }

    public static Vector2 Set(this Vector2 v, float? x = null, float? y = null)
    {
        return new Vector2(x ?? v.x, y ?? v.y);
    }

    public static Vector3 Set(this Vector3 v, float? x = null, float? y = null, float? z = null)
    {
        return new Vector3(x ?? v.x, y ?? v.y, z ?? v.z);
    }
}
