using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pulse.Client.Songs.Mechanics;

namespace pulse.Client.Songs
{
    /// <summary>
    /// Chart group is used for grouping charts within a folder.
    /// </summary>
    public class ChartGroup
    {

        public string FolderName
        {
            get
            {
                var allFolders = FolderPath.Split('\\');
                return allFolders[allFolders.Length - 1];
            }
        }
        public string FolderPath { get; set; }
        public List<Chart> Charts { get; set; }
        public List<ChartFile> Files { get; set; }

        public ChartGroup()
        {
            Charts = new List<Chart>();
            Files = new List<ChartFile>();
        }
    }
}
