@{
    ModuleVersion      = '0.1.0.0'
    RequiredAssemblies = 'PSNativeAutomation.dll'
    ModuleToProcess    = 'PSNativeAutomation.psm1'
    NestedModules      = 'PSNativeAutomation.dll'

    FormatsToProcess   = @(
        'PSNativeAutomation.format.ps1xml'
    )
}