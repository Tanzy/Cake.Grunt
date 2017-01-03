﻿using System;
using System.IO;
using System.Xml.Schema;
using Cake.Core;
using Cake.Core.IO;
using Cake.Core.Tooling;

namespace Cake.Grunt
{
    /// <summary>
    /// Class GruntRunner.
    /// </summary>
    public abstract class GruntRunner<TSettings> : Tool<TSettings> where TSettings : GruntRunnerSettings
    {
        private readonly IFileSystem _fileSystem;

        /// <summary>
        /// creates a new grunt runner
        /// </summary>
        /// <param name="fileSystem">the file system</param>
        /// <param name="environment">The cake environment</param>
        /// <param name="processRunner">The cake process runner</param>
        /// <param name="tools">The tools locator</param>
        protected GruntRunner(IFileSystem fileSystem, ICakeEnvironment environment, IProcessRunner processRunner, IToolLocator tools) : base(fileSystem, environment, processRunner, tools)
        {
            _fileSystem = fileSystem;
        }

        /// <summary>
        /// Gets the name of the tool.
        /// </summary>
        /// <returns>
        /// The name of the tool.
        /// </returns>
        protected override string GetToolName()
        {
            return "Grunt Runner";
        }

        /// <summary>
        /// Executes grunt
        /// </summary>
        public abstract void Execute(Action<TSettings> settings = null);

        /// <summary>
        /// Validates settings
        /// </summary>
        /// <param name="settings">the settings class</param>
        /// <exception cref="FileNotFoundException">when grunt file does not exist</exception>
        protected virtual void ValidateSettings(TSettings settings = null)
        {
            if (settings?.GruntFile != null && !_fileSystem.Exist(settings.GruntFile))
            {
                throw new FileNotFoundException("gruntfile not found", settings.GruntFile.FullPath);
            }
        }
    }
}
