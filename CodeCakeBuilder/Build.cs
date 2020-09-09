using Cake.Common.IO;
using Cake.Common.Tools.DotNetCore;
using Cake.Core;
using Cake.Core.Diagnostics;
using SimpleGitVersion;
using System;
using System.IO;
using System.Linq;

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

            Task( "Check-Repository" )
                .Does( () =>
                {
                    globalInfo.TerminateIfShouldStop();
                } );

            Task( "Clean" )
                .IsDependentOn( "Check-Repository" )
                .Does( () =>
                 {
                     globalInfo.GetDotnetSolution().Clean();
                     Cake.CleanDirectories( globalInfo.ReleasesFolder );

                 } );


            Task( "Build" )
                .IsDependentOn( "Clean" )
                .IsDependentOn( "Check-Repository" )
                .Does( () =>
                 {
                     globalInfo.GetDotnetSolution().Build();
                 } );

            Task( "Unit-Testing" )
                .IsDependentOn( "Build" )
                .WithCriteria( () => Cake.InteractiveMode() == InteractiveMode.NoInteraction
                                     || Cake.ReadInteractiveOption( "RunUnitTests", "Run Unit Tests?", 'Y', 'N' ) == 'Y' )
               .Does( () =>
                {
                    globalInfo.GetDotnetSolution().Test();
                } );

            Task( "Deploy" )
                .IsDependentOn( "Unit-Testing" )
                .Does( () =>
                 {
                     Console.WriteLine( Environment.CurrentDirectory );
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
                 } );

            // The Default task for this script can be set here.
            Task( "Default" )
                .IsDependentOn( "Deploy" );

        }

    }
}
