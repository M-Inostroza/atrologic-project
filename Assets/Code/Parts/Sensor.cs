﻿using System;
using System.Collections.Generic;
using UnityEngine;

public class Sensor : Part
{
    private Vector2 direction = Vector2.right;
    private RaycastHit2D hit;

    public static event Action<float> OnAngleDetected;

    private new void Update() // This is avoiding the part update to exe, check the wheel use to find solution, then keep testing the laser
    {
        base.Update();
        DetectObjectToRight();
    }

    private void DetectObjectToRight()
    {
        direction = transform.right;

        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, direction, 10);

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider == null) continue;

            if (hit.collider.CompareTag("Player") || hit.collider.CompareTag("Resource"))
            {
                continue;
            }

            Vector2 normal = hit.normal;
            float angle = Vector2.SignedAngle(Vector2.up, normal);

            angle = Mathf.Abs(angle);

            Debug.Log($"Detected object: {hit.collider.name}, Surface Angle: {angle} degrees");

            OnAngleDetected?.Invoke(angle);

            return;
        }
    }
}