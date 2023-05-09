# IO.Swagger.Model.LinkViewModule
## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Id** | **string** | The ID in the link summary. | [optional] 
**SiteId** | **string** | The site ID in which the object is shared by the link. | [optional] 
**SelfId** | **string** | The selfID of the object which is shared by the link. | [optional] 
**LinkId** | **string** | The link ID. | [optional] 
**LogonName** | **string** | The link login name. | [optional] 
**CreateTime** | **DateTime?** | The time when the link is created. | [optional] 
**ExpireTime** | **DateTime?** | The time when the link is expired.  This is only available for anonymous links. | [optional] 
**Name** | **string** | The object name that is shared via the link. | [optional] 
**ObjectUrl** | **string** | The object URL that is shared via the link. | [optional] 
**LinkType** | **string** | The link type.  &lt;value&gt;  32(for flexible link)  64 (for organization link)  128 (for anonymous link)  &lt;/value&gt; | [optional] 
**ShareBy** | **string** | The user who created the link. | [optional] 
**InheritFrom** | **string** | The parent from which the permission inherits. | [optional] 
**Inherittype** | **string** | The status whether the permission is inherited. | [optional] 
**ShareWith** | **int?** | The number of users and groups with whom the link is shared. | [optional] 
**LinkUrl** | **string** | The link URL. | [optional] 
**Permission** | **string** | The permission with which the link is shared. | [optional] 
**FileType** | **string** | The type of the object shared via the link. | [optional] 
**BlockDownLoad** | **string** | The status whether the Block Download setting is enabled for the link.  &lt;value&gt;  Yes (for blocking download)  No (for not blocking download)  &lt;/value&gt; | [optional] 
**SensitiveLevel** | **string** | The sensitivity level of the object shared via the link. | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

