﻿using Deesix.Application.Interfaces;

namespace Deesix.Infrastructure.Generators;

public class Generator(
    IWorldGenerator worldGenerator, 
    IRealmGenerator realmGenerator, 
    IRegionGenerator regionGenerator, 
    ILocationGenerator locationGenerator) : IGenerator
{
    public IWorldGenerator World { get; } = worldGenerator;
    public IRealmGenerator Realm { get; } = realmGenerator;
    public IRegionGenerator Region { get; } = regionGenerator;
    public ILocationGenerator Location { get; } = locationGenerator;
}
