using System;
using System.ServiceProcess;
using NUnit.Framework;

namespace Renterator.DataAccess.Tests.Helpers
{
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false)]
	public class ServiceStartAttribute : Attribute, ITestAction
	{
		private static readonly TimeSpan Timeout = TimeSpan.FromSeconds(10);

		public ServiceStartAttribute(string serviceName)
		{
			this.ServiceName = serviceName;
		}

		public string ServiceName { get; private set; }

		public void BeforeTest(TestDetails testDetails)
		{
			using (ServiceController service = new ServiceController(this.ServiceName))
			{
				switch(service.Status)
				{
					case ServiceControllerStatus.Stopped:
					case ServiceControllerStatus.Paused:
					case ServiceControllerStatus.ContinuePending:
						service.Start();
						service.WaitForStatus(ServiceControllerStatus.Running, Timeout);
						break;
					default:
						return;
				}
			}
		}

		public void AfterTest(TestDetails testDetails)
		{
		}

		public ActionTargets Targets
		{
			get { return ActionTargets.Suite | ActionTargets.Test; }
		}
	}
}
