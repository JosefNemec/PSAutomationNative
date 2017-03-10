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
        private static bool isStringMatching(string source, string pattern, bool caseSensitive)
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

        private static bool isMatchingCondition(AutomationElement element, GetBaseControlCommand command)
        {
            // ---- Name ----
            if (!String.IsNullOrEmpty(command.Name))
            {
                if (!isStringMatching(element.Current.Name, command.Name, command.CaseSensitive))
                {
                    return false;
                }
            }

            // ---- Automation ID ----
            if (!String.IsNullOrEmpty(command.AutomationId))
            {
                if (!isStringMatching(element.Current.AutomationId, command.AutomationId, command.CaseSensitive))
                {
                    return false;
                }
            }

            // ---- Class ----
            if (!String.IsNullOrEmpty(command.Class))
            {
                if (!isStringMatching(element.Current.ClassName, command.Class, command.CaseSensitive))
                {
                    return false;
                }
            }

            // ---- Control Type ----
            if (!String.IsNullOrEmpty(command.ControlType))
            {
                if (element.Current.ControlType.LocalizedControlType != command.ControlType)
                {
                    return false;
                }
            }

            if (command is GetWindowCommand)
            {
                var windowCommand= (GetWindowCommand)command;

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

                    if (!isStringMatching(processName, windowCommand.ProcessName, windowCommand.CaseSensitive))
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

            if (command is GetControlCommand)
            {
                var controlCommand = (GetControlCommand)command;
                
                if (!String.IsNullOrEmpty(controlCommand.Value))
                {
                    var valuePattern = element.GetCurrentPattern(ValuePattern.Pattern) as ValuePattern;

                    if (valuePattern != null)
                    {
                        if (!isStringMatching(valuePattern.Current.Value, controlCommand.Value, controlCommand.CaseSensitive))
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        private static List<AutomationElement> findElements(AutomationElement root, GetBaseControlCommand command)
        {
            var newElements = new List<AutomationElement>();
            var objectElements = root.FindAll(TreeScope.Children, Condition.TrueCondition);
            
            foreach (AutomationElement element in objectElements)
            {
                if (isMatchingCondition(element, command))
                {
                    newElements.Add(element);

                    if (command.First)
                    {
                        return newElements;
                    }
                }

                if (command.Scope == TreeScope.Descendants)
                {
                    var recurElems = findElements(element, command);
                    if (recurElems != null && recurElems.Count != 0)
                    {
                        newElements.AddRange(recurElems);
                    }
                }
            }

            return newElements;
        }

        public static List<AutomationElement> FindControls(GetBaseControlCommand command)
        {
            return findElements(command.Parent, command);
        }
    }
}
