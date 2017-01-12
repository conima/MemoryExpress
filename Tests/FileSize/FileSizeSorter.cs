using System.Collections.Generic;
using Framework;

namespace Tests.FileSize
{
    public class FileSizeSorter
    {
        /// <summary>
        /// Sort a list of File Size strings by the value they represent
        /// </summary>
        /// <remarks>
        /// We'd like this method like to sort a given list of strings that represent file sizes (eg. "23.5 kB")
        /// 
        /// File sizes may come with decimals or without, will have whitespace seperating the number and unit
        /// and will use the following units (In Descending order):
        ///     - TB  (TeraByte) - 1024^4 - 1,099,511,627,776 Bytes
        ///     - GB  (GigaByte) - 1024^3 - 1,073,741,824     Bytes
        ///     - MB  (MegaByte) - 1024^2 - 1,048,576         Bytes
        ///     - kB  (KiloByte) - 1024   - 1,024             Bytes
        ///     - B   (Byte)     - 1      - 1                 Bytes
        /// 
        /// See Unit Tests for more details.
        /// </remarks>
        /// <param name="fileSizes">An enumerable of strings containing a value that represents a filesize as described above.</param>
        /// <param name="descending">A boolean determining if the results should be in descending order.</param>
        /// <returns>
        /// An enumerable of strings (same values provided) sorted by the file-sizes that they represent.
        /// </returns>
        public virtual IEnumerable<string> Sort(IEnumerable<string> fileSizes, bool descending = false)
        {
            throw new TestUnfinishedException();
        }
    }
}
