![Hackathon Logo](docs/images/hackathon.png?raw=true "Hackathon Logo")
# Sitecore Hackathon 2024

- MUST READ: **[Submission requirements](SUBMISSION_REQUIREMENTS.md)**
- [Entry form template](ENTRYFORM.md)
  
## Team name
Team Horizon 2024

## Category
1. Best use of AI

# Our Solution Summary

This is an attempt to design a solution that leverages Azure AI Translator service to automate document transcreations to different languages. The idea is to integrate XM Cloud with Batch document translation service, where by documents are pushed from Media Library in bulk into Azure Blob storage. They are then translated in bulk and are stored in a staged location within Azure Blob storage. The XM Cloud can pull those in Media Libary. 

I realise was a huge undertaking, but I have started building the various componets to deliver the vision. An Azure AI Services Func app microservice is located within the DocuTranslator subfolder.

## Video link
⟹ Provide a video highlighing your Hackathon module submission and provide a link to the video. You can use any video hosting, file share or even upload the video to this repository. _Just remember to update the link below_

⟹ [Replace this Video link](#video-link)



## Pre-requisites and Dependencies

⟹ Does your module rely on other Sitecore modules or frameworks?

- List any dependencies
- Or other modules that must be installed
- Or services that must be enabled/configuredn Azure AI Services Func app microservice within the DocuTranslator

_Remove this subsection if your entry does not have any prerequisites other than Sitecore_

## Installation instructions
⟹ To run locally, refer to  'Development' section.
Then Use the Sitecore CLI to serialize items into local environemnt

 ```ps1
1. dotnet sitecore cloud login
2. dotnet sitecore connect --ref xmcloud --cm  https://xmcloudcm.localhost --allow-write true -n default
3. dotnet sitecore ser pull -n "default"
```

A solution requires the following settings to run locally
 ```ps1
1. "docu_translation_endpoint": "Azure AI services document translation endpoint",
2. "azure_key": "Azure AI services document translation key",
3. "docu_source_uri": "The URL for the source container containing documents to be translated",
4. "docu_target_uri": "The URL for the target container to which the translated documents are written",
5. "docu_target_lang": "The language code for the translated documents, e.g., fr for French"
 ```
### Configuration
⟹ If there are any custom configuration that has to be set manually then remember to add all details here.

_Remove this subsection if your entry does not require any configuration that is not fully covered in the installation instructions already_

## Usage instructions
⟹ Provide documentation about your module, how do the users use your module, where are things located, what do the icons mean, are there any secret shortcuts etc.

Include screenshots where necessary. You can add images to the `./images` folder and then link to them from your documentation:

![Hackathon Logo](docs/images/hackathon.png?raw=true "Hackathon Logo")

You can embed images of different formats too:

![Deal With It](docs/images/deal-with-it.gif?raw=true "Deal With It")

And you can embed external images too:

![Random](https://thiscatdoesnotexist.com/)

## XM Cloud Local setup
⟹ [Adopted from XM Cloud Starter Kit (Next JS)](https://github.com/sitecorelabs/xmcloud-foundation-head-staging)


1. In an ADMIN terminal:

    ```ps1
    .\init.ps1 -InitEnv -LicenseXmlPath "C:\path\to\license.xml" -AdminPassword "DesiredAdminPassword"
    ```

2. Restart your terminal and run:

    ```ps1
    .\up.ps1
    ```
