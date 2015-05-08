/*---------------------------------------------------------------------------
  RemObjects Internet Pack for .NET - Core Library
  (c)opyright RemObjects Software, LLC. 2003-2015. All rights reserved.

  Using this code requires a valid license of the RemObjects Internet Pack
  which can be obtained at http://www.remobjects.com?ip.
---------------------------------------------------------------------------*/

using System;
using System.Threading;

namespace RemObjects.InternetPack
{
	public abstract class AsyncWorker : IAsyncWorker
	{
		public AsyncServer AsyncOwner
		{
			get
			{
				return this.fOwner;
			}
			set
			{
				this.fOwner = value;
			}
		}
		private AsyncServer fOwner;

		public virtual Server Owner
		{
			get
			{
				return this.fOwner;
			}
			set
			{
				this.fOwner = (AsyncServer)value;
			}
		}

		public Connection DataConnection
		{
			get
			{
				return this.fDataConnection;
			}
			set
			{
				this.fDataConnection = value;
			}
		}
		private Connection fDataConnection;

		public abstract void Setup();

		public virtual void Done()
		{
			if (this.fOwner != null)
				this.fOwner.ClientClosed(this);

			if (this.DataConnection != null)
				this.DataConnection.Dispose();
		}
	}

	public abstract class Worker : IWorker
	{
		protected Worker()
		{
		}

		public Connection DataConnection
		{
			get
			{
				return this.fDataConnection;
			}
			set
			{
				this.fDataConnection = value;
			}
		}
		private Connection fDataConnection;

		public Thread Thread
		{
			get
			{
				return this.fThread;
			}
			set
			{
				this.fThread = value;
			}
		}
		public Thread fThread;

		public Server Owner
		{
			get
			{
				return this.fOwner;
			}
			set
			{
				this.fOwner = value;
			}
		}
		private Server fOwner;

		protected abstract void DoWork();

		public event EventHandler Done;

		public void Work()
		{
			try
			{
				this.DoWork();
			}
			catch (Exception)
			{
			}
			finally
			{
				this.DataConnection.Close();

				if (this.Done != null)
					this.Done(this, EventArgs.Empty);
			}
		}
	}
}