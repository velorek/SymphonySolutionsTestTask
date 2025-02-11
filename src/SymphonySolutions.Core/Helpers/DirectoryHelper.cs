namespace SymphonySolutions.Core.Helpers
{
    public static  class DirectoryHelper
    {
        public static string TryGetSolutionDirectory(string currentPath = null)
        {
            var directory = new DirectoryInfo(
                currentPath ?? Directory.GetCurrentDirectory());
            while (directory != null && directory.GetFiles("*.sln").Length == 0)
            {
                directory = directory.Parent;
            }
            return directory.FullName;
        }
    }
}
