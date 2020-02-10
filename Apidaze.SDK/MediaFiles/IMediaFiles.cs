using System.Collections.Generic;

namespace Apidaze.SDK.MediaFiles
{
    public interface IMediaFiles
    {
        List<dynamic> GetMediaFilesList(bool details, string filter, string lastToken, int maxItems);

        dynamic UploadMediaFile(string name, string path, bool isStream = true);

        dynamic DeleteMediaFile(string fileName);

        dynamic DownloadMediaFile(string fileName);

        dynamic ShowMediaFileSummary(string fileName);
    }
}