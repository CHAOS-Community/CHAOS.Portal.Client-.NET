using System;
using Microsoft.Silverlight.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Geckon.Test.UI.Messenger
{
	[TestClass]
	public class MessengerTest
	{
		[TestMethod]
		[Tag("SendString")]
		public void ShouldSendStringMessage()
		{
			var messenger = new Geckon.UI.MVVM.Standard.Messenger.Messenger();

			var random = new Random();

			var data1 = 0;
			var data2 = 0;
			var data3 = 0;
			var dataToSend = random.Next();
			var dataToSend2 = random.Next();

			messenger.AddListener("Test1", d => data1 = (int)d);
			messenger.AddListener("Test2", d => data2 = (int)d);
			messenger.AddListener("Test1", d => data3 = (int)d);

			GC.Collect();
			GC.WaitForPendingFinalizers();

			messenger.Send("Test1", dataToSend);

			Assert.AreEqual(dataToSend, data1);
			Assert.AreEqual(0, data2);
			Assert.AreEqual(dataToSend, data3);

			messenger.Send("Test1", dataToSend2);

			Assert.AreEqual(dataToSend2, data1);
		}

		[TestMethod]
		public void ShouldSendTypedgMessage()
		{
			var messenger = new Geckon.UI.MVVM.Standard.Messenger.Messenger();

			var random = new Random();

			int data1 = 0;
			uint data2 = 0;
			int data3 = 0;
			var dataToSend = random.Next();
			var dataToSend2 = random.Next();

			messenger.AddListener<int>(d => data1 = d);
			messenger.AddListener<uint>(d => data2 = d);
			messenger.AddListener<int>(d => data3 = d);

			GC.Collect();
			GC.WaitForPendingFinalizers();

			messenger.Send(dataToSend);

			Assert.AreEqual(dataToSend, data1);
			Assert.AreEqual(0u, data2);
			Assert.AreEqual(dataToSend, data3);

			messenger.Send(dataToSend2);

			Assert.AreEqual(dataToSend2, data1);
		}

		[TestMethod]
		public void ShouldRemoveStringListener()
		{
			var messenger = new Geckon.UI.MVVM.Standard.Messenger.Messenger();

			var data1 = 0;
			var data2 = 0;
			var data3 = 0;
			var dataToSend = new Random().Next();

			Action<object> method2 = d => data2 = (int)d;

			messenger.AddListener("Test1", d => data1 = (int)d);
			messenger.AddListener("Test1", method2);
			messenger.AddListener("Test1", d => data3 = (int)d);

			messenger.RemoveListener("Test1", method2);

			GC.Collect();
			GC.WaitForPendingFinalizers();

			messenger.Send("Test1", dataToSend);

			Assert.AreEqual(dataToSend, data1);
			Assert.AreEqual(0, data2);
			Assert.AreEqual(dataToSend, data3);
		}

		[TestMethod]
		public void ShouldRemoveTypedListener()
		{
			var messenger = new Geckon.UI.MVVM.Standard.Messenger.Messenger();

			int data1 = 0;
			int data2 = 0;
			int data3 = 0;
			int dataToSend = new Random().Next();

			Action<int> method2 = d => data2 = d;

			messenger.AddListener<int>(d => data1 = d);
			messenger.AddListener<int>(method2);
			messenger.AddListener<int>(d => data3 = d);

			messenger.RemoveListener(method2);

			GC.Collect();
			GC.WaitForPendingFinalizers();

			messenger.Send(dataToSend);

			Assert.AreEqual(dataToSend, data1);
			Assert.AreEqual(0, data2);
			Assert.AreEqual(dataToSend, data3);
		}
	}
}