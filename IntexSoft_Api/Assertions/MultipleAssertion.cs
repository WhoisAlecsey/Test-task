using System;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntexSoft_Api.Assertions
{
    /// <summary>
    /// Contains functionality for the Multiple Assertions
    /// </summary>
    public static class MultipleAssertion
    {
        public static void AssertAll(params Action[] assertionsToRun)
        {
            var errorMessages = new StringBuilder();
            foreach (var action in assertionsToRun)
            {
                try
                {
                    action.Invoke();
                }
                catch (Exception exc)
                {
                    errorMessages.Append($"{exc.Message} {Environment.NewLine}");
                }
            }

            if (errorMessages.Length != 0)
            {
                Assert.Fail($"{Environment.NewLine}The following conditions failed:" +
                            $"{Environment.NewLine}{errorMessages}");
            }
        }
    }
}