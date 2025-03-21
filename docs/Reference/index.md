---
title: Reference
layout: page
pageType: index
toc: true
pageStatus: published
discussionId: 
---

Azure DevOps Migration Tools are mainly powered by configuration which allows you to control most aspects of the execution flow. This page will guide you through the configuration options available to you.

## Creating a Configuration File

The easyest way to get started with the configruation is to create both a reference file, thats one with everything, and a minimal file that will get you started using the tool. 

### Creating a Reference File

If you run `devopsmigration init --options Reference -c configuration-ref.json` the output should be a file with all posible options using the Sample data.

### Creating a Minimal File

To get a file that you can use its best to start with a minimal file. If you run `devopsmigration init --options Basic --overwrite` thsi will create that minimal file.

### Other options

Right now we support:

- *Reference* - Create the reference file with everything
- *Basic | WorkItemTracking* - Create a minimal file with just the WorkItemTracking processor enabled
- *PipelineProcessor* - Create a minimal file with just the PipelineProcessor enabled

**Note:** Azure DevOps Migration Tools do not ship with internal default configuration and will not function without one.

To create your config file just type `devopsmigration init` in the directory that you unzipped the tools and a minimal `configuration.json` configuration
file will be created. Modify this as you need.

Note that the generated file show all the possible options, you configuration file will probably only need a subset of those shown.

### Global configuration
The global configuration created by the `init` command look something like this:

```json
{
  "Serilog": {
    "MinimumLevel": "Debug"
  },
  "MigrationTools": {
    "Version": "16.0",
    "Endpoints": {
      "Source": {
        "EndpointType": "TfsTeamProjectEndpoint",
        "Collection": "https://dev.azure.com/nkdagility-preview/",
        "Project": "migrationSource1",
        "AllowCrossProjectLinking": false,
        "Authentication": {
          "AuthenticationMode": "AccessToken",
          "AccessToken": "",
          "NetworkCredentials": {
            "UserName": "",
            "Password": "",
            "Domain": ""
          }
        },
        "LanguageMaps": {
          "AreaPath": "Area",
          "IterationPath": "Iteration"
        }
      },
      "Target": {
        "EndpointType": "TfsTeamProjectEndpoint",
        "Collection": "https://dev.azure.com/nkdagility-preview/",
        "Project": "migrationTest5",
        "TfsVersion": "AzureDevOps",
        "Authentication": {
          "AuthenticationMode": "AccessToken",
          "AccessToken": "",
          "NetworkCredentials": {
            "UserName": "",
            "Password": "",
            "Domain": ""
          }
        },
        "ReflectedWorkItemIdField": "nkdScrum.ReflectedWorkItemId",
        "AllowCrossProjectLinking": false,
        "LanguageMaps": {
          "AreaPath": "Area",
          "IterationPath": "Iteration"
        }
      }
    },
    "CommonTools": {
      "WorkItemTypeMappingTool": {
        "Enabled": true,
        "Mappings": {
          "User Story": "Product Backlog Item"
        }
      },
      "StringManipulatorTool": {
        "Enabled": true,
        "MaxStringLength": 1000000,
        "Manipulators": [
          {
            "$type": "RegexStringManipulator",
            "Enabled": true,
            "Pattern": "[^( -~)\n\r\t]+",
            "Replacement": "",
            "Description": "Remove invalid characters from the end of the string"
          }
        ]
      },
      "TfsAttachmentTool": {
        "RefName": "TfsAttachmentTool",
        "Enabled": true,
        "ExportBasePath": "c:\\temp\\WorkItemAttachmentExport",
        "MaxRevisions": 480000000
      },
      "TfsChangeSetMappingTool": {
        "Enabled": true,
        "File": "C:\\temp\\ChangeSetMappingFile.json"
      },
      "FieldMappingTool": {
        "Enabled": true,
        "FieldMaps": [
          {
            "FieldMapType": "FieldtoFieldMap",
            "ApplyTo": [ "SomeWorkItemType" ],
            "sourceField": "System.AcceptanceCriteria",
            "targetField": "System.AcceptanceCriteria2"

          },
          {
            "FieldMapType": "FieldtoFieldMap",
            "ApplyTo": [ "SomeWorkItemType" ],
            "sourceField": "System.Description",
            "targetField": "System.Description2"

          }
        ]
      },
      "GitRepoMappingTool": {
        "Enabled": true,
        "Mappings": {
          "Source Repo Name": "Target Repo Name"
        }
      },
      "TfsNodeStructureTool": {
        "Enabled": true,
        "Areas": {
          "Filters": [ " *\\Team 1,*\\Team 1\\**" ],
          "Mappings": {
            "^Skypoint Cloud([\\\\]?.*)$": "MigrationTest5$1",
            "^7473924d-c47f-4089-8f5c-077c728b576e([\\\\]?.*)$": "MigrationTest5$1"
          }
        },
        "Iterations": {
          "Filters": [],
          "Mappings": {
            "^Skypoint Cloud([\\\\]?.*)$": "MigrationTest5$1",
            "^7473924d-c47f-4089-8f5c-077c728b576e([\\\\]?.*)$": "MigrationTest5$1"
          }
        },
        "ShouldCreateMissingRevisionPaths": true,
        "ReplicateAllExistingNodes": true
      },
      "TfsRevisionManagerTool": {
        "Enabled": true,
        "ReplayRevisions": true,
        "MaxRevisions": 0
      },
      "TfsTeamSettingsTool": {
        "Enabled": true,
        "MigrateTeamSettings": true,
        "UpdateTeamSettings": true,
        "MigrateTeamCapacities": true,
        "Teams": [ "Team 1", "Team 2" ]
      }
    },
    "Processors": [
      {
        "ProcessorType": "TfsWorkItemMigrationProcessor",
        "Enabled": true,
        "UpdateCreatedDate": true,
        "UpdateCreatedBy": true,
        "WIQLQuery": "SELECT [System.Id] FROM WorkItems WHERE [System.TeamProject] = @TeamProject AND [System.WorkItemType] NOT IN ('Test Suite', 'Test Plan','Shared Steps','Shared Parameter','Feedback Request') ORDER BY [System.ChangedDate] desc",
        "FixHtmlAttachmentLinks": false,
        "WorkItemCreateRetryLimit": 5,
        "FilterWorkItemsThatAlreadyExistInTarget": false,
        "PauseAfterEachWorkItem": false,
        "AttachRevisionHistory": false,
        "GenerateMigrationComment": true,
        "WorkItemIDs": [ 12 ],
        "MaxGracefulFailures": 0,
        "SkipRevisionWithInvalidIterationPath": false,
        "SkipRevisionWithInvalidAreaPath": false
      }
    ]
  }
}
```

