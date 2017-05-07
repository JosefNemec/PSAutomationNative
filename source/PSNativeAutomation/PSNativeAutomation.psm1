class UIObject
{
    [object]$ObjectReference
    [scriptblock]$ScriptReference
    [string]$Name
    [int]$DefaultTimeout = 3000

    UIObject([scriptblock]$ScriptReference, [string]$Name)
    {
        $this.ScriptReference = $ScriptReference
        $this.Name = $Name
    }

    UIObject([object]$ObjectReference)
    {
        $this.ObjectReference = $ObjectReference
    }

    [object]GetNativeObject()
    {
        if ($this.ScriptReference)
        {
            return $this.WaitForObject($this.DefaultTimeout, $true)
        }
        elseif ($this.ObjectReference)
        {
            return $this.ObjectReference
        }
        else
        {
            throw "Cannot get object, not script or object reference found."
        }
    }

    [scriptblock]GetChildReference([scriptblock]$ChildReference)
    {
        return [scriptblock]::Create($this.ScriptReference.ToString() + " | " + $ChildReference.ToString())
    }

    [void]Click()
    {
        $this.Click(10, 10)
    }

    [void]Click([int]$XOffset, [int]$YOffset)
    {
       $this.GetNativeObject() | Invoke-UIClick -XOffset $XOffset -YOffset $YOffset
    }

    [void]ClickDouble()
    {
        $this.ClickDouble(10, 10)
    }

    [void]ClickDouble([int]$XOffset, [int]$YOffset)
    {
       $this.GetNativeObject() | Invoke-UIClick -XOffset $XOffset -YOffset $YOffset -DoubleClick
    }

    [void]ClickRight()
    {
        $this.ClickRight(10, 10)
    }

    [void]ClickRight([int]$XOffset, [int]$YOffset)
    {
       $this.GetNativeObject() | Invoke-UIClick -XOffset $XOffset -YOffset $YOffset -RightClick
    }

    [void]ClickMiddle()
    {
        $this.ClickMiddle(10, 10)
    }

    [void]ClickMiddle([int]$XOffset, [int]$YOffset)
    {
       $this.GetNativeObject() | Invoke-UIClick -XOffset $XOffset -YOffset $YOffset -MiddleClick
    }

    [void]Invoke()
    {
        $this.GetNativeObject() | Invoke-UIInvokePattern
    }

    [void]SendKeys([string]$Keys)
    {
         $this.GetNativeObject() | Send-UIKeys -Keys $Keys
    }

    [void]ToggleState()
    {
         $this.GetNativeObject() | Set-UIToggleState
    }

    [object]GetToggleState()
    {
         return $this.GetNativeObject() | Get-UIToggleState
    }

    [void]SetValue([string]$Value)
    {
         $this.GetNativeObject() | Set-UIValue -Value $Value
    }

    [string]GetValue()
    {
         return $this.GetNativeObject() | Get-UIValue
    }

    [void]Select()
    {
        $this.Select($false)
    }

    [void]Select([bool]$AddToSelection)
    {
        $this.GetNativeObject()| Set-UISelection -Add:$AddToSelection
    }

    [void]RemoveSelection()
    {
        $this.GetNativeObject() | Remove-UISelection
    }

    [bool]GetSelectionState()
    {
        return $this.GetNativeObject() | Get-UISelectionState
    }

    [void]Focus()
    {
        $this.GetNativeObject() | Set-UIFocus
    }

    [string]GetName()
    {
        return $this.GetNativeObject().Current.Name
    }

    [string]GetHelpText()
    {
        return $this.GetNativeObject().Current.HelpText
    }

    [object]GetObject()
    {
        return $this.GetNativeObject()
    }

    [bool]GetEnabledState()
    {
        return $this.GetNativeObject() | Get-UIEnabledState
    }

    [void]WaitForObjectVisible([int]$Milliseconds)
    {
        $watch = New-Object "System.Diagnostics.Stopwatch"
        $watch.Start()
        $result = $null

        try
        {
            while ($watch.ElapsedMilliseconds -lt $Milliseconds)
            {                
                $result = $false

                try
                {
                    $obj = $this.GetNativeObject()
                    if (-not $obj.Current.IsOffscreen)
                    {
                        $watch.Stop()
                        return
                    }
                }
                catch
                {
                    $result = $false
                }

                Start-Sleep -Milliseconds 250
            }
        }
        finally
        {
            $watch.Stop()
        }

        if (-not $result)
        {
            throw "Object {0} wasn't found or didn't became visible in {1} ms." -f $this.Name, $Milliseconds
        }
    }

    [void]WaitForObject([int]$Milliseconds)
    {
        $this.WaitForObject($Milliseconds, $false)
    }

    [object]WaitForObject([int]$Milliseconds, [bool]$ReturnObject)
    {
        $watch = New-Object "System.Diagnostics.Stopwatch"
        $watch.Start()
        $result = $null

        try
        {
            while ($watch.ElapsedMilliseconds -lt $Milliseconds)
            {                
                $result = $false

                try
                {
                    $result = & $this.ScriptReference
                    $watch.Stop()
                    break
                }
                catch
                {
                    $result = $false
                }

                Start-Sleep -Milliseconds 250
            }
        }
        finally
        {
            $watch.Stop()
        }

        if (-not $result)
        {
            throw "Couldn't find object {0} failed in {1} ms." -f $this.Name, $Milliseconds
        }

        if ($ReturnObject)
        {
            return $result
        }
        else
        {
            return $null
        }
    }

    [bool]Exists()
    {
        try
        {
            & $this.ScriptReference | Out-Null
            return $true
        }
        catch
        {
            return $false
        }
    }

    [bool]IsVisible()
    {
        try
        {
            return -not $this.GetNativeObject().Current.IsOffscreen
        }
        catch
        {
            return $false
        }
    }

    [string]ToString()
    {
        if ($this.ScriptReference)
        {
            return $this.Name + " " + $this.ScriptReference
        }
        else
        {
            return $this.Name + " " + $this.ObjectReference.ToString()
        }        
    }
}

