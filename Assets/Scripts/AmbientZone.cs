using Unity.Cinemachine;
using UnityEngine;
using Unity.Mathematics;
using UnityEngine.Splines;

public class AmbientZone : MonoBehaviour
    {
        public SplineContainer spline;
        public Transform Player;

    // Update is called once per frame
    void Update()
    {
        Vector3 localPlayerPos = spline.transform.InverseTransformPoint(Player.position);

        SplineUtility.GetNearestPoint(spline.Spline, localPlayerPos, out float3 nearestPointLocal, out float normalizedT); //finds nearest point on the spline

        Vector3 nearestWorldPos = spline.transform.TransformPoint(nearestPointLocal);

        transform.position = nearestWorldPos;
        Vector3 tangent = spline.transform.TransformDirection(spline.Spline.EvaluateTangent(normalizedT));

        if (tangent != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(tangent);

        if (!spline.Spline.Closed) //check if closed
            return;

        Vector3 sub = transform.position - Player.position;
        Vector3 splineRight = Vector3.Cross(Vector3.up, tangent);

        if (Vector3.Dot(sub, splineRight) < 0f)
        {
            transform.position = Player.position + new Vector3(0f, 1f, 0f);
            transform.rotation = Player.rotation;
        }
    }

    }
