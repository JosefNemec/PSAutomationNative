param(
    [ValidateSet("Release", "Debug")]
    [string]$Configuration = "Release",
    [string]$OutputPath = (Join-Path $PWD "$Configuration\PSNativeAutomation")
)

$ErrorActionPreference = "Stop"

# -------------------------------------------
#            Compile application 
# -------------------------------------------
if (!$SkipBuild)
{
    if (Test-Path $OutputPath)
    {
        Remove-Item $OutputPath -Recurse -Force
    }

    $solutionDir = Join-Path $pwd "..\source"
    $msbuildPath = "c:\Program Files (x86)\Microsoft Visual Studio\2017\Community\MSBuild\15.0\Bin\MSBuild.exe";
    $arguments = "build.xml /p:SolutionDir=`"$solutionDir`" /p:OutputPath=`"$outputPath`";Configuration=$configuration /property:Platform=AnyCPU /t:Build";
    $compiler = Start-Process $msbuildPath $arguments -PassThru -NoNewWindow
    $handle = $compiler.Handle # cache proc.Handle http://stackoverflow.com/a/23797762/1479211
    $compiler.WaitForExit()

    if ($compiler.ExitCode -ne 0)
    {
        $appCompileSuccess = $false
        Write-Host "Build failed." -ForegroundColor "Red"
    }
    else
    {
        $appCompileSuccess = $true
    }
}
else
{
    $appCompileSuccess = $true
}

return $appCompileSuccess