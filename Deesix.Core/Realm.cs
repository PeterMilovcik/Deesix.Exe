﻿namespace Deesix.Exe.Core;

public class Realm
{
    public required string Id { get; set; }
    public required string Path { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required List<Region> Regions { get; set; }
    public World? World { get; set; }
}