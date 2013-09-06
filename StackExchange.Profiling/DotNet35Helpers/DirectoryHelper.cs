using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.IO
{
	public static class DirectoryHelper
	{
		//
		// Summary:
		//     Returns an enumerable collection of file names in a specified path.
		//
		// Parameters:
		//   path:
		//     The directory to search.
		//
		// Returns:
		//     An enumerable collection of file names in the directory specified by path.
		//
		// Exceptions:
		//   System.ArgumentException:
		//     path is a zero-length string, contains only white space, or contains invalid
		//     characters as defined by System.IO.Path.GetInvalidPathChars().
		//
		//   System.ArgumentNullException:
		//     path is null.
		//
		//   System.IO.DirectoryNotFoundException:
		//     path is invalid, such as referring to an unmapped drive.
		//
		//   System.IO.IOException:
		//     path is a file name.
		//
		//   System.IO.PathTooLongException:
		//     The specified path, file name, or combined exceed the system-defined maximum
		//     length. For example, on Windows-based platforms, paths must be less than
		//     248 characters and file names must be less than 260 characters.
		//
		//   System.Security.SecurityException:
		//     The caller does not have the required permission.
		//
		//   System.UnauthorizedAccessException:
		//     The caller does not have the required permission.
		public static IEnumerable<string> EnumerateFiles(string path)
		{
			var directory = new DirectoryInfo(path);
			return directory.GetFiles().Select(i=>i.FullName);
		}
	}
}
