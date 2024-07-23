﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml.Serialization;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Spectre.Console.Cli;
using YamlDotNet.Serialization;

namespace MigrationTools.Host.Commands
{
    internal class BaseMigrationCommandSettings : CommandSettings
    {
        [Description("Pre configure paramiters using this config file. Run `Init` to create it.")]
        [CommandOption("--config|--configFile")]
        [DefaultValue("configuration.json")]
        [JsonIgnore, YamlIgnore]
        public string ConfigFile { get; set; }
    }
}
