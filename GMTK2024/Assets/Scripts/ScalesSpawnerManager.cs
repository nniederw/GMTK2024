using System.Collections.Generic;
using System.Linq;
using System;
public static class ScalesSpawnerManager
{
    private static HashSet<ScalesSpawner> ScalesSpawners = new HashSet<ScalesSpawner>();
    public static void AddSpawnLocation(ScalesSpawner spawner)
    {
        ScalesSpawners.Add(spawner);
    }
    public static bool HasSpawnableSpot() => ScalesSpawners.Where(i => !i.IsActive()).Any();
    private static ScalesSpawner GetRandomDestroyedScales()
    {
        var ss = ScalesSpawners.Where(i => !i.IsActive()).ToArray();
        if (ss.Length == 0) throw new Exception($"The function {nameof(GetRandomDestroyedScales)} was called, but there aren't any destroyed Scales");
        Random rnd = new Random();
        return ss[rnd.Next(0, ss.Length)];
    }
    public static void RespawnScale() => GetRandomDestroyedScales().Spawn();
    public static void Reset()
    {
        ScalesSpawners.Clear();
    }
}