# IO.Swagger.Model.UserSummaryViewModule
## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**DisplayName** | **string** | The display name of the user. | [optional] 
**LoginName** | **string** | The login name of the user. | [optional] 
**Email** | **string** | The email address of the user. | [optional] 
**UserStatus** | **string** | The user’s sign-in status in Microsoft 365.  &lt;value&gt;  Blocked (for a user in Azure AD whose sign-in to Microsoft 365 is blocked)  Activate (for a user in Azure AD whose sign in to Microsoft 365 is active)  Not in Azure AD (for a user who is not in Azure AD)  &lt;/value&gt; | [optional] 
**TrustStatus** | **string** | The user’s trust status in Insights for Microsoft 365.  &lt;value&gt;  Trusted (for a trusted user in Insights for Microsoft 365)  Not Trusted (for a user who is not trusted in Insights for Microsoft 365)  &lt;/value&gt; | [optional] 
**SensitiveItems** | **int?** | The number of sensitive items to which the user has been granted direct permissions. | [optional] 
**LastSignin** | **string** | The user’s last sign-in time to Microsoft 365. | [optional] 
**CreatedOn** | **DateTime?** | The time when the user is created in Azure AD. | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

