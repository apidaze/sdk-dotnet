using Apidaze.SDK.Common;
using Apidaze.SDK.MediaFiles;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using static Apidaze.SDK.Tests.Unit.TestUtil;

namespace Apidaze.SDK.Tests.Unit.Calls
{
    /// <summary>
    /// Defines test class CallsTests.
    /// Implements the <see cref="BaseTest" />
    /// </summary>
    /// <seealso cref="BaseTest" />
    [TestClass]
    public class MediaFilesTests : BaseTest
    {

        /// <summary>
        /// The media files API
        /// </summary>
        private MediaFiles.MediaFiles _mediaFilesApi;

        /// <summary>
        /// The source files dir
        /// </summary>
        private static readonly string SOURCE_FILES_DIR =
            Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase,
                @"MediaFiles/TestData"));

        /// <summary>
        /// The source file name
        /// </summary>
        private static readonly string SOURCE_FILE_NAME = "mediafile.wav";

        /// <summary>
        /// The source file
        /// </summary>
        private static readonly FileInfo SOURCE_FILE = new FileInfo(Path.Combine(SOURCE_FILES_DIR, SOURCE_FILE_NAME));

        /// <summary>
        /// Startups this instance.
        /// </summary>
        [TestInitialize]
        public void Startup()
        {
            _mediaFilesApi = new MediaFiles.MediaFiles(MockIRestClient.Object, CredentialsForTest);
        }

        /// <summary>
        /// Cleans up.
        /// </summary>
        [TestCleanup]
        public void CleanUp()
        {
            File.Delete(SOURCE_FILE.FullName);
        }

        /// <summary>
        /// Defines the test method GetMediaFileList_ListOfMediaFileListAreOnServer_ReturnsListOfMediaFiles.
        /// </summary>
        [TestMethod]
        public void GetMediaFileList_ListOfMediaFileListAreOnServer_ReturnsListOfMediaFiles()
        {
            // Arrange
            var mediaFiles = new List<MediaFile> { new MediaFile() { Name = "file1.wav" }, new MediaFile() { Name = "file2.wav" } };
            MockIRestClient.Setup(rc => rc.Execute(It.IsAny<RestRequest>())).Returns(
                new RestResponse { StatusCode = HttpStatusCode.OK, Content = JsonConvert.SerializeObject(mediaFiles) });

            // Act
            var result = _mediaFilesApi.GetMediaFilesList();

            // Assert
            MockIRestClient.Verify(x => x.Execute(It.IsAny<RestRequest>()), Times.Once);
            mediaFiles.Should().BeEquivalentTo(result);
        }

        /// <summary>
        /// Defines the test method DownloadMediaFile_FileExistsOnServer_MemoryStream.
        /// </summary>
        [TestMethod]
        public void DownloadMediaFile_FileExistsOnServer_MemoryStream()
        {
            // Arrange
            var expectedStream = new FileStream(SOURCE_FILE.FullName, FileMode.Create);
            MockIRestClient.Setup(rc => rc.DownloadData(It.IsAny<RestRequest>())).Returns(expectedStream.ReadAsBytes());

            // Act
            var result = _mediaFilesApi.DownloadMediaFile(SOURCE_FILE_NAME);

            // Assert
            Assert.AreSame(expectedStream.ReadAsBytes(), result);
            MockIRestClient.Verify(x => x.DownloadData(It.IsAny<RestRequest>()), Times.Once);

            // Clean
            expectedStream.Close();
        }

        /// <summary>
        /// Defines the test method UploadMediaFile_FileWavFromDisk_StatusOK.
        /// </summary>
        [TestMethod]
        public void UploadMediaFile_FileWavFromDisk_StatusOK()
        {
            // Arrange
            var expectedStream = new FileStream(SOURCE_FILE.FullName, FileMode.Create);

            MockIRestClient.Setup(rc => rc.Execute(It.IsAny<RestRequest>())).Returns(new RestResponse { StatusCode = HttpStatusCode.OK, Content = JsonConvert.SerializeObject(new Response()) });

            // Act
            var result = _mediaFilesApi.UploadMediaFile("test", SOURCE_FILE.FullName);

            // Assert
            Assert.IsNotNull(result);
            MockIRestClient.Verify(x => x.Execute(It.IsAny<RestRequest>()), Times.Once);

            // Clean
            expectedStream.Close();
        }

        /// <summary>
        /// Defines the test method DeleteMediaFileFromServer_FileName_NoContent.
        /// </summary>
        [TestMethod]
        public void DeleteMediaFileFromServer_FileName_NoContent()
        {
            // Arrange
            MockIRestClient.Setup(rc => rc.Execute(It.IsAny<RestRequest>())).Returns(new RestResponse { StatusCode = HttpStatusCode.NoContent });

            // Act
            _mediaFilesApi.DeleteMediaFile("test");

            // Assert
            MockIRestClient.Verify(x => x.Execute(It.IsAny<RestRequest>()), Times.Once);
        }


        /// <summary>
        /// Defines the test method ShowSummaryMediaFile_FileName_HeadersContent.
        /// </summary>
        [TestMethod]
        public void ShowSummaryMediaFile_FileName_NoContent()
        {
            // Arrange
            MockIRestClient.Setup(rc => rc.Execute(It.IsAny<RestRequest>())).Returns(new RestResponse { StatusCode = HttpStatusCode.NoContent });

            // Act
            _mediaFilesApi.ShowMediaFileSummary("test");

            // Assert
            MockIRestClient.Verify(x => x.Execute(It.IsAny<RestRequest>()), Times.Once);
        }
    }
}