## Anatomy of the Configuration File


### Endpoints

{% include content-collection-table.html collection = "reference" typeName = "Endpoints" %}

### Processors

{% include content-collection-table.html collection = "reference" typeName = "Processors" %}

### Common Tools

{% include content-collection-table.html collection = "reference" typeName = "Tools" %}


And the description of the available options are:

#### TelemetryEnableTrace

Allows you to submit trace to Application Insights to allow the development team to diagnose any issues that may be found. If you are submitting a support ticket then please include the Session GUID found in your log file for that run. This will help us find the problem.

**Note:** All exceptions that you encounter will surface inside of Visual Studio as the developers are working on the source. This will make sure that they tackle issues as they arise.

#### Source & Target
Both the `Source` and `Target` entries hold the collection URL and the Team Project name that you are connecting to. The `Source` is where the tool will read the data to migrate. The `Target` is where the tool will write the data.

For multi Language support you can add the name used in the source and target for both 'Area' and 'Iteration'. This allows a migration from one language version of TFS / Azure DevOps to another.

#### ReflectedWorkItemIdField

This is the field that will be used to store the state for the migration . See [Server Configuration](server-configuration.md)

#### CommonEnrichersConfig

This configuration allows to set the configuration for some enrichers at the global level, and the configuration can
then be re-used in processors. Currently supported only by `TfsNodeStructureOption`, to be re-used in
`WorkItemMigrationConfig` and `TestPlansAndSuitesMigrationConfig`.

#### WorkItemMigrationConfig
You can specify BasePaths for Areas/Iterations to migrate. The area/iteration has to start with that string to be eligible for migration.
E.g. BasePath = "Product\\Area\\Path1"

With existing areas:
"Product\\Area\\Path1\\TestArea"
"SomeOtherProduct\\Area\\Path1\TestArea"
"Product\\OtherArea\\Path1\\TestArea"

