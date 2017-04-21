class UIObject
{
    [scriptblock]$Reference

    UIObject([scriptblock]$Reference)
    {
        $this.Reference = $Reference
    }

    [scriptblock]GetChildReference([scriptblock]$ChildReference)
    {
        return [scriptblock]::Create($this.Reference.ToString() + " | " + $ChildReference.ToString())
    }

    [void]Click()
    {
        $this.Click(5, 5)
    }

    [void]Click([int]$XOffset, [int]$YOffset)
    {
        & $this.Reference | Invoke-UIClick -XOffset $XOffset -YOffset $YOffset
    }

    [void]Invoke()
    {
        & $this.Reference | Invoke-UIInvokePattern
    }

    [void]SendKeys([string]$Keys)
    {
         & $this.Reference | Send-UIKeys -Keys $Keys
    }

    [void]ToggleState()
    {
         & $this.Reference | Set-UIToggleState
    }

    [object]GetToggleState()
    {
         return & $this.Reference | Get-UIToggleState
    }

    [void]SetValue([string]$Value)
    {
         & $this.Reference | Set-UIValue -Value $Value
    }

    [string]GetValue()
    {
         return & $this.Reference | Get-UIValue
    }

    [void]Select()
    {
        $this.Select($false)
    }

    [void]Select([bool]$AddToSelection)
    {
        & $this.Reference | Set-UISelection -Add:$AddToSelection
    }

    [void]RemoveSelection()
    {
        & $this.Reference | Remove-UISelection
    }

    [void]Focus()
    {
        & $this.Reference | Set-UIFocus
    }

    [string]GetName()
    {
        return (& $this.Reference).Current.Name
    }
}

class Window : UIObject
{    
    Window([scriptblock]$Reference) : base($Reference)
    {       
    }

    [object]GetState()
    {
        return & $this.Reference | Get-UIWindowState
    }

    [void]Close()
    {
        & $this.Reference | Invoke-UIWindowPattern "Close"
    }

    [void]Maximize()
    {
        & $this.Reference | Invoke-UIWindowPattern "Maximize"
    }

    [void]Minimize()
    {
        & $this.Reference | Invoke-UIWindowPattern "Minimize"
    }

    [void]Restore()
    {
        & $this.Reference | Invoke-UIWindowPattern "Restore"
    }
}