$runningMode = $OctopusRunningMode
$identityServiceApiUrl = $OctopusIdentityServiceApiUrl
$recordStoreApplicationApiUrl = $OctopusRecordStoreApplicationApiUrl
$membershipMaintenanceApiUrl = $OctopusMembershipMaintenanceApiUrl
$signalRHubUrl = $OctopusSignalRHubUrl
$invalidsApiUrl = $OctopusInvalidsApiUrl
$workflowServicesUrl = $OctopusWorkflowServicesUrl
$policyQueryApiUrl = $OctopusPolicyQueryApiUrl
$applicationFormsUrl = $OctopusApplicationFormsUrl
$serviceErrorApiUrl = $OctopusServiceErrorUrl
$applicationformssearchapiurl = $OctopusSearchUrl
$investmentsUrl = $OctopusInvestmentsUrl
$hermesMessagingMonitorApiUrl  = $OctopusHermesMessagingMonitorApiUrl
$hermesMessagingMonitorUnityUrl = $OctopusHermesMessagingMonitorUnityUrl
$buildNumber = $OctopusParameters["Octopus.Release.Number"] 
$encashmentsUnityUrl = $OctopusEncashmentsUnityUrl
$encashmentsApiUrl = $OctopusEncashmentsApiUrl
$policyUnityUrl = $OctopusPolicyUnityUrl
$identityMaintenanceUnityUrl = $OctopusIdentityMaintenanceUnityUrl
$investmentsIntegrationUrl = $OctopusInvestmentsIntegrationUrl
$presentationVenueUnityUrl = $OctopusPresentationVenueUnityUrl
$applicationFormSearchUnityUrl = $OctopusApplicationFormSearchUnityUrl
$presentationVenueApiUrl = $OctopusPresentationVenueApiUrl
$outboundSmsUnityUrl = $OctopusOutboundSmsUnityUrl
$outboundSmsApiUrl = $OctopusOutboundSmsApiUrl
$applicationFormCapturePresentationUrl = $OctopusApplicationFormsCaptureUnityUrl
								   
set-content Configuration/serverConfiguration.json "`
{`
""runningMode"": ""$runningMode"", ""applicationId"": {},`
""noAccessRightsUrl"": ""/NoAccessRights/"", ""identityNotFoundUrl"": `
""/IdentityNotFound/"", ""applicationIcon"": """", ""useAuth"": ""true"",  `
""identityServiceApiUrl"": ""$identityServiceApiUrl"", `
""recordStoreApplicationApiUrl"": ""$recordStoreApplicationApiUrl"",`
""membershipMaintenanceApiUrl"":""$membershipMaintenanceApiUrl"",`
""signalRHubUrl"":""$signalRHubUrl"",`
""applicationFormsUrl"":""$applicationFormsUrl"",`
""policyQueryApiUrl"":""$policyQueryApiUrl"", `
""serviceErrorApiUrl"":""$serviceErrorApiUrl"",`
""applicationFormTrackingSearchUrl"":""$applicationformssearchapiurl"",`
""investmentsUrl"":""$OctopusInvestmentsUrl"",`
""presentationVenueApiUrl"":""$OctopusPresentationVenueApiUrl"",`
""hermesMessagingMonitorApiUrl"":""$hermesMessagingMonitorApiUrl"",`
""encashmentApiUrl"":""$encashmentsApiUrl"",`
""hermesMessagingMonitorUnityUrl"": { ""ApplicationId"": ""Hermes.MessagingMonitor"", ""UnityUrl"": ""$hermesMessagingMonitorUnityUrl""},`
""encashmentsUnityUrl"": { ""ApplicationId"": ""Clientele.Encashments"", ""UnityUrl"": ""$encashmentsUnityUrl""}, `
""policyUnityUrl"": { ""ApplicationId"": ""Clientele.Policy"", ""UnityUrl"": ""$policyUnityUrl""}, `
""identityMaintenanceUnityUrl"": { ""ApplicationId"": ""Clientele.IdentityMaintenance"", ""UnityUrl"": ""$identityMaintenanceUnityUrl""}, `
""investmentsIntegrationUrl"": { ""ApplicationId"": ""Clientele.Investments"", ""UnityUrl"": ""$investmentsIntegrationUrl""}, `
""outboundSmsUnityUrl"": { ""ApplicationId"": ""Clientele.OutboundSms"", ""UnityUrl"": ""$outboundSmsUnityUrl""}, `
""outboundSmsApiUrl"":""$outboundSmsApiUrl"",`
""venueIntegrationLocationUrl"": { ""ApplicationId"": ""Clientele.VenueLocation"", ""UnityUrl"": ""$presentationVenueUnityUrl"" }, `
""applicationFormSearchIntegrationUrl"": { ""ApplicationId"": ""Clientele.Search.SearchService"", ""UnityUrl"": ""$applicationFormSearchUnityUrl"" }, `
""applicationFormsCapturePresentationUrl"": { ""ApplicationId"": ""Clientele.ApplicationFormsCapture"", ""UnityUrl"": ""$applicationFormCapturePresentationUrl"" }, `
""BuildNumber"" : ""$buildNumber"" `
}"