only the first one matches the BasePath "Product\\Area\\Path1" and would be migrated, the other ones are ignored.

#### Field Maps

There are a number of field maps available for when you need to change the data as you are processing it. These mappings work for both in place bulk edit, and for project to project migrations.

```
"FieldMaps": [
    {
      "$type": "VstsSyncMigrator.Engine.Configuration.FieldMap.MultiValueConditionalMapConfig",
      "WorkItemTypeName": "*",
      "sourceFieldsAndValues": {
        "Field1": "Value1",
        "Field2": "Value2"
      },
      "targetFieldsAndValues": {
        "Field1": "Value1",
        "Field2": "Value2"
      }
    },
    {
      "$type": "VstsSyncMigrator.Engine.Configuration.FieldMap.FieldBlankMapConfig",
      "WorkItemTypeName": "*",
      "targetField": "TfsMigrationTool.ReflectedWorkItemId"
    },
    {
      "$type": "VstsSyncMigrator.Engine.Configuration.FieldMap.FieldValueMapConfig",
      "WorkItemTypeName": "*",
      "sourceField": "System.State",
      "targetField": "System.State",
      "defaultValue": "New",
      "valueMapping": {
        "Approved": "New",
        "New": "New",
        "Committed": "Active",
        "In Progress": "Active",
        "To Do": "New",
        "Done": "Closed"
      }
    },
    {
      "$type": "VstsSyncMigrator.Engine.Configuration.FieldMap.FieldtoFieldMapConfig",
      "WorkItemTypeName": "*",
      "sourceField": "Microsoft.VSTS.Common.BacklogPriority",
      "targetField": "Microsoft.VSTS.Common.StackRank"
    },
    {
      "$type": "VstsSyncMigrator.Engine.Configuration.FieldMap.FieldtoFieldMultiMapConfig",
      "WorkItemTypeName": "*",
      "SourceToTargetMappings": {
        "SourceField1": "TargetField1",
        "SourceField2": "TargetField2"
      }
    },
    {
      "$type": "VstsSyncMigrator.Engine.Configuration.FieldMap.FieldtoTagMapConfig",
      "WorkItemTypeName": "*",
      "sourceField": "System.State",
      "formatExpression": "ScrumState:{0}"
    },
    {
      "$type": "VstsSyncMigrator.Engine.Configuration.FieldMap.FieldMergeMapConfig",
      "WorkItemTypeName": "*",
      "sourceField1": "System.Description",
      "sourceField2": "Microsoft.VSTS.Common.AcceptanceCriteria",
      "targetField": "System.Description",
      "formatExpression": "{0} <br/><br/><h3>Acceptance Criteria</h3>{1}",
      "doneMatch": "##DONE##"
    },
    {
      "$type": "VstsSyncMigrator.Engine.Configuration.FieldMap.RegexFieldMapConfig",
      "WorkItemTypeName": "*",
      "sourceField": "COMPANY.PRODUCT.Release",
      "targetField": "COMPANY.DEVISION.MinorReleaseVersion",
      "pattern": "PRODUCT \\d{4}.(\\d{1})",
      "replacement": "$1"
    },
    {
      "$type": "VstsSyncMigrator.Engine.Configuration.FieldMap.FieldValuetoTagMapConfig",
      "WorkItemTypeName": "*",
      "sourceField": "Microsoft.VSTS.CMMI.Blocked",
      "pattern": "Yes",
      "formatExpression": "{0}"
    },
    {
      "$type": "VstsSyncMigrator.Engine.Configuration.FieldMap.TreeToTagMapConfig",
      "WorkItemTypeName": "*",
      "toSkip": 3,
      "timeTravel": 1
    }
  ],
  ```

#### Iteration Maps and Area Maps

These two configuration elements apply after the `NodeBasePaths` selector, i.e.
only on Areas and Iterations that have been selected for migration. They allow
to change the area path, respectively the iteration path, of migrated work items.

These remapping rules are applied both while creating path nodes in the target
project and when migrating work items.

These remapping rules are applied with a higher priority than the
`PrefixProjectToNodes` option. This means that if no declared rule matches the
path and the `PrefixProjectToNodes` option is enabled, then the old behavior is
used.

The syntax is a dictionary of regular expressions and the replacement text.

