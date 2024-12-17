using System;

namespace pacman;

public class SaveData
{
    
    public int Score { get; set; }
    public float PlayerX { get; set; }
    public float PlayerY { get; set; }
    public int Lives { get; set; }
    public DateTime SaveTime { get; set; }
}