using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using UnityEngine;

[CreateAssetMenu(fileName = "CookProcess", menuName = "InventorySystem/ItemProcessing/ProcessResults", order = 0)]
public class ProcessResults : ScriptableObject 
{
    [SerializeField]
    SerializedDictionary<string, List<string>> results = new SerializedDictionary<string, List<string>>();
}