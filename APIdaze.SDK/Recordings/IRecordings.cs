using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace APIdaze.SDK.Recordings
{
    public interface IRecordings
    {
        IEnumerable<string> GetRecordingsList();

        Stream DownloadRecording(string sourceFileName);

        void DownloadRecodingToFileAsync(string sourceFileName, string target);

        void DownloadRecodingToFileAsync(string sourceFileName, string target, bool replaceExisting);

        FileInfo DownloadRecodingToFile(string sourceFileName, string target);

        FileInfo DownloadRecodingToFile(string sourceFileName, string target, bool replaceExisting);

        void DeleteRecording(string fileName);
    }
}