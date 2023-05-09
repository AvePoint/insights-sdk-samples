# IO.Swagger.Model.ExportOptions
## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Language** | **string** | Sets the language of the report you are about to export.  &lt;value&gt;Default: en-US Support: en-US/ja-JP/fr-FR&lt;/value&gt; | [optional] 
**SiteUrls** | **List&lt;string&gt;** | Sets the URLs of site collections for which you want to export the permission report.   &lt;value&gt;100 URLs at most.&lt;/value&gt; | [optional] 
**Emails** | **List&lt;string&gt;** | Sets the email addresses of users for which you want to export the permission report.   &lt;value&gt;  100 email addresses at most.  &lt;/value&gt; | [optional] 
**DataSources** | **List&lt;string&gt;** | Sets the workspace in which you want to export the access report of users. Multiple values are allowed.  &lt;value&gt;  microsoft teams   sharepoint online  onedrive for business  microsoft 365 group  &lt;/value&gt; | [optional] 
**ExportOptionType** | **ExportOptionType** |  | [optional] 
**ExportOrganizationLink** | **bool?** | Sets whether to export organization links.  &lt;value&gt;  True (for organization link)  False (for external link)  &lt;/value&gt; | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

