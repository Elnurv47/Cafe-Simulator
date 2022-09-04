using System;
using System.Collections.Generic;
using UnityEngine;

public static class Cafe
{
    private static List<Chair> _chairs;
    
    public static Vector3 EntrancePosition { get => new Vector3(45, 0, 85); }

    public static void Initialize()
    {
        _chairs = new List<Chair>();
        ObjectPlacementSystem.OnObjectPlaced += ObjectPlacementSystem_OnObjectPlaced;
    }

    public static Chair FindAvailableChair()
    {
        foreach (var chair in _chairs)
        {
            if (chair.IsAvailable)
            {
                return chair;
            }
        }

        return null;
    }

    private static void ObjectPlacementSystem_OnObjectPlaced(IPlaceableObject placedObject)
    {
        if (placedObject is Chair)
        {
            _chairs.Add(placedObject as Chair);
        }
    }
}