*Warning*: These follow the
[.net regular expression language](https://docs.microsoft.com/en-us/dotnet/standard/base-types/regular-expression-language-quick-reference).
The key in the dictionary is a regular expression search pattern, while the
value is a regular expression replacement pattern. It is therefore possible to
use back-references in the replacement string.

*Warning*: Special characters in the acceptation of regular expressions _and_
json both need to be escaped. For a key, this means, for example, that a
literal backslash must be escaped for the regular expression language `\\`
_and_ each of these backslashes must then be escaped for the json encoding:
`\\\\`. In the replacement string, a literal `$` must be escaped with an
additional `$` if it is followed by a number (due to the special meaning in
regular expression replacement strings), while a backslash must be escaped
(`\\`) due to the special meaning in json.

*Advice*: To avoid unexpected results, always match terminating backslashes in
the search pattern and replacement string: if a search pattern ends with a
backslash, you should also put one in the replacement string, and if the search
pattern does not include a terminating backslash, then none should be included
in the replacement string.

##### Examples explained

```json
"IterationMaps": {
  "^OriginalProject\\\\Path1(?=\\\\Sprint 2022)": "TargetProject\\AnotherPath\\NewTeam",
  "^OriginalProject\\\\Path1(?=\\\\Sprint 2020)": "TargetProject\\AnotherPath\\Archives\\Sprints 2020",
  "^OriginalProject\\\\Path2": "TargetProject\\YetAnotherPath\\Path2",
},
"AreaMaps": {
  "^OriginalProject\\\\(DescopeThis|DescopeThat)": "TargetProject\\Archive\\Descoped\\",
  "^OriginalProject\\\\(?!DescopeThis|DescopeThat)": "TargetProject\\NewArea\\",
}
```

- `"^OriginalProject\\\\Path1(?=\\\\Sprint 2022)": "TargetProject\\AnotherPath\\NewTeam",`

  In an iteration path, `OriginalProject\Path1` found at the beginning of the
  path, when followed by `\Sprint 2022`, will be replaced by
  `TargetProject\AnotherPath\NewTeam`.

  `OriginalProject\Path1\Sprint 2022\Sprint 01` will become
  `TargetProject\AnotherPath\NewTeam\Sprint 2022\Sprint 01` but
  `OriginalProject\Path1\Sprint 2020\Sprint 03` will _not_ be transformed by
  this rule.

- `"^OriginalProject\\\\Path1(?=\\\\Sprint 2020)": "TargetProject\\AnotherPath\\Archives\\Sprints 2020",`

  In an iteration path, `OriginalProject\Path1` found at the beginning of the
  path, when followed by `\Sprint 2020`, will be replaced by
  `TargetProject\AnotherPath\Archives\\Sprints 2020`.

  `OriginalProject\Path1\Sprint 2020\Sprint 01` will become
  `TargetProject\AnotherPath\Archives\Sprint 2020\Sprint 01` but
  `OriginalProject\Path1\Sprint 2021\Sprint 03` will _not_ be transformed by
  this rule.

- `"^OriginalProject\\\\Path2": "TargetProject\\YetAnotherPath\\Path2",`

  In an iteration path, `OriginalProject\Path2` will be replaced by
  `TargetProject\YetAnotherPath\Path2`.

- `"^OriginalProject\\\\(DescopeThis|DescopeThat)": "TargetProject\\Archive\\Descoped\\",`

  In an area path, `OriginalProject\` found at the beginning of the path, when
  followed by either `DescopeThis` or `DescopeThat` will be replaced by `TargetProject\Archive\Descoped\`.

  `OriginalProject\DescopeThis\Area` will be transformed to
  `TargetProject\Archive\Descoped\DescopeThis\Area`.
  `OriginalProject\DescopeThat\Product` will be transformed to
  `TargetProject\Archive\Descoped\DescopeThat\Product`.

- `"^OriginalProject\\\\(?!DescopeThis|DescopeThat)": "TargetProject\\NewArea\\",`

  In an area path, `OriginalProject\` found at the beginning of the path will be
  replaced by `TargetProject\NewArea\` unless it is followed by `DescopeThis` or
  `DescopeThat`.

  `OriginalProject\ValidArea\` would be replaced by
  `TargetProject\NewArea\ValidArea\` but `OriginalProject\DescopeThis` would not
  be modified by this rule.




