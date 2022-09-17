using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "Room", order = 1)]
public class Room : ScriptableObject
{
    [Header("Полы комнаты (Варианты)")]
    public TileBase[] floorTile;
    [Header("Декорации комнат (Варианты)")]
    public TileBase decorTile;
    [Header("тайл который будет стеной для комнаты")]
    public TileBase wallTile;



}
