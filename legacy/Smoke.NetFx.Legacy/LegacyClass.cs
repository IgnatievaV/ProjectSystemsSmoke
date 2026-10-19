namespace Smoke.NetFx.Legacy
{
    public class LegacyClass
    {
        // Resolves only after a packages.config restore (NuGet tool window / nuget restore).
        public string Json => Newtonsoft.Json.JsonConvert.SerializeObject(new { Legacy = true });
    }
}
