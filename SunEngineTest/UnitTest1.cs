using System.IO;
using NUnit.Framework;
using SunEngine;

namespace SunEngineTest
{
    public class Tests
    {
        private const string DefaultConfigurationFileName = "Config";
        private const string InvalidConfigurationProperty = "invalidProperty:";
        private const string ValidConfigurationProperty = "config:";
        private const string ValidConfigurationFileName = "ConfigurationFileName";

        [Test]
        public void ShouldSetConfigPathToDefaultIfPropertiesNotPassed()
        {
            Program.SetUpConfigurationDirectory(new string[0]);
            Assert.AreEqual(GetFullPath(DefaultConfigurationFileName), Program.configDir);
        }

        [Test]
        public void ShouldSetConfigPathToDefaultIfConfigurationPropertyNotPassed()
        {
            Program.SetUpConfigurationDirectory(new[] {InvalidConfigurationProperty});
            Assert.AreEqual(GetFullPath(DefaultConfigurationFileName), Program.configDir);
        }

        [Test]
        public void ShouldSetConfigPathToDefaultIfConfigurationPropertyPassedWithEmptyValue()
        {
            Program.SetUpConfigurationDirectory(new[] {ValidConfigurationProperty + ""});
            Assert.AreEqual(GetFullPath(DefaultConfigurationFileName), Program.configDir);
        }

        [Test]
        public void ShouldSetConfigPathToDefaultIfConfigurationPropertyPassedWithBlankValue()
        {
            Program.SetUpConfigurationDirectory(new[] {ValidConfigurationProperty + " "});
            Assert.AreEqual(GetFullPath(DefaultConfigurationFileName), Program.configDir);
        }

        [Test]
        public void ShouldSetConfigPathToPropertyValueIfItPassed()
        {
            Program.SetUpConfigurationDirectory(new[] {ValidConfigurationProperty + ValidConfigurationFileName});
            Assert.AreEqual(GetFullPath(ValidConfigurationFileName), Program.configDir);
        }

        private string GetFullPath(string configPath)
        {
            return Path.GetFullPath(configPath);
        }
    }
}