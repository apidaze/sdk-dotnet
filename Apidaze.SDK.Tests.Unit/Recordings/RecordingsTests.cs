using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Apidaze.SDK.Messages;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Extensions;
using static Apidaze.SDK.Tests.Unit.TestUtil;

namespace Apidaze.SDK.Tests.Unit.Recordings
{
    [TestClass]
    public class RecordingsTests : BaseTest
    {
        /// <summary>
        /// The recordings API
        /// </summary>
        private SDK.Recordings.Recordings _recordingsApi;

        private static readonly string SOURCE_FILES_DIR = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, @"Recordings/TestData"));
        private static readonly string SOURCE_FILE_NAME = "mediafile.wav";
        /// <summary>
        /// The source file
        /// </summary>
        private static readonly FileInfo SOURCE_FILE = new FileInfo(Path.Combine(SOURCE_FILES_DIR, SOURCE_FILE_NAME));
        private static readonly string TARGET_DIR = Path.GetFullPath(@"target/");
        private static readonly FileInfo TARGET_FILE = new FileInfo(Path.Combine(TARGET_DIR, SOURCE_FILE_NAME));
        /// <summary>
        /// The target file with changed name
        /// </summary>
        private static readonly FileInfo TARGET_FILE_WITH_CHANGED_NAME = new FileInfo(Path.Combine(TARGET_DIR, "new-file.wav"));

        /// <summary>
        /// Startups this instance.
        /// </summary>
        [TestInitialize]
        public void Startup()
        {
            _recordingsApi = new SDK.Recordings.Recordings(MockIRestClient.Object, CredentialsForTest);
            File.Delete(TARGET_FILE.Name);
            File.Delete(TARGET_FILE_WITH_CHANGED_NAME.Name);
        }

        /// <summary>
        /// Cleans up.
        /// </summary>
        [TestCleanup]
        public void CleanUp()
        {
            File.Delete(TARGET_FILE.FullName);
            File.Delete(SOURCE_FILE.FullName);
            File.Delete(TARGET_FILE_WITH_CHANGED_NAME.FullName);
        }

        /// <summary>
        /// Defines the test method GetRecordingsList_ListOfRecordingsAreOnServer_ReturnsListOfRecordings.
        /// </summary>
        [TestMethod]
        public void GetRecordingsList_ListOfRecordingsAreOnServer_ReturnsListOfRecordings()
        {
            // Arrange
            var recordings = new List<string> { "file1.wav", "file2.wav", "file3.wav" };
            MockIRestClient.Setup(rc => rc.Execute<List<string>>(It.IsAny<RestRequest>())).Returns(
                new RestResponse<List<string>> { StatusCode = HttpStatusCode.OK, Content = JsonConvert.SerializeObject(recordings) });

            // Act
            var result = _recordingsApi.GetRecordingsList();

            // Assert
            MockIRestClient.Verify(x => x.Execute<List<string>>(It.IsAny<RestRequest>()), Times.Once);
            recordings.Should().BeEquivalentTo(result);
        }

        /// <summary>
        /// Defines the test method DeleteRecording_FileName_DeleteExecutedOnce.
        /// </summary>
        [TestMethod]
        public void DeleteRecording_FileName_DeleteExecutedOnce()
        {
            // Arrange
            var fileName = "file1.wav";

            MockIRestClient.Setup(rc => rc.Execute(It.IsAny<RestRequest>())).Returns(
                new RestResponse { StatusCode = HttpStatusCode.NoContent });

            // Act
            _recordingsApi.DeleteRecording(fileName);

            // Assert
            MockIRestClient.Verify(x => x.Execute(It.IsAny<RestRequest>()), Times.Once);
        }

        /// <summary>
        /// Defines the test method DownloadRecording_SourceFile_Stream.
        /// </summary>
        [TestMethod]
        public void DownloadRecording_SourceFile_Stream()
        {
            // Arrange
            var expectedStream = new FileStream(SOURCE_FILE.FullName, FileMode.Create);
            MockIRestClient.Setup(rc => rc.DownloadData(It.IsAny<RestRequest>())).Returns(expectedStream.ReadAsBytes());

            // Act
            var result = _recordingsApi.DownloadRecording(SOURCE_FILE_NAME);

            // Assert
            Assert.AreSame(expectedStream.ReadAsBytes(), result.ReadAsBytes());
            MockIRestClient.Verify(x => x.DownloadData(It.IsAny<RestRequest>()), Times.Once);

            // Clean
            result.Close();
            expectedStream.Close();
        }

        /// <summary>
        /// Defines the test method DownloadRecordingAsync_SourceFile_Stream.
        /// </summary>
        [TestMethod]
        public async Task DownloadRecordingAsync_SourceFile_Stream()
        {
            // Arrange
            var expectedStream = new FileStream(SOURCE_FILE.FullName, FileMode.Create);
            MockIRestClient.Setup(rc => rc.ExecuteTaskAsync(It.IsAny<RestRequest>())).ReturnsAsync(
                  new RestResponse() { StatusCode = HttpStatusCode.OK, RawBytes = expectedStream.ReadAsBytes() });

            // Act
            await _recordingsApi.DownloadRecordingToFileAsync(SOURCE_FILE_NAME, TARGET_DIR);

            // Assert
            MockIRestClient.Verify(x => x.ExecuteTaskAsync(It.IsAny<RestRequest>()), Times.Once);
            Assert.AreEqual(Path.GetFileName(expectedStream.Name), TARGET_FILE.Name);

            // Clean
            expectedStream.Close();
        }

        /// <summary>
        /// Defines the test method DownloadRecordingAsync_ApiReturnsError_FailureInvoked.
        /// </summary>
        [TestMethod]
        public async Task DownloadRecordingAsync_ApiReturnsError_FailureInvoked()
        {
            // Arrange
            MockIRestClient.Setup(rc => rc.ExecuteTaskAsync(It.IsAny<RestRequest>())).ReturnsAsync(
                new RestResponse() { StatusCode = HttpStatusCode.InternalServerError });

            // Act + Assert
            await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => _recordingsApi.DownloadRecordingToFileAsync(SOURCE_FILE_NAME, TARGET_DIR));
        }

        /// <summary>
        /// Defines the test method DownloadRecordingToFile_SourceFile_FileWithOriginalName.
        /// </summary>
        [TestMethod]
        public void DownloadRecordingToFile_SourceFile_FileWithOriginalName()
        {
            // Arrange
            var expectedStream = new FileStream(SOURCE_FILE.FullName, FileMode.Create);
            MockIRestClient.Setup(rc => rc.Execute(It.IsAny<RestRequest>())).Returns(
                new RestResponse() { StatusCode = HttpStatusCode.OK, RawBytes = expectedStream.ReadAsBytes() });

            // Act
            var result = _recordingsApi.DownloadRecordingToFile(SOURCE_FILE_NAME, TARGET_DIR);

            // Assert
            MockIRestClient.Verify(x => x.Execute(It.IsAny<RestRequest>()), Times.Once);
            Assert.AreEqual(Path.GetFileName(expectedStream.Name), result.Name);

            // Clean
            expectedStream.Close();
        }

        /// <summary>
        /// Defines the test method DownloadRecordingToFile_SourceFile_FileWithChangedName.
        /// </summary>
        [TestMethod]
        public void DownloadRecordingToFile_SourceFile_FileWithChangedName()
        {
            // Arrange
            var expectedStream = new FileStream(SOURCE_FILE.FullName, FileMode.Create);
            MockIRestClient.Setup(rc => rc.Execute(It.IsAny<RestRequest>())).Returns(
                new RestResponse() { StatusCode = HttpStatusCode.OK, RawBytes = expectedStream.ReadAsBytes() });

            // Act
            var result = _recordingsApi.DownloadRecordingToFile(SOURCE_FILE_NAME, TARGET_FILE_WITH_CHANGED_NAME.FullName);

            // Assert
            MockIRestClient.Verify(x => x.Execute(It.IsAny<RestRequest>()), Times.Once);
            Assert.AreEqual(Path.GetFileName(TARGET_FILE_WITH_CHANGED_NAME.FullName), result.Name);

            // Clean
            expectedStream.Close();
        }
    }
}
