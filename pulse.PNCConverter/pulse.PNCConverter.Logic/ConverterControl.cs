using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using pulse.Client.IO;
using pulse.Client.Songs;
using pulse.Client.Songs.Mechanics;

namespace pulse.PNCConverter.Logic
{
    public class ConverterControl
    {
        private readonly CultureInfo _culture = new CultureInfo("en-US");
        public void ConvertFolder(string directory)
        {
            // Root songs dir.
            var directoryInfo = new DirectoryInfo(directory);
            var songDirs = directoryInfo.GetDirectories();

            var groups = songDirs.Select(ProcessSongDir).Where(g => g != null);

            var packer = new FilePacker();

            foreach (var group in groups)
            {
                packer.PackObjectToFile(group, string.Format("C:\\PCG\\{0}.pcg", group.FolderName));
            }
        }

        private ChartGroup ProcessSongDir(DirectoryInfo dir)
        {
            var files = dir.GetFiles();

            var chartGroup = new ChartGroup();
            chartGroup.FolderPath = dir.FullName;

            Console.WriteLine("Process group: {0}", chartGroup.FolderPath);

            foreach (var file in files)
            {
                if (file.Extension == ".pnc")
                {
                    var chart = ProcessPnc(file);
                    if (chart != null)
                        chartGroup.Charts.Add(ProcessPnc(file));
                }
                else
                    chartGroup.Files.Add(PackFile(file));
            }

            if (!chartGroup.Charts.Any())
                return null;

            return chartGroup;
        }

        private ChartFile PackFile(FileInfo file)
        {
            return new ChartFile
                   {
                       FileName = file.Name,
                       Data = File.ReadAllBytes(file.FullName)
                   };
        }

        private Chart ProcessPnc(FileInfo pnc)
        {
            var chart = new Chart();
            var pncContents = File.ReadAllLines(pnc.FullName);

            if (pncContents[0] != "pncv1")
                return null;

            bool inTiming = false;
            bool inObjects = false;
            bool inEditor = false;

            foreach (var line in pncContents)
            {
                if (line.Contains('='))
                    ProcessPair(chart, line);
                else if (line == "[timing]")
                    inTiming = true;
                else if (line == "[objects]")
                    inObjects = true;
                else if (line == "[editor]")
                    inEditor = true;
                else if (inObjects)
                    ProcessObject(chart, line);
                else if (inTiming)
                    ProcessTiming(chart, line);
                else if (inEditor)
                    ProcessEditor(chart, line);
            }

            return chart;
        }

        private void ProcessPair(Chart chart, string line)
        {
            var kvp = line.Split('=');

            switch (kvp[0].ToLower())
            {
                case "filename":
                    chart.FileName = kvp[1];
                    break;
                case "songname":
                    chart.SongName = kvp[1];
                    break;
                case "artist":
                    chart.ArtistName = kvp[1];
                    break;
                case "bg":
                    chart.BackgroundName = kvp[1];
                    break;
                case "difficulty":
                    chart.Difficulty = kvp[1];
                    break;
                case "leadin":
                    chart.LeadInTime = Convert.ToDouble(kvp[1], _culture);
                    break;
                case "preview":
                    chart.Preview = Convert.ToDouble(kvp[1], _culture);
                    break;
                case "creator":
                    chart.Creator = kvp[1];
                    break;
            }
        }

        private void ProcessObject(Chart chart, string line)
        {
            var keys = line.Split(',');

            if (keys[0] != "n")
                return;

            var offset = Convert.ToDouble(keys[1], _culture);
            var lane = Convert.ToInt32(keys[2], _culture);
            double holdOffset = -1;
            if (keys.Length == 5)
                holdOffset = Convert.ToInt32(keys[4], _culture);

            chart.Notes.Add(new Note(offset, holdOffset, lane));
        }

        private void ProcessTiming(Chart chart, string line)
        {
            var keys = line.Split(',');
            var offset = Convert.ToDouble(keys[0], _culture);
            var bpmSnap = Convert.ToDouble(keys[1], _culture);
            var bpmChange = Convert.ToBoolean(keys[2]);
            var noteSpeed = Convert.ToInt32(keys[3], _culture);

            chart.TimingSections.Add(new TimingSection(offset, bpmSnap, noteSpeed, bpmChange));
        }

        private void ProcessEditor(Chart chart, string line)
        {
            var keys = line.Split(',');
            var bookmarkOffset = Convert.ToDouble(keys[1], _culture);

            chart.Bookmarks.Add(new Bookmark(bookmarkOffset));
        }
    }
}
