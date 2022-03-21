using CommandLine;

namespace Btpt.App
{
    public class CommandLineOptions
    {
        [Value(index: 0, Required = true, HelpText = "Provider to define punch the clock method.")]
        public string Provider { get; set; }

        [Value(index: 1, Required = true, HelpText = "The application user name.")]
        public string UserName { get; set; }

        [Value(index: 2, Required = true, HelpText = "The application password.")]
        public string Password { get; set; }

        [Option(shortName: 'z', longName: "timezone", Required = false, HelpText = "The timezone for operation.", Default = 3)]
        public int TimeZone { get; set; }

        [Option(shortName: 'a', longName: "latitude", Required = false, HelpText = "The current latitude position.", Default = 0)]
        public double Latitude { get; set; }

        [Option(shortName: 'o', longName: "longitude", Required = false, HelpText = "The current longitude position.", Default = 0)]
        public double Longitude { get; set; }
    }
}