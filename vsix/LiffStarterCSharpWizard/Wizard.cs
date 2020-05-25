using EnvDTE;
using Microsoft.VisualStudio.TemplateWizard;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiffStarterCSharpWizard
{
    public class Wizard : IWizard
    {
        private DTE dte;
        private static readonly string workflowContent =@"name: Deploy to Github Pages

on:
  push:
    branches: [ master ]

jobs:
  build:
    runs-on: ubuntu-latest

    steps:
    - uses: actions/checkout@v2

    - name: Setup .NET Core SDK
      uses: actions/setup-dotnet@v1
      with:
        dotnet-version: '3.1.300'
    
    - name: Build Application
      run: dotnet publish -c Release ./{0}/{0}.csproj

    - name: Deploy
      uses: peaceiris/actions-gh-pages@v3
      with:
           github_token: ${{{{ secrets.GITHUB_TOKEN }}}}
           publish_dir: ./{0}/bin/Release/netstandard2.1/publish/wwwroot/";

        public void BeforeOpeningFile(ProjectItem projectItem)
        {
            
        }

        public void ProjectFinishedGenerating(Project project)
        {
            
        }

        public void ProjectItemFinishedGenerating(ProjectItem projectItem)
        {
            
        }

        public void RunFinished()
        {
            var solutionDir = System.IO.Path.GetDirectoryName(dte.Solution.FullName);
            var workflowsDir = Path.Combine(solutionDir, ".github", "workflows");
            Directory.CreateDirectory(workflowsDir);
            File.WriteAllText(
                Path.Combine(workflowsDir, "gh-pages.yml"), 
                string.Format(
                    workflowContent,
                    dte.Solution.Projects.Cast<Project>().First().Name));
        }

        public void RunStarted(object automationObject, Dictionary<string, string> replacementsDictionary, WizardRunKind runKind, object[] customParams)
        {
            dte = automationObject as DTE;
        }

        public bool ShouldAddProjectItem(string filePath)
        {
            return true;
        }
    }
}
