using System.Windows;
using Geckon.Common.ValueConverters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Geckon.Test.Common.ValueConverters
{
	[TestClass]
	public class BoolToVisiblityConverterTest
	{
		[TestMethod]
		public void ShouldConvertBoolToVisiblity()
		{
			var converter = new BoolToVisiblityConverter();

			Assert.AreEqual(Visibility.Visible, converter.Convert(true, typeof(Visibility), null, null));
			Assert.AreEqual(Visibility.Collapsed, converter.Convert(false, typeof(Visibility), null, null));
			Assert.AreEqual(Visibility.Collapsed, converter.Convert(true, typeof(Visibility), true, null));
			Assert.AreEqual(Visibility.Visible, converter.Convert(false, typeof(Visibility), true, null));
			Assert.AreEqual(Visibility.Visible, converter.Convert(true, typeof(Visibility), false, null));
			Assert.AreEqual(Visibility.Collapsed, converter.Convert(false, typeof(Visibility), false, null));

			Assert.AreEqual(Visibility.Collapsed, converter.Convert(true, typeof(Visibility), "true", null));
		}

		[TestMethod]
		public void ShouldConvertVisiblityToBool()
		{
			var converter = new BoolToVisiblityConverter();

			Assert.AreEqual(true, converter.ConvertBack(Visibility.Visible, typeof(bool), null, null));
			Assert.AreEqual(false, converter.ConvertBack(Visibility.Collapsed, typeof(bool), null, null));
			Assert.AreEqual(false, converter.ConvertBack(Visibility.Visible, typeof(bool), true, null));
			Assert.AreEqual(true, converter.ConvertBack(Visibility.Collapsed, typeof(bool), true, null));

			Assert.AreEqual(true, converter.ConvertBack("Visible", typeof(bool), null, null));
			Assert.AreEqual(false, converter.ConvertBack("Collapsed", typeof(bool), null, null));
		}
	}
}