class ListBox : UIObject
{
    ListBox([scriptblock]$ScriptReference, [string]$Name) : base($ScriptReference, $Name)
    {
    }

    ListBox([object]$ObjectReference) : base($ObjectReference)
    {
    }

    [void]SelectItem([string]$Name)
    {
        $this.SelectItem($Name, $false)
    }

    [void]SelectItem([string]$Name, [bool]$AddToSelection)
    {
        $this.GetNativeObject() | Get-UIListBoxItem -Name $Name | Set-UISelection -Add:$AddToSelection
    }

    [UIObject]GetItem([string]$Name)
    {
        $obj = $this.GetNativeObject() | Get-UIListBoxItem -Name $Name -First
        return [UIObject]::New($obj)
    }

    [object]GetItems()
    {
        return $this.GetNativeObject() | Get-UIListBoxItem | ForEach-Object {
            [UIObject]::New($_)
        }
    }

    [object]GetItemNames()
    {
        return $this.GetNativeObject() | Get-UIListBoxItem | ForEach-Object {
            $_.Current.Name
        }
    }
}

class ListView : UIObject
{
    ListView([scriptblock]$ScriptReference, [string]$Name) : base($ScriptReference, $Name)
    {
    }

    ListView([object]$ObjectReference) : base($ObjectReference)
    {
    }

    [void]SelectItem([string]$Name)
    {
        $this.SelectItem($Name, $false)
    }

    [void]SelectItem([string]$Name, [bool]$AddToSelection)
    {
        $this.GetNativeObject() | Get-UIListViewItem -Name $Name | Set-UISelection -Add:$AddToSelection
    }

    [object]GetItems()
    {
        return $this.GetNativeObject() | Get-UIListViewItem | ForEach-Object {
            [UIObject]::New($_)
        }
    }

    [object]GetItemNames()
    {
        return $this.GetNativeObject() | Get-UIListViewItem | ForEach-Object {
            $_.Current.Name
        }
    }
}

class Menu : UIObject
{
    Menu([scriptblock]$ScriptReference, [string]$Name) : base($ScriptReference, $Name)
    {
    }

    Menu([object]$ObjectReference) : base($ObjectReference)
    {
    }

    [void]ClickItem([string]$Name)
    {
        $this.GetNativeObject() | Get-UIMenuItem -Name $Name | Invoke-UIClick -XOffset 5 -YOffset 5
    }

    [void]InvokeItem([string]$Name)
    {
        $this.GetNativeObject() | Get-UIMenuItem -Name $Name | Invoke-UIInvokePattern
    }

    [object]GetItems()
    {
        return $this.GetNativeObject() | Get-UIMenuItem | ForEach-Object {
            [UIObject]::New($_)
        }
    }

    [object]GetItemNames()
    {
        return $this.GetNativeObject() | Get-UIMenuItem | ForEach-Object {
            $_.Current.Name
        }
    }
}

class TabControl : UIObject
{
    TabControl([scriptblock]$ScriptReference, [string]$Name) : base($ScriptReference, $Name)
    {
    }

    TabControl([object]$ObjectReference) : base($ObjectReference)
    {
    }

    [void]SelectItem([string]$Name)
    {
        $this.GetNativeObject() | Get-UITabItem -Name $Name | Set-UISelection
    }

    [object]GetItems()
    {
        return $this.GetNativeObject() | Get-UITabItem | ForEach-Object {
            [UIObject]::New($_)
        }
    }

    [object]GetItemNames()
    {
        return $this.GetNativeObject() | Get-UITabItem | ForEach-Object {
            $_.Current.Name
        }
    }
}

class Window : UIObject
{    
    Window([scriptblock]$ScriptReference, [string]$Name) : base($ScriptReference, $Name)
    {       
    }

    [object]GetState()
    {
        return $this.WaitForObject($this.DefaultTimeout, $true) | Get-UIWindowState
    }

    [void]Close()
    {
        $this.WaitForObject($this.DefaultTimeout, $true) | Invoke-UIWindowPattern "Close"
    }

    [void]Maximize()
    {
        $this.WaitForObject($this.DefaultTimeout, $true) | Invoke-UIWindowPattern "Maximize"
    }

    [void]Minimize()
    {
        $this.WaitForObject($this.DefaultTimeout, $true) | Invoke-UIWindowPattern "Minimize"
    }

    [void]Restore()
    {
        $this.WaitForObject($this.DefaultTimeout, $true) | Invoke-UIWindowPattern "Restore"
    }
}