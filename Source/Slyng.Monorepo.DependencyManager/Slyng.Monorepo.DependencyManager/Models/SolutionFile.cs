﻿using Microsoft.Build.Evaluation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Build.Utilities;
using Slyng.Monorepo.DependencyManager.Helpers;

namespace Slyng.Monorepo.DependencyManager.Models
{
    public class SolutionFile
    {
        public SolutionFile(string path)
        {
            FullPath = path;
            ColorConsole.WriteEmbeddedColorLine($"Solution: [blue]{SolutionFileName}[/blue]");
            Projects = FileHelper.GetFilesByType(FullDirectory, "*.csproj").Select(csproj => new ProjectFile(csproj)).ToList();
        }


        public string FullPath { get; set; }
        public List<ProjectFile> Projects { get; private set; }

        public string FullDirectory
        {
            get
            {
                return Path.GetDirectoryName(FullPath) + "\\";
            }
        }

        public string SolutionName
        {
            get
            {
                return SolutionFileName.Replace(".sln", "");
            }
        }

        public string SolutionFileName
        {
            get
            {
                return Path.GetFileName(FullPath);
            }
        }
        /// <summary>
        /// Relative path from the root of the repository to the solution file.
        /// </summary>
        public string RelativePath
        {
            get
            {
                return FullPath.Replace(Global.RootPath, "").TrimStart('\\');
            }
        }

        public string RelativeDirectory
        {
            get
            {
                return FullDirectory.Replace(Global.RootPath, "").TrimStart('\\');
            }
        }

        public string PipelineDirectory
        {
            get
            {
                if (string.IsNullOrEmpty(Global.Config.AzureDevops.OmitFolderFromPipelineDirectory))
                {
                    return RelativeDirectory.TrimStart('\\');
                }
                return RelativeDirectory.Replace(Global.Config.AzureDevops.OmitFolderFromPipelineDirectory, "").TrimStart('\\');
            }
        }




    }
}
