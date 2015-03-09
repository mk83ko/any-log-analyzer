using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Mkko
{
    public class ConfigParameterProvider
    {
        public const string Logfile = "logfile";
        public const string Definitions = "definitions";
        public const string Format = "format";
        public const string Output = "output";
        public const string ReturnCode = "returnCode";
        public const string Configuration = "configuration";
        public const string ValuesToDisplay = "valuesToDisplay";

        private static string DefaultConfigurationFile
        {
            get
            {
                var codeBase = Assembly.GetExecutingAssembly().CodeBase;
                var uri = new UriBuilder(codeBase);
                var path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path) + @"\configuration.json";
            }
        }

        private static ConfigParameterProvider instance;
        private ParameterWrapper parameters;

        private bool numberOfWarningsAsReturnCode = false;

        private ConfigParameterProvider()
        {
            this.parameters = new ParameterWrapper();
        }

        public static ConfigParameterProvider Instance
        {
            get { return instance ?? (instance = new ConfigParameterProvider()); }
        }

        public static List<string> GetAllKnownCliArguments()
        {
            var args = new List<string>
            {
                Logfile,
                Definitions,
                Format,
                Output,
                Configuration,
                ValuesToDisplay
            };

            return args;
        }

        public List<string> GetParametersForCliArgument(string config, string argument)
        {
            return (this.parameters.GetParameter(config, argument) ?? this.parameters.GetParameter("default", argument));
        }

        public bool IsReturnCodeNumberOfWarnings()
        {
            return this.numberOfWarningsAsReturnCode;
        }

        void InitializeConfigParameters(CliArguments args)
        {
            var configFile = FilesystemIoHelper.GetStreamReader(args.Configuration ?? DefaultConfigurationFile);

            
        }
    }

    class ParameterWrapper
    {
        private Dictionary<string, Dictionary<string, List<string>>> parameters = new Dictionary<string, Dictionary<string, List<string>>>();

        public void AddParameter(string config, string key, List<string> parameter)
        {
            if (!parameters.ContainsKey(config))
                parameters.Add(key, new Dictionary<string, List<string>>());

            Dictionary<string, List<string>> configuration; 
            parameters.TryGetValue(config, out configuration);

            if (configuration.ContainsKey(key))
                configuration.Remove(key);

            configuration.Add(key, parameter);
        }

        public List<string> GetParameter(string config, string key)
        {
            if (!parameters.ContainsKey(config))
                return new List<string>();

            Dictionary<string, List<string>> configuration;
            parameters.TryGetValue(config, out configuration);
            if (!configuration.ContainsKey(key))
                return new List<string>();

            List<string> parameter;
            configuration.TryGetValue(key, out parameter);
            return parameter;
        }
    }

    class JsonConfig
    {
        private string key;
        private Dictionary<string, List<String>> parameters;


    }
}