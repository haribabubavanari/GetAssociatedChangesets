using System;
using Microsoft.VisualStudio.Services.Common;
using System.Net;
using WindowsCredential = Microsoft.VisualStudio.Services.Common.WindowsCredential;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.VersionControl.Client;
using Microsoft.TeamFoundation.Client;

namespace WorkitemChangesetsReport
{
    class TFSConnect
    {
        /// <summary>
        /// Get Version COntrol Server object
        /// </summary>
        /// <returns></returns>
        public static VersionControlServer GetVersionControlServer()
        {
            TfsTeamProjectCollection tfs = new TfsTeamProjectCollection(new Uri(WorkitemChangesetsReport.Properties.Settings.Default.TfsUrl, UriKind.Absolute),
                                                         new VssCredentials(new WindowsCredential(new NetworkCredential(
                                                         WorkitemChangesetsReport.Properties.Settings.Default.UserName,
                                                         WorkitemChangesetsReport.Properties.Settings.Default.Password,
                                                         WorkitemChangesetsReport.Properties.Settings.Default.Domain)))
                                                         );

            return tfs.GetService<VersionControlServer>();
        }

        /// <summary>
        /// Get Workitem Tracking Client object
        /// </summary>
        /// <returns></returns>
        public static WorkItemTrackingHttpClient GetWorkitemClient()
        {
            var _workitemClient = new WorkItemTrackingHttpClient(new Uri(WorkitemChangesetsReport.Properties.Settings.Default.TfsUrl, UriKind.Absolute),
                                                         new VssCredentials(new WindowsCredential(new NetworkCredential(
                                                         WorkitemChangesetsReport.Properties.Settings.Default.UserName,
                                                         WorkitemChangesetsReport.Properties.Settings.Default.Password,
                                                         WorkitemChangesetsReport.Properties.Settings.Default.Domain)))
                                                         );
            return _workitemClient;
        }

    }
}
