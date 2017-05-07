using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation;
using PSNativeAutomation.Commands;

namespace PSNativeAutomation
{
    public class ObjectFinder
    {
        private static bool IsStringMatching(string source, string pattern, bool caseSensitive)
        {
            if (pattern.Contains("*"))
            {
                WildcardOptions options;

                if (caseSensitive == true)
                {
                    options = WildcardOptions.Compiled;
                }
                else
                {
                    options = WildcardOptions.IgnoreCase | WildcardOptions.Compiled;
                }

                var patternMatch = new WildcardPattern(pattern, options);
                return patternMatch.IsMatch(source);
            }
            else
            {
                if (caseSensitive == true)
                {
                    if (source != pattern)
                    {
                        return false;
                    }
                }
                else
                {
                    if (!string.Equals(source, pattern, StringComparison.OrdinalIgnoreCase))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private static bool IsMatchingCondition(AutomationElement element, GetControlBase command)
        {
            // ---- Name ----
            if (!String.IsNullOrEmpty(command.Name))
            {
                if (!IsStringMatching(element.Current.Name, command.Name, command.CaseSensitive.IsPresent))
                {
                    return false;
                }
            }

            // ---- Automation ID ----
            if (!String.IsNullOrEmpty(command.AutomationId))
            {
                if (!IsStringMatching(element.Current.AutomationId, command.AutomationId, command.CaseSensitive.IsPresent))
                {
                    return false;
                }
            }

            // ---- Class ----
            if (!String.IsNullOrEmpty(command.Class))
            {
                if (!IsStringMatching(element.Current.ClassName, command.Class, command.CaseSensitive.IsPresent))
                {
                    return false;
                }
            }

            // ---- Control Type ----
            if (!String.IsNullOrEmpty(command.ControlType))
            {                
                if (!IsStringMatching(element.Current.LocalizedControlType, command.ControlType, command.CaseSensitive.IsPresent))
                {
                    return false;
                }
            }

            if (command is GetWindowCommand windowCommand)
            {
                // ---- Process Name ----
                if (!String.IsNullOrEmpty(windowCommand.ProcessName))
                {
                    string processName = string.Empty;

                    try
                    {
                        processName = (System.Diagnostics.Process.GetProcessById(element.Current.ProcessId)).ProcessName;
                    }
                    catch
                    {
                        return false;
                    }

                    if (!IsStringMatching(processName, windowCommand.ProcessName, windowCommand.CaseSensitive.IsPresent))
                    {
                        return false;
                    }

                }

                // ---- Process ID ----
                if (windowCommand.PID < uint.MaxValue)
                {
                    if (element.Current.ProcessId != windowCommand.PID)
                    {
                        return false;
                    }
                }
            }

            // ---- Value ----
            if (command is GetControlCommand controlCommand)
            {
                if (!String.IsNullOrEmpty(controlCommand.Value))
                {
                    if (element.GetCurrentPattern(ValuePattern.Pattern) is ValuePattern valuePattern)
                    {
                        if (!IsStringMatching(valuePattern.Current.Value, controlCommand.Value, controlCommand.CaseSensitive.IsPresent))
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        private static List<AutomationElement> FindElements(AutomationElement root, GetControlBase command)
        {
            var newElements = new List<AutomationElement>();
            var objectElements = root.FindAll(TreeScope.Children, Condition.TrueCondition);
            
            foreach (AutomationElement element in objectElements)
            {
                if (IsMatchingCondition(element, command))
                {
                    newElements.Add(element);

                    if (command.First.IsPresent)
                    {
                        return newElements;
                    }
                }

                if (command.Scope == TreeScope.Descendants)
                {
                    var recurElems = FindElements(element, command);
                    if (recurElems != null && recurElems.Count != 0)
                    {
                        newElements.AddRange(recurElems);
                    }
                }
            }

            return newElements;
        }

        public static List<AutomationElement> FindControls(GetControlBase command)
        {
            return FindElements(command.Parent, command);
        }
    }
}
