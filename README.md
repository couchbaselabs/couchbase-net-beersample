couchbase-net-beersample
========================

The official "Beer Sample" example application using the Couchbase .NET SDK 2.0

NOTE: This is an older example that has been updated to work with Couchbase Server 5.0+. However a more complete example that demonstrates additional Couchbase Server features is the [Travel Sample](https://github.com/couchbaselabs/try-cb-dotnet).

*NOTE:* This is not an officially supported Couchbase project.

## Authentication

With Role Base Access Control introduced in Couchbase Server 5.0, it is necessary to authenticate the SDK with the cluster using a discrete username and password combination. The username and password can be added in the web.config under the "couchbase" section. If using a Couchbase Server version before RBAC was added, you must leave these values empty.

## Couchbase Server URI

The Couchbase Server URI can be updated in the web.config under the "couchbase" section and the default is to use "localhost".

## Views

This example uses a custom View definition to list all available beers and will need to be added manually. You can follow these steps to add the definition:

1. Navigate to the View definitions page in the cluster manager
2. Copy the _Beer_ design document to development
3. Add a new view to the _Beer_ design document called "all_beers"
4. Copy and paste the definition below into the new view definition
5. Publish the Beers design document, overwriting the existing one if prompted.

```javascript
function (doc, meta) {
  if (doc.type == "beer") {
    emit(meta.id, doc);
  }
}
```
