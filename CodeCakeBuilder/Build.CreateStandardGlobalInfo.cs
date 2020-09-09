using Cake.Common.Build;
using Cake.Common.Diagnostics;
using SimpleGitVersion;

namespace CodeCake
{
    public partial class Build
    {
        /// <summary>
        /// Creates a new <see cref="StandardGlobalInfo"/> initialized by the
        /// current environment.
        /// </summary>
        /// <param name="gitInfo">The git info.</param>
        /// <returns>A new info object.</returns>
        StandardGlobalInfo CreateStandardGlobalInfo( SimpleRepositoryInfo gitInfo )
        {
            StandardGlobalInfo result = new StandardGlobalInfo( Cake, gitInfo );
            // By default:
            if( !gitInfo.IsValid )
            {
            }
            else
            {
                // gitInfo is valid: it is either ci or a release build. 
                CSemVer.SVersion v = gitInfo.Info.FinalVersion;
                // If a /LocalFeed/ directory exists above, we publish the packages in it.
                string localFeedRoot = Cake.FindSiblingDirectoryAbove( Cake.Environment.WorkingDirectory.FullPath, "LocalFeed" );
                if( localFeedRoot != null )
                {
                    if( v.AsCSVersion == null )
                    {
                        if( v.Prerelease.EndsWith( ".local" ) )
                        {
                            // Local releases must not be pushed on any remote and are copied to LocalFeed/Local
                            // feed (if LocalFeed/ directory above exists).
                            result.IsLocalCIRelease = true;
                            result.LocalFeedPath = System.IO.Path.Combine( localFeedRoot, "Local" );
                        }
                        else
                        {
                            // CI build versions are routed to LocalFeed/CI
                            result.LocalFeedPath = System.IO.Path.Combine( localFeedRoot, "CI" );
                        }
                    }
                    else
                    {
                        // Release or prerelease go to LocalFeed/Release
                        result.LocalFeedPath = System.IO.Path.Combine( localFeedRoot, "Release" );
                    }
                    System.IO.Directory.CreateDirectory( result.LocalFeedPath );
                }
                else result.IsLocalCIRelease = v.Prerelease.EndsWith( ".local" );

                // Creating the right remote feed.
                if( !result.IsLocalCIRelease
                    && (Cake.InteractiveMode() == InteractiveMode.NoInteraction
                        || Cake.ReadInteractiveOption( "PushToRemote", "Push to Remote feeds?", 'Y', 'N' ) == 'Y') )
                {
                    result.PushToRemote = true;
                }
            }
            return result;
        }

    }
}
