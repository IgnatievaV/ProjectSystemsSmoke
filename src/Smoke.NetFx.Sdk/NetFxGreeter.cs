using Newtonsoft.Json;
using Smoke.Library;

namespace Smoke.NetFx.Sdk
{
    public static class NetFxGreeter
    {
        // Greeter comes from the net48 target of the multi-targeted Smoke.Library.
        public static string HelloJson(string name) => JsonConvert.SerializeObject(new { Message = Greeter.Hello(name) });
    }
}
