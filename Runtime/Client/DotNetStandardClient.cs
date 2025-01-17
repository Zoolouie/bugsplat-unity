﻿using BugSplatDotNetStandard;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace BugSplatUnity.Runtime.Client
{
    internal class DotNetStandardClient : INativeCrashReportClient, IExceptionClient<Task<HttpResponseMessage>>
    {
        private readonly BugSplatDotNetStandard.BugSplat _bugsplat;

        public DotNetStandardClient(BugSplatDotNetStandard.BugSplat bugsplat)
        {
            _bugsplat = bugsplat;
        }

        public Task<HttpResponseMessage> Post(string stackTrace, IReportPostOptions options = null)
        {
            return _bugsplat.Post(stackTrace, CreateExceptionPostOptions(options));
        }

        public Task<HttpResponseMessage> Post(Exception ex, IReportPostOptions options = null)
        {
            return _bugsplat.Post(ex, CreateExceptionPostOptions(options));
        }

        public Task<HttpResponseMessage> Post(FileInfo minidumpFileInfo, IReportPostOptions options = null)
        {
            return _bugsplat.Post(minidumpFileInfo, CreateMinidumpPostOptions(options));
        }

        private ExceptionPostOptions CreateExceptionPostOptions(IReportPostOptions options)
        {
            var exceptionPostOptions = new ExceptionPostOptions();
            exceptionPostOptions.AdditionalAttachments.AddRange(options.AdditionalAttachments);
            exceptionPostOptions.AdditionalFormDataParams.AddRange(options.AdditionalFormDataParams);
            exceptionPostOptions.Description = options.Description;
            exceptionPostOptions.Email = options.Email;
            exceptionPostOptions.Key = options.Key;
            exceptionPostOptions.User = options.User;
            exceptionPostOptions.ExceptionType = (BugSplatDotNetStandard.BugSplat.ExceptionTypeId)options.CrashTypeId;
            return exceptionPostOptions;
        }

        private MinidumpPostOptions CreateMinidumpPostOptions(IReportPostOptions options)
        {
            var minidumpPostOptions = new MinidumpPostOptions();
            minidumpPostOptions.AdditionalAttachments.AddRange(options.AdditionalAttachments);
            minidumpPostOptions.AdditionalFormDataParams.AddRange(options.AdditionalFormDataParams);
            minidumpPostOptions.Description = options.Description;
            minidumpPostOptions.Email = options.Email;
            minidumpPostOptions.Key = options.Key;
            minidumpPostOptions.User = options.User;
            minidumpPostOptions.MinidumpType = (BugSplatDotNetStandard.BugSplat.MinidumpTypeId)options.CrashTypeId;
            return minidumpPostOptions;
        }
    }
}
