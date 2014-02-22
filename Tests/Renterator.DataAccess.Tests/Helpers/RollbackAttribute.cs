using System;
using System.Transactions;
using NUnit.Framework;

namespace Renterator.DataAccess.Tests.Helpers
{
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
	public class RollbackAttribute : Attribute, ITestAction
	{
		private TransactionScope currentScope;

		public void BeforeTest(TestDetails testDetails)
		{
			if (currentScope != null)
			{
				currentScope.Dispose();
			}

			currentScope = new TransactionScope(TransactionScopeOption.RequiresNew);
		}

		public void AfterTest(TestDetails testDetails)
		{
			currentScope.Dispose();
			currentScope = null;
		}

		public ActionTargets Targets
		{
			get { return ActionTargets.Suite | ActionTargets.Test; }
		}
	}
}
