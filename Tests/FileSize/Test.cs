using System;
using System.Linq;
using Framework;
using Framework.FileSize;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.FileSize
{
    /// <summary>
    /// For more details on the test please see FileSizeSorter.cs
    /// </summary>
    [TestClass]
    public class Test
    {
        /// <summary>
        /// Tests that the implementation sorts correctly.
        /// </summary>
        [TestMethod]
        public void SortByFileSize_Sorted()
        {
            var algorithm = InstanceProvider.GetInstance<FileSizeSorter>();
            var sorted = algorithm.Sort(FileSizeHelper.SampleValues).ToArray();

            Assert.IsNotNull(sorted, 
                "Your method returned a null result.");
            Assert.AreEqual(FileSizeHelper.SampleValues.Length, sorted.Length, 
                "Your method returned a differnet number of results than what was provided.");

            try
            {
                FileSizeHelper.AssertIsSorted(sorted);
            }
            catch (FileSizeHelper.OutOfOrderException ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>
        /// Tests that the implementation sorts correctly in descending order.
        /// </summary>
        [TestMethod]
        public void SortByFileSize_SortedDescending()
        {
            var algorithm = InstanceProvider.GetInstance<FileSizeSorter>();
            var sorted = algorithm.Sort(FileSizeHelper.SampleValues, descending:true).ToArray();

            Assert.IsNotNull(sorted,
                "Your method returned a null result.");
            Assert.AreEqual(FileSizeHelper.SampleValues.Length, sorted.Length,
                "Your method returned a differnet number of results than what was provided.");

            try
            {
                FileSizeHelper.AssertIsSorted(sorted, descending:true);
            }
            catch (FileSizeHelper.OutOfOrderException ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>
        /// Tests that the implementation sorts correctly when given 
        /// inconsistent (leading/trailing whitespace, etc...) input.
        /// </summary>
        [TestMethod]
        public void SortByFileSize_HandlesNonUniformInput()
        {
            var algorithm = InstanceProvider.GetInstance<FileSizeSorter>();
            var sorted = algorithm.Sort(FileSizeHelper.SampleValuesWithImperfections).ToArray();

            Assert.IsNotNull(sorted,
                "Your method returned a null result.");
            Assert.AreEqual(FileSizeHelper.SampleValuesWithImperfections.Length, sorted.Length,
                "Your method returned a differnet number of results than what was provided.");

            try
            {
                FileSizeHelper.AssertIsSorted(sorted, withImperfections:true);
            }
            catch (FileSizeHelper.OutOfOrderException ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}
