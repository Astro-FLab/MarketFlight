using Cake.Common.IO;
using Cake.Common.Tools.DotNetCore;
using Cake.Core;
using Cake.Core.Diagnostics;
using SimpleGitVersion;
using System;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

namespace CodeCake
{

    /// <summary>
    /// Standard build "script".
    /// </summary>
    [AddPath( "%UserProfile%/.nuget/packages/**/tools*" )]
    public partial class Build : CodeCakeHost
    {
        public Build()
        {
            Cake.Log.Verbosity = Verbosity.Diagnostic;
            SimpleRepositoryInfo gitInfo = Cake.GetSimpleRepositoryInfo();
            StandardGlobalInfo globalInfo = CreateStandardGlobalInfo( gitInfo )
                                                .AddDotnet()
                                                .SetCIBuildTag();
            globalInfo.GetDotnetSolution().Clean();
            Cake.CleanDirectories( globalInfo.ReleasesFolder );
            globalInfo.GetDotnetSolution().Build();

            globalInfo.GetDotnetSolution().Test();
            Cake.DotNetCorePublish( "MarketFlight" );
            string SourcePath = @"MarketFlight\bin\Debug\netcoreapp3.1\publish\";
            string release = "release/";
            //Now Create all of the directories
            foreach( string dirPath in Directory.GetDirectories( SourcePath, "*",
                SearchOption.AllDirectories ) )
                Directory.CreateDirectory( dirPath.Replace( SourcePath, release ) );

            //Copy all the files & Replaces any files with the same name
            foreach( string newPath in Directory.GetFiles( SourcePath, "*.*",
                SearchOption.AllDirectories ) )
                File.Copy( newPath, newPath.Replace( SourcePath, release ), true );
            Task( "Default" );

        }

    }
}
