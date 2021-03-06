﻿using System;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Esb.Modules
{
	public class LoggingModule : IModule
	{
		private readonly InboxLoggingObserver _inboxLoggingObserver = new InboxLoggingObserver();

		private readonly string _inboxMessagePipelineName = typeof (InboxMessagePipeline).FullName;

		public void Initialize(IServiceBus bus)
		{
			Guard.AgainstNull(bus, "bus");

			bus.Events.PipelineCreated += PipelineCreated;
		}

		private void PipelineCreated(object sender, PipelineEventArgs e)
		{
			if (!e.Pipeline.GetType().FullName.Equals(_inboxMessagePipelineName, StringComparison.InvariantCultureIgnoreCase))
			{
				return;
			}

			e.Pipeline.RegisterObserver(_inboxLoggingObserver);
		}
	}
}