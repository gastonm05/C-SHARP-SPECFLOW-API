::////////////////////////////////////////////////////////////////////////
:: This batch file will pack and push the CCC-API Tests Nuget package to
:: the Cision Nuget repository. Note that the version specified
:: will override the version in the nuspec file.
:: 
:: See the ShowHelp section for full details on usage.
::////////////////////////////////////////////////////////////////////////
@echo off
@setlocal

call :SetDefaults
call :ParseArguments %*
call :PrepAndPush

:SetDefaults
rem ===========================================================================
SET CISION_REPO=http://ciwinbuild1.qwestcolo.local/CisionNugetServer
SET CISION_REPO_KEY=CisionNugetServer
SET PROJECT_FILE=CCC-API.csproj
SET SHOW_HELP=false

:ParseArguments
rem ===========================================================================
if /I .%1 == . goto :eof
if /I .%1 == .-version goto :ArgumentVersion
if /I .%1 == .-v goto :ArgumentVersion
if /I .%1 == .-releaseNotes goto :ArgumentReleaseNotes
if /I .%1 == .-r goto :ArgumentReleaseNotes
if /I .%1 == .-help goto :ArgumentHelp
if /I .%1 == .-h goto :ArgumentHelp
if /I .%1 == .-? goto :ArgumentHelp

:ArgumentVersion
rem ===========================================================================
SET PACKAGE_VERSION=%2
SET PACKAGE=CCC-API.%PACKAGE_VERSION%.nupkg
shift
shift
goto :ParseArguments

:ArgumentReleaseNotes
rem ===========================================================================
SET MESSAGE=%2
shift
shift
goto :ParseArguments

:ArgumentHelp
rem ===========================================================================
SET SHOW_HELP=true
shift
shift
goto :ParseArguments

:PrepAndPush
rem ===========================================================================
if %SHOW_HELP% == true (goto :ShowHelp)

nuget pack %PROJECT_FILE% -Version %PACKAGE_VERSION% -Properties ReleaseNotes=%MESSAGE%
if not %ERRORLEVEL%==0 (goto :Error)

nuget push %PACKAGE% -Source %CISION_REPO% %CISION_REPO_KEY%
if not %ERRORLEVEL%==0 (goto :Error)

goto :Exit

:ShowHelp
rem ===========================================================================
echo ==========================================================================
echo Usage: PushToNuget {options}
echo
echo   -version or -v
echo      Specify the version to use when packaging (required)
echo   -releaseNotes or -r
echo  	  Specify the release notes to include with this package (required)
echo
echo   Examples:  PushToNuget.bat -version 1.0.0.1 -releaseNotes "Some changes I made"
echo              PushToNuget.bat -v 1.0.0.1 -r "Some changes I made"
echo 
echo   -help or -h or -? 
echo      Prints this
echo ==========================================================================
goto :Exit

:Wait
rem ===========================================================================
Pause

:Error
rem ===========================================================================
exit /b 1

:Exit
rem ===========================================================================
exit /b 0

:End